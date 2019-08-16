pipeline{
    agent { label 'master' }
    parameters{
        string(
            name: "GIT_HTTPS_PATH",
            defaultValue: "https://github.com/Tavisca-ajain/WebApi.git",
            description: "GIT HTTPS PATH"
        )
        string(
            name: "SOLUTION_PATH",
            defaultValue: "WebApi.sln",
            description: "SOLUTION_PATH"
        )
        string(
            name: "DOTNETCORE_VERSION",
            defaultValue: "2.1",
            description: "Version"
        )
        string(
            name: "TEST_SOLUTION_PATH",
            defaultValue: "WebApiTests/WebApiTests.csproj",
            description: "TEST SOLUTION PATH"
        )
        
        string(
            name: "PROJECT_PATH",
            defaultValue: "WebApi/WebApi.csproj",
            description: "PROJECT PATH"
        )
        choice(
            name: "RELEASE_ENVIRONMENT",
            choices: ["Build","Test", "Publish"],
            description: "Choose which operation to complete"
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Test" || params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    echo '====================Restore Project Start ================'
                    dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Project Completed============'
                    echo '====================Build Project Start ================'
                    dotnet build ${PPOJECT_PATH} 
                    echo '=====================Build Project Completed============'
                '''
            }
        }
        stage('Test'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Test" || params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    echo '====================Test Project Start ================'
                    dotnet test ${TEST_SOLUTION_PATH}
                    echo '=====================Test Project Completed============'
                '''
            }
        }
        stage('Publish'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps{
                powershell '''
                    echo '====================Publish Project Start ================'
                    dotnet publish ${PROJECT_PATH}
                    echo '=====================Publish Project Completed============'
                '''
            }
        }
        stage ('push artifact') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Publish"}
            }
            steps {
                script{
                //powershell 'mkdir archive'
                zip zipFile: 'publish.zip', archive: false, dir: 'C:\Users\anjain\source\repos\WebApi\WebApi\bin\Debug\netcoreapp2.1\publish'
                archiveArtifacts artifacts: 'publish.zip', fingerprint: true
                }
            }
        }
    }
    post{
        always{
            deleteDir()
       }
    }
}