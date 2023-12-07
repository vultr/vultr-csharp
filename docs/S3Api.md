# Org.OpenAPITools.Api.S3Api

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateObjectStorage**](S3Api.md#createobjectstorage) | **POST** /object-storage | Create Object Storage |
| [**DeleteObjectStorage**](S3Api.md#deleteobjectstorage) | **DELETE** /object-storage/{object-storage-id} | Delete Object Storage |
| [**GetObjectStorage**](S3Api.md#getobjectstorage) | **GET** /object-storage/{object-storage-id} | Get Object Storage |
| [**ListObjectStorageClusters**](S3Api.md#listobjectstorageclusters) | **GET** /object-storage/clusters | Get All Clusters |
| [**ListObjectStorages**](S3Api.md#listobjectstorages) | **GET** /object-storage | List Object Storages |
| [**RegenerateObjectStorageKeys**](S3Api.md#regenerateobjectstoragekeys) | **POST** /object-storage/{object-storage-id}/regenerate-keys | Regenerate Object Storage Keys |
| [**UpdateObjectStorage**](S3Api.md#updateobjectstorage) | **PUT** /object-storage/{object-storage-id} | Update Object Storage |

<a id="createobjectstorage"></a>
# **CreateObjectStorage**
> CreateObjectStorage202Response CreateObjectStorage (CreateObjectStorageRequest? createObjectStorageRequest = null)

Create Object Storage

Create new Object Storage. The `cluster_id` attribute is required.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateObjectStorageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var createObjectStorageRequest = new CreateObjectStorageRequest?(); // CreateObjectStorageRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Object Storage
                CreateObjectStorage202Response result = apiInstance.CreateObjectStorage(createObjectStorageRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.CreateObjectStorage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateObjectStorageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Object Storage
    ApiResponse<CreateObjectStorage202Response> response = apiInstance.CreateObjectStorageWithHttpInfo(createObjectStorageRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.CreateObjectStorageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createObjectStorageRequest** | [**CreateObjectStorageRequest?**](CreateObjectStorageRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateObjectStorage202Response**](CreateObjectStorage202Response.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteobjectstorage"></a>
# **DeleteObjectStorage**
> void DeleteObjectStorage (string objectStorageId)

Delete Object Storage

Delete an Object Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteObjectStorageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var objectStorageId = "objectStorageId_example";  // string | The [Object Storage id](#operation/list-object-storages).

            try
            {
                // Delete Object Storage
                apiInstance.DeleteObjectStorage(objectStorageId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.DeleteObjectStorage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteObjectStorageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Object Storage
    apiInstance.DeleteObjectStorageWithHttpInfo(objectStorageId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.DeleteObjectStorageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **objectStorageId** | **string** | The [Object Storage id](#operation/list-object-storages). |  |

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getobjectstorage"></a>
# **GetObjectStorage**
> CreateObjectStorage202Response GetObjectStorage (string objectStorageId)

Get Object Storage

Get information about an Object Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetObjectStorageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var objectStorageId = "objectStorageId_example";  // string | The [Object Storage id](#operation/list-object-storages).

            try
            {
                // Get Object Storage
                CreateObjectStorage202Response result = apiInstance.GetObjectStorage(objectStorageId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.GetObjectStorage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetObjectStorageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Object Storage
    ApiResponse<CreateObjectStorage202Response> response = apiInstance.GetObjectStorageWithHttpInfo(objectStorageId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.GetObjectStorageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **objectStorageId** | **string** | The [Object Storage id](#operation/list-object-storages). |  |

### Return type

[**CreateObjectStorage202Response**](CreateObjectStorage202Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listobjectstorageclusters"></a>
# **ListObjectStorageClusters**
> ListObjectStorageClusters200Response ListObjectStorageClusters (int? perPage = null, string? cursor = null)

Get All Clusters

Get a list of all Object Storage Clusters.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListObjectStorageClustersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            var apiInstance = new S3Api(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // Get All Clusters
                ListObjectStorageClusters200Response result = apiInstance.ListObjectStorageClusters(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.ListObjectStorageClusters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListObjectStorageClustersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get All Clusters
    ApiResponse<ListObjectStorageClusters200Response> response = apiInstance.ListObjectStorageClustersWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.ListObjectStorageClustersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListObjectStorageClusters200Response**](ListObjectStorageClusters200Response.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listobjectstorages"></a>
# **ListObjectStorages**
> ListObjectStorages200Response ListObjectStorages (int? perPage = null, string? cursor = null)

List Object Storages

Get a list of all Object Storage in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListObjectStoragesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Object Storages
                ListObjectStorages200Response result = apiInstance.ListObjectStorages(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.ListObjectStorages: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListObjectStoragesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Object Storages
    ApiResponse<ListObjectStorages200Response> response = apiInstance.ListObjectStoragesWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.ListObjectStoragesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListObjectStorages200Response**](ListObjectStorages200Response.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="regenerateobjectstoragekeys"></a>
# **RegenerateObjectStorageKeys**
> RegenerateObjectStorageKeys201Response RegenerateObjectStorageKeys (string objectStorageId)

Regenerate Object Storage Keys

Regenerate the keys for an Object Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RegenerateObjectStorageKeysExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var objectStorageId = "objectStorageId_example";  // string | The [Object Storage id](#operation/list-object-storages).

            try
            {
                // Regenerate Object Storage Keys
                RegenerateObjectStorageKeys201Response result = apiInstance.RegenerateObjectStorageKeys(objectStorageId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.RegenerateObjectStorageKeys: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RegenerateObjectStorageKeysWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Regenerate Object Storage Keys
    ApiResponse<RegenerateObjectStorageKeys201Response> response = apiInstance.RegenerateObjectStorageKeysWithHttpInfo(objectStorageId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.RegenerateObjectStorageKeysWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **objectStorageId** | **string** | The [Object Storage id](#operation/list-object-storages). |  |

### Return type

[**RegenerateObjectStorageKeys201Response**](RegenerateObjectStorageKeys201Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateobjectstorage"></a>
# **UpdateObjectStorage**
> void UpdateObjectStorage (string objectStorageId, UpdateObjectStorageRequest? updateObjectStorageRequest = null)

Update Object Storage

Update the label for an Object Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateObjectStorageExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new S3Api(config);
            var objectStorageId = "objectStorageId_example";  // string | The [Object Storage id](#operation/list-object-storages).
            var updateObjectStorageRequest = new UpdateObjectStorageRequest?(); // UpdateObjectStorageRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Object Storage
                apiInstance.UpdateObjectStorage(objectStorageId, updateObjectStorageRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling S3Api.UpdateObjectStorage: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateObjectStorageWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Object Storage
    apiInstance.UpdateObjectStorageWithHttpInfo(objectStorageId, updateObjectStorageRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling S3Api.UpdateObjectStorageWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **objectStorageId** | **string** | The [Object Storage id](#operation/list-object-storages). |  |
| **updateObjectStorageRequest** | [**UpdateObjectStorageRequest?**](UpdateObjectStorageRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

void (empty response body)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | No Content |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

