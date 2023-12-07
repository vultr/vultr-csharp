/*
 * Vultr API
 *
 * # Introduction  The Vultr API v2 is a set of HTTP endpoints that adhere to RESTful design principles and CRUD actions with predictable URIs. It uses standard HTTP response codes, authentication, and verbs. The API has consistent and well-formed JSON requests and responses with cursor-based pagination to simplify list handling. Error messages are descriptive and easy to understand. All functions of the Vultr customer portal are accessible via the API, enabling you to script complex unattended scenarios with any tool fluent in HTTP.  ## Requests  Communicate with the API by making an HTTP request at the correct endpoint. The chosen method determines the action taken.  | Method | Usage | | - -- -- - | - -- -- -- -- -- -- | | DELETE | Use the DELETE method to destroy a resource in your account. If it is not found, the operation will return a 4xx error and an appropriate message. | | GET | To retrieve information about a resource, use the GET method. The data is returned as a JSON object. GET methods are read-only and do not affect any resources. | | PATCH | Some resources support partial modification with PATCH, which modifies specific attributes without updating the entire object representation. | | POST | Issue a POST method to create a new object. Include all needed attributes in the request body encoded as JSON. | | PUT | Use the PUT method to update information about a resource. PUT will set new values on the item without regard to their current values. |  **Rate Limit:** Vultr safeguards the API against bursts of incoming traffic based on the request's IP address to ensure stability for all users. If your application sends more than 30 requests per second, the API may return HTTP status code 429.  ## Response Codes  We use standard HTTP response codes to show the success or failure of requests. Response codes in the 2xx range indicate success, while codes in the 4xx range indicate an error, such as an authorization failure or a malformed request. All 4xx errors will return a JSON response object with an `error` attribute explaining the error. Codes in the 5xx range indicate a server-side problem preventing Vultr from fulfilling your request.  | Response | Description | | - -- -- - | - -- -- -- -- -- -- | | 200 OK | The response contains your requested information. | | 201 Created | Your request was accepted. The resource was created. | | 202 Accepted | Your request was accepted. The resource was created or updated. | | 204 No Content | Your request succeeded, there is no additional information returned. | | 400 Bad Request | Your request was malformed. | | 401 Unauthorized | You did not supply valid authentication credentials. | | 403 Forbidden | You are not allowed to perform that action. | | 404 Not Found | No results were found for your request. | | 429 Too Many Requests | Your request exceeded the API rate limit. | | 500 Internal Server Error | We were unable to perform the request due to server-side problems. |  ## Meta and Pagination  Many API calls will return a `meta` object with paging information.  ### Definitions  | Term | Description | | - -- -- - | - -- -- -- -- -- -- | | **List** | The items returned from the database for your request (not necessarily shown in a single response depending on the **cursor** size). | | **Page** | A subset of the full **list** of items. Choose the size of a **page** with the `per_page` parameter. | | **Total** | The `total` attribute indicates the number of items in the full **list**.| | **Cursor** | Use the `cursor` query parameter to select a next or previous **page**. | | **Next** & **Prev** | Use the `next` and `prev` attributes of the `links` meta object as `cursor` values. |  ### How to use Paging  If your result **list** total exceeds the default **cursor** size (the default depends on the route, but is usually 100 records) or the value defined by the `per_page` query param (when present) the response will be returned to you paginated.  ### Paging Example  > These examples have abbreviated attributes and sample values. Your actual `cursor` values will be encoded alphanumeric strings.  To return a **page** with the first two plans in the List:      curl \"https://api.vultr.com/v2/plans?per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  The API returns an object similar to this:      {         \"plans\": [             {                 \"id\": \"vc2-1c-2gb\",                 \"vcpu_count\": 1,                 \"ram\": 2048,                 \"locations\": []             },             {                 \"id\": \"vc2-24c-97gb\",                 \"vcpu_count\": 24,                 \"ram\": 98304,                 \"locations\": []             }         ],         \"meta\": {             \"total\": 19,             \"links\": {                 \"next\": \"WxYzExampleNext\",                 \"prev\": \"WxYzExamplePrev\"             }         }     }  The object contains two plans. The `total` attribute indicates that 19 plans are available in the List. To navigate forward in the **list**, use the `next` value (`WxYzExampleNext` in this example) as your `cursor` query parameter.      curl \"https://api.vultr.com/v2/plans?per_page=2&cursor=WxYzExampleNext\" \\       -X GET       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  Likewise, you can use the example `prev` value `WxYzExamplePrev` to navigate backward.  ## Parameters  You can pass information to the API with three different types of parameters.  ### Path parameters  Some API calls require variable parameters as part of the endpoint path. For example, to retrieve information about a user, supply the `user-id` in the endpoint.      curl \"https://api.vultr.com/v2/users/{user-id}\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Query parameters  Some API calls allow filtering with query parameters. For example, the `/v2/plans` endpoint supports a `type` query parameter. Setting `type=vhf` instructs the API only to return High Frequency Compute plans in the list. You'll find more specifics about valid filters in the endpoint documentation below.      curl \"https://api.vultr.com/v2/plans?type=vhf\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  You can also combine filtering with paging. Use the `per_page` parameter to retreive a subset of vhf plans.      curl \"https://api.vultr.com/v2/plans?type=vhf&per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Request Body  PUT, POST, and PATCH methods may include an object in the request body with a content type of **application/json**. The documentation for each endpoint below has more information about the expected object.  ## API Example Conventions  The examples in this documentation use `curl`, a command-line HTTP client, to demonstrate useage. Linux and macOS computers usually have curl installed by default, and it's [available for download](https://curl.se/download.html) on all popular platforms including Windows.  Each example is split across multiple lines with the `\\` character, which is compatible with a `bash` terminal. A typical example looks like this:      curl \"https://api.vultr.com/v2/domains\" \\       -X POST \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\" \\       -H \"Content-Type: application/json\" \\       - -data '{         \"domain\" : \"example.com\",         \"ip\" : \"192.0.2.123\"       }'  * The `-X` parameter sets the request method. For consistency, we show the method on all examples, even though it's not explicitly required for GET methods. * The `-H` lines set required HTTP headers. These examples are formatted to expand the VULTR\\_API\\_KEY environment variable for your convenience. * Examples that require a JSON object in the request body pass the required data via the `- -data` parameter.  All values in this guide are examples. Do not rely on the OS or Plan IDs listed in this guide; use the appropriate endpoint to retreive values before creating resources. 
 *
 * The version of the OpenAPI document: 2.0
 * Contact: support@vultr.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using Org.OpenAPITools.Client;
using Org.OpenAPITools.Api;
// uncomment below to import models
//using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test.Api
{
    /// <summary>
    ///  Class for testing InstancesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class InstancesApiTests : IDisposable
    {
        private InstancesApi instance;

        public InstancesApiTests()
        {
            instance = new InstancesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of InstancesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' InstancesApi
            //Assert.IsType<InstancesApi>(instance);
        }

        /// <summary>
        /// Test AttachInstanceIso
        /// </summary>
        [Fact]
        public void AttachInstanceIsoTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //AttachInstanceIsoRequest? attachInstanceIsoRequest = null;
            //var response = instance.AttachInstanceIso(instanceId, attachInstanceIsoRequest);
            //Assert.IsType<AttachInstanceIso202Response>(response);
        }

        /// <summary>
        /// Test AttachInstanceNetwork
        /// </summary>
        [Fact]
        public void AttachInstanceNetworkTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //AttachInstanceNetworkRequest? attachInstanceNetworkRequest = null;
            //instance.AttachInstanceNetwork(instanceId, attachInstanceNetworkRequest);
        }

        /// <summary>
        /// Test AttachInstanceVpc
        /// </summary>
        [Fact]
        public void AttachInstanceVpcTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //AttachInstanceVpcRequest? attachInstanceVpcRequest = null;
            //instance.AttachInstanceVpc(instanceId, attachInstanceVpcRequest);
        }

        /// <summary>
        /// Test AttachInstanceVpc2
        /// </summary>
        [Fact]
        public void AttachInstanceVpc2Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //AttachInstanceVpc2Request? attachInstanceVpc2Request = null;
            //instance.AttachInstanceVpc2(instanceId, attachInstanceVpc2Request);
        }

        /// <summary>
        /// Test CreateInstance
        /// </summary>
        [Fact]
        public void CreateInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateInstanceRequest? createInstanceRequest = null;
            //var response = instance.CreateInstance(createInstanceRequest);
            //Assert.IsType<CreateInstance202Response>(response);
        }

        /// <summary>
        /// Test CreateInstanceBackupSchedule
        /// </summary>
        [Fact]
        public void CreateInstanceBackupScheduleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = null;
            //instance.CreateInstanceBackupSchedule(instanceId, createInstanceBackupScheduleRequest);
        }

        /// <summary>
        /// Test CreateInstanceIpv4
        /// </summary>
        [Fact]
        public void CreateInstanceIpv4Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //CreateInstanceIpv4Request? createInstanceIpv4Request = null;
            //var response = instance.CreateInstanceIpv4(instanceId, createInstanceIpv4Request);
            //Assert.IsType<Object>(response);
        }

        /// <summary>
        /// Test CreateInstanceReverseIpv4
        /// </summary>
        [Fact]
        public void CreateInstanceReverseIpv4Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = null;
            //instance.CreateInstanceReverseIpv4(instanceId, createInstanceReverseIpv4Request);
        }

        /// <summary>
        /// Test CreateInstanceReverseIpv6
        /// </summary>
        [Fact]
        public void CreateInstanceReverseIpv6Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = null;
            //instance.CreateInstanceReverseIpv6(instanceId, createInstanceReverseIpv6Request);
        }

        /// <summary>
        /// Test DeleteInstance
        /// </summary>
        [Fact]
        public void DeleteInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //instance.DeleteInstance(instanceId);
        }

        /// <summary>
        /// Test DeleteInstanceIpv4
        /// </summary>
        [Fact]
        public void DeleteInstanceIpv4Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //string ipv4 = null;
            //instance.DeleteInstanceIpv4(instanceId, ipv4);
        }

        /// <summary>
        /// Test DeleteInstanceReverseIpv6
        /// </summary>
        [Fact]
        public void DeleteInstanceReverseIpv6Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //string ipv6 = null;
            //instance.DeleteInstanceReverseIpv6(instanceId, ipv6);
        }

        /// <summary>
        /// Test DetachInstanceIso
        /// </summary>
        [Fact]
        public void DetachInstanceIsoTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.DetachInstanceIso(instanceId);
            //Assert.IsType<DetachInstanceIso202Response>(response);
        }

        /// <summary>
        /// Test DetachInstanceNetwork
        /// </summary>
        [Fact]
        public void DetachInstanceNetworkTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //DetachInstanceNetworkRequest? detachInstanceNetworkRequest = null;
            //instance.DetachInstanceNetwork(instanceId, detachInstanceNetworkRequest);
        }

        /// <summary>
        /// Test DetachInstanceVpc
        /// </summary>
        [Fact]
        public void DetachInstanceVpcTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //DetachInstanceVpcRequest? detachInstanceVpcRequest = null;
            //instance.DetachInstanceVpc(instanceId, detachInstanceVpcRequest);
        }

        /// <summary>
        /// Test DetachInstanceVpc2
        /// </summary>
        [Fact]
        public void DetachInstanceVpc2Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //DetachInstanceVpc2Request? detachInstanceVpc2Request = null;
            //instance.DetachInstanceVpc2(instanceId, detachInstanceVpc2Request);
        }

        /// <summary>
        /// Test GetInstance
        /// </summary>
        [Fact]
        public void GetInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstance(instanceId);
            //Assert.IsType<CreateInstance202Response>(response);
        }

        /// <summary>
        /// Test GetInstanceBackupSchedule
        /// </summary>
        [Fact]
        public void GetInstanceBackupScheduleTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstanceBackupSchedule(instanceId);
            //Assert.IsType<GetInstanceBackupSchedule200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceBandwidth
        /// </summary>
        [Fact]
        public void GetInstanceBandwidthTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //int? dateRange = null;
            //var response = instance.GetInstanceBandwidth(instanceId, dateRange);
            //Assert.IsType<GetBandwidthBaremetal200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceIpv4
        /// </summary>
        [Fact]
        public void GetInstanceIpv4Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //bool? publicNetwork = null;
            //int? perPage = null;
            //string? cursor = null;
            //var response = instance.GetInstanceIpv4(instanceId, publicNetwork, perPage, cursor);
            //Assert.IsType<GetIpv4Baremetal200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceIpv6
        /// </summary>
        [Fact]
        public void GetInstanceIpv6Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstanceIpv6(instanceId);
            //Assert.IsType<GetIpv6Baremetal200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceIsoStatus
        /// </summary>
        [Fact]
        public void GetInstanceIsoStatusTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstanceIsoStatus(instanceId);
            //Assert.IsType<GetInstanceIsoStatus200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceNeighbors
        /// </summary>
        [Fact]
        public void GetInstanceNeighborsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstanceNeighbors(instanceId);
            //Assert.IsType<GetInstanceNeighbors200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceUpgrades
        /// </summary>
        [Fact]
        public void GetInstanceUpgradesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //string? type = null;
            //var response = instance.GetInstanceUpgrades(instanceId, type);
            //Assert.IsType<GetInstanceUpgrades200Response>(response);
        }

        /// <summary>
        /// Test GetInstanceUserdata
        /// </summary>
        [Fact]
        public void GetInstanceUserdataTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.GetInstanceUserdata(instanceId);
            //Assert.IsType<GetInstanceUserdata200Response>(response);
        }

        /// <summary>
        /// Test HaltInstance
        /// </summary>
        [Fact]
        public void HaltInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //instance.HaltInstance(instanceId);
        }

        /// <summary>
        /// Test HaltInstances
        /// </summary>
        [Fact]
        public void HaltInstancesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //HaltInstancesRequest? haltInstancesRequest = null;
            //instance.HaltInstances(haltInstancesRequest);
        }

        /// <summary>
        /// Test ListInstanceIpv6Reverse
        /// </summary>
        [Fact]
        public void ListInstanceIpv6ReverseTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //var response = instance.ListInstanceIpv6Reverse(instanceId);
            //Assert.IsType<ListInstanceIpv6Reverse200Response>(response);
        }

        /// <summary>
        /// Test ListInstancePrivateNetworks
        /// </summary>
        [Fact]
        public void ListInstancePrivateNetworksTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //int? perPage = null;
            //string? cursor = null;
            //var response = instance.ListInstancePrivateNetworks(instanceId, perPage, cursor);
            //Assert.IsType<ListInstancePrivateNetworks200Response>(response);
        }

        /// <summary>
        /// Test ListInstanceVpc2
        /// </summary>
        [Fact]
        public void ListInstanceVpc2Test()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //int? perPage = null;
            //string? cursor = null;
            //var response = instance.ListInstanceVpc2(instanceId, perPage, cursor);
            //Assert.IsType<ListInstanceVpc2200Response>(response);
        }

        /// <summary>
        /// Test ListInstanceVpcs
        /// </summary>
        [Fact]
        public void ListInstanceVpcsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //int? perPage = null;
            //string? cursor = null;
            //var response = instance.ListInstanceVpcs(instanceId, perPage, cursor);
            //Assert.IsType<ListInstanceVpcs200Response>(response);
        }

        /// <summary>
        /// Test ListInstances
        /// </summary>
        [Fact]
        public void ListInstancesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? perPage = null;
            //string? cursor = null;
            //string? tag = null;
            //string? label = null;
            //string? mainIp = null;
            //string? region = null;
            //string? firewallGroupId = null;
            //var response = instance.ListInstances(perPage, cursor, tag, label, mainIp, region, firewallGroupId);
            //Assert.IsType<ListInstances200Response>(response);
        }

        /// <summary>
        /// Test PostInstancesInstanceIdIpv4ReverseDefault
        /// </summary>
        [Fact]
        public void PostInstancesInstanceIdIpv4ReverseDefaultTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = null;
            //instance.PostInstancesInstanceIdIpv4ReverseDefault(instanceId, postInstancesInstanceIdIpv4ReverseDefaultRequest);
        }

        /// <summary>
        /// Test RebootInstance
        /// </summary>
        [Fact]
        public void RebootInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //instance.RebootInstance(instanceId);
        }

        /// <summary>
        /// Test RebootInstances
        /// </summary>
        [Fact]
        public void RebootInstancesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //RebootInstancesRequest? rebootInstancesRequest = null;
            //instance.RebootInstances(rebootInstancesRequest);
        }

        /// <summary>
        /// Test ReinstallInstance
        /// </summary>
        [Fact]
        public void ReinstallInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //ReinstallInstanceRequest? reinstallInstanceRequest = null;
            //var response = instance.ReinstallInstance(instanceId, reinstallInstanceRequest);
            //Assert.IsType<CreateInstance202Response>(response);
        }

        /// <summary>
        /// Test RestoreInstance
        /// </summary>
        [Fact]
        public void RestoreInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //RestoreInstanceRequest? restoreInstanceRequest = null;
            //var response = instance.RestoreInstance(instanceId, restoreInstanceRequest);
            //Assert.IsType<RestoreInstance202Response>(response);
        }

        /// <summary>
        /// Test StartInstance
        /// </summary>
        [Fact]
        public void StartInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //instance.StartInstance(instanceId);
        }

        /// <summary>
        /// Test StartInstances
        /// </summary>
        [Fact]
        public void StartInstancesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //StartInstancesRequest? startInstancesRequest = null;
            //instance.StartInstances(startInstancesRequest);
        }

        /// <summary>
        /// Test UpdateInstance
        /// </summary>
        [Fact]
        public void UpdateInstanceTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string instanceId = null;
            //UpdateInstanceRequest? updateInstanceRequest = null;
            //var response = instance.UpdateInstance(instanceId, updateInstanceRequest);
            //Assert.IsType<CreateInstance202Response>(response);
        }
    }
}
