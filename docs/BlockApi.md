# Org.OpenAPITools.Api.BlockApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AttachBlock**](BlockApi.md#attachblock) | **POST** /blocks/{block-id}/attach | Attach Block Storage |
| [**CreateBlock**](BlockApi.md#createblock) | **POST** /blocks | Create Block Storage |
| [**DeleteBlock**](BlockApi.md#deleteblock) | **DELETE** /blocks/{block-id} | Delete Block Storage |
| [**DetachBlock**](BlockApi.md#detachblock) | **POST** /blocks/{block-id}/detach | Detach Block Storage |
| [**GetBlock**](BlockApi.md#getblock) | **GET** /blocks/{block-id} | Get Block Storage |
| [**ListBlocks**](BlockApi.md#listblocks) | **GET** /blocks | List Block storages |
| [**UpdateBlock**](BlockApi.md#updateblock) | **PATCH** /blocks/{block-id} | Update Block Storage |

<a id="attachblock"></a>
# **AttachBlock**
> void AttachBlock (string blockId, AttachBlockRequest? attachBlockRequest = null)

Attach Block Storage

Attach Block Storage to Instance `instance_id`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var blockId = "blockId_example";  // string | The [Block Storage id](#operation/list-blocks).
            var attachBlockRequest = new AttachBlockRequest?(); // AttachBlockRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach Block Storage
                apiInstance.AttachBlock(blockId, attachBlockRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.AttachBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach Block Storage
    apiInstance.AttachBlockWithHttpInfo(blockId, attachBlockRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.AttachBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **blockId** | **string** | The [Block Storage id](#operation/list-blocks). |  |
| **attachBlockRequest** | [**AttachBlockRequest?**](AttachBlockRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createblock"></a>
# **CreateBlock**
> CreateBlock202Response CreateBlock (CreateBlockRequest? createBlockRequest = null)

Create Block Storage

Create new Block Storage in a `region` with a size of `size_gb`. Size may range between 10 and 40000 depending on the `block_type`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var createBlockRequest = new CreateBlockRequest?(); // CreateBlockRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Block Storage
                CreateBlock202Response result = apiInstance.CreateBlock(createBlockRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.CreateBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Block Storage
    ApiResponse<CreateBlock202Response> response = apiInstance.CreateBlockWithHttpInfo(createBlockRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.CreateBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createBlockRequest** | [**CreateBlockRequest?**](CreateBlockRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateBlock202Response**](CreateBlock202Response.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteblock"></a>
# **DeleteBlock**
> void DeleteBlock (string blockId)

Delete Block Storage

Delete Block Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var blockId = "blockId_example";  // string | The [Block Storage id](#operation/list-blocks).

            try
            {
                // Delete Block Storage
                apiInstance.DeleteBlock(blockId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.DeleteBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Block Storage
    apiInstance.DeleteBlockWithHttpInfo(blockId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.DeleteBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **blockId** | **string** | The [Block Storage id](#operation/list-blocks). |  |

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

<a id="detachblock"></a>
# **DetachBlock**
> void DetachBlock (string blockId, DetachBlockRequest? detachBlockRequest = null)

Detach Block Storage

Detach Block Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var blockId = "blockId_example";  // string | The [Block Storage id](#operation/list-blocks).
            var detachBlockRequest = new DetachBlockRequest?(); // DetachBlockRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Detach Block Storage
                apiInstance.DetachBlock(blockId, detachBlockRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.DetachBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach Block Storage
    apiInstance.DetachBlockWithHttpInfo(blockId, detachBlockRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.DetachBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **blockId** | **string** | The [Block Storage id](#operation/list-blocks). |  |
| **detachBlockRequest** | [**DetachBlockRequest?**](DetachBlockRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getblock"></a>
# **GetBlock**
> CreateBlock202Response GetBlock (string blockId)

Get Block Storage

Get information for Block Storage.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var blockId = "blockId_example";  // string | The [Block Storage id](#operation/list-blocks).

            try
            {
                // Get Block Storage
                CreateBlock202Response result = apiInstance.GetBlock(blockId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.GetBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Block Storage
    ApiResponse<CreateBlock202Response> response = apiInstance.GetBlockWithHttpInfo(blockId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.GetBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **blockId** | **string** | The [Block Storage id](#operation/list-blocks). |  |

### Return type

[**CreateBlock202Response**](CreateBlock202Response.md)

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listblocks"></a>
# **ListBlocks**
> ListBlocks200Response ListBlocks (int? perPage = null, string? cursor = null)

List Block storages

List all Block Storage in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListBlocksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Block storages
                ListBlocks200Response result = apiInstance.ListBlocks(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.ListBlocks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListBlocksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Block storages
    ApiResponse<ListBlocks200Response> response = apiInstance.ListBlocksWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.ListBlocksWithHttpInfo: " + e.Message);
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

[**ListBlocks200Response**](ListBlocks200Response.md)

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
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateblock"></a>
# **UpdateBlock**
> void UpdateBlock (string blockId, UpdateBlockRequest? updateBlockRequest = null)

Update Block Storage

Update information for Block Storage. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BlockApi(config);
            var blockId = "blockId_example";  // string | The [Block Storage id](#operation/list-blocks).
            var updateBlockRequest = new UpdateBlockRequest?(); // UpdateBlockRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Block Storage
                apiInstance.UpdateBlock(blockId, updateBlockRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BlockApi.UpdateBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Block Storage
    apiInstance.UpdateBlockWithHttpInfo(blockId, updateBlockRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BlockApi.UpdateBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **blockId** | **string** | The [Block Storage id](#operation/list-blocks). |  |
| **updateBlockRequest** | [**UpdateBlockRequest?**](UpdateBlockRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

