# Org.OpenAPITools.Api.StartupApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateStartupScript**](StartupApi.md#createstartupscript) | **POST** /startup-scripts | Create Startup Script |
| [**DeleteStartupScript**](StartupApi.md#deletestartupscript) | **DELETE** /startup-scripts/{startup-id} | Delete Startup Script |
| [**GetStartupScript**](StartupApi.md#getstartupscript) | **GET** /startup-scripts/{startup-id} | Get Startup Script |
| [**ListStartupScripts**](StartupApi.md#liststartupscripts) | **GET** /startup-scripts | List Startup Scripts |
| [**UpdateStartupScript**](StartupApi.md#updatestartupscript) | **PATCH** /startup-scripts/{startup-id} | Update Startup Script |

<a id="createstartupscript"></a>
# **CreateStartupScript**
> GetStartupScript200Response CreateStartupScript (CreateStartupScriptRequest? createStartupScriptRequest = null)

Create Startup Script

Create a new Startup Script. The `name` and `script` attributes are required, and scripts are base-64 encoded.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateStartupScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new StartupApi(config);
            var createStartupScriptRequest = new CreateStartupScriptRequest?(); // CreateStartupScriptRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Startup Script
                GetStartupScript200Response result = apiInstance.CreateStartupScript(createStartupScriptRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StartupApi.CreateStartupScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateStartupScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Startup Script
    ApiResponse<GetStartupScript200Response> response = apiInstance.CreateStartupScriptWithHttpInfo(createStartupScriptRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StartupApi.CreateStartupScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createStartupScriptRequest** | [**CreateStartupScriptRequest?**](CreateStartupScriptRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetStartupScript200Response**](GetStartupScript200Response.md)

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

<a id="deletestartupscript"></a>
# **DeleteStartupScript**
> void DeleteStartupScript (string startupId)

Delete Startup Script

Delete a Startup Script.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteStartupScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new StartupApi(config);
            var startupId = "startupId_example";  // string | The [Startup Script id](#operation/list-startup-scripts).

            try
            {
                // Delete Startup Script
                apiInstance.DeleteStartupScript(startupId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StartupApi.DeleteStartupScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteStartupScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Startup Script
    apiInstance.DeleteStartupScriptWithHttpInfo(startupId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StartupApi.DeleteStartupScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startupId** | **string** | The [Startup Script id](#operation/list-startup-scripts). |  |

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

<a id="getstartupscript"></a>
# **GetStartupScript**
> GetStartupScript200Response GetStartupScript (string startupId)

Get Startup Script

Get information for a Startup Script.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetStartupScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new StartupApi(config);
            var startupId = "startupId_example";  // string | The [Startup Script id](#operation/list-startup-scripts).

            try
            {
                // Get Startup Script
                GetStartupScript200Response result = apiInstance.GetStartupScript(startupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StartupApi.GetStartupScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetStartupScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Startup Script
    ApiResponse<GetStartupScript200Response> response = apiInstance.GetStartupScriptWithHttpInfo(startupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StartupApi.GetStartupScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startupId** | **string** | The [Startup Script id](#operation/list-startup-scripts). |  |

### Return type

[**GetStartupScript200Response**](GetStartupScript200Response.md)

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

<a id="liststartupscripts"></a>
# **ListStartupScripts**
> ListStartupScripts200Response ListStartupScripts (int? perPage = null, string? cursor = null)

List Startup Scripts

Get a list of all Startup Scripts.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListStartupScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new StartupApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Startup Scripts
                ListStartupScripts200Response result = apiInstance.ListStartupScripts(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StartupApi.ListStartupScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListStartupScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Startup Scripts
    ApiResponse<ListStartupScripts200Response> response = apiInstance.ListStartupScriptsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StartupApi.ListStartupScriptsWithHttpInfo: " + e.Message);
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

[**ListStartupScripts200Response**](ListStartupScripts200Response.md)

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

<a id="updatestartupscript"></a>
# **UpdateStartupScript**
> void UpdateStartupScript (string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = null)

Update Startup Script

Update a Startup Script. The attributes `name` and `script` are optional. If not set, the attributes will retain their original values. The `script` attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateStartupScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new StartupApi(config);
            var startupId = "startupId_example";  // string | The [Startup Script id](#operation/list-startup-scripts).
            var updateStartupScriptRequest = new UpdateStartupScriptRequest?(); // UpdateStartupScriptRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Startup Script
                apiInstance.UpdateStartupScript(startupId, updateStartupScriptRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StartupApi.UpdateStartupScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateStartupScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Startup Script
    apiInstance.UpdateStartupScriptWithHttpInfo(startupId, updateStartupScriptRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StartupApi.UpdateStartupScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startupId** | **string** | The [Startup Script id](#operation/list-startup-scripts). |  |
| **updateStartupScriptRequest** | [**UpdateStartupScriptRequest?**](UpdateStartupScriptRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

