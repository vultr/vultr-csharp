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
    ///  Class for testing ManagedDatabasesApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ManagedDatabasesApiTests : IDisposable
    {
        private ManagedDatabasesApi instance;

        public ManagedDatabasesApiTests()
        {
            instance = new ManagedDatabasesApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ManagedDatabasesApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ManagedDatabasesApi
            //Assert.IsType<ManagedDatabasesApi>(instance);
        }

        /// <summary>
        /// Test CreateConnectionPool
        /// </summary>
        [Fact]
        public void CreateConnectionPoolTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateConnectionPoolRequest? createConnectionPoolRequest = null;
            //var response = instance.CreateConnectionPool(createConnectionPoolRequest);
            //Assert.IsType<CreateConnectionPool202Response>(response);
        }

        /// <summary>
        /// Test CreateDatabase
        /// </summary>
        [Fact]
        public void CreateDatabaseTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateDatabaseRequest? createDatabaseRequest = null;
            //var response = instance.CreateDatabase(createDatabaseRequest);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test CreateDatabaseDb
        /// </summary>
        [Fact]
        public void CreateDatabaseDbTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateDatabaseDbRequest? createDatabaseDbRequest = null;
            //var response = instance.CreateDatabaseDb(createDatabaseDbRequest);
            //Assert.IsType<CreateDatabaseDb202Response>(response);
        }

        /// <summary>
        /// Test CreateDatabaseUser
        /// </summary>
        [Fact]
        public void CreateDatabaseUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //CreateDatabaseUserRequest? createDatabaseUserRequest = null;
            //var response = instance.CreateDatabaseUser(createDatabaseUserRequest);
            //Assert.IsType<CreateDatabaseUser202Response>(response);
        }

        /// <summary>
        /// Test DatabaseAddReadReplica
        /// </summary>
        [Fact]
        public void DatabaseAddReadReplicaTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = null;
            //var response = instance.DatabaseAddReadReplica(databaseAddReadReplicaRequest);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test DatabaseDetachMigration
        /// </summary>
        [Fact]
        public void DatabaseDetachMigrationTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.DatabaseDetachMigration();
        }

        /// <summary>
        /// Test DatabaseFork
        /// </summary>
        [Fact]
        public void DatabaseForkTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatabaseForkRequest? databaseForkRequest = null;
            //var response = instance.DatabaseFork(databaseForkRequest);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test DatabasePromoteReadReplica
        /// </summary>
        [Fact]
        public void DatabasePromoteReadReplicaTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //instance.DatabasePromoteReadReplica();
        }

        /// <summary>
        /// Test DatabaseRestoreFromBackup
        /// </summary>
        [Fact]
        public void DatabaseRestoreFromBackupTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = null;
            //var response = instance.DatabaseRestoreFromBackup(databaseRestoreFromBackupRequest);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test DatabaseStartMigration
        /// </summary>
        [Fact]
        public void DatabaseStartMigrationTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //DatabaseStartMigrationRequest? databaseStartMigrationRequest = null;
            //var response = instance.DatabaseStartMigration(databaseStartMigrationRequest);
            //Assert.IsType<ViewMigrationStatus200Response>(response);
        }

        /// <summary>
        /// Test DeleteConnectionPool
        /// </summary>
        [Fact]
        public void DeleteConnectionPoolTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string poolName = null;
            //instance.DeleteConnectionPool(databaseId, poolName);
        }

        /// <summary>
        /// Test DeleteDatabase
        /// </summary>
        [Fact]
        public void DeleteDatabaseTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //instance.DeleteDatabase(databaseId);
        }

        /// <summary>
        /// Test DeleteDatabaseDb
        /// </summary>
        [Fact]
        public void DeleteDatabaseDbTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string dbName = null;
            //instance.DeleteDatabaseDb(databaseId, dbName);
        }

        /// <summary>
        /// Test DeleteDatabaseUser
        /// </summary>
        [Fact]
        public void DeleteDatabaseUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string username = null;
            //instance.DeleteDatabaseUser(databaseId, username);
        }

        /// <summary>
        /// Test GetBackupInformation
        /// </summary>
        [Fact]
        public void GetBackupInformationTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetBackupInformation();
            //Assert.IsType<GetBackupInformation200Response>(response);
        }

        /// <summary>
        /// Test GetConnectionPool
        /// </summary>
        [Fact]
        public void GetConnectionPoolTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string poolName = null;
            //var response = instance.GetConnectionPool(databaseId, poolName);
            //Assert.IsType<CreateConnectionPool202Response>(response);
        }

        /// <summary>
        /// Test GetDatabase
        /// </summary>
        [Fact]
        public void GetDatabaseTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //var response = instance.GetDatabase(databaseId);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test GetDatabaseDb
        /// </summary>
        [Fact]
        public void GetDatabaseDbTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string dbName = null;
            //var response = instance.GetDatabaseDb(databaseId, dbName);
            //Assert.IsType<CreateDatabaseDb202Response>(response);
        }

        /// <summary>
        /// Test GetDatabaseUsage
        /// </summary>
        [Fact]
        public void GetDatabaseUsageTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.GetDatabaseUsage();
            //Assert.IsType<GetDatabaseUsage200Response>(response);
        }

        /// <summary>
        /// Test GetDatabaseUser
        /// </summary>
        [Fact]
        public void GetDatabaseUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string username = null;
            //var response = instance.GetDatabaseUser(databaseId, username);
            //Assert.IsType<CreateDatabaseUser202Response>(response);
        }

        /// <summary>
        /// Test ListAdvancedOptions
        /// </summary>
        [Fact]
        public void ListAdvancedOptionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListAdvancedOptions();
            //Assert.IsType<ListAdvancedOptions200Response>(response);
        }

        /// <summary>
        /// Test ListAvailableVersions
        /// </summary>
        [Fact]
        public void ListAvailableVersionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListAvailableVersions();
            //Assert.IsType<ListAvailableVersions200Response>(response);
        }

        /// <summary>
        /// Test ListConnectionPools
        /// </summary>
        [Fact]
        public void ListConnectionPoolsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListConnectionPools();
            //Assert.IsType<ListConnectionPools200Response>(response);
        }

        /// <summary>
        /// Test ListDatabaseDbs
        /// </summary>
        [Fact]
        public void ListDatabaseDbsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListDatabaseDbs();
            //Assert.IsType<ListDatabaseDbs200Response>(response);
        }

        /// <summary>
        /// Test ListDatabasePlans
        /// </summary>
        [Fact]
        public void ListDatabasePlansTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string? engine = null;
            //int? nodes = null;
            //string? region = null;
            //var response = instance.ListDatabasePlans(engine, nodes, region);
            //Assert.IsType<ListDatabasePlans200Response>(response);
        }

        /// <summary>
        /// Test ListDatabaseUsers
        /// </summary>
        [Fact]
        public void ListDatabaseUsersTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListDatabaseUsers();
            //Assert.IsType<ListDatabaseUsers200Response>(response);
        }

        /// <summary>
        /// Test ListDatabases
        /// </summary>
        [Fact]
        public void ListDatabasesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string? label = null;
            //string? tag = null;
            //string? region = null;
            //var response = instance.ListDatabases(label, tag, region);
            //Assert.IsType<ListDatabases200Response>(response);
        }

        /// <summary>
        /// Test ListMaintenanceUpdates
        /// </summary>
        [Fact]
        public void ListMaintenanceUpdatesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ListMaintenanceUpdates();
            //Assert.IsType<ListMaintenanceUpdates200Response>(response);
        }

        /// <summary>
        /// Test ListServiceAlerts
        /// </summary>
        [Fact]
        public void ListServiceAlertsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ListServiceAlertsRequest? listServiceAlertsRequest = null;
            //var response = instance.ListServiceAlerts(listServiceAlertsRequest);
            //Assert.IsType<ListServiceAlerts200Response>(response);
        }

        /// <summary>
        /// Test SetDatabaseUserAcl
        /// </summary>
        [Fact]
        public void SetDatabaseUserAclTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string username = null;
            //SetDatabaseUserAclRequest? setDatabaseUserAclRequest = null;
            //var response = instance.SetDatabaseUserAcl(databaseId, username, setDatabaseUserAclRequest);
            //Assert.IsType<CreateDatabaseUser202Response>(response);
        }

        /// <summary>
        /// Test StartMaintenanceUpdates
        /// </summary>
        [Fact]
        public void StartMaintenanceUpdatesTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.StartMaintenanceUpdates();
            //Assert.IsType<StartMaintenanceUpdates200Response>(response);
        }

        /// <summary>
        /// Test StartVersionUpgrade
        /// </summary>
        [Fact]
        public void StartVersionUpgradeTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //StartVersionUpgradeRequest? startVersionUpgradeRequest = null;
            //var response = instance.StartVersionUpgrade(startVersionUpgradeRequest);
            //Assert.IsType<StartVersionUpgrade200Response>(response);
        }

        /// <summary>
        /// Test UpdateAdvancedOptions
        /// </summary>
        [Fact]
        public void UpdateAdvancedOptionsTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = null;
            //var response = instance.UpdateAdvancedOptions(updateAdvancedOptionsRequest);
            //Assert.IsType<ListAdvancedOptions200Response>(response);
        }

        /// <summary>
        /// Test UpdateConnectionPool
        /// </summary>
        [Fact]
        public void UpdateConnectionPoolTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string poolName = null;
            //UpdateConnectionPoolRequest? updateConnectionPoolRequest = null;
            //var response = instance.UpdateConnectionPool(databaseId, poolName, updateConnectionPoolRequest);
            //Assert.IsType<CreateConnectionPool202Response>(response);
        }

        /// <summary>
        /// Test UpdateDatabase
        /// </summary>
        [Fact]
        public void UpdateDatabaseTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //UpdateDatabaseRequest? updateDatabaseRequest = null;
            //var response = instance.UpdateDatabase(databaseId, updateDatabaseRequest);
            //Assert.IsType<CreateDatabase202Response>(response);
        }

        /// <summary>
        /// Test UpdateDatabaseUser
        /// </summary>
        [Fact]
        public void UpdateDatabaseUserTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string databaseId = null;
            //string username = null;
            //UpdateDatabaseUserRequest? updateDatabaseUserRequest = null;
            //var response = instance.UpdateDatabaseUser(databaseId, username, updateDatabaseUserRequest);
            //Assert.IsType<CreateDatabaseUser202Response>(response);
        }

        /// <summary>
        /// Test ViewMigrationStatus
        /// </summary>
        [Fact]
        public void ViewMigrationStatusTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ViewMigrationStatus();
            //Assert.IsType<ViewMigrationStatus200Response>(response);
        }
    }
}
