# Org.OpenAPITools.Api.SshApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateSshKey**](SshApi.md#createsshkey) | **POST** /ssh-keys | Create SSH key |
| [**DeleteSshKey**](SshApi.md#deletesshkey) | **DELETE** /ssh-keys/{ssh-key-id} | Delete SSH Key |
| [**GetSshKey**](SshApi.md#getsshkey) | **GET** /ssh-keys/{ssh-key-id} | Get SSH Key |
| [**ListSshKeys**](SshApi.md#listsshkeys) | **GET** /ssh-keys | List SSH Keys |
| [**UpdateSshKey**](SshApi.md#updatesshkey) | **PATCH** /ssh-keys/{ssh-key-id} | Update SSH Key |

<a id="createsshkey"></a>
# **CreateSshKey**
> GetSshKey200Response CreateSshKey (CreateSshKeyRequest? createSshKeyRequest = null)

Create SSH key

Create a new SSH Key for use with future instances. This does not update any running instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateSshKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SshApi(config);
            var createSshKeyRequest = new CreateSshKeyRequest?(); // CreateSshKeyRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create SSH key
                GetSshKey200Response result = apiInstance.CreateSshKey(createSshKeyRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SshApi.CreateSshKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateSshKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create SSH key
    ApiResponse<GetSshKey200Response> response = apiInstance.CreateSshKeyWithHttpInfo(createSshKeyRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SshApi.CreateSshKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createSshKeyRequest** | [**CreateSshKeyRequest?**](CreateSshKeyRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**GetSshKey200Response**](GetSshKey200Response.md)

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

<a id="deletesshkey"></a>
# **DeleteSshKey**
> void DeleteSshKey (string sshKeyId)

Delete SSH Key

Delete an SSH Key.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteSshKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SshApi(config);
            var sshKeyId = "sshKeyId_example";  // string | The [SSH Key id](#operation/list-ssh-keys).

            try
            {
                // Delete SSH Key
                apiInstance.DeleteSshKey(sshKeyId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SshApi.DeleteSshKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteSshKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete SSH Key
    apiInstance.DeleteSshKeyWithHttpInfo(sshKeyId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SshApi.DeleteSshKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sshKeyId** | **string** | The [SSH Key id](#operation/list-ssh-keys). |  |

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

<a id="getsshkey"></a>
# **GetSshKey**
> GetSshKey200Response GetSshKey (string sshKeyId)

Get SSH Key

Get information about an SSH Key.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetSshKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SshApi(config);
            var sshKeyId = "sshKeyId_example";  // string | The [SSH Key id](#operation/list-ssh-keys).

            try
            {
                // Get SSH Key
                GetSshKey200Response result = apiInstance.GetSshKey(sshKeyId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SshApi.GetSshKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetSshKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get SSH Key
    ApiResponse<GetSshKey200Response> response = apiInstance.GetSshKeyWithHttpInfo(sshKeyId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SshApi.GetSshKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sshKeyId** | **string** | The [SSH Key id](#operation/list-ssh-keys). |  |

### Return type

[**GetSshKey200Response**](GetSshKey200Response.md)

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

<a id="listsshkeys"></a>
# **ListSshKeys**
> ListSshKeys200Response ListSshKeys (int? perPage = null, string? cursor = null)

List SSH Keys

List all SSH Keys in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListSshKeysExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SshApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List SSH Keys
                ListSshKeys200Response result = apiInstance.ListSshKeys(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SshApi.ListSshKeys: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListSshKeysWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List SSH Keys
    ApiResponse<ListSshKeys200Response> response = apiInstance.ListSshKeysWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SshApi.ListSshKeysWithHttpInfo: " + e.Message);
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

[**ListSshKeys200Response**](ListSshKeys200Response.md)

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

<a id="updatesshkey"></a>
# **UpdateSshKey**
> void UpdateSshKey (string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = null)

Update SSH Key

Update an SSH Key. The attributes `name` and `ssh_key` are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateSshKeyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new SshApi(config);
            var sshKeyId = "sshKeyId_example";  // string | The [SSH Key id](#operation/list-ssh-keys).
            var updateSshKeyRequest = new UpdateSshKeyRequest?(); // UpdateSshKeyRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update SSH Key
                apiInstance.UpdateSshKey(sshKeyId, updateSshKeyRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SshApi.UpdateSshKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateSshKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update SSH Key
    apiInstance.UpdateSshKeyWithHttpInfo(sshKeyId, updateSshKeyRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SshApi.UpdateSshKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sshKeyId** | **string** | The [SSH Key id](#operation/list-ssh-keys). |  |
| **updateSshKeyRequest** | [**UpdateSshKeyRequest?**](UpdateSshKeyRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

