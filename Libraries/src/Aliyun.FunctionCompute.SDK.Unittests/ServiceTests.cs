using System;
using System.Collections.Generic;
using Aliyun.FunctionCompute.SDK.Request;
using Aliyun.FunctionCompute.SDK.Response;
using Xunit;

namespace Aliyun.FunctionCompute.SDK.Unittests
{
    [Collection("fcDotnet.Unittests")]
    public class ServiceUnitTests : IDisposable
    {
        readonly TestConfig tf = new TestConfig();

        public List<string> ServiceNames = new List<string>();

        public void Dispose()
        { 
            Console.WriteLine("ServiceUnitTests TearDownBase .....");
            foreach(string name in ServiceNames.ToArray())
            {
                Console.WriteLine(string.Format("delete service {0} .....",name));
                try
                {
                    tf.Client.DeleteService(new DeleteServiceRequest(name));
                }
                catch (Exception)
                {
                    //
                }
                finally
                {
                    this.ServiceNames.Remove(name);
                }
            }
        }

        [Fact]
        public void TestServiceCRUD()
        {
            string name = "test-charp-" + TestConfig.RandomString(8);
            this.ServiceNames.Add(name);
            string desc = "create by c# sdk";
           
            CreateServiceResponse response = tf.Client.CreateService(
                new CreateServiceRequest(
                    name, desc,
                    tf.ServiceRole, tf.LogConfig, true, tf.VpcConfig, tf.NasConfig
                    )
                );
            Assert.Equal(name, response.Data.ServiceName);
            Assert.Equal(desc, response.Data.Description);
            Assert.Equal(tf.ServiceRole, response.Data.Role);
            Assert.True(response.Data.InternetAccess);
            Assert.Equal(tf.LogConfig, response.Data.LogConfig);
            Assert.Equal(tf.VpcConfig, response.Data.VpcConfig);
            Assert.Equal(tf.NasConfig, response.Data.NasConfig);

            string newDesc = "create by c# sdk new update";
            UpdateServiceResponse response2 = tf.Client.UpdateService(
                new UpdateServiceRequest(
                    name, newDesc)
                );
            Assert.Equal(name, response2.Data.ServiceName);
            Assert.Equal(newDesc, response2.Data.Description);
            Assert.Equal(tf.ServiceRole, response2.Data.Role);
            Assert.True(response2.Data.InternetAccess);
            Assert.Equal(tf.LogConfig, response2.Data.LogConfig);
            Assert.Equal(tf.VpcConfig, response2.Data.VpcConfig);
            Assert.Equal(tf.NasConfig, response2.Data.NasConfig);


            GetServiceResponse response3 = tf.Client.GetService(
                new GetServiceRequest(name)
                );
            Assert.Equal(name, response3.Data.ServiceName);
            Assert.Equal(newDesc, response3.Data.Description);
            Assert.Equal(tf.ServiceRole, response3.Data.Role);
            Assert.True(response3.Data.InternetAccess);
            Assert.Equal(tf.LogConfig, response3.Data.LogConfig);
            Assert.Equal(tf.VpcConfig, response3.Data.VpcConfig);
            Assert.Equal(tf.NasConfig, response3.Data.NasConfig);

            var response4 = tf.Client.DeleteService(new DeleteServiceRequest(name));
            this.ServiceNames.Remove(name);
            Assert.Equal(204,response4.StatusCode);
        }

        [Fact]
        public void TestListServices()
        {
            string prefix = "csharp_test_list_";
            string[] names = { prefix + "abc", prefix + "abd", prefix + "ade", prefix + "bcd", prefix + "bde", prefix + "zzz" };
            foreach (string element in names)
            {
                tf.Client.CreateService(new CreateServiceRequest(element));
                this.ServiceNames.Add(element);
            }

            var response1 = tf.Client.ListServices(new ListServicesRequest(2, prefix + "b"));
            Assert.Equal(2, response1.Data.Services.GetLength(0));
            Assert.Equal(prefix + "bcd", response1.Data.Services[0].ServiceName);
            Assert.Equal(prefix + "bde", response1.Data.Services[1].ServiceName);

            var response2 = tf.Client.ListServices(new ListServicesRequest(100, prefix));
            Assert.Equal(6, response2.Data.Services.GetLength(0));
        }
    }
}
