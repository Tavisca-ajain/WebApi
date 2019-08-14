pipeline{
agent any{
stages{
stage('Build'){
sh ''' restore WebApi.sln --source htps://api.nuget.org/v3/index.json
build WebApi.sln -p:Configuration=release -v:n'''
}
stage('Test'){
sh 'test WebApiTests/WebApiTests.csproj'
}
stage('Post'){
always($echo "job build succeded")
}
}
}
}