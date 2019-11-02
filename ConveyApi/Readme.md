# Deployment
In order to deploy this as a serverless application in your AWS account you will need to install the dotnet lambda CLI tool.

> dotnet tool install -g Amazon.Lambda.Tools

After the installation is completed you will need to deploy it as a serverless app using the lambda tooling

> dotnet lambda deploy-serverless

When prompted, specify the name of the CloudFormation stack you want the tooling to create, for example `helloworld-dev-cfstack`. 
It will ask you for a bucket to store the .zip file of the compiled app into. This bucket needs to already exist when you provide the name.

The deployment process will take a few seconds but when it is completed you will be given a URL. For example:

> https://ofd78c0.execute-api.us-east-1.amazonaws.com/Prod/

- You may hit this API to receive a `hello world` response. 
- Hitting `/ping` will give you back a `pong` response. 
- Hitting `/accounts/838d18ec-127d-4c96-af67-0556a346c114` will give you back a response from the `GetAccount` query.