    pipeline{
    agent { label 'master' }
    parameters{
        string(
            name: "GIT_HTTPS_PATH",
            defaultValue: "https://github.com/Tavisca-ajain/WebApi.git",
            description: "GIT HTTPS PATH"
        )
        string(
            name: "PROJECT_NAME",
            defaultValue: "WebApi"
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
        )
         string(
            name: "DOCKERFILE",
            defaultValue: "mcr.microsoft.com/dotnet/core/aspnet",
        )
         string(
            name: "ENV_NAME",
            defaultValue: "Api",
        )
         string(
            name: "SOLUTION_DLL_FILE",
            defaultValue: "WebApi.dll",
        )
        string(
            name: "DOCKER_USER_NAME",
            defaultValue: '',
            description: "Enter Docker hub Username"
        )
        string(
            name: "DOCKER_PASSWORD",
             defaultValue: '',
            description:  "Enter Docker hub Password"
        )
        string(
            name: 'SONAR_PROJECT_TOKEN',
            defaultValue: '', 
            description: 'Path to the Solution'
        )
        choice(
            name: "RELEASE_ENVIRONMENT",
            choices: ["Build","Deploy"],
            description: "Tick what you want to do"
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps{
                bat '''
                echo "----------------------------Build Project Started-----------------------------"
                echo "Helllo"
				dotnet "C:/Program Files (x86)/Jenkins/sonar/SonarScanner.MSBuild.dll" begin /k:"api" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="%SONAR_PROJECT_TOKEN%"
				dotnet build %SOLUTION_PATH% -p:Configuration=release -v:q
				echo "----------------------------Build Project Completed-----------------------------"
				echo "----------------------------Test Project Started-----------------------------"
				dotnet test %TEST_SOLUTION_PATH%
				echo "----------------------------Test Project Completed-----------------------------"
				dotnet "C:/Program Files (x86)/Jenkins/sonar/SonarScanner.MSBuild.dll" end /d:sonar.login="%SONAR_PROJECT_TOKEN%"
                echo '====================Publish Start ================'
                dotnet publish %SOLUTION_PATH% -c Release -o ../publish
                echo '=====================Publish Completed============'
                '''
            }
        }
        stage ('Creating Docker Image') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps {
                powershell "Copy-Item build API/bin/Debug/netcoreapp2.1/publish/* infra/docker/ -Recurse"
                powershell "docker build  infra/docker/ --tag=${Project_Name}:${BUILD_NUMBER}"    
                powershell "docker tag ${PROJECT_NAME}:${BUILD_NUMBER} ${DOCKER_USER_NAME}/${PROJECT_NAME}:${BUILD_NUMBER}"
                powershell "docker login -u ${DOCKER_USER_NAME} -p ${DOCKER_PASSWORD}" 
                powershell "docker push ${DOCKER_USER_NAME}/${PROJECT_NAME}:${BUILD_NUMBER}"
            }
        }
    }
    
}