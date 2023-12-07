# Org.OpenAPITools.Api.BaremetalApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AttachBaremetalsVpc2**](BaremetalApi.md#attachbaremetalsvpc2) | **POST** /bare-metals/{baremetal-id}/vpc2/attach | Attach VPC 2.0 Network to Bare Metal Instance |
| [**CreateBaremetal**](BaremetalApi.md#createbaremetal) | **POST** /bare-metals | Create Bare Metal Instance |
| [**DeleteBaremetal**](BaremetalApi.md#deletebaremetal) | **DELETE** /bare-metals/{baremetal-id} | Delete Bare Metal |
| [**DetachBaremetalVpc2**](BaremetalApi.md#detachbaremetalvpc2) | **POST** /bare-metals/{baremetal-id}/vpc2/detach | Detach VPC 2.0 Network from Bare Metal Instance |
| [**GetBandwidthBaremetal**](BaremetalApi.md#getbandwidthbaremetal) | **GET** /bare-metals/{baremetal-id}/bandwidth | Bare Metal Bandwidth |
| [**GetBareMetalUserdata**](BaremetalApi.md#getbaremetaluserdata) | **GET** /bare-metals/{baremetal-id}/user-data | Get Bare Metal User Data |
| [**GetBareMetalVnc**](BaremetalApi.md#getbaremetalvnc) | **GET** /bare-metals/{baremetal-id}/vnc | Get VNC URL for a Bare Metal |
| [**GetBareMetalsUpgrades**](BaremetalApi.md#getbaremetalsupgrades) | **GET** /bare-metals/{baremetal-id}/upgrades | Get Available Bare Metal Upgrades |
| [**GetBaremetal**](BaremetalApi.md#getbaremetal) | **GET** /bare-metals/{baremetal-id} | Get Bare Metal |
| [**GetIpv4Baremetal**](BaremetalApi.md#getipv4baremetal) | **GET** /bare-metals/{baremetal-id}/ipv4 | Bare Metal IPv4 Addresses |
| [**GetIpv6Baremetal**](BaremetalApi.md#getipv6baremetal) | **GET** /bare-metals/{baremetal-id}/ipv6 | Bare Metal IPv6 Addresses |
| [**HaltBaremetal**](BaremetalApi.md#haltbaremetal) | **POST** /bare-metals/{baremetal-id}/halt | Halt Bare Metal |
| [**HaltBaremetals**](BaremetalApi.md#haltbaremetals) | **POST** /bare-metals/halt | Halt Bare Metals |
| [**ListBaremetalVpc2**](BaremetalApi.md#listbaremetalvpc2) | **GET** /bare-metals/{baremetal-id}/vpc2 | List Bare Metal Instance VPC 2.0 Networks |
| [**ListBaremetals**](BaremetalApi.md#listbaremetals) | **GET** /bare-metals | List Bare Metal Instances |
| [**RebootBareMetals**](BaremetalApi.md#rebootbaremetals) | **POST** /bare-metals/reboot | Reboot Bare Metals |
| [**RebootBaremetal**](BaremetalApi.md#rebootbaremetal) | **POST** /bare-metals/{baremetal-id}/reboot | Reboot Bare Metal |
| [**ReinstallBaremetal**](BaremetalApi.md#reinstallbaremetal) | **POST** /bare-metals/{baremetal-id}/reinstall | Reinstall Bare Metal |
| [**StartBareMetals**](BaremetalApi.md#startbaremetals) | **POST** /bare-metals/start | Start Bare Metals |
| [**StartBaremetal**](BaremetalApi.md#startbaremetal) | **POST** /bare-metals/{baremetal-id}/start | Start Bare Metal |
| [**UpdateBaremetal**](BaremetalApi.md#updatebaremetal) | **PATCH** /bare-metals/{baremetal-id} | Update Bare Metal |

<a id="attachbaremetalsvpc2"></a>
# **AttachBaremetalsVpc2**
> void AttachBaremetalsVpc2 (string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = null)

Attach VPC 2.0 Network to Bare Metal Instance

Attach a VPC 2.0 Network to a Bare Metal Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachBaremetalsVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal ID](#operation/list-baremetals).
            var attachBaremetalsVpc2Request = new AttachBaremetalsVpc2Request?(); // AttachBaremetalsVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach VPC 2.0 Network to Bare Metal Instance
                apiInstance.AttachBaremetalsVpc2(baremetalId, attachBaremetalsVpc2Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.AttachBaremetalsVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachBaremetalsVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach VPC 2.0 Network to Bare Metal Instance
    apiInstance.AttachBaremetalsVpc2WithHttpInfo(baremetalId, attachBaremetalsVpc2Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.AttachBaremetalsVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal ID](#operation/list-baremetals). |  |
| **attachBaremetalsVpc2Request** | [**AttachBaremetalsVpc2Request?**](AttachBaremetalsVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="createbaremetal"></a>
# **CreateBaremetal**
> CreateBaremetal202Response CreateBaremetal (CreateBaremetalRequest? createBaremetalRequest = null)

Create Bare Metal Instance

Create a new Bare Metal instance in a `region` with the desired `plan`. Choose one of the following to deploy the instance:  * `os_id` * `snapshot_id` * `app_id` * `image_id`  Supply other attributes as desired.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var createBaremetalRequest = new CreateBaremetalRequest?(); // CreateBaremetalRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Bare Metal Instance
                CreateBaremetal202Response result = apiInstance.CreateBaremetal(createBaremetalRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.CreateBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Bare Metal Instance
    ApiResponse<CreateBaremetal202Response> response = apiInstance.CreateBaremetalWithHttpInfo(createBaremetalRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.CreateBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createBaremetalRequest** | [**CreateBaremetalRequest?**](CreateBaremetalRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateBaremetal202Response**](CreateBaremetal202Response.md)

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

<a id="deletebaremetal"></a>
# **DeleteBaremetal**
> void DeleteBaremetal (string baremetalId)

Delete Bare Metal

Delete a Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Delete Bare Metal
                apiInstance.DeleteBaremetal(baremetalId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.DeleteBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Bare Metal
    apiInstance.DeleteBaremetalWithHttpInfo(baremetalId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.DeleteBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

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

<a id="detachbaremetalvpc2"></a>
# **DetachBaremetalVpc2**
> void DetachBaremetalVpc2 (string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = null)

Detach VPC 2.0 Network from Bare Metal Instance

Detach a VPC 2.0 Network from an Bare Metal Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachBaremetalVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [bare-metal ID](#operation/list-baremetals).
            var detachBaremetalVpc2Request = new DetachBaremetalVpc2Request?(); // DetachBaremetalVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Detach VPC 2.0 Network from Bare Metal Instance
                apiInstance.DetachBaremetalVpc2(baremetalId, detachBaremetalVpc2Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.DetachBaremetalVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachBaremetalVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach VPC 2.0 Network from Bare Metal Instance
    apiInstance.DetachBaremetalVpc2WithHttpInfo(baremetalId, detachBaremetalVpc2Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.DetachBaremetalVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [bare-metal ID](#operation/list-baremetals). |  |
| **detachBaremetalVpc2Request** | [**DetachBaremetalVpc2Request?**](DetachBaremetalVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="getbandwidthbaremetal"></a>
# **GetBandwidthBaremetal**
> GetBandwidthBaremetal200Response GetBandwidthBaremetal (string baremetalId)

Bare Metal Bandwidth

Get bandwidth information for the Bare Metal instance.<br><br>The `bandwidth` object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBandwidthBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Bare Metal Bandwidth
                GetBandwidthBaremetal200Response result = apiInstance.GetBandwidthBaremetal(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetBandwidthBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBandwidthBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Bare Metal Bandwidth
    ApiResponse<GetBandwidthBaremetal200Response> response = apiInstance.GetBandwidthBaremetalWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetBandwidthBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetBandwidthBaremetal200Response**](GetBandwidthBaremetal200Response.md)

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

<a id="getbaremetaluserdata"></a>
# **GetBareMetalUserdata**
> GetBareMetalUserdata200Response GetBareMetalUserdata (string baremetalId)

Get Bare Metal User Data

Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBareMetalUserdataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Get Bare Metal User Data
                GetBareMetalUserdata200Response result = apiInstance.GetBareMetalUserdata(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetBareMetalUserdata: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBareMetalUserdataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Bare Metal User Data
    ApiResponse<GetBareMetalUserdata200Response> response = apiInstance.GetBareMetalUserdataWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetBareMetalUserdataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetBareMetalUserdata200Response**](GetBareMetalUserdata200Response.md)

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

<a id="getbaremetalvnc"></a>
# **GetBareMetalVnc**
> GetBareMetalVnc200Response GetBareMetalVnc (string baremetalId)

Get VNC URL for a Bare Metal

Get the VNC URL for a Bare Metal

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBareMetalVncExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Get VNC URL for a Bare Metal
                GetBareMetalVnc200Response result = apiInstance.GetBareMetalVnc(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetBareMetalVnc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBareMetalVncWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get VNC URL for a Bare Metal
    ApiResponse<GetBareMetalVnc200Response> response = apiInstance.GetBareMetalVncWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetBareMetalVncWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetBareMetalVnc200Response**](GetBareMetalVnc200Response.md)

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

<a id="getbaremetalsupgrades"></a>
# **GetBareMetalsUpgrades**
> GetBareMetalsUpgrades200Response GetBareMetalsUpgrades (string baremetalId, string? type = null)

Get Available Bare Metal Upgrades

Get available upgrades for a Bare Metal

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBareMetalsUpgradesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).
            var type = "type_example";  // string? | Filter upgrade by type:  - all (applications, plans) - applications - os (optional) 

            try
            {
                // Get Available Bare Metal Upgrades
                GetBareMetalsUpgrades200Response result = apiInstance.GetBareMetalsUpgrades(baremetalId, type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetBareMetalsUpgrades: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBareMetalsUpgradesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Available Bare Metal Upgrades
    ApiResponse<GetBareMetalsUpgrades200Response> response = apiInstance.GetBareMetalsUpgradesWithHttpInfo(baremetalId, type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetBareMetalsUpgradesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |
| **type** | **string?** | Filter upgrade by type:  - all (applications, plans) - applications - os | [optional]  |

### Return type

[**GetBareMetalsUpgrades200Response**](GetBareMetalsUpgrades200Response.md)

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

<a id="getbaremetal"></a>
# **GetBaremetal**
> GetBaremetal200Response GetBaremetal (string baremetalId)

Get Bare Metal

Get information for a Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Get Bare Metal
                GetBaremetal200Response result = apiInstance.GetBaremetal(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Bare Metal
    ApiResponse<GetBaremetal200Response> response = apiInstance.GetBaremetalWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetBaremetal200Response**](GetBaremetal200Response.md)

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

<a id="getipv4baremetal"></a>
# **GetIpv4Baremetal**
> GetIpv4Baremetal200Response GetIpv4Baremetal (string baremetalId)

Bare Metal IPv4 Addresses

Get the IPv4 information for the Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetIpv4BaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Bare Metal IPv4 Addresses
                GetIpv4Baremetal200Response result = apiInstance.GetIpv4Baremetal(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetIpv4Baremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetIpv4BaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Bare Metal IPv4 Addresses
    ApiResponse<GetIpv4Baremetal200Response> response = apiInstance.GetIpv4BaremetalWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetIpv4BaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetIpv4Baremetal200Response**](GetIpv4Baremetal200Response.md)

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

<a id="getipv6baremetal"></a>
# **GetIpv6Baremetal**
> GetIpv6Baremetal200Response GetIpv6Baremetal (string baremetalId)

Bare Metal IPv6 Addresses

Get the IPv6 information for the Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetIpv6BaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Bare Metal IPv6 Addresses
                GetIpv6Baremetal200Response result = apiInstance.GetIpv6Baremetal(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.GetIpv6Baremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetIpv6BaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Bare Metal IPv6 Addresses
    ApiResponse<GetIpv6Baremetal200Response> response = apiInstance.GetIpv6BaremetalWithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.GetIpv6BaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

### Return type

[**GetIpv6Baremetal200Response**](GetIpv6Baremetal200Response.md)

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

<a id="haltbaremetal"></a>
# **HaltBaremetal**
> void HaltBaremetal (string baremetalId)

Halt Bare Metal

Halt the Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class HaltBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Halt Bare Metal
                apiInstance.HaltBaremetal(baremetalId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.HaltBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the HaltBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Halt Bare Metal
    apiInstance.HaltBaremetalWithHttpInfo(baremetalId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.HaltBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

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

<a id="haltbaremetals"></a>
# **HaltBaremetals**
> void HaltBaremetals (HaltBaremetalsRequest? haltBaremetalsRequest = null)

Halt Bare Metals

Halt Bare Metals.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class HaltBaremetalsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var haltBaremetalsRequest = new HaltBaremetalsRequest?(); // HaltBaremetalsRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Halt Bare Metals
                apiInstance.HaltBaremetals(haltBaremetalsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.HaltBaremetals: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the HaltBaremetalsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Halt Bare Metals
    apiInstance.HaltBaremetalsWithHttpInfo(haltBaremetalsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.HaltBaremetalsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **haltBaremetalsRequest** | [**HaltBaremetalsRequest?**](HaltBaremetalsRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="listbaremetalvpc2"></a>
# **ListBaremetalVpc2**
> ListBaremetalVpc2200Response ListBaremetalVpc2 (string baremetalId)

List Bare Metal Instance VPC 2.0 Networks

List the VPC 2.0 networks for a Bare Metal Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListBaremetalVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal ID](#operation/list-baremetals).

            try
            {
                // List Bare Metal Instance VPC 2.0 Networks
                ListBaremetalVpc2200Response result = apiInstance.ListBaremetalVpc2(baremetalId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.ListBaremetalVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListBaremetalVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Bare Metal Instance VPC 2.0 Networks
    ApiResponse<ListBaremetalVpc2200Response> response = apiInstance.ListBaremetalVpc2WithHttpInfo(baremetalId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.ListBaremetalVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal ID](#operation/list-baremetals). |  |

### Return type

[**ListBaremetalVpc2200Response**](ListBaremetalVpc2200Response.md)

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

<a id="listbaremetals"></a>
# **ListBaremetals**
> ListBaremetals200Response ListBaremetals (int? perPage = null, string? cursor = null)

List Bare Metal Instances

List all Bare Metal instances in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListBaremetalsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Bare Metal Instances
                ListBaremetals200Response result = apiInstance.ListBaremetals(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.ListBaremetals: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListBaremetalsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Bare Metal Instances
    ApiResponse<ListBaremetals200Response> response = apiInstance.ListBaremetalsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.ListBaremetalsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500.  | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListBaremetals200Response**](ListBaremetals200Response.md)

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

<a id="rebootbaremetals"></a>
# **RebootBareMetals**
> void RebootBareMetals (HaltBaremetalsRequest? haltBaremetalsRequest = null)

Reboot Bare Metals

Reboot Bare Metals.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RebootBareMetalsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var haltBaremetalsRequest = new HaltBaremetalsRequest?(); // HaltBaremetalsRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Reboot Bare Metals
                apiInstance.RebootBareMetals(haltBaremetalsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.RebootBareMetals: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RebootBareMetalsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reboot Bare Metals
    apiInstance.RebootBareMetalsWithHttpInfo(haltBaremetalsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.RebootBareMetalsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **haltBaremetalsRequest** | [**HaltBaremetalsRequest?**](HaltBaremetalsRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="rebootbaremetal"></a>
# **RebootBaremetal**
> void RebootBaremetal (string baremetalId)

Reboot Bare Metal

Reboot the Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RebootBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Reboot Bare Metal
                apiInstance.RebootBaremetal(baremetalId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.RebootBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RebootBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reboot Bare Metal
    apiInstance.RebootBaremetalWithHttpInfo(baremetalId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.RebootBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

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

<a id="reinstallbaremetal"></a>
# **ReinstallBaremetal**
> GetBaremetal200Response ReinstallBaremetal (string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = null)

Reinstall Bare Metal

Reinstall the Bare Metal instance using an optional `hostname`.   **Note:** This action may take some time to complete.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ReinstallBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).
            var reinstallBaremetalRequest = new ReinstallBaremetalRequest?(); // ReinstallBaremetalRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Reinstall Bare Metal
                GetBaremetal200Response result = apiInstance.ReinstallBaremetal(baremetalId, reinstallBaremetalRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.ReinstallBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReinstallBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reinstall Bare Metal
    ApiResponse<GetBaremetal200Response> response = apiInstance.ReinstallBaremetalWithHttpInfo(baremetalId, reinstallBaremetalRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.ReinstallBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |
| **reinstallBaremetalRequest** | [**ReinstallBaremetalRequest?**](ReinstallBaremetalRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetBaremetal200Response**](GetBaremetal200Response.md)

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

<a id="startbaremetals"></a>
# **StartBareMetals**
> void StartBareMetals (HaltBaremetalsRequest? haltBaremetalsRequest = null)

Start Bare Metals

Start Bare Metals.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartBareMetalsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var haltBaremetalsRequest = new HaltBaremetalsRequest?(); // HaltBaremetalsRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Start Bare Metals
                apiInstance.StartBareMetals(haltBaremetalsRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.StartBareMetals: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartBareMetalsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Bare Metals
    apiInstance.StartBareMetalsWithHttpInfo(haltBaremetalsRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.StartBareMetalsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **haltBaremetalsRequest** | [**HaltBaremetalsRequest?**](HaltBaremetalsRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="startbaremetal"></a>
# **StartBaremetal**
> void StartBaremetal (string baremetalId)

Start Bare Metal

Start the Bare Metal instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).

            try
            {
                // Start Bare Metal
                apiInstance.StartBaremetal(baremetalId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.StartBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Bare Metal
    apiInstance.StartBaremetalWithHttpInfo(baremetalId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.StartBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |

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

<a id="updatebaremetal"></a>
# **UpdateBaremetal**
> GetBaremetal200Response UpdateBaremetal (string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = null)

Update Bare Metal

Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing `os_id`, `app_id` or `image_id` may take a few extra seconds to complete.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateBaremetalExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BaremetalApi(config);
            var baremetalId = "baremetalId_example";  // string | The [Bare Metal id](#operation/list-baremetals).
            var updateBaremetalRequest = new UpdateBaremetalRequest?(); // UpdateBaremetalRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Bare Metal
                GetBaremetal200Response result = apiInstance.UpdateBaremetal(baremetalId, updateBaremetalRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BaremetalApi.UpdateBaremetal: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateBaremetalWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Bare Metal
    ApiResponse<GetBaremetal200Response> response = apiInstance.UpdateBaremetalWithHttpInfo(baremetalId, updateBaremetalRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BaremetalApi.UpdateBaremetalWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **baremetalId** | **string** | The [Bare Metal id](#operation/list-baremetals). |  |
| **updateBaremetalRequest** | [**UpdateBaremetalRequest?**](UpdateBaremetalRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetBaremetal200Response**](GetBaremetal200Response.md)

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

