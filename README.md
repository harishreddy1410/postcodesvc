### About
Search post codes - 
This application is AWS Lambda API for lookup and auto complete the UK's post codes search. its built using AWS CloudFormation and AWS Serverless application model(SAM)  
URL: 
* https://rpzxxc61d0.execute-api.us-east-1.amazonaws.com/Prod/lookup?postcode=NE301DP
* https://rpzxxc61d0.execute-api.us-east-1.amazonaws.com/Prod/autocomplete?postcode=NE3

### Pre requisites  
* AWS Account - free trial is enough
* Docker installed in machine to test locally and running state 
* AWS cli and Sam cli installed
* IDE : VSCode 
* Dotnet core 3.1 sdk installed 
* nodejs package installed
* Optional : Postman for testing API's
### Commands to build, run and deploy
 * #### build using - `npm run build`
 * #### start in local using - `npm run start`
 * #### deploy using - `npm run deploy`
 * #### Unit test running - `cd .\src\PostCodeSvc.Tests\` followed by `dotnet test`

### Steps Involved
1) Create the application using the SAM pre built templates, In my case AWS Toolkit in VSCode had some issues while creating the application and hence I have used the below command 
#### `sam init --name postcodesvc --location "C:\Users\{USERNAME}\AppData\Roaming\AWS SAM\aws-sam-cli-app-templates\dotnetcore3.1\cookiecutter-aws-sam-hello-dotnet"`
2) Since I have used the default template of SAM, I needed to update the namings where ever applicable 
3) #### `cd .\postcodesvc\src\PostCodeSvc>` 
4) Optionaly #### `dotnet build` as it was .net core lambda I have built using dotnet cli
    /*RESTART VSCODE*/
5) cd postcodesvc
6) #### `sam build` built application using SAM cli (Note: Make sure docker is in running state, to test lambda in local development machine)
7) #### `sam local invoke` 
    /*To check if the lambda function is working, you should see a JSON response from the handler with Http status code 200 
8) Written the business requirement code 
9) #### `sam build`
10) #### `sam local start-api`
11) To deploy the code in to AWS, Please configure the AWS profile 
        In my case I have used the below command and values, you can use the same 
        `aws configure --profile developer`
        AWS Access Key ID [None]: ############################
        AWS Secret Access Key [None]: ##############################
        Default region name [None]: us-east-1
        Default output format [None]: json
12) #### `sam package --template-file template.yaml --output-template-file package.yml   --s3-bucket postcodesvcbuildatrifacts   --profile developer`
    /*--resolve-s3 can create the bucket automatically, but I have used the bucket I have created 
13) #### `sam deploy --capabilities CAPABILITY_IAM --template-file C:\AwsWork\postcodesvc\postcodesvc\package.yml --stack-name postcodeapistack`
    /*Terminal will show the resources creation progress 
    /*I faced error - my API was not working after deployment, so I have deleted bin and obj folders
    /*then ran sam build in root repository 
    /*ran sam package
14) #### `sam deploy --guided`
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
   15) Verified the created AWS resources and tested changes
