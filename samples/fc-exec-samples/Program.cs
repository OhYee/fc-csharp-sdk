using System;
using Aliyun.FunctionCompute.SDK.Client;
using Aliyun.FunctionCompute.SDK.Request;
using Aliyun.FunctionCompute.SDK.Response;
using Aliyun.FunctionCompute.SDK.model;
using Aliyun.FunctionCompute.SDK.Constants;
using System.IO;
using System.Collections.Generic;
using System.Text;

// more sample code can refrence 
// https://github.com/aliyun/fc-csharp-sdk/tree/master/Libraries/src/Aliyun.FunctionCompute.SDK.Unittests  


namespace fc_csharp_sdk_samples
{
    class Program
    {
        public static readonly string ACCOUNT_ID = Environment.GetEnvironmentVariable("ACCOUNT_ID");
        public static readonly string ACCESS_KEY_ID = Environment.GetEnvironmentVariable("ACCESS_KEY_ID");
        public static readonly string ACCESS_KEY_SECRET = Environment.GetEnvironmentVariable("ACCESS_KEY_SECRET");
        public static readonly string REGION = Environment.GetEnvironmentVariable("REGION");
        public static readonly string FC_ENDPOINT = Environment.GetEnvironmentVariable("FC_ENDPOINT");
        public static readonly string serviceName = Environment.GetEnvironmentVariable("serviceName");
        public static readonly string functionName = Environment.GetEnvironmentVariable("functionName");
        public static readonly string qualifier = Environment.GetEnvironmentVariable("qualifier");

        static void Main(string[] args)
        {
            Console.WriteLine(ACCOUNT_ID);
            Console.WriteLine(REGION);
            var fcClient = new FCClient(REGION, ACCOUNT_ID, ACCESS_KEY_ID, ACCESS_KEY_SECRET);
            fcClient.SetEndpoint("http://1011863232026330.dev-cluster-1.test.fc.aliyun-inc.com");
            var resp = fcClient.ListInstances(new ListInstancesRequest(serviceName, functionName, qualifier));
            Console.WriteLine(resp.Data);
            
            var insid = "";
            if (resp.Data != null && resp.Data.Instances != null)
            {
                foreach (var instance in resp.Data.Instances)
                {
                    Console.WriteLine(instance.InstanceId, instance.VersionId);
                    insid = instance.InstanceId;
                }

                if (insid != "")
                {
                    fcClient.InstanceExec(new InstanceExecRequest(serviceName, functionName, insid, new string[] { "/bin/bash" }));
                }

            }
        }
    }
}

// ttp://1011863232026330.dev-cluster-1.test.fc.aliyun-inc.com/2016-08-15/proxy/throttle/throttle1/