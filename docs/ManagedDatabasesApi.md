# Org.OpenAPITools.Api.ManagedDatabasesApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateConnectionPool**](ManagedDatabasesApi.md#createconnectionpool) | **POST** /databases/{database-id}/connection-pools | Create Connection Pool |
| [**CreateDatabase**](ManagedDatabasesApi.md#createdatabase) | **POST** /databases | Create Managed Database |
| [**CreateDatabaseDb**](ManagedDatabasesApi.md#createdatabasedb) | **POST** /databases/{database-id}/dbs | Create Logical Database |
| [**CreateDatabaseUser**](ManagedDatabasesApi.md#createdatabaseuser) | **POST** /databases/{database-id}/users | Create Database User |
| [**DatabaseAddReadReplica**](ManagedDatabasesApi.md#databaseaddreadreplica) | **POST** /databases/{database-id}/read-replica | Add Read-Only Replica |
| [**DatabaseDetachMigration**](ManagedDatabasesApi.md#databasedetachmigration) | **DELETE** /databases/{database-id}/migration | Detach Migration |
| [**DatabaseFork**](ManagedDatabasesApi.md#databasefork) | **POST** /databases/{database-id}/fork | Fork Managed Database |
| [**DatabasePromoteReadReplica**](ManagedDatabasesApi.md#databasepromotereadreplica) | **POST** /databases/{database-id}/promote-read-replica | Promote Read-Only Replica |
| [**DatabaseRestoreFromBackup**](ManagedDatabasesApi.md#databaserestorefrombackup) | **POST** /databases/{database-id}/restore | Restore from Backup |
| [**DatabaseStartMigration**](ManagedDatabasesApi.md#databasestartmigration) | **POST** /databases/{database-id}/migration | Start Migration |
| [**DeleteConnectionPool**](ManagedDatabasesApi.md#deleteconnectionpool) | **DELETE** /databases/{database-id}/connection-pools/{pool-name} | Delete Connection Pool |
| [**DeleteDatabase**](ManagedDatabasesApi.md#deletedatabase) | **DELETE** /databases/{database-id} | Delete Managed Database |
| [**DeleteDatabaseDb**](ManagedDatabasesApi.md#deletedatabasedb) | **DELETE** /databases/{database-id}/dbs/{db-name} | Delete Logical Database |
| [**DeleteDatabaseUser**](ManagedDatabasesApi.md#deletedatabaseuser) | **DELETE** /databases/{database-id}/users/{username} | Delete Database User |
| [**GetBackupInformation**](ManagedDatabasesApi.md#getbackupinformation) | **GET** /databases/{database-id}/backups | Get Backup Information |
| [**GetConnectionPool**](ManagedDatabasesApi.md#getconnectionpool) | **GET** /databases/{database-id}/connection-pools/{pool-name} | Get Connection Pool |
| [**GetDatabase**](ManagedDatabasesApi.md#getdatabase) | **GET** /databases/{database-id} | Get Managed Database |
| [**GetDatabaseDb**](ManagedDatabasesApi.md#getdatabasedb) | **GET** /databases/{database-id}/dbs/{db-name} | Get Logical Database |
| [**GetDatabaseUsage**](ManagedDatabasesApi.md#getdatabaseusage) | **GET** /databases/{database-id}/usage | Get Database Usage Information |
| [**GetDatabaseUser**](ManagedDatabasesApi.md#getdatabaseuser) | **GET** /databases/{database-id}/users/{username} | Get Database User |
| [**ListAdvancedOptions**](ManagedDatabasesApi.md#listadvancedoptions) | **GET** /databases/{database-id}/advanced-options | List Advanced Options |
| [**ListAvailableVersions**](ManagedDatabasesApi.md#listavailableversions) | **GET** /databases/{database-id}/version-upgrade | List Available Versions |
| [**ListConnectionPools**](ManagedDatabasesApi.md#listconnectionpools) | **GET** /databases/{database-id}/connection-pools | List Connection Pools |
| [**ListDatabaseDbs**](ManagedDatabasesApi.md#listdatabasedbs) | **GET** /databases/{database-id}/dbs | List Logical Databases |
| [**ListDatabasePlans**](ManagedDatabasesApi.md#listdatabaseplans) | **GET** /databases/plans | List Managed Database Plans |
| [**ListDatabaseUsers**](ManagedDatabasesApi.md#listdatabaseusers) | **GET** /databases/{database-id}/users | List Database Users |
| [**ListDatabases**](ManagedDatabasesApi.md#listdatabases) | **GET** /databases | List Managed Databases |
| [**ListMaintenanceUpdates**](ManagedDatabasesApi.md#listmaintenanceupdates) | **GET** /databases/{database-id}/maintenance | List Maintenance Updates |
| [**ListServiceAlerts**](ManagedDatabasesApi.md#listservicealerts) | **POST** /databases/{database-id}/alerts | List Service Alerts |
| [**SetDatabaseUserAcl**](ManagedDatabasesApi.md#setdatabaseuseracl) | **PUT** /databases/{database-id}/users/{username}/access-control | Set Database User Access Control |
| [**StartMaintenanceUpdates**](ManagedDatabasesApi.md#startmaintenanceupdates) | **POST** /databases/{database-id}/maintenance | Start Maintenance Updates |
| [**StartVersionUpgrade**](ManagedDatabasesApi.md#startversionupgrade) | **POST** /databases/{database-id}/version-upgrade | Start Version Upgrade |
| [**UpdateAdvancedOptions**](ManagedDatabasesApi.md#updateadvancedoptions) | **PUT** /databases/{database-id}/advanced-options | Update Advanced Options |
| [**UpdateConnectionPool**](ManagedDatabasesApi.md#updateconnectionpool) | **PUT** /databases/{database-id}/connection-pools/{pool-name} | Update Connection Pool |
| [**UpdateDatabase**](ManagedDatabasesApi.md#updatedatabase) | **PUT** /databases/{database-id} | Update Managed Database |
| [**UpdateDatabaseUser**](ManagedDatabasesApi.md#updatedatabaseuser) | **PUT** /databases/{database-id}/users/{username} | Update Database User |
| [**ViewMigrationStatus**](ManagedDatabasesApi.md#viewmigrationstatus) | **GET** /databases/{database-id}/migration | Get Migration Status |

<a id="createconnectionpool"></a>
# **CreateConnectionPool**
> CreateConnectionPool202Response CreateConnectionPool (CreateConnectionPoolRequest? createConnectionPoolRequest = null)

Create Connection Pool

Create a new connection pool within the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateConnectionPoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var createConnectionPoolRequest = new CreateConnectionPoolRequest?(); // CreateConnectionPoolRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Connection Pool
                CreateConnectionPool202Response result = apiInstance.CreateConnectionPool(createConnectionPoolRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.CreateConnectionPool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateConnectionPoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Connection Pool
    ApiResponse<CreateConnectionPool202Response> response = apiInstance.CreateConnectionPoolWithHttpInfo(createConnectionPoolRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.CreateConnectionPoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createConnectionPoolRequest** | [**CreateConnectionPoolRequest?**](CreateConnectionPoolRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateConnectionPool202Response**](CreateConnectionPool202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createdatabase"></a>
# **CreateDatabase**
> CreateDatabase202Response CreateDatabase (CreateDatabaseRequest? createDatabaseRequest = null)

Create Managed Database

Create a new Managed Database in a `region` with the desired `plan`. Supply optional attributes as desired.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var createDatabaseRequest = new CreateDatabaseRequest?(); // CreateDatabaseRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Managed Database
                CreateDatabase202Response result = apiInstance.CreateDatabase(createDatabaseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Managed Database
    ApiResponse<CreateDatabase202Response> response = apiInstance.CreateDatabaseWithHttpInfo(createDatabaseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDatabaseRequest** | [**CreateDatabaseRequest?**](CreateDatabaseRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createdatabasedb"></a>
# **CreateDatabaseDb**
> CreateDatabaseDb202Response CreateDatabaseDb (CreateDatabaseDbRequest? createDatabaseDbRequest = null)

Create Logical Database

Create a new logical database within the Managed Database (MySQL and PostgreSQL only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDatabaseDbExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var createDatabaseDbRequest = new CreateDatabaseDbRequest?(); // CreateDatabaseDbRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Logical Database
                CreateDatabaseDb202Response result = apiInstance.CreateDatabaseDb(createDatabaseDbRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabaseDb: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDatabaseDbWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Logical Database
    ApiResponse<CreateDatabaseDb202Response> response = apiInstance.CreateDatabaseDbWithHttpInfo(createDatabaseDbRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabaseDbWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDatabaseDbRequest** | [**CreateDatabaseDbRequest?**](CreateDatabaseDbRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabaseDb202Response**](CreateDatabaseDb202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createdatabaseuser"></a>
# **CreateDatabaseUser**
> CreateDatabaseUser202Response CreateDatabaseUser (CreateDatabaseUserRequest? createDatabaseUserRequest = null)

Create Database User

Create a new database user within the Managed Database. Supply optional attributes as desired.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDatabaseUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var createDatabaseUserRequest = new CreateDatabaseUserRequest?(); // CreateDatabaseUserRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Database User
                CreateDatabaseUser202Response result = apiInstance.CreateDatabaseUser(createDatabaseUserRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabaseUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDatabaseUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Database User
    ApiResponse<CreateDatabaseUser202Response> response = apiInstance.CreateDatabaseUserWithHttpInfo(createDatabaseUserRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.CreateDatabaseUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDatabaseUserRequest** | [**CreateDatabaseUserRequest?**](CreateDatabaseUserRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabaseUser202Response**](CreateDatabaseUser202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databaseaddreadreplica"></a>
# **DatabaseAddReadReplica**
> CreateDatabase202Response DatabaseAddReadReplica (DatabaseAddReadReplicaRequest? databaseAddReadReplicaRequest = null)

Add Read-Only Replica

Create a read-only replica node for the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabaseAddReadReplicaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseAddReadReplicaRequest = new DatabaseAddReadReplicaRequest?(); // DatabaseAddReadReplicaRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Add Read-Only Replica
                CreateDatabase202Response result = apiInstance.DatabaseAddReadReplica(databaseAddReadReplicaRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseAddReadReplica: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseAddReadReplicaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Read-Only Replica
    ApiResponse<CreateDatabase202Response> response = apiInstance.DatabaseAddReadReplicaWithHttpInfo(databaseAddReadReplicaRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseAddReadReplicaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseAddReadReplicaRequest** | [**DatabaseAddReadReplicaRequest?**](DatabaseAddReadReplicaRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasedetachmigration"></a>
# **DatabaseDetachMigration**
> void DatabaseDetachMigration ()

Detach Migration

Detach a migration from the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabaseDetachMigrationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Detach Migration
                apiInstance.DatabaseDetachMigration();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseDetachMigration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseDetachMigrationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach Migration
    apiInstance.DatabaseDetachMigrationWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseDetachMigrationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasefork"></a>
# **DatabaseFork**
> CreateDatabase202Response DatabaseFork (DatabaseForkRequest? databaseForkRequest = null)

Fork Managed Database

Fork a Managed Database to a new subscription from a backup.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabaseForkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseForkRequest = new DatabaseForkRequest?(); // DatabaseForkRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Fork Managed Database
                CreateDatabase202Response result = apiInstance.DatabaseFork(databaseForkRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseFork: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseForkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Fork Managed Database
    ApiResponse<CreateDatabase202Response> response = apiInstance.DatabaseForkWithHttpInfo(databaseForkRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseForkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseForkRequest** | [**DatabaseForkRequest?**](DatabaseForkRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasepromotereadreplica"></a>
# **DatabasePromoteReadReplica**
> void DatabasePromoteReadReplica ()

Promote Read-Only Replica

Promote a read-only replica node to its own primary Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabasePromoteReadReplicaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Promote Read-Only Replica
                apiInstance.DatabasePromoteReadReplica();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabasePromoteReadReplica: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasePromoteReadReplicaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Promote Read-Only Replica
    apiInstance.DatabasePromoteReadReplicaWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabasePromoteReadReplicaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databaserestorefrombackup"></a>
# **DatabaseRestoreFromBackup**
> CreateDatabase202Response DatabaseRestoreFromBackup (DatabaseRestoreFromBackupRequest? databaseRestoreFromBackupRequest = null)

Restore from Backup

Create a new Managed Database from a backup.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabaseRestoreFromBackupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseRestoreFromBackupRequest = new DatabaseRestoreFromBackupRequest?(); // DatabaseRestoreFromBackupRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Restore from Backup
                CreateDatabase202Response result = apiInstance.DatabaseRestoreFromBackup(databaseRestoreFromBackupRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseRestoreFromBackup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseRestoreFromBackupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Restore from Backup
    ApiResponse<CreateDatabase202Response> response = apiInstance.DatabaseRestoreFromBackupWithHttpInfo(databaseRestoreFromBackupRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseRestoreFromBackupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseRestoreFromBackupRequest** | [**DatabaseRestoreFromBackupRequest?**](DatabaseRestoreFromBackupRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasestartmigration"></a>
# **DatabaseStartMigration**
> ViewMigrationStatus200Response DatabaseStartMigration (DatabaseStartMigrationRequest? databaseStartMigrationRequest = null)

Start Migration

Start a migration to the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DatabaseStartMigrationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseStartMigrationRequest = new DatabaseStartMigrationRequest?(); // DatabaseStartMigrationRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Start Migration
                ViewMigrationStatus200Response result = apiInstance.DatabaseStartMigration(databaseStartMigrationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseStartMigration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseStartMigrationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Migration
    ApiResponse<ViewMigrationStatus200Response> response = apiInstance.DatabaseStartMigrationWithHttpInfo(databaseStartMigrationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DatabaseStartMigrationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseStartMigrationRequest** | [**DatabaseStartMigrationRequest?**](DatabaseStartMigrationRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**ViewMigrationStatus200Response**](ViewMigrationStatus200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteconnectionpool"></a>
# **DeleteConnectionPool**
> void DeleteConnectionPool (string databaseId, string poolName)

Delete Connection Pool

Delete a connection pool within a Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteConnectionPoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var poolName = "poolName_example";  // string | The [connection pool name](#operation/list-connection-pools).

            try
            {
                // Delete Connection Pool
                apiInstance.DeleteConnectionPool(databaseId, poolName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DeleteConnectionPool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteConnectionPoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Connection Pool
    apiInstance.DeleteConnectionPoolWithHttpInfo(databaseId, poolName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DeleteConnectionPoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **poolName** | **string** | The [connection pool name](#operation/list-connection-pools). |  |

### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletedatabase"></a>
# **DeleteDatabase**
> void DeleteDatabase (string databaseId)

Delete Managed Database

Delete a Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).

            try
            {
                // Delete Managed Database
                apiInstance.DeleteDatabase(databaseId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Managed Database
    apiInstance.DeleteDatabaseWithHttpInfo(databaseId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |

### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletedatabasedb"></a>
# **DeleteDatabaseDb**
> void DeleteDatabaseDb (string databaseId, string dbName)

Delete Logical Database

Delete a logical database within a Managed Database (MySQL and PostgreSQL only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDatabaseDbExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var dbName = "dbName_example";  // string | The [logical database name](#operation/list-database-dbs).

            try
            {
                // Delete Logical Database
                apiInstance.DeleteDatabaseDb(databaseId, dbName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabaseDb: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDatabaseDbWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Logical Database
    apiInstance.DeleteDatabaseDbWithHttpInfo(databaseId, dbName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabaseDbWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **dbName** | **string** | The [logical database name](#operation/list-database-dbs). |  |

### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletedatabaseuser"></a>
# **DeleteDatabaseUser**
> void DeleteDatabaseUser (string databaseId, string username)

Delete Database User

Delete a database user within a Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDatabaseUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var username = "username_example";  // string | The [database user](#operation/list-database-users).

            try
            {
                // Delete Database User
                apiInstance.DeleteDatabaseUser(databaseId, username);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabaseUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDatabaseUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Database User
    apiInstance.DeleteDatabaseUserWithHttpInfo(databaseId, username);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.DeleteDatabaseUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **username** | **string** | The [database user](#operation/list-database-users). |  |

### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getbackupinformation"></a>
# **GetBackupInformation**
> GetBackupInformation200Response GetBackupInformation ()

Get Backup Information

Get backup information for the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBackupInformationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Get Backup Information
                GetBackupInformation200Response result = apiInstance.GetBackupInformation();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetBackupInformation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBackupInformationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Backup Information
    ApiResponse<GetBackupInformation200Response> response = apiInstance.GetBackupInformationWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetBackupInformationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetBackupInformation200Response**](GetBackupInformation200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getconnectionpool"></a>
# **GetConnectionPool**
> CreateConnectionPool202Response GetConnectionPool (string databaseId, string poolName)

Get Connection Pool

Get information about a Managed Database connection pool (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetConnectionPoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var poolName = "poolName_example";  // string | The [connection pool name](#operation/list-connection-pools).

            try
            {
                // Get Connection Pool
                CreateConnectionPool202Response result = apiInstance.GetConnectionPool(databaseId, poolName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetConnectionPool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetConnectionPoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Connection Pool
    ApiResponse<CreateConnectionPool202Response> response = apiInstance.GetConnectionPoolWithHttpInfo(databaseId, poolName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetConnectionPoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **poolName** | **string** | The [connection pool name](#operation/list-connection-pools). |  |

### Return type

[**CreateConnectionPool202Response**](CreateConnectionPool202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdatabase"></a>
# **GetDatabase**
> CreateDatabase202Response GetDatabase (string databaseId)

Get Managed Database

Get information about a Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).

            try
            {
                // Get Managed Database
                CreateDatabase202Response result = apiInstance.GetDatabase(databaseId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Managed Database
    ApiResponse<CreateDatabase202Response> response = apiInstance.GetDatabaseWithHttpInfo(databaseId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdatabasedb"></a>
# **GetDatabaseDb**
> CreateDatabaseDb202Response GetDatabaseDb (string databaseId, string dbName)

Get Logical Database

Get information about a logical database within a Managed Database (MySQL and PostgreSQL only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDatabaseDbExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var dbName = "dbName_example";  // string | The [logical database name](#operation/list-database-dbs).

            try
            {
                // Get Logical Database
                CreateDatabaseDb202Response result = apiInstance.GetDatabaseDb(databaseId, dbName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseDb: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDatabaseDbWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Logical Database
    ApiResponse<CreateDatabaseDb202Response> response = apiInstance.GetDatabaseDbWithHttpInfo(databaseId, dbName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseDbWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **dbName** | **string** | The [logical database name](#operation/list-database-dbs). |  |

### Return type

[**CreateDatabaseDb202Response**](CreateDatabaseDb202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdatabaseusage"></a>
# **GetDatabaseUsage**
> GetDatabaseUsage200Response GetDatabaseUsage ()

Get Database Usage Information

Get disk, memory, and vCPU usage information for a Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDatabaseUsageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Get Database Usage Information
                GetDatabaseUsage200Response result = apiInstance.GetDatabaseUsage();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseUsage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDatabaseUsageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Database Usage Information
    ApiResponse<GetDatabaseUsage200Response> response = apiInstance.GetDatabaseUsageWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseUsageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetDatabaseUsage200Response**](GetDatabaseUsage200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getdatabaseuser"></a>
# **GetDatabaseUser**
> CreateDatabaseUser202Response GetDatabaseUser (string databaseId, string username)

Get Database User

Get information about a Managed Database user.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDatabaseUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var username = "username_example";  // string | The [database user](#operation/list-database-users).

            try
            {
                // Get Database User
                CreateDatabaseUser202Response result = apiInstance.GetDatabaseUser(databaseId, username);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDatabaseUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Database User
    ApiResponse<CreateDatabaseUser202Response> response = apiInstance.GetDatabaseUserWithHttpInfo(databaseId, username);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.GetDatabaseUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **username** | **string** | The [database user](#operation/list-database-users). |  |

### Return type

[**CreateDatabaseUser202Response**](CreateDatabaseUser202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listadvancedoptions"></a>
# **ListAdvancedOptions**
> ListAdvancedOptions200Response ListAdvancedOptions ()

List Advanced Options

List all configured and available advanced options for the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListAdvancedOptionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Advanced Options
                ListAdvancedOptions200Response result = apiInstance.ListAdvancedOptions();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListAdvancedOptions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListAdvancedOptionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Advanced Options
    ApiResponse<ListAdvancedOptions200Response> response = apiInstance.ListAdvancedOptionsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListAdvancedOptionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListAdvancedOptions200Response**](ListAdvancedOptions200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listavailableversions"></a>
# **ListAvailableVersions**
> ListAvailableVersions200Response ListAvailableVersions ()

List Available Versions

List all available version upgrades within the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListAvailableVersionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Available Versions
                ListAvailableVersions200Response result = apiInstance.ListAvailableVersions();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListAvailableVersions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListAvailableVersionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Available Versions
    ApiResponse<ListAvailableVersions200Response> response = apiInstance.ListAvailableVersionsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListAvailableVersionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListAvailableVersions200Response**](ListAvailableVersions200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listconnectionpools"></a>
# **ListConnectionPools**
> ListConnectionPools200Response ListConnectionPools ()

List Connection Pools

List all connection pools within the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListConnectionPoolsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Connection Pools
                ListConnectionPools200Response result = apiInstance.ListConnectionPools();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListConnectionPools: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListConnectionPoolsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Connection Pools
    ApiResponse<ListConnectionPools200Response> response = apiInstance.ListConnectionPoolsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListConnectionPoolsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListConnectionPools200Response**](ListConnectionPools200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listdatabasedbs"></a>
# **ListDatabaseDbs**
> ListDatabaseDbs200Response ListDatabaseDbs ()

List Logical Databases

List all logical databases within the Managed Database (MySQL and PostgreSQL only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDatabaseDbsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Logical Databases
                ListDatabaseDbs200Response result = apiInstance.ListDatabaseDbs();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabaseDbs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDatabaseDbsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Logical Databases
    ApiResponse<ListDatabaseDbs200Response> response = apiInstance.ListDatabaseDbsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabaseDbsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListDatabaseDbs200Response**](ListDatabaseDbs200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listdatabaseplans"></a>
# **ListDatabasePlans**
> ListDatabasePlans200Response ListDatabasePlans (string? engine = null, int? nodes = null, string? region = null)

List Managed Database Plans

List all Managed Databases plans.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDatabasePlansExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var engine = "engine_example";  // string? | Filter by engine type  * `mysql` * `pg` * `redis`. (optional) 
            var nodes = 56;  // int? | Filter by number of nodes. (optional) 
            var region = "region_example";  // string? | Filter by [Region id](#operation/list-regions). (optional) 

            try
            {
                // List Managed Database Plans
                ListDatabasePlans200Response result = apiInstance.ListDatabasePlans(engine, nodes, region);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabasePlans: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDatabasePlansWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Managed Database Plans
    ApiResponse<ListDatabasePlans200Response> response = apiInstance.ListDatabasePlansWithHttpInfo(engine, nodes, region);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabasePlansWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **engine** | **string?** | Filter by engine type  * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;redis&#x60;. | [optional]  |
| **nodes** | **int?** | Filter by number of nodes. | [optional]  |
| **region** | **string?** | Filter by [Region id](#operation/list-regions). | [optional]  |

### Return type

[**ListDatabasePlans200Response**](ListDatabasePlans200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listdatabaseusers"></a>
# **ListDatabaseUsers**
> ListDatabaseUsers200Response ListDatabaseUsers ()

List Database Users

List all database users within the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDatabaseUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Database Users
                ListDatabaseUsers200Response result = apiInstance.ListDatabaseUsers();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabaseUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDatabaseUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Database Users
    ApiResponse<ListDatabaseUsers200Response> response = apiInstance.ListDatabaseUsersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabaseUsersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListDatabaseUsers200Response**](ListDatabaseUsers200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listdatabases"></a>
# **ListDatabases**
> ListDatabases200Response ListDatabases (string? label = null, string? tag = null, string? region = null)

List Managed Databases

List all Managed Databases in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDatabasesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var label = "label_example";  // string? | Filter by label. (optional) 
            var tag = "tag_example";  // string? | Filter by specific tag. (optional) 
            var region = "region_example";  // string? | Filter by [Region id](#operation/list-regions). (optional) 

            try
            {
                // List Managed Databases
                ListDatabases200Response result = apiInstance.ListDatabases(label, tag, region);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabases: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDatabasesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Managed Databases
    ApiResponse<ListDatabases200Response> response = apiInstance.ListDatabasesWithHttpInfo(label, tag, region);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListDatabasesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **label** | **string?** | Filter by label. | [optional]  |
| **tag** | **string?** | Filter by specific tag. | [optional]  |
| **region** | **string?** | Filter by [Region id](#operation/list-regions). | [optional]  |

### Return type

[**ListDatabases200Response**](ListDatabases200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listmaintenanceupdates"></a>
# **ListMaintenanceUpdates**
> ListMaintenanceUpdates200Response ListMaintenanceUpdates ()

List Maintenance Updates

List all available version upgrades within the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListMaintenanceUpdatesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // List Maintenance Updates
                ListMaintenanceUpdates200Response result = apiInstance.ListMaintenanceUpdates();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListMaintenanceUpdates: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListMaintenanceUpdatesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Maintenance Updates
    ApiResponse<ListMaintenanceUpdates200Response> response = apiInstance.ListMaintenanceUpdatesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListMaintenanceUpdatesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListMaintenanceUpdates200Response**](ListMaintenanceUpdates200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listservicealerts"></a>
# **ListServiceAlerts**
> ListServiceAlerts200Response ListServiceAlerts (ListServiceAlertsRequest? listServiceAlertsRequest = null)

List Service Alerts

List service alert messages for the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListServiceAlertsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var listServiceAlertsRequest = new ListServiceAlertsRequest?(); // ListServiceAlertsRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // List Service Alerts
                ListServiceAlerts200Response result = apiInstance.ListServiceAlerts(listServiceAlertsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ListServiceAlerts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListServiceAlertsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Service Alerts
    ApiResponse<ListServiceAlerts200Response> response = apiInstance.ListServiceAlertsWithHttpInfo(listServiceAlertsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ListServiceAlertsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **listServiceAlertsRequest** | [**ListServiceAlertsRequest?**](ListServiceAlertsRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**ListServiceAlerts200Response**](ListServiceAlerts200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="setdatabaseuseracl"></a>
# **SetDatabaseUserAcl**
> CreateDatabaseUser202Response SetDatabaseUserAcl (string databaseId, string username, SetDatabaseUserAclRequest? setDatabaseUserAclRequest = null)

Set Database User Access Control

Configure access control settings for a Managed Database user (Redis engine type only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class SetDatabaseUserAclExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var username = "username_example";  // string | The [database user](#operation/list-database-users).
            var setDatabaseUserAclRequest = new SetDatabaseUserAclRequest?(); // SetDatabaseUserAclRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Set Database User Access Control
                CreateDatabaseUser202Response result = apiInstance.SetDatabaseUserAcl(databaseId, username, setDatabaseUserAclRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.SetDatabaseUserAcl: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SetDatabaseUserAclWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Database User Access Control
    ApiResponse<CreateDatabaseUser202Response> response = apiInstance.SetDatabaseUserAclWithHttpInfo(databaseId, username, setDatabaseUserAclRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.SetDatabaseUserAclWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **username** | **string** | The [database user](#operation/list-database-users). |  |
| **setDatabaseUserAclRequest** | [**SetDatabaseUserAclRequest?**](SetDatabaseUserAclRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabaseUser202Response**](CreateDatabaseUser202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="startmaintenanceupdates"></a>
# **StartMaintenanceUpdates**
> StartMaintenanceUpdates200Response StartMaintenanceUpdates ()

Start Maintenance Updates

Start maintenance updates for the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartMaintenanceUpdatesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Start Maintenance Updates
                StartMaintenanceUpdates200Response result = apiInstance.StartMaintenanceUpdates();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.StartMaintenanceUpdates: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartMaintenanceUpdatesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Maintenance Updates
    ApiResponse<StartMaintenanceUpdates200Response> response = apiInstance.StartMaintenanceUpdatesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.StartMaintenanceUpdatesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**StartMaintenanceUpdates200Response**](StartMaintenanceUpdates200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="startversionupgrade"></a>
# **StartVersionUpgrade**
> StartVersionUpgrade200Response StartVersionUpgrade (StartVersionUpgradeRequest? startVersionUpgradeRequest = null)

Start Version Upgrade

Start a version upgrade for the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartVersionUpgradeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var startVersionUpgradeRequest = new StartVersionUpgradeRequest?(); // StartVersionUpgradeRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Start Version Upgrade
                StartVersionUpgrade200Response result = apiInstance.StartVersionUpgrade(startVersionUpgradeRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.StartVersionUpgrade: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartVersionUpgradeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Version Upgrade
    ApiResponse<StartVersionUpgrade200Response> response = apiInstance.StartVersionUpgradeWithHttpInfo(startVersionUpgradeRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.StartVersionUpgradeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startVersionUpgradeRequest** | [**StartVersionUpgradeRequest?**](StartVersionUpgradeRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**StartVersionUpgrade200Response**](StartVersionUpgrade200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateadvancedoptions"></a>
# **UpdateAdvancedOptions**
> ListAdvancedOptions200Response UpdateAdvancedOptions (UpdateAdvancedOptionsRequest? updateAdvancedOptionsRequest = null)

Update Advanced Options

Updates an advanced configuration option for the Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateAdvancedOptionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var updateAdvancedOptionsRequest = new UpdateAdvancedOptionsRequest?(); // UpdateAdvancedOptionsRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Advanced Options
                ListAdvancedOptions200Response result = apiInstance.UpdateAdvancedOptions(updateAdvancedOptionsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.UpdateAdvancedOptions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateAdvancedOptionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Advanced Options
    ApiResponse<ListAdvancedOptions200Response> response = apiInstance.UpdateAdvancedOptionsWithHttpInfo(updateAdvancedOptionsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.UpdateAdvancedOptionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **updateAdvancedOptionsRequest** | [**UpdateAdvancedOptionsRequest?**](UpdateAdvancedOptionsRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**ListAdvancedOptions200Response**](ListAdvancedOptions200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateconnectionpool"></a>
# **UpdateConnectionPool**
> CreateConnectionPool202Response UpdateConnectionPool (string databaseId, string poolName, UpdateConnectionPoolRequest? updateConnectionPoolRequest = null)

Update Connection Pool

Update connection-pool information within a Managed Database (PostgreSQL engine types only).

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateConnectionPoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var poolName = "poolName_example";  // string | The [connection pool name](#operation/list-connection-pools).
            var updateConnectionPoolRequest = new UpdateConnectionPoolRequest?(); // UpdateConnectionPoolRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Connection Pool
                CreateConnectionPool202Response result = apiInstance.UpdateConnectionPool(databaseId, poolName, updateConnectionPoolRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.UpdateConnectionPool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateConnectionPoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Connection Pool
    ApiResponse<CreateConnectionPool202Response> response = apiInstance.UpdateConnectionPoolWithHttpInfo(databaseId, poolName, updateConnectionPoolRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.UpdateConnectionPoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **poolName** | **string** | The [connection pool name](#operation/list-connection-pools). |  |
| **updateConnectionPoolRequest** | [**UpdateConnectionPoolRequest?**](UpdateConnectionPoolRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateConnectionPool202Response**](CreateConnectionPool202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatedatabase"></a>
# **UpdateDatabase**
> CreateDatabase202Response UpdateDatabase (string databaseId, UpdateDatabaseRequest? updateDatabaseRequest = null)

Update Managed Database

Update information for a Managed Database. All attributes are optional. If not set, the attributes will retain their original values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var updateDatabaseRequest = new UpdateDatabaseRequest?(); // UpdateDatabaseRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Managed Database
                CreateDatabase202Response result = apiInstance.UpdateDatabase(databaseId, updateDatabaseRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.UpdateDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Managed Database
    ApiResponse<CreateDatabase202Response> response = apiInstance.UpdateDatabaseWithHttpInfo(databaseId, updateDatabaseRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.UpdateDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **updateDatabaseRequest** | [**UpdateDatabaseRequest?**](UpdateDatabaseRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabase202Response**](CreateDatabase202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatedatabaseuser"></a>
# **UpdateDatabaseUser**
> CreateDatabaseUser202Response UpdateDatabaseUser (string databaseId, string username, UpdateDatabaseUserRequest? updateDatabaseUserRequest = null)

Update Database User

Update database user information within a Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDatabaseUserExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);
            var databaseId = "databaseId_example";  // string | The [Managed Database ID](#operation/list-databases).
            var username = "username_example";  // string | The [database user](#operation/list-database-users).
            var updateDatabaseUserRequest = new UpdateDatabaseUserRequest?(); // UpdateDatabaseUserRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Database User
                CreateDatabaseUser202Response result = apiInstance.UpdateDatabaseUser(databaseId, username, updateDatabaseUserRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.UpdateDatabaseUser: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDatabaseUserWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Database User
    ApiResponse<CreateDatabaseUser202Response> response = apiInstance.UpdateDatabaseUserWithHttpInfo(databaseId, username, updateDatabaseUserRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.UpdateDatabaseUserWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **databaseId** | **string** | The [Managed Database ID](#operation/list-databases). |  |
| **username** | **string** | The [database user](#operation/list-database-users). |  |
| **updateDatabaseUserRequest** | [**UpdateDatabaseUserRequest?**](UpdateDatabaseUserRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDatabaseUser202Response**](CreateDatabaseUser202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="viewmigrationstatus"></a>
# **ViewMigrationStatus**
> ViewMigrationStatus200Response ViewMigrationStatus ()

Get Migration Status

View the status of a migration attached to the Managed Database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ViewMigrationStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ManagedDatabasesApi(config);

            try
            {
                // Get Migration Status
                ViewMigrationStatus200Response result = apiInstance.ViewMigrationStatus();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ManagedDatabasesApi.ViewMigrationStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ViewMigrationStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Migration Status
    ApiResponse<ViewMigrationStatus200Response> response = apiInstance.ViewMigrationStatusWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ManagedDatabasesApi.ViewMigrationStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ViewMigrationStatus200Response**](ViewMigrationStatus200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

