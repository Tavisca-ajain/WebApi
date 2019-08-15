    
pipeline{
  agent any
  parameters{
   choice(
      choices: ['BUILD' , 'TEST' , 'PUBLISH'],
      name: 'CHOSEN_ACTION')
    string(
      name: 'GIT_PATH',
      defaultValue: 'https://github.com/Tavisca-ajain/WebApi')
    string(
      name: 'SOLUTION_FILE_PATH',
      defaultValue: 'WebApi.sln')
    string(
      name: 'TEST_PROJECT_PATH',
      defaultValue: 'WebApiTests/WebApiTests.csproj')
    string(
      name: 'NETCORE_VERSION',
      defaultValue: '')
  }
  stages{
    stage('Build'){
      when {
        expression {params.CHOSEN_ACTION=='BUILD' ||  params.CHOSEN_ACTION=='TEST' ||  params.CHOSEN_ACTION=='PUBLISH'}
      }
      steps{
        powershell '''dotnet${NETCORE_VERSION} restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json
        
              dotnet${NETCORE_VERSION} build ${SOLUTION_PATH_FILE} -p:CONFIGURATION=release -V:n'''
      }
    }
    stage('test'){
      when {
        expression { params.CHOSEN_ACTION=='TEST' ||  params.CHOSEN_ACTION=='PUBLISH'}
      }
      steps{
        powershell 'dotnet${NETCORE_VERSION} test ${TEST_PROJECT_PATH}'
      }
    }
    stage('publish'){
      when {
        expression {params.CHOSEN_ACTION=='PUBLISH'}
      }
      steps{
        powershell 'dotnet publish WebApi/WebApi.csproj'
      }
    }
  }
  post{
    always{
      powershell 'echo Release...'
    }
  }
}