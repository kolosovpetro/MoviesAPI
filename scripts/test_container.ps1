$count = 0
do
{
    $count++
    Write-Output "[$env:STAGE_NAME] Stating container attempt $count"
    $testStart = Invoke-WebRequest -Uri "http://localhost:8080/api/movies"

    if ($testStart.StatusCode -eq 200)
    {
        $content = $testStart.Content
        Write-Output "Response: $content"
        $started = $true
    }
    else
    {
        Start-Sleep -Seconds 1
    }
} until ($started -or ($count -eq 10))

if (!$started)
{
    Write-Output "[$env:STAGE_NAME] Container failed to start"
    exit 1
}