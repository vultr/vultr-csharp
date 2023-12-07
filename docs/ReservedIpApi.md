# Org.OpenAPITools.Api.ReservedIpApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AttachReservedIp**](ReservedIpApi.md#attachreservedip) | **POST** /reserved-ips/{reserved-ip}/attach | Attach Reserved IP |
| [**ConvertReservedIp**](ReservedIpApi.md#convertreservedip) | **POST** /reserved-ips/convert | Convert Instance IP to Reserved IP |
| [**CreateReservedIp**](ReservedIpApi.md#createreservedip) | **POST** /reserved-ips | Create Reserved IP |
| [**DeleteReservedIp**](ReservedIpApi.md#deletereservedip) | **DELETE** /reserved-ips/{reserved-ip} | Delete Reserved IP |
| [**DetachReservedIp**](ReservedIpApi.md#detachreservedip) | **POST** /reserved-ips/{reserved-ip}/detach | Detach Reserved IP |
| [**GetReservedIp**](ReservedIpApi.md#getreservedip) | **GET** /reserved-ips/{reserved-ip} | Get Reserved IP |
| [**ListReservedIps**](ReservedIpApi.md#listreservedips) | **GET** /reserved-ips | List Reserved IPs |
| [**PatchReservedIpsReservedIp**](ReservedIpApi.md#patchreservedipsreservedip) | **PATCH** /reserved-ips/{reserved-ip} | Update Reserved IP |

<a id="attachreservedip"></a>
# **AttachReservedIp**
> void AttachReservedIp (string reservedIp, AttachReservedIpRequest? attachReservedIpRequest = null)

Attach Reserved IP

Attach a Reserved IP to an compute instance or a baremetal instance - `instance_id`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var reservedIp = "reservedIp_example";  // string | The [Reserved IP id](#operation/list-reserved-ips)
            var attachReservedIpRequest = new AttachReservedIpRequest?(); // AttachReservedIpRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach Reserved IP
                apiInstance.AttachReservedIp(reservedIp, attachReservedIpRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.AttachReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach Reserved IP
    apiInstance.AttachReservedIpWithHttpInfo(reservedIp, attachReservedIpRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.AttachReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **reservedIp** | **string** | The [Reserved IP id](#operation/list-reserved-ips) |  |
| **attachReservedIpRequest** | [**AttachReservedIpRequest?**](AttachReservedIpRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="convertreservedip"></a>
# **ConvertReservedIp**
> GetReservedIp200Response ConvertReservedIp (ConvertReservedIpRequest? convertReservedIpRequest = null)

Convert Instance IP to Reserved IP

Convert the `ip_address` of an existing [instance](#operation/list-instances) into a Reserved IP.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ConvertReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var convertReservedIpRequest = new ConvertReservedIpRequest?(); // ConvertReservedIpRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Convert Instance IP to Reserved IP
                GetReservedIp200Response result = apiInstance.ConvertReservedIp(convertReservedIpRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.ConvertReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ConvertReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Convert Instance IP to Reserved IP
    ApiResponse<GetReservedIp200Response> response = apiInstance.ConvertReservedIpWithHttpInfo(convertReservedIpRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.ConvertReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **convertReservedIpRequest** | [**ConvertReservedIpRequest?**](ConvertReservedIpRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetReservedIp200Response**](GetReservedIp200Response.md)

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

<a id="createreservedip"></a>
# **CreateReservedIp**
> GetReservedIp200Response CreateReservedIp (CreateReservedIpRequest? createReservedIpRequest = null)

Create Reserved IP

Create a new Reserved IP. The `region` and `ip_type` attributes are required.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var createReservedIpRequest = new CreateReservedIpRequest?(); // CreateReservedIpRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Reserved IP
                GetReservedIp200Response result = apiInstance.CreateReservedIp(createReservedIpRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.CreateReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Reserved IP
    ApiResponse<GetReservedIp200Response> response = apiInstance.CreateReservedIpWithHttpInfo(createReservedIpRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.CreateReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createReservedIpRequest** | [**CreateReservedIpRequest?**](CreateReservedIpRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetReservedIp200Response**](GetReservedIp200Response.md)

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

<a id="deletereservedip"></a>
# **DeleteReservedIp**
> void DeleteReservedIp (string reservedIp)

Delete Reserved IP

Delete a Reserved IP.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var reservedIp = "reservedIp_example";  // string | The [Reserved IP id](#operation/list-reserved-ips).

            try
            {
                // Delete Reserved IP
                apiInstance.DeleteReservedIp(reservedIp);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.DeleteReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Reserved IP
    apiInstance.DeleteReservedIpWithHttpInfo(reservedIp);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.DeleteReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **reservedIp** | **string** | The [Reserved IP id](#operation/list-reserved-ips). |  |

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="detachreservedip"></a>
# **DetachReservedIp**
> void DetachReservedIp (string reservedIp)

Detach Reserved IP

Detach a Reserved IP.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var reservedIp = "reservedIp_example";  // string | The [Reserved IP id](#operation/list-reserved-ips)

            try
            {
                // Detach Reserved IP
                apiInstance.DetachReservedIp(reservedIp);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.DetachReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach Reserved IP
    apiInstance.DetachReservedIpWithHttpInfo(reservedIp);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.DetachReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **reservedIp** | **string** | The [Reserved IP id](#operation/list-reserved-ips) |  |

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

<a id="getreservedip"></a>
# **GetReservedIp**
> GetReservedIp200Response GetReservedIp (string reservedIp)

Get Reserved IP

Get information about a Reserved IP.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var reservedIp = "reservedIp_example";  // string | The [Reserved IP id](#operation/list-reserved-ips).

            try
            {
                // Get Reserved IP
                GetReservedIp200Response result = apiInstance.GetReservedIp(reservedIp);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.GetReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Reserved IP
    ApiResponse<GetReservedIp200Response> response = apiInstance.GetReservedIpWithHttpInfo(reservedIp);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.GetReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **reservedIp** | **string** | The [Reserved IP id](#operation/list-reserved-ips). |  |

### Return type

[**GetReservedIp200Response**](GetReservedIp200Response.md)

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

<a id="listreservedips"></a>
# **ListReservedIps**
> ListReservedIps200Response ListReservedIps (int? perPage = null, string? cursor = null)

List Reserved IPs

List all Reserved IPs in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListReservedIpsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Reserved IPs
                ListReservedIps200Response result = apiInstance.ListReservedIps(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.ListReservedIps: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListReservedIpsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Reserved IPs
    ApiResponse<ListReservedIps200Response> response = apiInstance.ListReservedIpsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.ListReservedIpsWithHttpInfo: " + e.Message);
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

[**ListReservedIps200Response**](ListReservedIps200Response.md)

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

<a id="patchreservedipsreservedip"></a>
# **PatchReservedIpsReservedIp**
> GetReservedIp200Response PatchReservedIpsReservedIp (string reservedIp, PatchReservedIpsReservedIpRequest? patchReservedIpsReservedIpRequest = null)

Update Reserved IP

Update information on a Reserved IP.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PatchReservedIpsReservedIpExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ReservedIpApi(config);
            var reservedIp = "reservedIp_example";  // string | The [Reserved IP id](#operation/list-reserved-ips).
            var patchReservedIpsReservedIpRequest = new PatchReservedIpsReservedIpRequest?(); // PatchReservedIpsReservedIpRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Reserved IP
                GetReservedIp200Response result = apiInstance.PatchReservedIpsReservedIp(reservedIp, patchReservedIpsReservedIpRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ReservedIpApi.PatchReservedIpsReservedIp: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PatchReservedIpsReservedIpWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Reserved IP
    ApiResponse<GetReservedIp200Response> response = apiInstance.PatchReservedIpsReservedIpWithHttpInfo(reservedIp, patchReservedIpsReservedIpRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ReservedIpApi.PatchReservedIpsReservedIpWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **reservedIp** | **string** | The [Reserved IP id](#operation/list-reserved-ips). |  |
| **patchReservedIpsReservedIpRequest** | [**PatchReservedIpsReservedIpRequest?**](PatchReservedIpsReservedIpRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetReservedIp200Response**](GetReservedIp200Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

