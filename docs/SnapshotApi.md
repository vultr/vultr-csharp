# Org.OpenAPITools.Api.SnapshotApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateSnapshot**](SnapshotApi.md#createsnapshot) | **POST** /snapshots | Create Snapshot |
| [**CreateSnapshotCreateFromUrl**](SnapshotApi.md#createsnapshotcreatefromurl) | **POST** /snapshots/create-from-url | Create Snapshot from URL |
| [**DeleteSnapshot**](SnapshotApi.md#deletesnapshot) | **DELETE** /snapshots/{snapshot-id} | Delete Snapshot |
| [**GetSnapshot**](SnapshotApi.md#getsnapshot) | **GET** /snapshots/{snapshot-id} | Get Snapshot |
| [**ListSnapshots**](SnapshotApi.md#listsnapshots) | **GET** /snapshots | List Snapshots |
| [**PutSnapshotsSnapshotId**](SnapshotApi.md#putsnapshotssnapshotid) | **PUT** /snapshots/{snapshot-id} | Update Snapshot |

<a id="createsnapshot"></a>
# **CreateSnapshot**
> GetSnapshot200Response CreateSnapshot (CreateSnapshotRequest? createSnapshotRequest = null)

Create Snapshot

Create a new Snapshot for `instance_id`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateSnapshotExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var createSnapshotRequest = new CreateSnapshotRequest?(); // CreateSnapshotRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Snapshot
                GetSnapshot200Response result = apiInstance.CreateSnapshot(createSnapshotRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.CreateSnapshot: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateSnapshotWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Snapshot
    ApiResponse<GetSnapshot200Response> response = apiInstance.CreateSnapshotWithHttpInfo(createSnapshotRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.CreateSnapshotWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createSnapshotRequest** | [**CreateSnapshotRequest?**](CreateSnapshotRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetSnapshot200Response**](GetSnapshot200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createsnapshotcreatefromurl"></a>
# **CreateSnapshotCreateFromUrl**
> GetSnapshot200Response CreateSnapshotCreateFromUrl (CreateSnapshotCreateFromUrlRequest? createSnapshotCreateFromUrlRequest = null)

Create Snapshot from URL

Create a new Snapshot from a RAW image located at `url`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateSnapshotCreateFromUrlExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var createSnapshotCreateFromUrlRequest = new CreateSnapshotCreateFromUrlRequest?(); // CreateSnapshotCreateFromUrlRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Snapshot from URL
                GetSnapshot200Response result = apiInstance.CreateSnapshotCreateFromUrl(createSnapshotCreateFromUrlRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.CreateSnapshotCreateFromUrl: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateSnapshotCreateFromUrlWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Snapshot from URL
    ApiResponse<GetSnapshot200Response> response = apiInstance.CreateSnapshotCreateFromUrlWithHttpInfo(createSnapshotCreateFromUrlRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.CreateSnapshotCreateFromUrlWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createSnapshotCreateFromUrlRequest** | [**CreateSnapshotCreateFromUrlRequest?**](CreateSnapshotCreateFromUrlRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetSnapshot200Response**](GetSnapshot200Response.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletesnapshot"></a>
# **DeleteSnapshot**
> void DeleteSnapshot (string snapshotId)

Delete Snapshot

Delete a Snapshot.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteSnapshotExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var snapshotId = "snapshotId_example";  // string | The [Snapshot id](#operation/list-snapshots).

            try
            {
                // Delete Snapshot
                apiInstance.DeleteSnapshot(snapshotId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.DeleteSnapshot: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteSnapshotWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Snapshot
    apiInstance.DeleteSnapshotWithHttpInfo(snapshotId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.DeleteSnapshotWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **snapshotId** | **string** | The [Snapshot id](#operation/list-snapshots). |  |

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

<a id="getsnapshot"></a>
# **GetSnapshot**
> GetSnapshot200Response GetSnapshot (string snapshotId)

Get Snapshot

Get information about a Snapshot.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetSnapshotExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var snapshotId = "snapshotId_example";  // string | The [Snapshot id](#operation/list-snapshots).

            try
            {
                // Get Snapshot
                GetSnapshot200Response result = apiInstance.GetSnapshot(snapshotId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.GetSnapshot: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetSnapshotWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Snapshot
    ApiResponse<GetSnapshot200Response> response = apiInstance.GetSnapshotWithHttpInfo(snapshotId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.GetSnapshotWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **snapshotId** | **string** | The [Snapshot id](#operation/list-snapshots). |  |

### Return type

[**GetSnapshot200Response**](GetSnapshot200Response.md)

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

<a id="listsnapshots"></a>
# **ListSnapshots**
> ListSnapshots200Response ListSnapshots (string? description = null, int? perPage = null, string? cursor = null)

List Snapshots

Get information about all Snapshots in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListSnapshotsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var description = "description_example";  // string? | Filter the list of Snapshots by `description` (optional) 
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Snapshots
                ListSnapshots200Response result = apiInstance.ListSnapshots(description, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.ListSnapshots: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListSnapshotsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Snapshots
    ApiResponse<ListSnapshots200Response> response = apiInstance.ListSnapshotsWithHttpInfo(description, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.ListSnapshotsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **description** | **string?** | Filter the list of Snapshots by &#x60;description&#x60; | [optional]  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListSnapshots200Response**](ListSnapshots200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **401** | Unauthorized |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="putsnapshotssnapshotid"></a>
# **PutSnapshotsSnapshotId**
> void PutSnapshotsSnapshotId (string snapshotId, PutSnapshotsSnapshotIdRequest? putSnapshotsSnapshotIdRequest = null)

Update Snapshot

Update the description for a Snapshot.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PutSnapshotsSnapshotIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SnapshotApi(config);
            var snapshotId = "snapshotId_example";  // string | The [Snapshot id](#operation/list-snapshots).
            var putSnapshotsSnapshotIdRequest = new PutSnapshotsSnapshotIdRequest?(); // PutSnapshotsSnapshotIdRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Snapshot
                apiInstance.PutSnapshotsSnapshotId(snapshotId, putSnapshotsSnapshotIdRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SnapshotApi.PutSnapshotsSnapshotId: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PutSnapshotsSnapshotIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Snapshot
    apiInstance.PutSnapshotsSnapshotIdWithHttpInfo(snapshotId, putSnapshotsSnapshotIdRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SnapshotApi.PutSnapshotsSnapshotIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **snapshotId** | **string** | The [Snapshot id](#operation/list-snapshots). |  |
| **putSnapshotsSnapshotIdRequest** | [**PutSnapshotsSnapshotIdRequest?**](PutSnapshotsSnapshotIdRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

