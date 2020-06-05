#css-activity

1.       An AWS Lambda function that finds all instances based on a given pair of AWS tag key/values and prints the instances ids of the instances that are tagged with the given values to standard output.

2.       An SSM Automation Document that:

    a.       Accepts two parameters: tag key, tag value

    b.      Invokes the Lambda function (Created in Step 1) using the tag key/value passed as the input to the automation document as input to the lambda function

    c.       Invokes a Run Command that lists the contents of the root directory on all Linux instances for the tag key/value passed as the input to the automation document
