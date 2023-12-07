# Org.OpenAPITools.Api.VPC2Api

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AttachVpc2Nodes**](VPC2Api.md#attachvpc2nodes) | **POST** /vpc2/{vpc-id}/nodes/attach | Attach nodes to a VPC 2.0 network |
| [**CreateVpc2**](VPC2Api.md#createvpc2) | **POST** /vpc2 | Create a VPC 2.0 network |
| [**DeleteVpc2**](VPC2Api.md#deletevpc2) | **DELETE** /vpc2/{vpc-id} | Delete a VPC 2.0 network |
| [**DetachVpc2Nodes**](VPC2Api.md#detachvpc2nodes) | **POST** /vpc2/{vpc-id}/nodes/detach | Remove nodes from a VPC 2.0 network |
| [**GetVpc2**](VPC2Api.md#getvpc2) | **GET** /vpc2/{vpc-id} | Get a VPC 2.0 network |
| [**ListVpc2**](VPC2Api.md#listvpc2) | **GET** /vpc2 | List VPC 2.0 networks |
| [**ListVpc2Nodes**](VPC2Api.md#listvpc2nodes) | **GET** /vpc2/{vpc-id}/nodes | Get a list of nodes attached to a VPC 2.0 network |
| [**UpdateVpc2**](VPC2Api.md#updatevpc2) | **PUT** /vpc2/{vpc-id} | Update a VPC 2.0 network |

<a id="attachvpc2nodes"></a>
# **AttachVpc2Nodes**
> void AttachVpc2Nodes (string vpcId, AttachVpc2NodesRequest? attachVpc2NodesRequest = null)

Attach nodes to a VPC 2.0 network

Attach nodes to a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachVpc2NodesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).
            var attachVpc2NodesRequest = new AttachVpc2NodesRequest?(); // AttachVpc2NodesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach nodes to a VPC 2.0 network
                apiInstance.AttachVpc2Nodes(vpcId, attachVpc2NodesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.AttachVpc2Nodes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachVpc2NodesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach nodes to a VPC 2.0 network
    apiInstance.AttachVpc2NodesWithHttpInfo(vpcId, attachVpc2NodesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.AttachVpc2NodesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |
| **attachVpc2NodesRequest** | [**AttachVpc2NodesRequest?**](AttachVpc2NodesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **202** | accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createvpc2"></a>
# **CreateVpc2**
> GetVpc2200Response CreateVpc2 (CreateVpc2Request? createVpc2Request = null)

Create a VPC 2.0 network

Create a new VPC 2.0 network in a `region`. VPCs should use [RFC1918 private address space](https://tools.ietf.org/html/rfc1918):      10.0.0.0    - 10.255.255.255  (10/8 prefix)     172.16.0.0  - 172.31.255.255  (172.16/12 prefix)     192.168.0.0 - 192.168.255.255 (192.168/16 prefix) 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var createVpc2Request = new CreateVpc2Request?(); // CreateVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create a VPC 2.0 network
                GetVpc2200Response result = apiInstance.CreateVpc2(createVpc2Request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.CreateVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a VPC 2.0 network
    ApiResponse<GetVpc2200Response> response = apiInstance.CreateVpc2WithHttpInfo(createVpc2Request);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.CreateVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createVpc2Request** | [**CreateVpc2Request?**](CreateVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetVpc2200Response**](GetVpc2200Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletevpc2"></a>
# **DeleteVpc2**
> void DeleteVpc2 (string vpcId)

Delete a VPC 2.0 network

Delete a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).

            try
            {
                // Delete a VPC 2.0 network
                apiInstance.DeleteVpc2(vpcId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.DeleteVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a VPC 2.0 network
    apiInstance.DeleteVpc2WithHttpInfo(vpcId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.DeleteVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |

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

<a id="detachvpc2nodes"></a>
# **DetachVpc2Nodes**
> void DetachVpc2Nodes (string vpcId, DetachVpc2NodesRequest? detachVpc2NodesRequest = null)

Remove nodes from a VPC 2.0 network

Remove nodes from a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachVpc2NodesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).
            var detachVpc2NodesRequest = new DetachVpc2NodesRequest?(); // DetachVpc2NodesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Remove nodes from a VPC 2.0 network
                apiInstance.DetachVpc2Nodes(vpcId, detachVpc2NodesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.DetachVpc2Nodes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachVpc2NodesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove nodes from a VPC 2.0 network
    apiInstance.DetachVpc2NodesWithHttpInfo(vpcId, detachVpc2NodesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.DetachVpc2NodesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |
| **detachVpc2NodesRequest** | [**DetachVpc2NodesRequest?**](DetachVpc2NodesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **202** | accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getvpc2"></a>
# **GetVpc2**
> GetVpc2200Response GetVpc2 (string vpcId)

Get a VPC 2.0 network

Get information about a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).

            try
            {
                // Get a VPC 2.0 network
                GetVpc2200Response result = apiInstance.GetVpc2(vpcId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.GetVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a VPC 2.0 network
    ApiResponse<GetVpc2200Response> response = apiInstance.GetVpc2WithHttpInfo(vpcId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.GetVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |

### Return type

[**GetVpc2200Response**](GetVpc2200Response.md)

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

<a id="listvpc2"></a>
# **ListVpc2**
> ListVpc2200Response ListVpc2 (int? perPage = null, string? cursor = null)

List VPC 2.0 networks

Get a list of all VPC 2.0 networks in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List VPC 2.0 networks
                ListVpc2200Response result = apiInstance.ListVpc2(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.ListVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List VPC 2.0 networks
    ApiResponse<ListVpc2200Response> response = apiInstance.ListVpc2WithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.ListVpc2WithHttpInfo: " + e.Message);
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

[**ListVpc2200Response**](ListVpc2200Response.md)

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

<a id="listvpc2nodes"></a>
# **ListVpc2Nodes**
> GetVpc2200Response ListVpc2Nodes (string vpcId, int? perPage = null, string? cursor = null)

Get a list of nodes attached to a VPC 2.0 network

Get a list of nodes attached to a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListVpc2NodesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // Get a list of nodes attached to a VPC 2.0 network
                GetVpc2200Response result = apiInstance.ListVpc2Nodes(vpcId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.ListVpc2Nodes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListVpc2NodesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a list of nodes attached to a VPC 2.0 network
    ApiResponse<GetVpc2200Response> response = apiInstance.ListVpc2NodesWithHttpInfo(vpcId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.ListVpc2NodesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**GetVpc2200Response**](GetVpc2200Response.md)

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

<a id="updatevpc2"></a>
# **UpdateVpc2**
> void UpdateVpc2 (string vpcId, UpdateVpc2Request? updateVpc2Request = null)

Update a VPC 2.0 network

Update information for a VPC 2.0 network.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPC2Api(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).
            var updateVpc2Request = new UpdateVpc2Request?(); // UpdateVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update a VPC 2.0 network
                apiInstance.UpdateVpc2(vpcId, updateVpc2Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPC2Api.UpdateVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a VPC 2.0 network
    apiInstance.UpdateVpc2WithHttpInfo(vpcId, updateVpc2Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPC2Api.UpdateVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |
| **updateVpc2Request** | [**UpdateVpc2Request?**](UpdateVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

