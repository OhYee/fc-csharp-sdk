using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aliyun.FunctionCompute.SDK.model;
using Aliyun.FunctionCompute.SDK.Request;
using Aliyun.FunctionCompute.SDK.Response;
using Newtonsoft.Json;
using Xunit;

namespace Aliyun.FunctionCompute.SDK.Unittests
{

    [Collection("fcDotnet.Unittests")]
    public class InstanceUnitTests
    {
        readonly TestConfig tf;

        public string Service;

        public InstanceUnitTests()
        {
            Console.WriteLine("InstanceUnitTests Setup .....");
            Service = "test-csharp-" + TestConfig.RandomString(8);
            tf = new TestConfig();
        }

        public void TestListInstances()
        {
            Console.WriteLine("List instances .....");
            var req = tf.Client.ListInstances(new ListInstancesRequest("throttle", "throttle1"));
            Console.WriteLine(req);
        }

        // public void Dispose()
        // {
        //     Console.WriteLine("InstanceUnitTests TearDownBase .....");
        //     var response = tf.Client.ListInstances(new ListInstancesRequest(Service));

        //     foreach (var r in response.Data.Instances)
        //     {
        //         string name = r.InstanceName;
        //         Console.WriteLine(string.Format("delete Instance {0} .....", name));
        //         try
        //         {
        //             tf.Client.DeleteInstance(new DeleteInstanceRequest(Service, name));
        //         }
        //         catch (Exception)
        //         {
        //             //
        //         }
        //     }

        //     try
        //     {
        //         Console.WriteLine(string.Format("delete service {0} .....", Service));
        //         tf.Client.DeleteService(new DeleteServiceRequest(Service));
        //     }
        //     catch (Exception)
        //     {
        //         //
        //     }
        // }

        //     [Fact]
        //     public void TestInstanceCRUD()
        //     {

        //         tf.Client.CreateService(new CreateServiceRequest(Service));

        //         string name = "test-csharp-func" + TestConfig.RandomString(8);
        //         byte[] contents = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/hello.zip");
        //         var code = new Code(Convert.ToBase64String(contents));
        //         var response = tf.Client.CreateInstance(new CreateInstanceRequest(Service, name, "python3", "index.handler", code, "desc"));
        //         //Console.WriteLine(response.Content);
        //         Assert.True(200 == response.StatusCode);


        //         Assert.Equal(200, response.StatusCode);
        //         Assert.Equal(response.Data.InstanceName, name);
        //         Assert.Equal("python3", response.Data.Runtime);
        //         Assert.Equal("index.handler", response.Data.Handler);
        //         Assert.Equal("desc", response.Data.Description);
        //         Assert.True(!string.IsNullOrEmpty(response.Data.InstanceId));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.CodeChecksum));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.CreatedTime));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.LastModifiedTime));
        //         Assert.True(response.Data.CodeSize > 0);
        //         Assert.True(response.Data.MemorySize == 256);

        //         var response2 = tf.Client.GetInstance(new GetInstanceRequest(Service, name));
        //         Assert.Equal(response.Data.InstanceName, name);
        //         Assert.Equal("python3", response.Data.Runtime);
        //         Assert.Equal("index.handler", response.Data.Handler);
        //         Assert.Equal("desc", response.Data.Description);
        //         Assert.True(!string.IsNullOrEmpty(response.Data.InstanceId));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.CodeChecksum));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.CreatedTime));
        //         Assert.True(!string.IsNullOrEmpty(response.Data.LastModifiedTime));
        //         Assert.True(response.Data.CodeSize > 0);
        //         Assert.True(response.Data.MemorySize == 256);


        //         var resp = tf.Client.GetInstanceCode(new GetInstanceCodeRequest(Service, name));
        //         Assert.False(resp.Data.Url.Contains(@"\u0026"));

        //         var response3 = tf.Client.UpdateInstance(new UpdateInstanceRequest(Service, name, "python3", "index.handler", code, "new-desc"));
        //         //Console.WriteLine(response3.Content);
        //         Assert.Equal("new-desc", response3.Data.Description);

        //         var response4 = tf.Client.DeleteInstance(new DeleteInstanceRequest(Service, name));
        //         Assert.Equal(204, response4.StatusCode);
        //     }


        //     [Fact]
        //     public void TestListInstances()
        //     {
        //         tf.Client.CreateService(new CreateServiceRequest(Service));

        //         string prefix = "csharp_test_list_func_";
        //         string[] names = { prefix + "abc", prefix + "abd", prefix + "ade", prefix + "bcd", prefix + "bde", prefix + "zzz" };
        //         foreach (string name in names)
        //         {
        //             byte[] contents = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/hello.zip");
        //             var code = new Code(Convert.ToBase64String(contents));
        //             var response = tf.Client.CreateInstance(new CreateInstanceRequest(Service, name, "python3", "index.handler", code, "desc"));
        //             Assert.True(200 == response.StatusCode);
        //         }

        //         var response1 = tf.Client.ListInstances(new ListInstancesRequest(Service, null, 2, prefix + "b"));
        //         Assert.Equal(2, response1.Data.Instances.GetLength(0));
        //         Assert.Equal(prefix + "bcd", response1.Data.Instances[0].InstanceName);
        //         Assert.Equal(prefix + "bde", response1.Data.Instances[1].InstanceName);

        //         var response2 = tf.Client.ListInstances(new ListInstancesRequest(Service, null, 100, prefix));
        //         Assert.Equal(6, response2.Data.Instances.GetLength(0));
        //     }


        //     [Fact]
        //     public void TestInstanceInvoke()
        //     {

        //         tf.Client.CreateService(
        //             new CreateServiceRequest(Service)
        //             );

        //         string name = "test-csharp-func" + TestConfig.RandomString(8);
        //         byte[] contents = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/hello.zip");
        //         var code = new Code(Convert.ToBase64String(contents));
        //         var response = tf.Client.CreateInstance(new CreateInstanceRequest(Service, name, "python3", "index.handler", code, "desc"));
        //         Assert.True(200 == response.StatusCode);

        //         byte[] hello = Encoding.UTF8.GetBytes("hello csharp world");
        //         var response2 = tf.Client.InvokeInstance(new InvokeInstanceRequest(Service, name, null, hello));
        //         //Console.WriteLine(response2.Content);
        //         Assert.Equal("hello csharp world", response2.Content);
        //         Assert.Equal(hello, response2.Data);

        //         var customHeaders = new Dictionary<string, string> {
        //             {"x-fc-invocation-type", "Async"}
        //         };
        //         var response3 = tf.Client.InvokeInstance(new InvokeInstanceRequest(Service, name, null, hello, customHeaders));
        //         Assert.Equal(202, response3.StatusCode);
        //     }


        //     [Fact]
        //     public void TestInstanceHttpInvoke()
        //     {

        //         tf.Client.CreateService(
        //             new CreateServiceRequest(Service)
        //             );

        //         string name = "test-csharp-func" + TestConfig.RandomString(8);
        //         byte[] contents = File.ReadAllBytes(Directory.GetCurrentDirectory() + "/hello.zip");
        //         var code = new Code(Convert.ToBase64String(contents));
        //         var response = tf.Client.CreateInstance(new CreateInstanceRequest(Service, name, "python3", "index.wsgi_echo_handler", code, "desc"));
        //         Assert.True(200 == response.StatusCode);

        //         tf.Client.CreateTrigger(new CreateTriggerRequest(Service, name, "my-http-trigger", "http", "dummy_arn", "", new HttpTriggerConfig(HttpAuthType.ANONYMOUS, new HttpMethod[] { HttpMethod.GET, HttpMethod.POST })));

        //         byte[] hello = Encoding.UTF8.GetBytes("hello csharp world");
        //         var customHeaders = new Dictionary<string, string> {
        //             {"Foo", "Bar"}
        //         };

        //         Dictionary<string, string[]> unescapedQueries = new Dictionary<string, string[]>
        //         {
        //             {"key", new string[]{ "value"} },
        //             {"key with space", new string[]{ "value with space" } },
        //         };

        //         var response3 = tf.Client.InvokeHttpInstance(new HttpInvokeInstanceRequest(Service, name, "POST", "/action%20with%20space", null,
        //                                     hello, unescapedQueries, customHeaders));

        //         var obj = JsonConvert.DeserializeObject<HttpInvokeResult>(response3.Content);
        //         Assert.Equal("POST", obj.Method);
        //         Assert.Equal("/action with space", obj.Path);
        //         Assert.Equal(202, response3.StatusCode);


        //         var response4 = tf.Client.InvokeHttpInstance(new HttpInvokeInstanceRequest(Service, name, "POST", "/action%20with%20space", null,
        //                                    null, null, null));

        //         var obj2 = JsonConvert.DeserializeObject<HttpInvokeResult>(response4.Content);
        //         Assert.Equal("POST", obj2.Method);
        //         Assert.Equal("/action with space", obj2.Path);
        //         Assert.Equal(202, response4.StatusCode);

        //         tf.Client.DeleteTrigger(new DeleteTriggerRequest(Service, name, "my-http-trigger"));
        //     }
    }
}
