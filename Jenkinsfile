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
         string(
            name: "DOCKERHUB_USER_NAME",
            description: "Enter Docker hub Username"
        )
        string(
            name: "DOCKERHUB_PASSWORD",
            description:  "Enter Docker hub Password"
        )
        string(
            name: "DOCKERHUB_REPO",
            defaultValue: "WebApi"
        )
        string(
            name: "SOLUTION_DLL_FILE",
            defaultValue: "WebApi.dll",
        )
        choice(
            name: "RELEASE_ENVIRONMENT",
            choices: ["Build","Deploy"],
            description: "Choose the operation"
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Deploy" }
            }
            steps{
                powershell '''
                    echo '====================Restore Project Start ================'
                    dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Project Completed============'
                    echo '====================Build Project Start ================'
                    dotnet build ${PPOJECT_PATH} 
                    echo '=====================Build Project Completed============'
                    echo '====================Test Project Start ================'
                    dotnet test ${TEST_SOLUTION_PATH}
                    echo '=====================Test Project Completed============'
                    echo '====================Publish Start ================'
                    dotnet publish ${PROJECT_PATH}
                    echo '=====================Publish Completed============'
                '''
            }
        }
        stage('Deploy'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps{
                writeFile file: 
                        'WebApi/bin/Debug/netcoreapp2.1/publish/Dockerfile', text: '''
                        FROM mcr.microsoft.com/dotnet/core/aspnet\n
                        ENV NAME ${DOCKER_REPO}\n
                        CMD ["dotnet", "${SOLUTION_DLL_FILE}"]\n'''
                
                powershell "docker build WebApi/bin/Debug/netcoreapp2.2/publish/ --tag=${DOCKER_REPO}:${BUILD_NUMBER}"    
                powershell "docker tag ${DOCKER_REPO}:${BUILD_NUMBER} ${DOCKER_USER_NAME}/${DOCKER_REPO}:${BUILD_NUMBER}"
                powershell "docker push ${DOCKER_USER_NAME}/${DOCKER_REPO}:${BUILD_NUMBER}"
        }
    }
}
    post{
        always{
            deleteDir()
       }
    }
}
