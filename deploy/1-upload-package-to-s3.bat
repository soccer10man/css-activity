
REM run from /deploy folder
REM First upload the template
call aws s3 cp css-activity.template s3://css-activity-2020/css-activity.template --acl bucket-owner-full-control --profile default
REM Secondly upload the lambda package
cd ../src/FindEC2
dotnet lambda package
call aws s3 cp ./bin/Release/netcoreapp2.1/FindEC2.zip s3://css-activity-2020/FindEC2.zip --acl bucket-owner-full-control --profile default
