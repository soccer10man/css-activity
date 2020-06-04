using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace FindEC2
{
    public class Function
    {

        private readonly AmazonEC2Client ec2Client = null;

        public Function()
        {
            this.ec2Client = new AmazonEC2Client(RegionEndpoint.USWest1);

        }

        /// <summary>
        /// A simple function that takes InstanceTag as input and returns a list of instance ids
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<List<string>> FunctionHandler(InstanceTag input, ILambdaContext context)
        {
            var result = new List<string>();

            string nextToken = null;
            do
            {
                var describeInstancesResponse = await ec2Client.DescribeInstancesAsync(
                    new DescribeInstancesRequest()
                    {
                        Filters = new List<Filter>()
                        {
                                        new Filter(){Name= $"tag:{input.Key}", Values = new List<string>{ input.Value } }
                        },
                        MaxResults = 50,
                        NextToken = nextToken
                    });

                nextToken = describeInstancesResponse.NextToken;

                //print what we have so far
                foreach (var resevation in describeInstancesResponse.Reservations)
                {
                    foreach (var instance in resevation.Instances)
                    {
                        result.Add(instance.InstanceId);
                        Console.WriteLine(instance.InstanceId);
                    }
                }
            } while (nextToken != null);




            return result;
        }
    }
}
