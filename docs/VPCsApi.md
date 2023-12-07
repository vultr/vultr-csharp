# Org.OpenAPITools.Api.VPCsApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateVpc**](VPCsApi.md#createvpc) | **POST** /vpcs | Create a VPC |
| [**DeleteVpc**](VPCsApi.md#deletevpc) | **DELETE** /vpcs/{vpc-id} | Delete a VPC |
| [**GetVpc**](VPCsApi.md#getvpc) | **GET** /vpcs/{vpc-id} | Get a VPC |
| [**ListVpcs**](VPCsApi.md#listvpcs) | **GET** /vpcs | List VPCs |
| [**UpdateVpc**](VPCsApi.md#updatevpc) | **PUT** /vpcs/{vpc-id} | Update a VPC |

<a id="createvpc"></a>
# **CreateVpc**
> GetVpc200Response CreateVpc (CreateVpcRequest? createVpcRequest = null)

Create a VPC

Create a new VPC in a `region`. VPCs should use [RFC1918 private address space](https://tools.ietf.org/html/rfc1918):      10.0.0.0    - 10.255.255.255  (10/8 prefix)     172.16.0.0  - 172.31.255.255  (172.16/12 prefix)     192.168.0.0 - 192.168.255.255 (192.168/16 prefix) 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPCsApi(config);
            var createVpcRequest = new CreateVpcRequest?(); // CreateVpcRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create a VPC
                GetVpc200Response result = apiInstance.CreateVpc(createVpcRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPCsApi.CreateVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a VPC
    ApiResponse<GetVpc200Response> response = apiInstance.CreateVpcWithHttpInfo(createVpcRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPCsApi.CreateVpcWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createVpcRequest** | [**CreateVpcRequest?**](CreateVpcRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetVpc200Response**](GetVpc200Response.md)

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

<a id="deletevpc"></a>
# **DeleteVpc**
> void DeleteVpc (string vpcId)

Delete a VPC

Delete a VPC.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPCsApi(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).

            try
            {
                // Delete a VPC
                apiInstance.DeleteVpc(vpcId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPCsApi.DeleteVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete a VPC
    apiInstance.DeleteVpcWithHttpInfo(vpcId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPCsApi.DeleteVpcWithHttpInfo: " + e.Message);
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

<a id="getvpc"></a>
# **GetVpc**
> GetVpc200Response GetVpc (string vpcId)

Get a VPC

Get information about a VPC.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPCsApi(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).

            try
            {
                // Get a VPC
                GetVpc200Response result = apiInstance.GetVpc(vpcId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPCsApi.GetVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get a VPC
    ApiResponse<GetVpc200Response> response = apiInstance.GetVpcWithHttpInfo(vpcId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPCsApi.GetVpcWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |

### Return type

[**GetVpc200Response**](GetVpc200Response.md)

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

<a id="listvpcs"></a>
# **ListVpcs**
> ListVpcs200Response ListVpcs (int? perPage = null, string? cursor = null)

List VPCs

Get a list of all VPCs in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListVpcsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPCsApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List VPCs
                ListVpcs200Response result = apiInstance.ListVpcs(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPCsApi.ListVpcs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListVpcsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List VPCs
    ApiResponse<ListVpcs200Response> response = apiInstance.ListVpcsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPCsApi.ListVpcsWithHttpInfo: " + e.Message);
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

[**ListVpcs200Response**](ListVpcs200Response.md)

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

<a id="updatevpc"></a>
# **UpdateVpc**
> void UpdateVpc (string vpcId, UpdateVpcRequest? updateVpcRequest = null)

Update a VPC

Update information for a VPC.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new VPCsApi(config);
            var vpcId = "vpcId_example";  // string | The [VPC ID](#operation/list-vpcs).
            var updateVpcRequest = new UpdateVpcRequest?(); // UpdateVpcRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update a VPC
                apiInstance.UpdateVpc(vpcId, updateVpcRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling VPCsApi.UpdateVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a VPC
    apiInstance.UpdateVpcWithHttpInfo(vpcId, updateVpcRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling VPCsApi.UpdateVpcWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vpcId** | **string** | The [VPC ID](#operation/list-vpcs). |  |
| **updateVpcRequest** | [**UpdateVpcRequest?**](UpdateVpcRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

