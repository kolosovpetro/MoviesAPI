pipeline {
    agent any

    stages {
        stage('Verify branch') {
            steps {
                echo "$GIT_BRANCH"
            }
        }        
        
        stage('Hello') {
            steps {
                echo 'Hello World'
            }
        }
        stage('Goodbye') {
            steps {
                echo 'Good bye World'
            }
        }
        stage('Powershell Core') {
            steps {
                pwsh 'Write-Output "Hello from pwsh"'
            }
        }
    }
}
