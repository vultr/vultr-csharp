/*
 * Vultr API
 *
 * # Introduction  The Vultr API v2 is a set of HTTP endpoints that adhere to RESTful design principles and CRUD actions with predictable URIs. It uses standard HTTP response codes, authentication, and verbs. The API has consistent and well-formed JSON requests and responses with cursor-based pagination to simplify list handling. Error messages are descriptive and easy to understand. All functions of the Vultr customer portal are accessible via the API, enabling you to script complex unattended scenarios with any tool fluent in HTTP.  ## Requests  Communicate with the API by making an HTTP request at the correct endpoint. The chosen method determines the action taken.  | Method | Usage | | - -- -- - | - -- -- -- -- -- -- | | DELETE | Use the DELETE method to destroy a resource in your account. If it is not found, the operation will return a 4xx error and an appropriate message. | | GET | To retrieve information about a resource, use the GET method. The data is returned as a JSON object. GET methods are read-only and do not affect any resources. | | PATCH | Some resources support partial modification with PATCH, which modifies specific attributes without updating the entire object representation. | | POST | Issue a POST method to create a new object. Include all needed attributes in the request body encoded as JSON. | | PUT | Use the PUT method to update information about a resource. PUT will set new values on the item without regard to their current values. |  **Rate Limit:** Vultr safeguards the API against bursts of incoming traffic based on the request's IP address to ensure stability for all users. If your application sends more than 30 requests per second, the API may return HTTP status code 429.  ## Response Codes  We use standard HTTP response codes to show the success or failure of requests. Response codes in the 2xx range indicate success, while codes in the 4xx range indicate an error, such as an authorization failure or a malformed request. All 4xx errors will return a JSON response object with an `error` attribute explaining the error. Codes in the 5xx range indicate a server-side problem preventing Vultr from fulfilling your request.  | Response | Description | | - -- -- - | - -- -- -- -- -- -- | | 200 OK | The response contains your requested information. | | 201 Created | Your request was accepted. The resource was created. | | 202 Accepted | Your request was accepted. The resource was created or updated. | | 204 No Content | Your request succeeded, there is no additional information returned. | | 400 Bad Request | Your request was malformed. | | 401 Unauthorized | You did not supply valid authentication credentials. | | 403 Forbidden | You are not allowed to perform that action. | | 404 Not Found | No results were found for your request. | | 429 Too Many Requests | Your request exceeded the API rate limit. | | 500 Internal Server Error | We were unable to perform the request due to server-side problems. |  ## Meta and Pagination  Many API calls will return a `meta` object with paging information.  ### Definitions  | Term | Description | | - -- -- - | - -- -- -- -- -- -- | | **List** | The items returned from the database for your request (not necessarily shown in a single response depending on the **cursor** size). | | **Page** | A subset of the full **list** of items. Choose the size of a **page** with the `per_page` parameter. | | **Total** | The `total` attribute indicates the number of items in the full **list**.| | **Cursor** | Use the `cursor` query parameter to select a next or previous **page**. | | **Next** & **Prev** | Use the `next` and `prev` attributes of the `links` meta object as `cursor` values. |  ### How to use Paging  If your result **list** total exceeds the default **cursor** size (the default depends on the route, but is usually 100 records) or the value defined by the `per_page` query param (when present) the response will be returned to you paginated.  ### Paging Example  > These examples have abbreviated attributes and sample values. Your actual `cursor` values will be encoded alphanumeric strings.  To return a **page** with the first two plans in the List:      curl \"https://api.vultr.com/v2/plans?per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  The API returns an object similar to this:      {         \"plans\": [             {                 \"id\": \"vc2-1c-2gb\",                 \"vcpu_count\": 1,                 \"ram\": 2048,                 \"locations\": []             },             {                 \"id\": \"vc2-24c-97gb\",                 \"vcpu_count\": 24,                 \"ram\": 98304,                 \"locations\": []             }         ],         \"meta\": {             \"total\": 19,             \"links\": {                 \"next\": \"WxYzExampleNext\",                 \"prev\": \"WxYzExamplePrev\"             }         }     }  The object contains two plans. The `total` attribute indicates that 19 plans are available in the List. To navigate forward in the **list**, use the `next` value (`WxYzExampleNext` in this example) as your `cursor` query parameter.      curl \"https://api.vultr.com/v2/plans?per_page=2&cursor=WxYzExampleNext\" \\       -X GET       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  Likewise, you can use the example `prev` value `WxYzExamplePrev` to navigate backward.  ## Parameters  You can pass information to the API with three different types of parameters.  ### Path parameters  Some API calls require variable parameters as part of the endpoint path. For example, to retrieve information about a user, supply the `user-id` in the endpoint.      curl \"https://api.vultr.com/v2/users/{user-id}\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Query parameters  Some API calls allow filtering with query parameters. For example, the `/v2/plans` endpoint supports a `type` query parameter. Setting `type=vhf` instructs the API only to return High Frequency Compute plans in the list. You'll find more specifics about valid filters in the endpoint documentation below.      curl \"https://api.vultr.com/v2/plans?type=vhf\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  You can also combine filtering with paging. Use the `per_page` parameter to retreive a subset of vhf plans.      curl \"https://api.vultr.com/v2/plans?type=vhf&per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Request Body  PUT, POST, and PATCH methods may include an object in the request body with a content type of **application/json**. The documentation for each endpoint below has more information about the expected object.  ## API Example Conventions  The examples in this documentation use `curl`, a command-line HTTP client, to demonstrate useage. Linux and macOS computers usually have curl installed by default, and it's [available for download](https://curl.se/download.html) on all popular platforms including Windows.  Each example is split across multiple lines with the `\\` character, which is compatible with a `bash` terminal. A typical example looks like this:      curl \"https://api.vultr.com/v2/domains\" \\       -X POST \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\" \\       -H \"Content-Type: application/json\" \\       - -data '{         \"domain\" : \"example.com\",         \"ip\" : \"192.0.2.123\"       }'  * The `-X` parameter sets the request method. For consistency, we show the method on all examples, even though it's not explicitly required for GET methods. * The `-H` lines set required HTTP headers. These examples are formatted to expand the VULTR\\_API\\_KEY environment variable for your convenience. * Examples that require a JSON object in the request body pass the required data via the `- -data` parameter.  All values in this guide are examples. Do not rely on the OS or Plan IDs listed in this guide; use the appropriate endpoint to retreive values before creating resources. 
 *
 * The version of the OpenAPI document: 2.0
 * Contact: support@vultr.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using Xunit;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Org.OpenAPITools.Model;
using Org.OpenAPITools.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace Org.OpenAPITools.Test.Model
{
    /// <summary>
    ///  Class for testing UpdateAdvancedOptionsRequest
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class UpdateAdvancedOptionsRequestTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for UpdateAdvancedOptionsRequest
        //private UpdateAdvancedOptionsRequest instance;

        public UpdateAdvancedOptionsRequestTests()
        {
            // TODO uncomment below to create an instance of UpdateAdvancedOptionsRequest
            //instance = new UpdateAdvancedOptionsRequest();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of UpdateAdvancedOptionsRequest
        /// </summary>
        [Fact]
        public void UpdateAdvancedOptionsRequestInstanceTest()
        {
            // TODO uncomment below to test "IsType" UpdateAdvancedOptionsRequest
            //Assert.IsType<UpdateAdvancedOptionsRequest>(instance);
        }

        /// <summary>
        /// Test the property 'AutovacuumAnalyzeScaleFactor'
        /// </summary>
        [Fact]
        public void AutovacuumAnalyzeScaleFactorTest()
        {
            // TODO unit test for the property 'AutovacuumAnalyzeScaleFactor'
        }

        /// <summary>
        /// Test the property 'AutovacuumAnalyzeThreshold'
        /// </summary>
        [Fact]
        public void AutovacuumAnalyzeThresholdTest()
        {
            // TODO unit test for the property 'AutovacuumAnalyzeThreshold'
        }

        /// <summary>
        /// Test the property 'AutovacuumFreezeMaxAge'
        /// </summary>
        [Fact]
        public void AutovacuumFreezeMaxAgeTest()
        {
            // TODO unit test for the property 'AutovacuumFreezeMaxAge'
        }

        /// <summary>
        /// Test the property 'AutovacuumMaxWorkers'
        /// </summary>
        [Fact]
        public void AutovacuumMaxWorkersTest()
        {
            // TODO unit test for the property 'AutovacuumMaxWorkers'
        }

        /// <summary>
        /// Test the property 'AutovacuumNaptime'
        /// </summary>
        [Fact]
        public void AutovacuumNaptimeTest()
        {
            // TODO unit test for the property 'AutovacuumNaptime'
        }

        /// <summary>
        /// Test the property 'AutovacuumVacuumCostDelay'
        /// </summary>
        [Fact]
        public void AutovacuumVacuumCostDelayTest()
        {
            // TODO unit test for the property 'AutovacuumVacuumCostDelay'
        }

        /// <summary>
        /// Test the property 'AutovacuumVacuumCostLimit'
        /// </summary>
        [Fact]
        public void AutovacuumVacuumCostLimitTest()
        {
            // TODO unit test for the property 'AutovacuumVacuumCostLimit'
        }

        /// <summary>
        /// Test the property 'AutovacuumVacuumScaleFactor'
        /// </summary>
        [Fact]
        public void AutovacuumVacuumScaleFactorTest()
        {
            // TODO unit test for the property 'AutovacuumVacuumScaleFactor'
        }

        /// <summary>
        /// Test the property 'AutovacuumVacuumThreshold'
        /// </summary>
        [Fact]
        public void AutovacuumVacuumThresholdTest()
        {
            // TODO unit test for the property 'AutovacuumVacuumThreshold'
        }

        /// <summary>
        /// Test the property 'BgwriterDelay'
        /// </summary>
        [Fact]
        public void BgwriterDelayTest()
        {
            // TODO unit test for the property 'BgwriterDelay'
        }

        /// <summary>
        /// Test the property 'BgwriterFlushAfter'
        /// </summary>
        [Fact]
        public void BgwriterFlushAfterTest()
        {
            // TODO unit test for the property 'BgwriterFlushAfter'
        }

        /// <summary>
        /// Test the property 'BgwriterLruMaxpages'
        /// </summary>
        [Fact]
        public void BgwriterLruMaxpagesTest()
        {
            // TODO unit test for the property 'BgwriterLruMaxpages'
        }

        /// <summary>
        /// Test the property 'BgwriterLruMultiplier'
        /// </summary>
        [Fact]
        public void BgwriterLruMultiplierTest()
        {
            // TODO unit test for the property 'BgwriterLruMultiplier'
        }

        /// <summary>
        /// Test the property 'DeadlockTimeout'
        /// </summary>
        [Fact]
        public void DeadlockTimeoutTest()
        {
            // TODO unit test for the property 'DeadlockTimeout'
        }

        /// <summary>
        /// Test the property 'DefaultToastCompression'
        /// </summary>
        [Fact]
        public void DefaultToastCompressionTest()
        {
            // TODO unit test for the property 'DefaultToastCompression'
        }

        /// <summary>
        /// Test the property 'IdleInTransactionSessionTimeout'
        /// </summary>
        [Fact]
        public void IdleInTransactionSessionTimeoutTest()
        {
            // TODO unit test for the property 'IdleInTransactionSessionTimeout'
        }

        /// <summary>
        /// Test the property 'Jit'
        /// </summary>
        [Fact]
        public void JitTest()
        {
            // TODO unit test for the property 'Jit'
        }

        /// <summary>
        /// Test the property 'LogAutovacuumMinDuration'
        /// </summary>
        [Fact]
        public void LogAutovacuumMinDurationTest()
        {
            // TODO unit test for the property 'LogAutovacuumMinDuration'
        }

        /// <summary>
        /// Test the property 'LogErrorVerbosity'
        /// </summary>
        [Fact]
        public void LogErrorVerbosityTest()
        {
            // TODO unit test for the property 'LogErrorVerbosity'
        }

        /// <summary>
        /// Test the property 'LogLinePrefix'
        /// </summary>
        [Fact]
        public void LogLinePrefixTest()
        {
            // TODO unit test for the property 'LogLinePrefix'
        }

        /// <summary>
        /// Test the property 'LogMinDurationStatement'
        /// </summary>
        [Fact]
        public void LogMinDurationStatementTest()
        {
            // TODO unit test for the property 'LogMinDurationStatement'
        }

        /// <summary>
        /// Test the property 'MaxFilesPerProcess'
        /// </summary>
        [Fact]
        public void MaxFilesPerProcessTest()
        {
            // TODO unit test for the property 'MaxFilesPerProcess'
        }

        /// <summary>
        /// Test the property 'MaxLocksPerTransaction'
        /// </summary>
        [Fact]
        public void MaxLocksPerTransactionTest()
        {
            // TODO unit test for the property 'MaxLocksPerTransaction'
        }

        /// <summary>
        /// Test the property 'MaxLogicalReplicationWorkers'
        /// </summary>
        [Fact]
        public void MaxLogicalReplicationWorkersTest()
        {
            // TODO unit test for the property 'MaxLogicalReplicationWorkers'
        }

        /// <summary>
        /// Test the property 'MaxParallelWorkers'
        /// </summary>
        [Fact]
        public void MaxParallelWorkersTest()
        {
            // TODO unit test for the property 'MaxParallelWorkers'
        }

        /// <summary>
        /// Test the property 'MaxParallelWorkersPerGather'
        /// </summary>
        [Fact]
        public void MaxParallelWorkersPerGatherTest()
        {
            // TODO unit test for the property 'MaxParallelWorkersPerGather'
        }

        /// <summary>
        /// Test the property 'MaxPredLocksPerTransaction'
        /// </summary>
        [Fact]
        public void MaxPredLocksPerTransactionTest()
        {
            // TODO unit test for the property 'MaxPredLocksPerTransaction'
        }

        /// <summary>
        /// Test the property 'MaxPreparedTransactions'
        /// </summary>
        [Fact]
        public void MaxPreparedTransactionsTest()
        {
            // TODO unit test for the property 'MaxPreparedTransactions'
        }

        /// <summary>
        /// Test the property 'MaxReplicationSlots'
        /// </summary>
        [Fact]
        public void MaxReplicationSlotsTest()
        {
            // TODO unit test for the property 'MaxReplicationSlots'
        }

        /// <summary>
        /// Test the property 'MaxStackDepth'
        /// </summary>
        [Fact]
        public void MaxStackDepthTest()
        {
            // TODO unit test for the property 'MaxStackDepth'
        }

        /// <summary>
        /// Test the property 'MaxStandbyArchiveDelay'
        /// </summary>
        [Fact]
        public void MaxStandbyArchiveDelayTest()
        {
            // TODO unit test for the property 'MaxStandbyArchiveDelay'
        }

        /// <summary>
        /// Test the property 'MaxStandbyStreamingDelay'
        /// </summary>
        [Fact]
        public void MaxStandbyStreamingDelayTest()
        {
            // TODO unit test for the property 'MaxStandbyStreamingDelay'
        }

        /// <summary>
        /// Test the property 'MaxWalSenders'
        /// </summary>
        [Fact]
        public void MaxWalSendersTest()
        {
            // TODO unit test for the property 'MaxWalSenders'
        }

        /// <summary>
        /// Test the property 'MaxWorkerProcesses'
        /// </summary>
        [Fact]
        public void MaxWorkerProcessesTest()
        {
            // TODO unit test for the property 'MaxWorkerProcesses'
        }

        /// <summary>
        /// Test the property 'PgPartmanBgwInterval'
        /// </summary>
        [Fact]
        public void PgPartmanBgwIntervalTest()
        {
            // TODO unit test for the property 'PgPartmanBgwInterval'
        }

        /// <summary>
        /// Test the property 'PgPartmanBgwRole'
        /// </summary>
        [Fact]
        public void PgPartmanBgwRoleTest()
        {
            // TODO unit test for the property 'PgPartmanBgwRole'
        }

        /// <summary>
        /// Test the property 'PgStatStatementsTrack'
        /// </summary>
        [Fact]
        public void PgStatStatementsTrackTest()
        {
            // TODO unit test for the property 'PgStatStatementsTrack'
        }

        /// <summary>
        /// Test the property 'TempFileLimit'
        /// </summary>
        [Fact]
        public void TempFileLimitTest()
        {
            // TODO unit test for the property 'TempFileLimit'
        }

        /// <summary>
        /// Test the property 'TrackActivityQuerySize'
        /// </summary>
        [Fact]
        public void TrackActivityQuerySizeTest()
        {
            // TODO unit test for the property 'TrackActivityQuerySize'
        }

        /// <summary>
        /// Test the property 'TrackCommitTimestamp'
        /// </summary>
        [Fact]
        public void TrackCommitTimestampTest()
        {
            // TODO unit test for the property 'TrackCommitTimestamp'
        }

        /// <summary>
        /// Test the property 'TrackFunctions'
        /// </summary>
        [Fact]
        public void TrackFunctionsTest()
        {
            // TODO unit test for the property 'TrackFunctions'
        }

        /// <summary>
        /// Test the property 'TrackIoTiming'
        /// </summary>
        [Fact]
        public void TrackIoTimingTest()
        {
            // TODO unit test for the property 'TrackIoTiming'
        }

        /// <summary>
        /// Test the property 'WalSenderTimeout'
        /// </summary>
        [Fact]
        public void WalSenderTimeoutTest()
        {
            // TODO unit test for the property 'WalSenderTimeout'
        }

        /// <summary>
        /// Test the property 'WalWriterDelay'
        /// </summary>
        [Fact]
        public void WalWriterDelayTest()
        {
            // TODO unit test for the property 'WalWriterDelay'
        }
    }
}
