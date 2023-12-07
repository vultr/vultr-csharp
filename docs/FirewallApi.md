# Org.OpenAPITools.Api.FirewallApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateFirewallGroup**](FirewallApi.md#createfirewallgroup) | **POST** /firewalls | Create Firewall Group |
| [**DeleteFirewallGroup**](FirewallApi.md#deletefirewallgroup) | **DELETE** /firewalls/{firewall-group-id} | Delete Firewall Group |
| [**DeleteFirewallGroupRule**](FirewallApi.md#deletefirewallgrouprule) | **DELETE** /firewalls/{firewall-group-id}/rules/{firewall-rule-id} | Delete Firewall Rule |
| [**GetFirewallGroup**](FirewallApi.md#getfirewallgroup) | **GET** /firewalls/{firewall-group-id} | Get Firewall Group |
| [**GetFirewallGroupRule**](FirewallApi.md#getfirewallgrouprule) | **GET** /firewalls/{firewall-group-id}/rules/{firewall-rule-id} | Get Firewall Rule |
| [**ListFirewallGroupRules**](FirewallApi.md#listfirewallgrouprules) | **GET** /firewalls/{firewall-group-id}/rules | List Firewall Rules |
| [**ListFirewallGroups**](FirewallApi.md#listfirewallgroups) | **GET** /firewalls | List Firewall Groups |
| [**PostFirewallsFirewallGroupIdRules**](FirewallApi.md#postfirewallsfirewallgroupidrules) | **POST** /firewalls/{firewall-group-id}/rules | Create Firewall Rules |
| [**UpdateFirewallGroup**](FirewallApi.md#updatefirewallgroup) | **PUT** /firewalls/{firewall-group-id} | Update Firewall Group |

<a id="createfirewallgroup"></a>
# **CreateFirewallGroup**
> CreateFirewallGroup201Response CreateFirewallGroup (CreateFirewallGroupRequest? createFirewallGroupRequest = null)

Create Firewall Group

Create a new Firewall Group.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateFirewallGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var createFirewallGroupRequest = new CreateFirewallGroupRequest?(); // CreateFirewallGroupRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Firewall Group
                CreateFirewallGroup201Response result = apiInstance.CreateFirewallGroup(createFirewallGroupRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.CreateFirewallGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateFirewallGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Firewall Group
    ApiResponse<CreateFirewallGroup201Response> response = apiInstance.CreateFirewallGroupWithHttpInfo(createFirewallGroupRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.CreateFirewallGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createFirewallGroupRequest** | [**CreateFirewallGroupRequest?**](CreateFirewallGroupRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateFirewallGroup201Response**](CreateFirewallGroup201Response.md)

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

<a id="deletefirewallgroup"></a>
# **DeleteFirewallGroup**
> void DeleteFirewallGroup (string firewallGroupId)

Delete Firewall Group

Delete a Firewall Group.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteFirewallGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).

            try
            {
                // Delete Firewall Group
                apiInstance.DeleteFirewallGroup(firewallGroupId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.DeleteFirewallGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteFirewallGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Firewall Group
    apiInstance.DeleteFirewallGroupWithHttpInfo(firewallGroupId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.DeleteFirewallGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |

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

<a id="deletefirewallgrouprule"></a>
# **DeleteFirewallGroupRule**
> void DeleteFirewallGroupRule (string firewallGroupId, string firewallRuleId)

Delete Firewall Rule

Delete a Firewall Rule.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteFirewallGroupRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).
            var firewallRuleId = "firewallRuleId_example";  // string | The [Firewall Rule id](#operation/list-firewall-group-rules).

            try
            {
                // Delete Firewall Rule
                apiInstance.DeleteFirewallGroupRule(firewallGroupId, firewallRuleId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.DeleteFirewallGroupRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteFirewallGroupRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Firewall Rule
    apiInstance.DeleteFirewallGroupRuleWithHttpInfo(firewallGroupId, firewallRuleId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.DeleteFirewallGroupRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |
| **firewallRuleId** | **string** | The [Firewall Rule id](#operation/list-firewall-group-rules). |  |

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

<a id="getfirewallgroup"></a>
# **GetFirewallGroup**
> CreateFirewallGroup201Response GetFirewallGroup (string firewallGroupId)

Get Firewall Group

Get information for a Firewall Group.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetFirewallGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).

            try
            {
                // Get Firewall Group
                CreateFirewallGroup201Response result = apiInstance.GetFirewallGroup(firewallGroupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.GetFirewallGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetFirewallGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Firewall Group
    ApiResponse<CreateFirewallGroup201Response> response = apiInstance.GetFirewallGroupWithHttpInfo(firewallGroupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.GetFirewallGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |

### Return type

[**CreateFirewallGroup201Response**](CreateFirewallGroup201Response.md)

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

<a id="getfirewallgrouprule"></a>
# **GetFirewallGroupRule**
> PostFirewallsFirewallGroupIdRules201Response GetFirewallGroupRule (string firewallGroupId, string firewallRuleId)

Get Firewall Rule

Get a Firewall Rule.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetFirewallGroupRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).
            var firewallRuleId = "firewallRuleId_example";  // string | The [Firewall Rule id](#operation/list-firewall-group-rules).

            try
            {
                // Get Firewall Rule
                PostFirewallsFirewallGroupIdRules201Response result = apiInstance.GetFirewallGroupRule(firewallGroupId, firewallRuleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.GetFirewallGroupRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetFirewallGroupRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Firewall Rule
    ApiResponse<PostFirewallsFirewallGroupIdRules201Response> response = apiInstance.GetFirewallGroupRuleWithHttpInfo(firewallGroupId, firewallRuleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.GetFirewallGroupRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |
| **firewallRuleId** | **string** | The [Firewall Rule id](#operation/list-firewall-group-rules). |  |

### Return type

[**PostFirewallsFirewallGroupIdRules201Response**](PostFirewallsFirewallGroupIdRules201Response.md)

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

<a id="listfirewallgrouprules"></a>
# **ListFirewallGroupRules**
> ListFirewallGroupRules200Response ListFirewallGroupRules (string firewallGroupId, int? perPage = null, string? cursor = null)

List Firewall Rules

Get the Firewall Rules for a Firewall Group.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListFirewallGroupRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Firewall Rules
                ListFirewallGroupRules200Response result = apiInstance.ListFirewallGroupRules(firewallGroupId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.ListFirewallGroupRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListFirewallGroupRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Firewall Rules
    ApiResponse<ListFirewallGroupRules200Response> response = apiInstance.ListFirewallGroupRulesWithHttpInfo(firewallGroupId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.ListFirewallGroupRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListFirewallGroupRules200Response**](ListFirewallGroupRules200Response.md)

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

<a id="listfirewallgroups"></a>
# **ListFirewallGroups**
> ListFirewallGroups200Response ListFirewallGroups (int? perPage = null, string? cursor = null)

List Firewall Groups

Get a list of all Firewall Groups.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListFirewallGroupsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Firewall Groups
                ListFirewallGroups200Response result = apiInstance.ListFirewallGroups(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.ListFirewallGroups: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListFirewallGroupsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Firewall Groups
    ApiResponse<ListFirewallGroups200Response> response = apiInstance.ListFirewallGroupsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.ListFirewallGroupsWithHttpInfo: " + e.Message);
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

[**ListFirewallGroups200Response**](ListFirewallGroups200Response.md)

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

<a id="postfirewallsfirewallgroupidrules"></a>
# **PostFirewallsFirewallGroupIdRules**
> PostFirewallsFirewallGroupIdRules201Response PostFirewallsFirewallGroupIdRules (string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = null)

Create Firewall Rules

Create a Firewall Rule for a Firewall Group. The attributes `ip_type`, `protocol`, `subnet`, and `subnet_size` are required.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostFirewallsFirewallGroupIdRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).
            var postFirewallsFirewallGroupIdRulesRequest = new PostFirewallsFirewallGroupIdRulesRequest?(); // PostFirewallsFirewallGroupIdRulesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Firewall Rules
                PostFirewallsFirewallGroupIdRules201Response result = apiInstance.PostFirewallsFirewallGroupIdRules(firewallGroupId, postFirewallsFirewallGroupIdRulesRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.PostFirewallsFirewallGroupIdRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PostFirewallsFirewallGroupIdRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Firewall Rules
    ApiResponse<PostFirewallsFirewallGroupIdRules201Response> response = apiInstance.PostFirewallsFirewallGroupIdRulesWithHttpInfo(firewallGroupId, postFirewallsFirewallGroupIdRulesRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.PostFirewallsFirewallGroupIdRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |
| **postFirewallsFirewallGroupIdRulesRequest** | [**PostFirewallsFirewallGroupIdRulesRequest?**](PostFirewallsFirewallGroupIdRulesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**PostFirewallsFirewallGroupIdRules201Response**](PostFirewallsFirewallGroupIdRules201Response.md)

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

<a id="updatefirewallgroup"></a>
# **UpdateFirewallGroup**
> void UpdateFirewallGroup (string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = null)

Update Firewall Group

Update information for a Firewall Group.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateFirewallGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new FirewallApi(config);
            var firewallGroupId = "firewallGroupId_example";  // string | The [Firewall Group id](#operation/list-firewall-groups).
            var updateFirewallGroupRequest = new UpdateFirewallGroupRequest?(); // UpdateFirewallGroupRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Firewall Group
                apiInstance.UpdateFirewallGroup(firewallGroupId, updateFirewallGroupRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FirewallApi.UpdateFirewallGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateFirewallGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Firewall Group
    apiInstance.UpdateFirewallGroupWithHttpInfo(firewallGroupId, updateFirewallGroupRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FirewallApi.UpdateFirewallGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **firewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups). |  |
| **updateFirewallGroupRequest** | [**UpdateFirewallGroupRequest?**](UpdateFirewallGroupRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

