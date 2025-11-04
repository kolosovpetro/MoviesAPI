# SPDX-FileCopyrightText: 2020-2025 VerifyEncoding contributors <https://github.com/ForNeVeR/VerifyEncoding>
#
# SPDX-License-Identifier: MIT

<#
.SYNOPSIS
    This function will verify that there's no UTF-8 BOM or CRLF line endings in the files inside of the project.
#>

param (
    [string] $SourceRoot,
    [switch] $Autofix,
    [string[]] $ExcludeExtensions = @(
    '.dotsettings'
)
)

function Test-Encoding
{
    param (
    # Path to the repository root. All text files under the root will be checked for UTF-8 BOM and CRLF.
    #
    # By default (if nothing's passed), the script will try auto-detecting the nearest Git root.
        [string] $SourceRoot,

    # Makes the script to perform file modifications to bring them to the standard.
        [switch] $Autofix,

    # List of file extensions (with leading dots) to ignore. Case-insensitive.
        [string[]] $ExcludeExtensions = @(
        '.dotsettings'
    )
    )

    Set-StrictMode -Version Latest
    $ErrorActionPreference = 'Stop'

    if (!$SourceRoot)
    {
        $SourceRoot = git rev-parse --show-toplevel

        if (!$?)
        {
            throw "Cannot call `"git rev-parse`": exit code $LASTEXITCODE."
        }
    }

    # For PowerShell to properly process the UTF-8 output from git ls-tree we need to set up the output encoding:
    [Console]::OutputEncoding = [Text.Encoding]::UTF8

    try
    {
        Push-Location $SourceRoot

        # Step 1: Get all file paths from git
        $gitFiles = git -c core.quotepath=off ls-tree -r HEAD --name-only

        # Step 2: Filter out deleted files
        $existingFiles = $gitFiles | Where-Object { Test-Path -LiteralPath $_ }

        # Step 3: Filter out directories (keep only files)
        $allFiles = @($existingFiles | Where-Object { -not (Get-Item -Force -LiteralPath $_).PSIsContainer })

        if (!$?)
        {
            throw "Cannot call `"git ls-tree`": exit code $LASTEXITCODE."
        }

        Write-Output "Total files in the repository: $($allFiles.Length)"

        $counter = [pscustomobject]@{ Value = 0 }

        $groupSize = 50

        $chunks = @($allFiles | Group-Object -Property { [math]::Floor($counter.Value++ / $groupSize) })

        Write-Output "Split into $( $chunks.Count ) chunks."

        # https://stackoverflow.com/questions/6119956/how-to-determine-if-git-handles-a-file-as-binary-or-as-text#comment15281840_6134127
        $nullHash = '4b825dc642cb6eb9a060e54bf8d69288fbee4904'

        $textFiles = @($chunks | ForEach-Object {
            $chunk = $_.Group
            $filePaths = git -c core.quotepath=off diff --numstat $nullHash HEAD -- @chunk
            if (!$?)
            {
                throw "Cannot call `"git diff`": exit code $LASTEXITCODE."
            }
            $filePaths |
                Where-Object { -not $_.StartsWith('-') } |
                ForEach-Object { [Regex]::Unescape($_.Split("`t", 3)[2]) }
        })

        Write-Output "Text files in the repository: $( $textFiles.Length )"

        $bom = @(0xEF, 0xBB, 0xBF)
        $bomErrors = @()
        $lineEndingErrors = @()

        foreach ($file in $textFiles)
        {
            if ($ExcludeExtensions -contains [IO.Path]::GetExtension($file).ToLowerInvariant())
            {
                continue
            }

            $fullPath = Resolve-Path -LiteralPath $file

            $bytes = [IO.File]::ReadAllBytes($fullPath) | Select-Object -First $bom.Length

            if (!$bytes)
            {
                continue
            } # filter empty files

            $bytesEqualsBom = @(Compare-Object $bytes $bom -SyncWindow 0).Length -eq 0

            if ($bytesEqualsBom -and $Autofix)
            {
                $fullContent = [IO.File]::ReadAllBytes($fullPath)
                $newContent = $fullContent | Select-Object -Skip $bom.Length
                [IO.File]::WriteAllBytes($fullPath, $newContent)
                Write-Output "Removed UTF-8 BOM from file $file"
            }
            elseif ($bytesEqualsBom)
            {
                $bomErrors += @($file)
            }

            $text = [IO.File]::ReadAllText($fullPath)

            $crlf = "`r`n"
            $lf = "`n"
            $cr = "`r"

            $hasWrongLineEndings = $text.Contains($crlf) -or $text.Contains($cr)

            if ($hasWrongLineEndings -and $Autofix)
            {
                $newText = $text -replace $crlf, $lf -replace $cr, $lf
                [IO.File]::WriteAllText($fullPath, $newText)
                Write-Output "Fixed the line endings for file $file"
            }
            elseif ($hasWrongLineEndings)
            {
                $lineEndingErrors += @($file)
            }
        }

        if ($bomErrors.Length)
        {
            throw "The following $( $bomErrors.Length ) files have UTF-8 BOM:`n" + ($bomErrors -join "`n")
        }
        if ($lineEndingErrors.Length)
        {
            throw "The following $( $lineEndingErrors.Length ) files have CRLF instead of LF:`n" + ($lineEndingErrors -join "`n")
        }
    }
    finally
    {
        Pop-Location
    }
}

# Convenience launch mode when not invoked as part of a module:
if (!$MyInvocation.PSCommandPath -or !$MyInvocation.PSCommandPath.EndsWith('.psm1')) {
    Write-Output "Direct script launcher mode.$(if ($MyInvocation.PSCommandPath) {
        ' Launched from "' + $MyInvocation.PSCommandPath + '".'
    })"
    Test-Encoding -SourceRoot:$SourceRoot -Autofix:$Autofix -ExcludedExtensions:$ExcludeExtensions
}
