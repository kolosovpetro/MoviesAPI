pipeline {
    agent any

    stages {
        stage('Verify branch') {
            steps {
                echo "$GIT_BRANCH"
            }
        }        
        
        stage('Docker build') {
            steps {
                pwsh(script: 'docker images -a')
                pwsh(script: 'docker build -t "movies:latest" .')
            }
            post {
                success {
                    echo 'Docker build success :)'
                }
                
                failure {
                    echo 'Docker build failed :('
                }
            }
        }
        stage('Docker compose up & test') {
            steps {
                pwsh(script: """
                    docker-compose up -d
                    ./scripts/test_container.ps1
                """)
            }
        }
        stage('Docker compose down') {
            steps {
                pwsh(script: 'docker-compose down')
            }
        }
    }
}
