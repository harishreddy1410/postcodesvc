/*postcodesvc

This project contains source code and supporting files for a serverless application that you can deploy with the SAM CLI. It includes the following files and folders.

- src - Code for the application's Lambda function.
- events - Invocation events that you can use to invoke the function.
- test - Unit tests for the application code. 
- template.yaml - A template that defines the application's AWS resources.

Pre requisites : 
> Docker installed in machine to test locally and running state 
> AWS cli and Sam cli installation
> VSCode 
> Dotnet core sdk 

Steps 
1) sam init --name postcodesvc --location "C:\Users\{USERNAME}\AppData\Roaming\AWS SAM\aws-sam-cli-app-templates\dotnetcore3.1\cookiecutter-aws-sam-hello-dotnet"
    /*As AWS toolkit was not working in VSCode I have downloaded the template using above command 
2) Update the namings are required places 
3) cd .\postcodesvc\src\PostCodeSvc> 
4) dotnet build 
    /*RESTART VSCODE
5) cd postcodesvc
6) sam build 
7) sam local invoke 
    /*To check if the lambda functions is working, you should see a JSON response with 200 status code 
8) /*Write the business requirement code :) 
9) sam build 
10) sam local start-api
11) To deploy the code to AWS, Please configure the AWS profile 
        In my case I have used the below command and values, you can use the same 
        aws configure --profile developer
        AWS Access Key ID [None]: ############################
        AWS Secret Access Key [None]: ##############################
        Default region name [None]: us-east-1
        Default output format [None]: json
12) sam package --template-file template.yaml --output-template-file package.yml   --s3-bucket postcodesvcbuildatrifacts   --profile developer
    /*--resolve-s3 can create the bucket automatically, but I have used the bucket I have created 
13) sam deploy --capabilities CAPABILITY_IAM --template-file C:\AwsWork\postcodesvc\postcodesvc\package.yml --stack-name postcodeapistack
    /*Terminal will show the resources creation progress 
    /*I faced error - my API was not working after deployment, so I have deleted bin and obj folders
    /*then ran sam build in root repository 
    /*ran sam package
14) sam deploy --guided
    /*Stack Name [sam-app]: postcodeapistack
        AWS Region [us-east-1]: 
        /*Shows you resources changes to be deployed and require a 'Y' to initiate deploy
        /*Confirm changes before deploy [y/N]: y
        /*SAM needs permission to be able to create roles to connect to the resources in your template
        /*Allow SAM CLI IAM role creation [Y/n]: Y
        /*Preserves the state of previously provisioned resources when an operation fails
        /*Disable rollback [y/N]: y
        /*PostCodeSvcFunction may not have authorization defined, Is this okay? [y/N]: y
        /*PostCodeSvcFunction may not have authorization defined, Is this okay? [y/N]: y
        /*Save arguments to configuration file [Y/n]: y
        /*SAM configuration file [samconfig.toml]: 
        /*SAM configuration environment [default]: 
