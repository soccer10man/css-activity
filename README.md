## CSS activity

1. An AWS Lambda function that finds all instances based on a given pair of AWS tag key/values and prints the instances ids of the instances that are tagged with the given values to standard output.

2.  An SSM Automation Document that:
    * a. Accepts two parameters: tag key, tag value
    * b. Invokes the Lambda function (Created in Step 1) using the tag key/value passed as the input to the automation document as input to the lambda function
    * c. Invokes a Run Command that lists the contents of the root directory on all Linux instances for the tag key/value passed as the input to the automation document


## Structures

1. deploy - contains the application artifacts like CF template, deployment script.
2. src - contains codebase for lambda function


## Deployment

1. Run the batch file in /deploy to build and package the artifacts and upload to S3 bucket. 
2. Log into AWS console and go to CloudFormation service and create a stack. Use https://css-activity-2020.s3-us-west-1.amazonaws.com/css-activity.template as a template(should be already in S3 after step 1). Use the default setting as the parameters are already set with correct default values.
3. Check all resources are created correctly.

## Testing

1. For testing, please spin up a couple of EC2 instances. I've used RHEL-8.2.0_HVM-20200423-x86_64-0-Hourly2-GP2 (ami-066df92ac6f03efca)) AMI and used the following Use Data to install SSM agent. You need to attach IAM role that allows SSM to install on EC2(arn:aws:iam::aws:policy/service-role/AmazonEC2RoleforSSM)

#!/bin/bash
cd /tmp
sudo yum install -y https://s3.amazonaws.com/ec2-downloads-windows/SSMAgent/latest/linux_amd64/amazon-ssm-agent.rpm
yum install -y amazon-ssm-agent.rpm
sudo systemctl start amazon-ssm-agent
sudo systemctl enable amazon-ssm-agent

2. Go to SSM and find the document under Owned by me tab. You will see CSS-runbook1. Excute this document.

