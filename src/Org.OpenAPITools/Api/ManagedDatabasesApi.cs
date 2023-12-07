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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IManagedDatabasesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Connection Pool
        /// </summary>
        /// <remarks>
        /// Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        CreateConnectionPool202Response CreateConnectionPool(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Connection Pool
        /// </summary>
        /// <remarks>
        /// Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        ApiResponse<CreateConnectionPool202Response> CreateConnectionPoolWithHttpInfo(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Managed Database
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response CreateDatabase(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Managed Database
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> CreateDatabaseWithHttpInfo(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Logical Database
        /// </summary>
        /// <remarks>
        /// Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseDb202Response</returns>
        CreateDatabaseDb202Response CreateDatabaseDb(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Logical Database
        /// </summary>
        /// <remarks>
        /// Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseDb202Response</returns>
        ApiResponse<CreateDatabaseDb202Response> CreateDatabaseDbWithHttpInfo(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Database User
        /// </summary>
        /// <remarks>
        /// Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        CreateDatabaseUser202Response CreateDatabaseUser(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Database User
        /// </summary>
        /// <remarks>
        /// Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        ApiResponse<CreateDatabaseUser202Response> CreateDatabaseUserWithHttpInfo(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0);
        /// <summary>
        /// Add Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Create a read-only replica node for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response DatabaseAddReadReplica(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0);

        /// <summary>
        /// Add Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Create a read-only replica node for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> DatabaseAddReadReplicaWithHttpInfo(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0);
        /// <summary>
        /// Detach Migration
        /// </summary>
        /// <remarks>
        /// Detach a migration from the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DatabaseDetachMigration(int operationIndex = 0);

        /// <summary>
        /// Detach Migration
        /// </summary>
        /// <remarks>
        /// Detach a migration from the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DatabaseDetachMigrationWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Fork Managed Database
        /// </summary>
        /// <remarks>
        /// Fork a Managed Database to a new subscription from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response DatabaseFork(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0);

        /// <summary>
        /// Fork Managed Database
        /// </summary>
        /// <remarks>
        /// Fork a Managed Database to a new subscription from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> DatabaseForkWithHttpInfo(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0);
        /// <summary>
        /// Promote Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Promote a read-only replica node to its own primary Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DatabasePromoteReadReplica(int operationIndex = 0);

        /// <summary>
        /// Promote Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Promote a read-only replica node to its own primary Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DatabasePromoteReadReplicaWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Restore from Backup
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response DatabaseRestoreFromBackup(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0);

        /// <summary>
        /// Restore from Backup
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> DatabaseRestoreFromBackupWithHttpInfo(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0);
        /// <summary>
        /// Start Migration
        /// </summary>
        /// <remarks>
        /// Start a migration to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ViewMigrationStatus200Response</returns>
        ViewMigrationStatus200Response DatabaseStartMigration(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0);

        /// <summary>
        /// Start Migration
        /// </summary>
        /// <remarks>
        /// Start a migration to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ViewMigrationStatus200Response</returns>
        ApiResponse<ViewMigrationStatus200Response> DatabaseStartMigrationWithHttpInfo(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Connection Pool
        /// </summary>
        /// <remarks>
        /// Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteConnectionPool(string databaseId, string poolName, int operationIndex = 0);

        /// <summary>
        /// Delete Connection Pool
        /// </summary>
        /// <remarks>
        /// Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteConnectionPoolWithHttpInfo(string databaseId, string poolName, int operationIndex = 0);
        /// <summary>
        /// Delete Managed Database
        /// </summary>
        /// <remarks>
        /// Delete a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteDatabase(string databaseId, int operationIndex = 0);

        /// <summary>
        /// Delete Managed Database
        /// </summary>
        /// <remarks>
        /// Delete a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteDatabaseWithHttpInfo(string databaseId, int operationIndex = 0);
        /// <summary>
        /// Delete Logical Database
        /// </summary>
        /// <remarks>
        /// Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteDatabaseDb(string databaseId, string dbName, int operationIndex = 0);

        /// <summary>
        /// Delete Logical Database
        /// </summary>
        /// <remarks>
        /// Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteDatabaseDbWithHttpInfo(string databaseId, string dbName, int operationIndex = 0);
        /// <summary>
        /// Delete Database User
        /// </summary>
        /// <remarks>
        /// Delete a database user within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteDatabaseUser(string databaseId, string username, int operationIndex = 0);

        /// <summary>
        /// Delete Database User
        /// </summary>
        /// <remarks>
        /// Delete a database user within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteDatabaseUserWithHttpInfo(string databaseId, string username, int operationIndex = 0);
        /// <summary>
        /// Get Backup Information
        /// </summary>
        /// <remarks>
        /// Get backup information for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBackupInformation200Response</returns>
        GetBackupInformation200Response GetBackupInformation(int operationIndex = 0);

        /// <summary>
        /// Get Backup Information
        /// </summary>
        /// <remarks>
        /// Get backup information for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBackupInformation200Response</returns>
        ApiResponse<GetBackupInformation200Response> GetBackupInformationWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Get Connection Pool
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        CreateConnectionPool202Response GetConnectionPool(string databaseId, string poolName, int operationIndex = 0);

        /// <summary>
        /// Get Connection Pool
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        ApiResponse<CreateConnectionPool202Response> GetConnectionPoolWithHttpInfo(string databaseId, string poolName, int operationIndex = 0);
        /// <summary>
        /// Get Managed Database
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response GetDatabase(string databaseId, int operationIndex = 0);

        /// <summary>
        /// Get Managed Database
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> GetDatabaseWithHttpInfo(string databaseId, int operationIndex = 0);
        /// <summary>
        /// Get Logical Database
        /// </summary>
        /// <remarks>
        /// Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseDb202Response</returns>
        CreateDatabaseDb202Response GetDatabaseDb(string databaseId, string dbName, int operationIndex = 0);

        /// <summary>
        /// Get Logical Database
        /// </summary>
        /// <remarks>
        /// Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseDb202Response</returns>
        ApiResponse<CreateDatabaseDb202Response> GetDatabaseDbWithHttpInfo(string databaseId, string dbName, int operationIndex = 0);
        /// <summary>
        /// Get Database Usage Information
        /// </summary>
        /// <remarks>
        /// Get disk, memory, and vCPU usage information for a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDatabaseUsage200Response</returns>
        GetDatabaseUsage200Response GetDatabaseUsage(int operationIndex = 0);

        /// <summary>
        /// Get Database Usage Information
        /// </summary>
        /// <remarks>
        /// Get disk, memory, and vCPU usage information for a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDatabaseUsage200Response</returns>
        ApiResponse<GetDatabaseUsage200Response> GetDatabaseUsageWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Get Database User
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database user.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        CreateDatabaseUser202Response GetDatabaseUser(string databaseId, string username, int operationIndex = 0);

        /// <summary>
        /// Get Database User
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database user.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        ApiResponse<CreateDatabaseUser202Response> GetDatabaseUserWithHttpInfo(string databaseId, string username, int operationIndex = 0);
        /// <summary>
        /// List Advanced Options
        /// </summary>
        /// <remarks>
        /// List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAdvancedOptions200Response</returns>
        ListAdvancedOptions200Response ListAdvancedOptions(int operationIndex = 0);

        /// <summary>
        /// List Advanced Options
        /// </summary>
        /// <remarks>
        /// List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAdvancedOptions200Response</returns>
        ApiResponse<ListAdvancedOptions200Response> ListAdvancedOptionsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Available Versions
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAvailableVersions200Response</returns>
        ListAvailableVersions200Response ListAvailableVersions(int operationIndex = 0);

        /// <summary>
        /// List Available Versions
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAvailableVersions200Response</returns>
        ApiResponse<ListAvailableVersions200Response> ListAvailableVersionsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Connection Pools
        /// </summary>
        /// <remarks>
        /// List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListConnectionPools200Response</returns>
        ListConnectionPools200Response ListConnectionPools(int operationIndex = 0);

        /// <summary>
        /// List Connection Pools
        /// </summary>
        /// <remarks>
        /// List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListConnectionPools200Response</returns>
        ApiResponse<ListConnectionPools200Response> ListConnectionPoolsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Logical Databases
        /// </summary>
        /// <remarks>
        /// List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabaseDbs200Response</returns>
        ListDatabaseDbs200Response ListDatabaseDbs(int operationIndex = 0);

        /// <summary>
        /// List Logical Databases
        /// </summary>
        /// <remarks>
        /// List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabaseDbs200Response</returns>
        ApiResponse<ListDatabaseDbs200Response> ListDatabaseDbsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Managed Database Plans
        /// </summary>
        /// <remarks>
        /// List all Managed Databases plans.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabasePlans200Response</returns>
        ListDatabasePlans200Response ListDatabasePlans(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Managed Database Plans
        /// </summary>
        /// <remarks>
        /// List all Managed Databases plans.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabasePlans200Response</returns>
        ApiResponse<ListDatabasePlans200Response> ListDatabasePlansWithHttpInfo(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0);
        /// <summary>
        /// List Database Users
        /// </summary>
        /// <remarks>
        /// List all database users within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabaseUsers200Response</returns>
        ListDatabaseUsers200Response ListDatabaseUsers(int operationIndex = 0);

        /// <summary>
        /// List Database Users
        /// </summary>
        /// <remarks>
        /// List all database users within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabaseUsers200Response</returns>
        ApiResponse<ListDatabaseUsers200Response> ListDatabaseUsersWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Managed Databases
        /// </summary>
        /// <remarks>
        /// List all Managed Databases in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabases200Response</returns>
        ListDatabases200Response ListDatabases(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Managed Databases
        /// </summary>
        /// <remarks>
        /// List all Managed Databases in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabases200Response</returns>
        ApiResponse<ListDatabases200Response> ListDatabasesWithHttpInfo(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0);
        /// <summary>
        /// List Maintenance Updates
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListMaintenanceUpdates200Response</returns>
        ListMaintenanceUpdates200Response ListMaintenanceUpdates(int operationIndex = 0);

        /// <summary>
        /// List Maintenance Updates
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListMaintenanceUpdates200Response</returns>
        ApiResponse<ListMaintenanceUpdates200Response> ListMaintenanceUpdatesWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Service Alerts
        /// </summary>
        /// <remarks>
        /// List service alert messages for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListServiceAlerts200Response</returns>
        ListServiceAlerts200Response ListServiceAlerts(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0);

        /// <summary>
        /// List Service Alerts
        /// </summary>
        /// <remarks>
        /// List service alert messages for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListServiceAlerts200Response</returns>
        ApiResponse<ListServiceAlerts200Response> ListServiceAlertsWithHttpInfo(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0);
        /// <summary>
        /// Set Database User Access Control
        /// </summary>
        /// <remarks>
        /// Configure access control settings for a Managed Database user (Redis engine type only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        CreateDatabaseUser202Response SetDatabaseUserAcl(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0);

        /// <summary>
        /// Set Database User Access Control
        /// </summary>
        /// <remarks>
        /// Configure access control settings for a Managed Database user (Redis engine type only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        ApiResponse<CreateDatabaseUser202Response> SetDatabaseUserAclWithHttpInfo(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0);
        /// <summary>
        /// Start Maintenance Updates
        /// </summary>
        /// <remarks>
        /// Start maintenance updates for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>StartMaintenanceUpdates200Response</returns>
        StartMaintenanceUpdates200Response StartMaintenanceUpdates(int operationIndex = 0);

        /// <summary>
        /// Start Maintenance Updates
        /// </summary>
        /// <remarks>
        /// Start maintenance updates for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of StartMaintenanceUpdates200Response</returns>
        ApiResponse<StartMaintenanceUpdates200Response> StartMaintenanceUpdatesWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Start Version Upgrade
        /// </summary>
        /// <remarks>
        /// Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>StartVersionUpgrade200Response</returns>
        StartVersionUpgrade200Response StartVersionUpgrade(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0);

        /// <summary>
        /// Start Version Upgrade
        /// </summary>
        /// <remarks>
        /// Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of StartVersionUpgrade200Response</returns>
        ApiResponse<StartVersionUpgrade200Response> StartVersionUpgradeWithHttpInfo(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Advanced Options
        /// </summary>
        /// <remarks>
        /// Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAdvancedOptions200Response</returns>
        ListAdvancedOptions200Response UpdateAdvancedOptions(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Advanced Options
        /// </summary>
        /// <remarks>
        /// Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAdvancedOptions200Response</returns>
        ApiResponse<ListAdvancedOptions200Response> UpdateAdvancedOptionsWithHttpInfo(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Connection Pool
        /// </summary>
        /// <remarks>
        /// Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        CreateConnectionPool202Response UpdateConnectionPool(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Connection Pool
        /// </summary>
        /// <remarks>
        /// Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        ApiResponse<CreateConnectionPool202Response> UpdateConnectionPoolWithHttpInfo(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Managed Database
        /// </summary>
        /// <remarks>
        /// Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        CreateDatabase202Response UpdateDatabase(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Managed Database
        /// </summary>
        /// <remarks>
        /// Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        ApiResponse<CreateDatabase202Response> UpdateDatabaseWithHttpInfo(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Database User
        /// </summary>
        /// <remarks>
        /// Update database user information within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        CreateDatabaseUser202Response UpdateDatabaseUser(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Database User
        /// </summary>
        /// <remarks>
        /// Update database user information within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        ApiResponse<CreateDatabaseUser202Response> UpdateDatabaseUserWithHttpInfo(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0);
        /// <summary>
        /// Get Migration Status
        /// </summary>
        /// <remarks>
        /// View the status of a migration attached to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ViewMigrationStatus200Response</returns>
        ViewMigrationStatus200Response ViewMigrationStatus(int operationIndex = 0);

        /// <summary>
        /// Get Migration Status
        /// </summary>
        /// <remarks>
        /// View the status of a migration attached to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ViewMigrationStatus200Response</returns>
        ApiResponse<ViewMigrationStatus200Response> ViewMigrationStatusWithHttpInfo(int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IManagedDatabasesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Connection Pool
        /// </summary>
        /// <remarks>
        /// Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        System.Threading.Tasks.Task<CreateConnectionPool202Response> CreateConnectionPoolAsync(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Connection Pool
        /// </summary>
        /// <remarks>
        /// Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateConnectionPool202Response>> CreateConnectionPoolWithHttpInfoAsync(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Managed Database
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> CreateDatabaseAsync(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Managed Database
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> CreateDatabaseWithHttpInfoAsync(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Logical Database
        /// </summary>
        /// <remarks>
        /// Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseDb202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseDb202Response> CreateDatabaseDbAsync(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Logical Database
        /// </summary>
        /// <remarks>
        /// Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseDb202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseDb202Response>> CreateDatabaseDbWithHttpInfoAsync(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Database User
        /// </summary>
        /// <remarks>
        /// Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseUser202Response> CreateDatabaseUserAsync(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Database User
        /// </summary>
        /// <remarks>
        /// Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseUser202Response>> CreateDatabaseUserWithHttpInfoAsync(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Add Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Create a read-only replica node for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseAddReadReplicaAsync(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Add Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Create a read-only replica node for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> DatabaseAddReadReplicaWithHttpInfoAsync(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach Migration
        /// </summary>
        /// <remarks>
        /// Detach a migration from the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DatabaseDetachMigrationAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach Migration
        /// </summary>
        /// <remarks>
        /// Detach a migration from the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DatabaseDetachMigrationWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Fork Managed Database
        /// </summary>
        /// <remarks>
        /// Fork a Managed Database to a new subscription from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseForkAsync(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Fork Managed Database
        /// </summary>
        /// <remarks>
        /// Fork a Managed Database to a new subscription from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> DatabaseForkWithHttpInfoAsync(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Promote Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Promote a read-only replica node to its own primary Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DatabasePromoteReadReplicaAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Promote Read-Only Replica
        /// </summary>
        /// <remarks>
        /// Promote a read-only replica node to its own primary Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DatabasePromoteReadReplicaWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Restore from Backup
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseRestoreFromBackupAsync(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Restore from Backup
        /// </summary>
        /// <remarks>
        /// Create a new Managed Database from a backup.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> DatabaseRestoreFromBackupWithHttpInfoAsync(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Migration
        /// </summary>
        /// <remarks>
        /// Start a migration to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ViewMigrationStatus200Response</returns>
        System.Threading.Tasks.Task<ViewMigrationStatus200Response> DatabaseStartMigrationAsync(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Migration
        /// </summary>
        /// <remarks>
        /// Start a migration to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ViewMigrationStatus200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ViewMigrationStatus200Response>> DatabaseStartMigrationWithHttpInfoAsync(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Connection Pool
        /// </summary>
        /// <remarks>
        /// Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteConnectionPoolAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Connection Pool
        /// </summary>
        /// <remarks>
        /// Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Managed Database
        /// </summary>
        /// <remarks>
        /// Delete a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteDatabaseAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Managed Database
        /// </summary>
        /// <remarks>
        /// Delete a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDatabaseWithHttpInfoAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Logical Database
        /// </summary>
        /// <remarks>
        /// Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteDatabaseDbAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Logical Database
        /// </summary>
        /// <remarks>
        /// Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDatabaseDbWithHttpInfoAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Database User
        /// </summary>
        /// <remarks>
        /// Delete a database user within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteDatabaseUserAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Database User
        /// </summary>
        /// <remarks>
        /// Delete a database user within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDatabaseUserWithHttpInfoAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Backup Information
        /// </summary>
        /// <remarks>
        /// Get backup information for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBackupInformation200Response</returns>
        System.Threading.Tasks.Task<GetBackupInformation200Response> GetBackupInformationAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Backup Information
        /// </summary>
        /// <remarks>
        /// Get backup information for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBackupInformation200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBackupInformation200Response>> GetBackupInformationWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Connection Pool
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        System.Threading.Tasks.Task<CreateConnectionPool202Response> GetConnectionPoolAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Connection Pool
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateConnectionPool202Response>> GetConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Managed Database
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> GetDatabaseAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Managed Database
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> GetDatabaseWithHttpInfoAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Logical Database
        /// </summary>
        /// <remarks>
        /// Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseDb202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseDb202Response> GetDatabaseDbAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Logical Database
        /// </summary>
        /// <remarks>
        /// Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseDb202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseDb202Response>> GetDatabaseDbWithHttpInfoAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Database Usage Information
        /// </summary>
        /// <remarks>
        /// Get disk, memory, and vCPU usage information for a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDatabaseUsage200Response</returns>
        System.Threading.Tasks.Task<GetDatabaseUsage200Response> GetDatabaseUsageAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Database Usage Information
        /// </summary>
        /// <remarks>
        /// Get disk, memory, and vCPU usage information for a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDatabaseUsage200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetDatabaseUsage200Response>> GetDatabaseUsageWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Database User
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database user.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseUser202Response> GetDatabaseUserAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Database User
        /// </summary>
        /// <remarks>
        /// Get information about a Managed Database user.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseUser202Response>> GetDatabaseUserWithHttpInfoAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Advanced Options
        /// </summary>
        /// <remarks>
        /// List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAdvancedOptions200Response</returns>
        System.Threading.Tasks.Task<ListAdvancedOptions200Response> ListAdvancedOptionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Advanced Options
        /// </summary>
        /// <remarks>
        /// List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAdvancedOptions200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListAdvancedOptions200Response>> ListAdvancedOptionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Available Versions
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAvailableVersions200Response</returns>
        System.Threading.Tasks.Task<ListAvailableVersions200Response> ListAvailableVersionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Available Versions
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAvailableVersions200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListAvailableVersions200Response>> ListAvailableVersionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Connection Pools
        /// </summary>
        /// <remarks>
        /// List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListConnectionPools200Response</returns>
        System.Threading.Tasks.Task<ListConnectionPools200Response> ListConnectionPoolsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Connection Pools
        /// </summary>
        /// <remarks>
        /// List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListConnectionPools200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListConnectionPools200Response>> ListConnectionPoolsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Logical Databases
        /// </summary>
        /// <remarks>
        /// List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabaseDbs200Response</returns>
        System.Threading.Tasks.Task<ListDatabaseDbs200Response> ListDatabaseDbsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Logical Databases
        /// </summary>
        /// <remarks>
        /// List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabaseDbs200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDatabaseDbs200Response>> ListDatabaseDbsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Managed Database Plans
        /// </summary>
        /// <remarks>
        /// List all Managed Databases plans.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabasePlans200Response</returns>
        System.Threading.Tasks.Task<ListDatabasePlans200Response> ListDatabasePlansAsync(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Managed Database Plans
        /// </summary>
        /// <remarks>
        /// List all Managed Databases plans.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabasePlans200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDatabasePlans200Response>> ListDatabasePlansWithHttpInfoAsync(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Database Users
        /// </summary>
        /// <remarks>
        /// List all database users within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabaseUsers200Response</returns>
        System.Threading.Tasks.Task<ListDatabaseUsers200Response> ListDatabaseUsersAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Database Users
        /// </summary>
        /// <remarks>
        /// List all database users within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabaseUsers200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDatabaseUsers200Response>> ListDatabaseUsersWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Managed Databases
        /// </summary>
        /// <remarks>
        /// List all Managed Databases in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabases200Response</returns>
        System.Threading.Tasks.Task<ListDatabases200Response> ListDatabasesAsync(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Managed Databases
        /// </summary>
        /// <remarks>
        /// List all Managed Databases in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabases200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDatabases200Response>> ListDatabasesWithHttpInfoAsync(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Maintenance Updates
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListMaintenanceUpdates200Response</returns>
        System.Threading.Tasks.Task<ListMaintenanceUpdates200Response> ListMaintenanceUpdatesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Maintenance Updates
        /// </summary>
        /// <remarks>
        /// List all available version upgrades within the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListMaintenanceUpdates200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListMaintenanceUpdates200Response>> ListMaintenanceUpdatesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Service Alerts
        /// </summary>
        /// <remarks>
        /// List service alert messages for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListServiceAlerts200Response</returns>
        System.Threading.Tasks.Task<ListServiceAlerts200Response> ListServiceAlertsAsync(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Service Alerts
        /// </summary>
        /// <remarks>
        /// List service alert messages for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListServiceAlerts200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListServiceAlerts200Response>> ListServiceAlertsWithHttpInfoAsync(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Set Database User Access Control
        /// </summary>
        /// <remarks>
        /// Configure access control settings for a Managed Database user (Redis engine type only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseUser202Response> SetDatabaseUserAclAsync(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Set Database User Access Control
        /// </summary>
        /// <remarks>
        /// Configure access control settings for a Managed Database user (Redis engine type only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseUser202Response>> SetDatabaseUserAclWithHttpInfoAsync(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Maintenance Updates
        /// </summary>
        /// <remarks>
        /// Start maintenance updates for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StartMaintenanceUpdates200Response</returns>
        System.Threading.Tasks.Task<StartMaintenanceUpdates200Response> StartMaintenanceUpdatesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Maintenance Updates
        /// </summary>
        /// <remarks>
        /// Start maintenance updates for the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StartMaintenanceUpdates200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<StartMaintenanceUpdates200Response>> StartMaintenanceUpdatesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Version Upgrade
        /// </summary>
        /// <remarks>
        /// Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StartVersionUpgrade200Response</returns>
        System.Threading.Tasks.Task<StartVersionUpgrade200Response> StartVersionUpgradeAsync(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Version Upgrade
        /// </summary>
        /// <remarks>
        /// Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StartVersionUpgrade200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<StartVersionUpgrade200Response>> StartVersionUpgradeWithHttpInfoAsync(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Advanced Options
        /// </summary>
        /// <remarks>
        /// Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAdvancedOptions200Response</returns>
        System.Threading.Tasks.Task<ListAdvancedOptions200Response> UpdateAdvancedOptionsAsync(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Advanced Options
        /// </summary>
        /// <remarks>
        /// Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAdvancedOptions200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListAdvancedOptions200Response>> UpdateAdvancedOptionsWithHttpInfoAsync(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Connection Pool
        /// </summary>
        /// <remarks>
        /// Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        System.Threading.Tasks.Task<CreateConnectionPool202Response> UpdateConnectionPoolAsync(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Connection Pool
        /// </summary>
        /// <remarks>
        /// Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateConnectionPool202Response>> UpdateConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Managed Database
        /// </summary>
        /// <remarks>
        /// Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        System.Threading.Tasks.Task<CreateDatabase202Response> UpdateDatabaseAsync(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Managed Database
        /// </summary>
        /// <remarks>
        /// Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabase202Response>> UpdateDatabaseWithHttpInfoAsync(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Database User
        /// </summary>
        /// <remarks>
        /// Update database user information within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        System.Threading.Tasks.Task<CreateDatabaseUser202Response> UpdateDatabaseUserAsync(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Database User
        /// </summary>
        /// <remarks>
        /// Update database user information within a Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDatabaseUser202Response>> UpdateDatabaseUserWithHttpInfoAsync(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Migration Status
        /// </summary>
        /// <remarks>
        /// View the status of a migration attached to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ViewMigrationStatus200Response</returns>
        System.Threading.Tasks.Task<ViewMigrationStatus200Response> ViewMigrationStatusAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Migration Status
        /// </summary>
        /// <remarks>
        /// View the status of a migration attached to the Managed Database.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ViewMigrationStatus200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ViewMigrationStatus200Response>> ViewMigrationStatusWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IManagedDatabasesApi : IManagedDatabasesApiSync, IManagedDatabasesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ManagedDatabasesApi : IManagedDatabasesApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedDatabasesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ManagedDatabasesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedDatabasesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ManagedDatabasesApi(string basePath)
        {
            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                new Org.OpenAPITools.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedDatabasesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public ManagedDatabasesApi(Org.OpenAPITools.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedDatabasesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public ManagedDatabasesApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Org.OpenAPITools.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Org.OpenAPITools.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Org.OpenAPITools.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Org.OpenAPITools.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Create Connection Pool Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        public CreateConnectionPool202Response CreateConnectionPool(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = CreateConnectionPoolWithHttpInfo(createConnectionPoolRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Connection Pool Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> CreateConnectionPoolWithHttpInfo(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createConnectionPoolRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Connection Pool Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        public async System.Threading.Tasks.Task<CreateConnectionPool202Response> CreateConnectionPoolAsync(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = await CreateConnectionPoolWithHttpInfoAsync(createConnectionPoolRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Connection Pool Create a new connection pool within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response>> CreateConnectionPoolWithHttpInfoAsync(CreateConnectionPoolRequest? createConnectionPoolRequest = default(CreateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createConnectionPoolRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Managed Database Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response CreateDatabase(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = CreateDatabaseWithHttpInfo(createDatabaseRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Managed Database Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> CreateDatabaseWithHttpInfo(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabase202Response>("/databases", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Managed Database Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> CreateDatabaseAsync(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await CreateDatabaseWithHttpInfoAsync(createDatabaseRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Managed Database Create a new Managed Database in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> CreateDatabaseWithHttpInfoAsync(CreateDatabaseRequest? createDatabaseRequest = default(CreateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabase202Response>("/databases", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Logical Database Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseDb202Response</returns>
        public CreateDatabaseDb202Response CreateDatabaseDb(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> localVarResponse = CreateDatabaseDbWithHttpInfo(createDatabaseDbRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Logical Database Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseDb202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> CreateDatabaseDbWithHttpInfo(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseDbRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabaseDb202Response>("/databases/{database-id}/dbs", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Logical Database Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseDb202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseDb202Response> CreateDatabaseDbAsync(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> localVarResponse = await CreateDatabaseDbWithHttpInfoAsync(createDatabaseDbRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Logical Database Create a new logical database within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseDbRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseDb202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response>> CreateDatabaseDbWithHttpInfoAsync(CreateDatabaseDbRequest? createDatabaseDbRequest = default(CreateDatabaseDbRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseDbRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabaseDb202Response>("/databases/{database-id}/dbs", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Database User Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        public CreateDatabaseUser202Response CreateDatabaseUser(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = CreateDatabaseUserWithHttpInfo(createDatabaseUserRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Database User Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> CreateDatabaseUserWithHttpInfo(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseUserRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabaseUser202Response>("/databases/{database-id}/users", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Database User Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseUser202Response> CreateDatabaseUserAsync(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = await CreateDatabaseUserWithHttpInfoAsync(createDatabaseUserRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Database User Create a new database user within the Managed Database. Supply optional attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response>> CreateDatabaseUserWithHttpInfoAsync(CreateDatabaseUserRequest? createDatabaseUserRequest = default(CreateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createDatabaseUserRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.CreateDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabaseUser202Response>("/databases/{database-id}/users", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Add Read-Only Replica Create a read-only replica node for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response DatabaseAddReadReplica(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = DatabaseAddReadReplicaWithHttpInfo(databaseAddReadReplicaRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Add Read-Only Replica Create a read-only replica node for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> DatabaseAddReadReplicaWithHttpInfo(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseAddReadReplicaRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseAddReadReplica";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabase202Response>("/databases/{database-id}/read-replica", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseAddReadReplica", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Add Read-Only Replica Create a read-only replica node for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseAddReadReplicaAsync(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await DatabaseAddReadReplicaWithHttpInfoAsync(databaseAddReadReplicaRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Add Read-Only Replica Create a read-only replica node for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseAddReadReplicaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> DatabaseAddReadReplicaWithHttpInfoAsync(DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = default(DatabaseAddReadReplicaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseAddReadReplicaRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseAddReadReplica";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabase202Response>("/databases/{database-id}/read-replica", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseAddReadReplica", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Migration Detach a migration from the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DatabaseDetachMigration(int operationIndex = 0)
        {
            DatabaseDetachMigrationWithHttpInfo();
        }

        /// <summary>
        /// Detach Migration Detach a migration from the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DatabaseDetachMigrationWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseDetachMigration";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseDetachMigration", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Migration Detach a migration from the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DatabaseDetachMigrationAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DatabaseDetachMigrationWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach Migration Detach a migration from the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DatabaseDetachMigrationWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseDetachMigration";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseDetachMigration", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Fork Managed Database Fork a Managed Database to a new subscription from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response DatabaseFork(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = DatabaseForkWithHttpInfo(databaseForkRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Fork Managed Database Fork a Managed Database to a new subscription from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> DatabaseForkWithHttpInfo(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseForkRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseFork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabase202Response>("/databases/{database-id}/fork", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseFork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Fork Managed Database Fork a Managed Database to a new subscription from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseForkAsync(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await DatabaseForkWithHttpInfoAsync(databaseForkRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Fork Managed Database Fork a Managed Database to a new subscription from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseForkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> DatabaseForkWithHttpInfoAsync(DatabaseForkRequest? databaseForkRequest = default(DatabaseForkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseForkRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseFork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabase202Response>("/databases/{database-id}/fork", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseFork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Promote Read-Only Replica Promote a read-only replica node to its own primary Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DatabasePromoteReadReplica(int operationIndex = 0)
        {
            DatabasePromoteReadReplicaWithHttpInfo();
        }

        /// <summary>
        /// Promote Read-Only Replica Promote a read-only replica node to its own primary Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DatabasePromoteReadReplicaWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabasePromoteReadReplica";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/databases/{database-id}/promote-read-replica", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabasePromoteReadReplica", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Promote Read-Only Replica Promote a read-only replica node to its own primary Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DatabasePromoteReadReplicaAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DatabasePromoteReadReplicaWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Promote Read-Only Replica Promote a read-only replica node to its own primary Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DatabasePromoteReadReplicaWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabasePromoteReadReplica";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/databases/{database-id}/promote-read-replica", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabasePromoteReadReplica", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Restore from Backup Create a new Managed Database from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response DatabaseRestoreFromBackup(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = DatabaseRestoreFromBackupWithHttpInfo(databaseRestoreFromBackupRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Restore from Backup Create a new Managed Database from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> DatabaseRestoreFromBackupWithHttpInfo(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseRestoreFromBackupRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseRestoreFromBackup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDatabase202Response>("/databases/{database-id}/restore", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseRestoreFromBackup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Restore from Backup Create a new Managed Database from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> DatabaseRestoreFromBackupAsync(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await DatabaseRestoreFromBackupWithHttpInfoAsync(databaseRestoreFromBackupRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Restore from Backup Create a new Managed Database from a backup.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseRestoreFromBackupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> DatabaseRestoreFromBackupWithHttpInfoAsync(DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = default(DatabaseRestoreFromBackupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseRestoreFromBackupRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseRestoreFromBackup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDatabase202Response>("/databases/{database-id}/restore", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseRestoreFromBackup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Migration Start a migration to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ViewMigrationStatus200Response</returns>
        public ViewMigrationStatus200Response DatabaseStartMigration(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> localVarResponse = DatabaseStartMigrationWithHttpInfo(databaseStartMigrationRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Migration Start a migration to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ViewMigrationStatus200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> DatabaseStartMigrationWithHttpInfo(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseStartMigrationRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseStartMigration";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<ViewMigrationStatus200Response>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseStartMigration", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Migration Start a migration to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ViewMigrationStatus200Response</returns>
        public async System.Threading.Tasks.Task<ViewMigrationStatus200Response> DatabaseStartMigrationAsync(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> localVarResponse = await DatabaseStartMigrationWithHttpInfoAsync(databaseStartMigrationRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Migration Start a migration to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseStartMigrationRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ViewMigrationStatus200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response>> DatabaseStartMigrationWithHttpInfoAsync(DatabaseStartMigrationRequest? databaseStartMigrationRequest = default(DatabaseStartMigrationRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = databaseStartMigrationRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DatabaseStartMigration";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<ViewMigrationStatus200Response>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DatabaseStartMigration", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Connection Pool Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteConnectionPool(string databaseId, string poolName, int operationIndex = 0)
        {
            DeleteConnectionPoolWithHttpInfo(databaseId, poolName);
        }

        /// <summary>
        /// Delete Connection Pool Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteConnectionPoolWithHttpInfo(string databaseId, string poolName, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->DeleteConnectionPool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Connection Pool Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteConnectionPoolAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteConnectionPoolWithHttpInfoAsync(databaseId, poolName, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Connection Pool Delete a connection pool within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->DeleteConnectionPool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Managed Database Delete a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteDatabase(string databaseId, int operationIndex = 0)
        {
            DeleteDatabaseWithHttpInfo(databaseId);
        }

        /// <summary>
        /// Delete Managed Database Delete a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteDatabaseWithHttpInfo(string databaseId, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabase");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/databases/{database-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Managed Database Delete a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteDatabaseAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteDatabaseWithHttpInfoAsync(databaseId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Managed Database Delete a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteDatabaseWithHttpInfoAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabase");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/databases/{database-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Logical Database Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteDatabaseDb(string databaseId, string dbName, int operationIndex = 0)
        {
            DeleteDatabaseDbWithHttpInfo(databaseId, dbName);
        }

        /// <summary>
        /// Delete Logical Database Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteDatabaseDbWithHttpInfo(string databaseId, string dbName, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabaseDb");
            }

            // verify the required parameter 'dbName' is set
            if (dbName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dbName' when calling ManagedDatabasesApi->DeleteDatabaseDb");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("db-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dbName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/databases/{database-id}/dbs/{db-name}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Logical Database Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteDatabaseDbAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteDatabaseDbWithHttpInfoAsync(databaseId, dbName, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Logical Database Delete a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteDatabaseDbWithHttpInfoAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabaseDb");
            }

            // verify the required parameter 'dbName' is set
            if (dbName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dbName' when calling ManagedDatabasesApi->DeleteDatabaseDb");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("db-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dbName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/databases/{database-id}/dbs/{db-name}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Database User Delete a database user within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteDatabaseUser(string databaseId, string username, int operationIndex = 0)
        {
            DeleteDatabaseUserWithHttpInfo(databaseId, username);
        }

        /// <summary>
        /// Delete Database User Delete a database user within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteDatabaseUserWithHttpInfo(string databaseId, string username, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->DeleteDatabaseUser");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Database User Delete a database user within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteDatabaseUserAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteDatabaseUserWithHttpInfoAsync(databaseId, username, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Database User Delete a database user within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteDatabaseUserWithHttpInfoAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->DeleteDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->DeleteDatabaseUser");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.DeleteDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Backup Information Get backup information for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBackupInformation200Response</returns>
        public GetBackupInformation200Response GetBackupInformation(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBackupInformation200Response> localVarResponse = GetBackupInformationWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Backup Information Get backup information for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBackupInformation200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBackupInformation200Response> GetBackupInformationWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetBackupInformation";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBackupInformation200Response>("/databases/{database-id}/backups", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBackupInformation", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Backup Information Get backup information for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBackupInformation200Response</returns>
        public async System.Threading.Tasks.Task<GetBackupInformation200Response> GetBackupInformationAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBackupInformation200Response> localVarResponse = await GetBackupInformationWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Backup Information Get backup information for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBackupInformation200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBackupInformation200Response>> GetBackupInformationWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetBackupInformation";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBackupInformation200Response>("/databases/{database-id}/backups", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBackupInformation", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Connection Pool Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        public CreateConnectionPool202Response GetConnectionPool(string databaseId, string poolName, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = GetConnectionPoolWithHttpInfo(databaseId, poolName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Connection Pool Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> GetConnectionPoolWithHttpInfo(string databaseId, string poolName, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->GetConnectionPool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Connection Pool Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        public async System.Threading.Tasks.Task<CreateConnectionPool202Response> GetConnectionPoolAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = await GetConnectionPoolWithHttpInfoAsync(databaseId, poolName, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Connection Pool Get information about a Managed Database connection pool (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response>> GetConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->GetConnectionPool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Managed Database Get information about a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response GetDatabase(string databaseId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = GetDatabaseWithHttpInfo(databaseId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Managed Database Get information about a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> GetDatabaseWithHttpInfo(string databaseId, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabase");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateDatabase202Response>("/databases/{database-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Managed Database Get information about a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> GetDatabaseAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await GetDatabaseWithHttpInfoAsync(databaseId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Managed Database Get information about a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> GetDatabaseWithHttpInfoAsync(string databaseId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabase");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateDatabase202Response>("/databases/{database-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Logical Database Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseDb202Response</returns>
        public CreateDatabaseDb202Response GetDatabaseDb(string databaseId, string dbName, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> localVarResponse = GetDatabaseDbWithHttpInfo(databaseId, dbName);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Logical Database Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseDb202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> GetDatabaseDbWithHttpInfo(string databaseId, string dbName, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabaseDb");
            }

            // verify the required parameter 'dbName' is set
            if (dbName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dbName' when calling ManagedDatabasesApi->GetDatabaseDb");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("db-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dbName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateDatabaseDb202Response>("/databases/{database-id}/dbs/{db-name}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Logical Database Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseDb202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseDb202Response> GetDatabaseDbAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response> localVarResponse = await GetDatabaseDbWithHttpInfoAsync(databaseId, dbName, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Logical Database Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="dbName">The [logical database name](#operation/list-database-dbs).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseDb202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseDb202Response>> GetDatabaseDbWithHttpInfoAsync(string databaseId, string dbName, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabaseDb");
            }

            // verify the required parameter 'dbName' is set
            if (dbName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dbName' when calling ManagedDatabasesApi->GetDatabaseDb");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("db-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dbName)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseDb";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateDatabaseDb202Response>("/databases/{database-id}/dbs/{db-name}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseDb", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Database Usage Information Get disk, memory, and vCPU usage information for a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDatabaseUsage200Response</returns>
        public GetDatabaseUsage200Response GetDatabaseUsage(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetDatabaseUsage200Response> localVarResponse = GetDatabaseUsageWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Database Usage Information Get disk, memory, and vCPU usage information for a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDatabaseUsage200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetDatabaseUsage200Response> GetDatabaseUsageWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseUsage";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetDatabaseUsage200Response>("/databases/{database-id}/usage", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseUsage", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Database Usage Information Get disk, memory, and vCPU usage information for a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDatabaseUsage200Response</returns>
        public async System.Threading.Tasks.Task<GetDatabaseUsage200Response> GetDatabaseUsageAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetDatabaseUsage200Response> localVarResponse = await GetDatabaseUsageWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Database Usage Information Get disk, memory, and vCPU usage information for a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDatabaseUsage200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetDatabaseUsage200Response>> GetDatabaseUsageWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseUsage";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetDatabaseUsage200Response>("/databases/{database-id}/usage", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseUsage", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Database User Get information about a Managed Database user.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        public CreateDatabaseUser202Response GetDatabaseUser(string databaseId, string username, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = GetDatabaseUserWithHttpInfo(databaseId, username);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Database User Get information about a Managed Database user.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> GetDatabaseUserWithHttpInfo(string databaseId, string username, int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->GetDatabaseUser");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Database User Get information about a Managed Database user.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseUser202Response> GetDatabaseUserAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = await GetDatabaseUserWithHttpInfoAsync(databaseId, username, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Database User Get information about a Managed Database user.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response>> GetDatabaseUserWithHttpInfoAsync(string databaseId, string username, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->GetDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->GetDatabaseUser");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter

            localVarRequestOptions.Operation = "ManagedDatabasesApi.GetDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Advanced Options List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAdvancedOptions200Response</returns>
        public ListAdvancedOptions200Response ListAdvancedOptions(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> localVarResponse = ListAdvancedOptionsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Advanced Options List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAdvancedOptions200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> ListAdvancedOptionsWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListAdvancedOptions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListAdvancedOptions200Response>("/databases/{database-id}/advanced-options", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAdvancedOptions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Advanced Options List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAdvancedOptions200Response</returns>
        public async System.Threading.Tasks.Task<ListAdvancedOptions200Response> ListAdvancedOptionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> localVarResponse = await ListAdvancedOptionsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Advanced Options List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAdvancedOptions200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response>> ListAdvancedOptionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListAdvancedOptions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListAdvancedOptions200Response>("/databases/{database-id}/advanced-options", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAdvancedOptions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Available Versions List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAvailableVersions200Response</returns>
        public ListAvailableVersions200Response ListAvailableVersions(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListAvailableVersions200Response> localVarResponse = ListAvailableVersionsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Available Versions List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAvailableVersions200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListAvailableVersions200Response> ListAvailableVersionsWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListAvailableVersions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListAvailableVersions200Response>("/databases/{database-id}/version-upgrade", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAvailableVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Available Versions List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAvailableVersions200Response</returns>
        public async System.Threading.Tasks.Task<ListAvailableVersions200Response> ListAvailableVersionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListAvailableVersions200Response> localVarResponse = await ListAvailableVersionsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Available Versions List all available version upgrades within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAvailableVersions200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListAvailableVersions200Response>> ListAvailableVersionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListAvailableVersions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListAvailableVersions200Response>("/databases/{database-id}/version-upgrade", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListAvailableVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Connection Pools List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListConnectionPools200Response</returns>
        public ListConnectionPools200Response ListConnectionPools(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListConnectionPools200Response> localVarResponse = ListConnectionPoolsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Connection Pools List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListConnectionPools200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListConnectionPools200Response> ListConnectionPoolsWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListConnectionPools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListConnectionPools200Response>("/databases/{database-id}/connection-pools", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListConnectionPools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Connection Pools List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListConnectionPools200Response</returns>
        public async System.Threading.Tasks.Task<ListConnectionPools200Response> ListConnectionPoolsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListConnectionPools200Response> localVarResponse = await ListConnectionPoolsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Connection Pools List all connection pools within the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListConnectionPools200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListConnectionPools200Response>> ListConnectionPoolsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListConnectionPools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListConnectionPools200Response>("/databases/{database-id}/connection-pools", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListConnectionPools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Logical Databases List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabaseDbs200Response</returns>
        public ListDatabaseDbs200Response ListDatabaseDbs(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabaseDbs200Response> localVarResponse = ListDatabaseDbsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Logical Databases List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabaseDbs200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDatabaseDbs200Response> ListDatabaseDbsWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabaseDbs";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDatabaseDbs200Response>("/databases/{database-id}/dbs", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabaseDbs", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Logical Databases List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabaseDbs200Response</returns>
        public async System.Threading.Tasks.Task<ListDatabaseDbs200Response> ListDatabaseDbsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabaseDbs200Response> localVarResponse = await ListDatabaseDbsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Logical Databases List all logical databases within the Managed Database (MySQL and PostgreSQL only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabaseDbs200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDatabaseDbs200Response>> ListDatabaseDbsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabaseDbs";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDatabaseDbs200Response>("/databases/{database-id}/dbs", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabaseDbs", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Managed Database Plans List all Managed Databases plans.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabasePlans200Response</returns>
        public ListDatabasePlans200Response ListDatabasePlans(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabasePlans200Response> localVarResponse = ListDatabasePlansWithHttpInfo(engine, nodes, region);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Managed Database Plans List all Managed Databases plans.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabasePlans200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDatabasePlans200Response> ListDatabasePlansWithHttpInfo(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (engine != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "engine", engine));
            }
            if (nodes != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "nodes", nodes));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabasePlans";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDatabasePlans200Response>("/databases/plans", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabasePlans", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Managed Database Plans List all Managed Databases plans.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabasePlans200Response</returns>
        public async System.Threading.Tasks.Task<ListDatabasePlans200Response> ListDatabasePlansAsync(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabasePlans200Response> localVarResponse = await ListDatabasePlansWithHttpInfoAsync(engine, nodes, region, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Managed Database Plans List all Managed Databases plans.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="engine">Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. (optional)</param>
        /// <param name="nodes">Filter by number of nodes. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabasePlans200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDatabasePlans200Response>> ListDatabasePlansWithHttpInfoAsync(string? engine = default(string?), int? nodes = default(int?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (engine != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "engine", engine));
            }
            if (nodes != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "nodes", nodes));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabasePlans";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDatabasePlans200Response>("/databases/plans", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabasePlans", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Database Users List all database users within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabaseUsers200Response</returns>
        public ListDatabaseUsers200Response ListDatabaseUsers(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabaseUsers200Response> localVarResponse = ListDatabaseUsersWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Database Users List all database users within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabaseUsers200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDatabaseUsers200Response> ListDatabaseUsersWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabaseUsers";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDatabaseUsers200Response>("/databases/{database-id}/users", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabaseUsers", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Database Users List all database users within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabaseUsers200Response</returns>
        public async System.Threading.Tasks.Task<ListDatabaseUsers200Response> ListDatabaseUsersAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabaseUsers200Response> localVarResponse = await ListDatabaseUsersWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Database Users List all database users within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabaseUsers200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDatabaseUsers200Response>> ListDatabaseUsersWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabaseUsers";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDatabaseUsers200Response>("/databases/{database-id}/users", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabaseUsers", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Managed Databases List all Managed Databases in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDatabases200Response</returns>
        public ListDatabases200Response ListDatabases(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabases200Response> localVarResponse = ListDatabasesWithHttpInfo(label, tag, region);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Managed Databases List all Managed Databases in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDatabases200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDatabases200Response> ListDatabasesWithHttpInfo(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (label != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "label", label));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabases";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDatabases200Response>("/databases", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabases", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Managed Databases List all Managed Databases in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDatabases200Response</returns>
        public async System.Threading.Tasks.Task<ListDatabases200Response> ListDatabasesAsync(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDatabases200Response> localVarResponse = await ListDatabasesWithHttpInfoAsync(label, tag, region, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Managed Databases List all Managed Databases in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDatabases200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDatabases200Response>> ListDatabasesWithHttpInfoAsync(string? label = default(string?), string? tag = default(string?), string? region = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (label != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "label", label));
            }
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListDatabases";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDatabases200Response>("/databases", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDatabases", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Maintenance Updates List all available version upgrades within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListMaintenanceUpdates200Response</returns>
        public ListMaintenanceUpdates200Response ListMaintenanceUpdates(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListMaintenanceUpdates200Response> localVarResponse = ListMaintenanceUpdatesWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Maintenance Updates List all available version upgrades within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListMaintenanceUpdates200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListMaintenanceUpdates200Response> ListMaintenanceUpdatesWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListMaintenanceUpdates";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListMaintenanceUpdates200Response>("/databases/{database-id}/maintenance", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMaintenanceUpdates", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Maintenance Updates List all available version upgrades within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListMaintenanceUpdates200Response</returns>
        public async System.Threading.Tasks.Task<ListMaintenanceUpdates200Response> ListMaintenanceUpdatesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListMaintenanceUpdates200Response> localVarResponse = await ListMaintenanceUpdatesWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Maintenance Updates List all available version upgrades within the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListMaintenanceUpdates200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListMaintenanceUpdates200Response>> ListMaintenanceUpdatesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListMaintenanceUpdates";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListMaintenanceUpdates200Response>("/databases/{database-id}/maintenance", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListMaintenanceUpdates", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Service Alerts List service alert messages for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListServiceAlerts200Response</returns>
        public ListServiceAlerts200Response ListServiceAlerts(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListServiceAlerts200Response> localVarResponse = ListServiceAlertsWithHttpInfo(listServiceAlertsRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Service Alerts List service alert messages for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListServiceAlerts200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListServiceAlerts200Response> ListServiceAlertsWithHttpInfo(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = listServiceAlertsRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListServiceAlerts";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<ListServiceAlerts200Response>("/databases/{database-id}/alerts", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListServiceAlerts", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Service Alerts List service alert messages for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListServiceAlerts200Response</returns>
        public async System.Threading.Tasks.Task<ListServiceAlerts200Response> ListServiceAlertsAsync(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListServiceAlerts200Response> localVarResponse = await ListServiceAlertsWithHttpInfoAsync(listServiceAlertsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Service Alerts List service alert messages for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="listServiceAlertsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListServiceAlerts200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListServiceAlerts200Response>> ListServiceAlertsWithHttpInfoAsync(ListServiceAlertsRequest? listServiceAlertsRequest = default(ListServiceAlertsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = listServiceAlertsRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.ListServiceAlerts";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<ListServiceAlerts200Response>("/databases/{database-id}/alerts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListServiceAlerts", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Database User Access Control Configure access control settings for a Managed Database user (Redis engine type only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        public CreateDatabaseUser202Response SetDatabaseUserAcl(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = SetDatabaseUserAclWithHttpInfo(databaseId, username, setDatabaseUserAclRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Set Database User Access Control Configure access control settings for a Managed Database user (Redis engine type only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> SetDatabaseUserAclWithHttpInfo(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->SetDatabaseUserAcl");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->SetDatabaseUserAcl");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter
            localVarRequestOptions.Data = setDatabaseUserAclRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.SetDatabaseUserAcl";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}/access-control", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("SetDatabaseUserAcl", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Database User Access Control Configure access control settings for a Managed Database user (Redis engine type only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseUser202Response> SetDatabaseUserAclAsync(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = await SetDatabaseUserAclWithHttpInfoAsync(databaseId, username, setDatabaseUserAclRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Set Database User Access Control Configure access control settings for a Managed Database user (Redis engine type only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="setDatabaseUserAclRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response>> SetDatabaseUserAclWithHttpInfoAsync(string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = default(SetDatabaseUserAclRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->SetDatabaseUserAcl");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->SetDatabaseUserAcl");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter
            localVarRequestOptions.Data = setDatabaseUserAclRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.SetDatabaseUserAcl";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}/access-control", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("SetDatabaseUserAcl", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Maintenance Updates Start maintenance updates for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>StartMaintenanceUpdates200Response</returns>
        public StartMaintenanceUpdates200Response StartMaintenanceUpdates(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<StartMaintenanceUpdates200Response> localVarResponse = StartMaintenanceUpdatesWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Maintenance Updates Start maintenance updates for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of StartMaintenanceUpdates200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<StartMaintenanceUpdates200Response> StartMaintenanceUpdatesWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.StartMaintenanceUpdates";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<StartMaintenanceUpdates200Response>("/databases/{database-id}/maintenance", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartMaintenanceUpdates", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Maintenance Updates Start maintenance updates for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StartMaintenanceUpdates200Response</returns>
        public async System.Threading.Tasks.Task<StartMaintenanceUpdates200Response> StartMaintenanceUpdatesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<StartMaintenanceUpdates200Response> localVarResponse = await StartMaintenanceUpdatesWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Maintenance Updates Start maintenance updates for the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StartMaintenanceUpdates200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<StartMaintenanceUpdates200Response>> StartMaintenanceUpdatesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.StartMaintenanceUpdates";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<StartMaintenanceUpdates200Response>("/databases/{database-id}/maintenance", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartMaintenanceUpdates", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Version Upgrade Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>StartVersionUpgrade200Response</returns>
        public StartVersionUpgrade200Response StartVersionUpgrade(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<StartVersionUpgrade200Response> localVarResponse = StartVersionUpgradeWithHttpInfo(startVersionUpgradeRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Version Upgrade Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of StartVersionUpgrade200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<StartVersionUpgrade200Response> StartVersionUpgradeWithHttpInfo(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = startVersionUpgradeRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.StartVersionUpgrade";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<StartVersionUpgrade200Response>("/databases/{database-id}/version-upgrade", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartVersionUpgrade", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Version Upgrade Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of StartVersionUpgrade200Response</returns>
        public async System.Threading.Tasks.Task<StartVersionUpgrade200Response> StartVersionUpgradeAsync(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<StartVersionUpgrade200Response> localVarResponse = await StartVersionUpgradeWithHttpInfoAsync(startVersionUpgradeRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Start Version Upgrade Start a version upgrade for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startVersionUpgradeRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (StartVersionUpgrade200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<StartVersionUpgrade200Response>> StartVersionUpgradeWithHttpInfoAsync(StartVersionUpgradeRequest? startVersionUpgradeRequest = default(StartVersionUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = startVersionUpgradeRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.StartVersionUpgrade";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<StartVersionUpgrade200Response>("/databases/{database-id}/version-upgrade", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartVersionUpgrade", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Advanced Options Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListAdvancedOptions200Response</returns>
        public ListAdvancedOptions200Response UpdateAdvancedOptions(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> localVarResponse = UpdateAdvancedOptionsWithHttpInfo(updateAdvancedOptionsRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Advanced Options Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListAdvancedOptions200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> UpdateAdvancedOptionsWithHttpInfo(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = updateAdvancedOptionsRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateAdvancedOptions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<ListAdvancedOptions200Response>("/databases/{database-id}/advanced-options", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAdvancedOptions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Advanced Options Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListAdvancedOptions200Response</returns>
        public async System.Threading.Tasks.Task<ListAdvancedOptions200Response> UpdateAdvancedOptionsAsync(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response> localVarResponse = await UpdateAdvancedOptionsWithHttpInfoAsync(updateAdvancedOptionsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Advanced Options Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="updateAdvancedOptionsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListAdvancedOptions200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListAdvancedOptions200Response>> UpdateAdvancedOptionsWithHttpInfoAsync(UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = default(UpdateAdvancedOptionsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = updateAdvancedOptionsRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateAdvancedOptions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<ListAdvancedOptions200Response>("/databases/{database-id}/advanced-options", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateAdvancedOptions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Connection Pool Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateConnectionPool202Response</returns>
        public CreateConnectionPool202Response UpdateConnectionPool(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = UpdateConnectionPoolWithHttpInfo(databaseId, poolName, updateConnectionPoolRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Connection Pool Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateConnectionPool202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> UpdateConnectionPoolWithHttpInfo(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->UpdateConnectionPool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter
            localVarRequestOptions.Data = updateConnectionPoolRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Connection Pool Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateConnectionPool202Response</returns>
        public async System.Threading.Tasks.Task<CreateConnectionPool202Response> UpdateConnectionPoolAsync(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response> localVarResponse = await UpdateConnectionPoolWithHttpInfoAsync(databaseId, poolName, updateConnectionPoolRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Connection Pool Update connection-pool information within a Managed Database (PostgreSQL engine types only).
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="poolName">The [connection pool name](#operation/list-connection-pools).</param>
        /// <param name="updateConnectionPoolRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateConnectionPool202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateConnectionPool202Response>> UpdateConnectionPoolWithHttpInfoAsync(string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = default(UpdateConnectionPoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateConnectionPool");
            }

            // verify the required parameter 'poolName' is set
            if (poolName == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'poolName' when calling ManagedDatabasesApi->UpdateConnectionPool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("pool-name", Org.OpenAPITools.Client.ClientUtils.ParameterToString(poolName)); // path parameter
            localVarRequestOptions.Data = updateConnectionPoolRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateConnectionPool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<CreateConnectionPool202Response>("/databases/{database-id}/connection-pools/{pool-name}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateConnectionPool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Managed Database Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabase202Response</returns>
        public CreateDatabase202Response UpdateDatabase(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = UpdateDatabaseWithHttpInfo(databaseId, updateDatabaseRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Managed Database Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabase202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> UpdateDatabaseWithHttpInfo(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateDatabase");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.Data = updateDatabaseRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CreateDatabase202Response>("/databases/{database-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Managed Database Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabase202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabase202Response> UpdateDatabaseAsync(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response> localVarResponse = await UpdateDatabaseWithHttpInfoAsync(databaseId, updateDatabaseRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Managed Database Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="updateDatabaseRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabase202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabase202Response>> UpdateDatabaseWithHttpInfoAsync(string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = default(UpdateDatabaseRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateDatabase");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.Data = updateDatabaseRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateDatabase";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<CreateDatabase202Response>("/databases/{database-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDatabase", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Database User Update database user information within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDatabaseUser202Response</returns>
        public CreateDatabaseUser202Response UpdateDatabaseUser(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = UpdateDatabaseUserWithHttpInfo(databaseId, username, updateDatabaseUserRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Database User Update database user information within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDatabaseUser202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> UpdateDatabaseUserWithHttpInfo(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->UpdateDatabaseUser");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter
            localVarRequestOptions.Data = updateDatabaseUserRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Database User Update database user information within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDatabaseUser202Response</returns>
        public async System.Threading.Tasks.Task<CreateDatabaseUser202Response> UpdateDatabaseUserAsync(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response> localVarResponse = await UpdateDatabaseUserWithHttpInfoAsync(databaseId, username, updateDatabaseUserRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Database User Update database user information within a Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="databaseId">The [Managed Database ID](#operation/list-databases).</param>
        /// <param name="username">The [database user](#operation/list-database-users).</param>
        /// <param name="updateDatabaseUserRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDatabaseUser202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDatabaseUser202Response>> UpdateDatabaseUserWithHttpInfoAsync(string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = default(UpdateDatabaseUserRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'databaseId' is set
            if (databaseId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'databaseId' when calling ManagedDatabasesApi->UpdateDatabaseUser");
            }

            // verify the required parameter 'username' is set
            if (username == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'username' when calling ManagedDatabasesApi->UpdateDatabaseUser");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("database-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(databaseId)); // path parameter
            localVarRequestOptions.PathParameters.Add("username", Org.OpenAPITools.Client.ClientUtils.ParameterToString(username)); // path parameter
            localVarRequestOptions.Data = updateDatabaseUserRequest;

            localVarRequestOptions.Operation = "ManagedDatabasesApi.UpdateDatabaseUser";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<CreateDatabaseUser202Response>("/databases/{database-id}/users/{username}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDatabaseUser", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Migration Status View the status of a migration attached to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ViewMigrationStatus200Response</returns>
        public ViewMigrationStatus200Response ViewMigrationStatus(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> localVarResponse = ViewMigrationStatusWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Migration Status View the status of a migration attached to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ViewMigrationStatus200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> ViewMigrationStatusWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ViewMigrationStatus";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ViewMigrationStatus200Response>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ViewMigrationStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Migration Status View the status of a migration attached to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ViewMigrationStatus200Response</returns>
        public async System.Threading.Tasks.Task<ViewMigrationStatus200Response> ViewMigrationStatusAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response> localVarResponse = await ViewMigrationStatusWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Migration Status View the status of a migration attached to the Managed Database.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ViewMigrationStatus200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ViewMigrationStatus200Response>> ViewMigrationStatusWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "ManagedDatabasesApi.ViewMigrationStatus";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ViewMigrationStatus200Response>("/databases/{database-id}/migration", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ViewMigrationStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
