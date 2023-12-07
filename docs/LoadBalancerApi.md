# Org.OpenAPITools.Api.LoadBalancerApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateLoadBalancer**](LoadBalancerApi.md#createloadbalancer) | **POST** /load-balancers | Create Load Balancer |
| [**CreateLoadBalancerForwardingRules**](LoadBalancerApi.md#createloadbalancerforwardingrules) | **POST** /load-balancers/{load-balancer-id}/forwarding-rules | Create Forwarding Rule |
| [**DeleteLoadBalancer**](LoadBalancerApi.md#deleteloadbalancer) | **DELETE** /load-balancers/{load-balancer-id} | Delete Load Balancer |
| [**DeleteLoadBalancerForwardingRule**](LoadBalancerApi.md#deleteloadbalancerforwardingrule) | **DELETE** /load-balancers/{load-balancer-id}/forwarding-rules/{forwarding-rule-id} | Delete Forwarding Rule |
| [**DeleteLoadBalancerSsl**](LoadBalancerApi.md#deleteloadbalancerssl) | **DELETE** /load-balancers/{load-balancer-id}/ssl | Delete Load Balancer SSL |
| [**GetLoadBalancer**](LoadBalancerApi.md#getloadbalancer) | **GET** /load-balancers/{load-balancer-id} | Get Load Balancer |
| [**GetLoadBalancerForwardingRule**](LoadBalancerApi.md#getloadbalancerforwardingrule) | **GET** /load-balancers/{load-balancer-id}/forwarding-rules/{forwarding-rule-id} | Get Forwarding Rule |
| [**GetLoadbalancerFirewallRule**](LoadBalancerApi.md#getloadbalancerfirewallrule) | **GET** /load-balancers/{loadbalancer-id}/firewall-rules/{firewall-rule-id} | Get Firewall Rule |
| [**ListLoadBalancerForwardingRules**](LoadBalancerApi.md#listloadbalancerforwardingrules) | **GET** /load-balancers/{load-balancer-id}/forwarding-rules | List Forwarding Rules |
| [**ListLoadBalancers**](LoadBalancerApi.md#listloadbalancers) | **GET** /load-balancers | List Load Balancers |
| [**ListLoadbalancerFirewallRules**](LoadBalancerApi.md#listloadbalancerfirewallrules) | **GET** /load-balancers/{loadbalancer-id}/firewall-rules | List Firewall Rules |
| [**UpdateLoadBalancer**](LoadBalancerApi.md#updateloadbalancer) | **PATCH** /load-balancers/{load-balancer-id} | Update Load Balancer |

<a id="createloadbalancer"></a>
# **CreateLoadBalancer**
> CreateLoadBalancer202Response CreateLoadBalancer (CreateLoadBalancerRequest? createLoadBalancerRequest = null)

Create Load Balancer

Create a new Load Balancer in a particular `region`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateLoadBalancerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var createLoadBalancerRequest = new CreateLoadBalancerRequest?(); // CreateLoadBalancerRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Load Balancer
                CreateLoadBalancer202Response result = apiInstance.CreateLoadBalancer(createLoadBalancerRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.CreateLoadBalancer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateLoadBalancerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Load Balancer
    ApiResponse<CreateLoadBalancer202Response> response = apiInstance.CreateLoadBalancerWithHttpInfo(createLoadBalancerRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.CreateLoadBalancerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createLoadBalancerRequest** | [**CreateLoadBalancerRequest?**](CreateLoadBalancerRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateLoadBalancer202Response**](CreateLoadBalancer202Response.md)

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

<a id="createloadbalancerforwardingrules"></a>
# **CreateLoadBalancerForwardingRules**
> void CreateLoadBalancerForwardingRules (string loadBalancerId, CreateLoadBalancerForwardingRulesRequest? createLoadBalancerForwardingRulesRequest = null)

Create Forwarding Rule

Create a new forwarding rule for a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateLoadBalancerForwardingRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).
            var createLoadBalancerForwardingRulesRequest = new CreateLoadBalancerForwardingRulesRequest?(); // CreateLoadBalancerForwardingRulesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Forwarding Rule
                apiInstance.CreateLoadBalancerForwardingRules(loadBalancerId, createLoadBalancerForwardingRulesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.CreateLoadBalancerForwardingRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateLoadBalancerForwardingRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Forwarding Rule
    apiInstance.CreateLoadBalancerForwardingRulesWithHttpInfo(loadBalancerId, createLoadBalancerForwardingRulesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.CreateLoadBalancerForwardingRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |
| **createLoadBalancerForwardingRulesRequest** | [**CreateLoadBalancerForwardingRulesRequest?**](CreateLoadBalancerForwardingRulesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteloadbalancer"></a>
# **DeleteLoadBalancer**
> void DeleteLoadBalancer (string loadBalancerId)

Delete Load Balancer

Delete a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteLoadBalancerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).

            try
            {
                // Delete Load Balancer
                apiInstance.DeleteLoadBalancer(loadBalancerId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteLoadBalancerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Load Balancer
    apiInstance.DeleteLoadBalancerWithHttpInfo(loadBalancerId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |

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

<a id="deleteloadbalancerforwardingrule"></a>
# **DeleteLoadBalancerForwardingRule**
> void DeleteLoadBalancerForwardingRule (string loadBalancerId, string forwardingRuleId)

Delete Forwarding Rule

Delete a Forwarding Rule on a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteLoadBalancerForwardingRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).
            var forwardingRuleId = "forwardingRuleId_example";  // string | The [Forwarding Rule id](#operation/list-load-balancer-forwarding-rules).

            try
            {
                // Delete Forwarding Rule
                apiInstance.DeleteLoadBalancerForwardingRule(loadBalancerId, forwardingRuleId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancerForwardingRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteLoadBalancerForwardingRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Forwarding Rule
    apiInstance.DeleteLoadBalancerForwardingRuleWithHttpInfo(loadBalancerId, forwardingRuleId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancerForwardingRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |
| **forwardingRuleId** | **string** | The [Forwarding Rule id](#operation/list-load-balancer-forwarding-rules). |  |

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

<a id="deleteloadbalancerssl"></a>
# **DeleteLoadBalancerSsl**
> void DeleteLoadBalancerSsl (string loadBalancerId)

Delete Load Balancer SSL

Delete a Load Balancer SSL.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteLoadBalancerSslExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/delete-load-balancer-ssl).

            try
            {
                // Delete Load Balancer SSL
                apiInstance.DeleteLoadBalancerSsl(loadBalancerId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancerSsl: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteLoadBalancerSslWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Load Balancer SSL
    apiInstance.DeleteLoadBalancerSslWithHttpInfo(loadBalancerId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.DeleteLoadBalancerSslWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/delete-load-balancer-ssl). |  |

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

<a id="getloadbalancer"></a>
# **GetLoadBalancer**
> CreateLoadBalancer202Response GetLoadBalancer (string loadBalancerId)

Get Load Balancer

Get information for a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetLoadBalancerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).

            try
            {
                // Get Load Balancer
                CreateLoadBalancer202Response result = apiInstance.GetLoadBalancer(loadBalancerId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.GetLoadBalancer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetLoadBalancerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Load Balancer
    ApiResponse<CreateLoadBalancer202Response> response = apiInstance.GetLoadBalancerWithHttpInfo(loadBalancerId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.GetLoadBalancerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |

### Return type

[**CreateLoadBalancer202Response**](CreateLoadBalancer202Response.md)

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

<a id="getloadbalancerforwardingrule"></a>
# **GetLoadBalancerForwardingRule**
> GetLoadBalancerForwardingRule200Response GetLoadBalancerForwardingRule (string loadBalancerId, string forwardingRuleId)

Get Forwarding Rule

Get information for a Forwarding Rule on a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetLoadBalancerForwardingRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).
            var forwardingRuleId = "forwardingRuleId_example";  // string | The [Forwarding Rule id](#operation/list-load-balancer-forwarding-rules).

            try
            {
                // Get Forwarding Rule
                GetLoadBalancerForwardingRule200Response result = apiInstance.GetLoadBalancerForwardingRule(loadBalancerId, forwardingRuleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.GetLoadBalancerForwardingRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetLoadBalancerForwardingRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Forwarding Rule
    ApiResponse<GetLoadBalancerForwardingRule200Response> response = apiInstance.GetLoadBalancerForwardingRuleWithHttpInfo(loadBalancerId, forwardingRuleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.GetLoadBalancerForwardingRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |
| **forwardingRuleId** | **string** | The [Forwarding Rule id](#operation/list-load-balancer-forwarding-rules). |  |

### Return type

[**GetLoadBalancerForwardingRule200Response**](GetLoadBalancerForwardingRule200Response.md)

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

<a id="getloadbalancerfirewallrule"></a>
# **GetLoadbalancerFirewallRule**
> LoadbalancerFirewallRule GetLoadbalancerFirewallRule (string loadbalancerId, string firewallRuleId)

Get Firewall Rule

Get a firewall rule for a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetLoadbalancerFirewallRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadbalancerId = "loadbalancerId_example";  // string | 
            var firewallRuleId = "firewallRuleId_example";  // string | 

            try
            {
                // Get Firewall Rule
                LoadbalancerFirewallRule result = apiInstance.GetLoadbalancerFirewallRule(loadbalancerId, firewallRuleId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.GetLoadbalancerFirewallRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetLoadbalancerFirewallRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Firewall Rule
    ApiResponse<LoadbalancerFirewallRule> response = apiInstance.GetLoadbalancerFirewallRuleWithHttpInfo(loadbalancerId, firewallRuleId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.GetLoadbalancerFirewallRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadbalancerId** | **string** |  |  |
| **firewallRuleId** | **string** |  |  |

### Return type

[**LoadbalancerFirewallRule**](LoadbalancerFirewallRule.md)

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

<a id="listloadbalancerforwardingrules"></a>
# **ListLoadBalancerForwardingRules**
> ListLoadBalancerForwardingRules200Response ListLoadBalancerForwardingRules (string loadBalancerId, int? perPage = null, string? cursor = null)

List Forwarding Rules

List the fowarding rules for a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListLoadBalancerForwardingRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Forwarding Rules
                ListLoadBalancerForwardingRules200Response result = apiInstance.ListLoadBalancerForwardingRules(loadBalancerId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.ListLoadBalancerForwardingRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListLoadBalancerForwardingRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Forwarding Rules
    ApiResponse<ListLoadBalancerForwardingRules200Response> response = apiInstance.ListLoadBalancerForwardingRulesWithHttpInfo(loadBalancerId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.ListLoadBalancerForwardingRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListLoadBalancerForwardingRules200Response**](ListLoadBalancerForwardingRules200Response.md)

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

<a id="listloadbalancers"></a>
# **ListLoadBalancers**
> ListLoadBalancers200Response ListLoadBalancers (int? perPage = null, string? cursor = null)

List Load Balancers

List the Load Balancers in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListLoadBalancersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Load Balancers
                ListLoadBalancers200Response result = apiInstance.ListLoadBalancers(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.ListLoadBalancers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListLoadBalancersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Load Balancers
    ApiResponse<ListLoadBalancers200Response> response = apiInstance.ListLoadBalancersWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.ListLoadBalancersWithHttpInfo: " + e.Message);
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

[**ListLoadBalancers200Response**](ListLoadBalancers200Response.md)

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

<a id="listloadbalancerfirewallrules"></a>
# **ListLoadbalancerFirewallRules**
> LoadbalancerFirewallRule ListLoadbalancerFirewallRules (string loadbalancerId, string? perPage = null, string? cursor = null)

List Firewall Rules

List the firewall rules for a Load Balancer.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListLoadbalancerFirewallRulesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadbalancerId = "loadbalancerId_example";  // string | 
            var perPage = "perPage_example";  // string? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Firewall Rules
                LoadbalancerFirewallRule result = apiInstance.ListLoadbalancerFirewallRules(loadbalancerId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.ListLoadbalancerFirewallRules: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListLoadbalancerFirewallRulesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Firewall Rules
    ApiResponse<LoadbalancerFirewallRule> response = apiInstance.ListLoadbalancerFirewallRulesWithHttpInfo(loadbalancerId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.ListLoadbalancerFirewallRulesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadbalancerId** | **string** |  |  |
| **perPage** | **string?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**LoadbalancerFirewallRule**](LoadbalancerFirewallRule.md)

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

<a id="updateloadbalancer"></a>
# **UpdateLoadBalancer**
> void UpdateLoadBalancer (string loadBalancerId, UpdateLoadBalancerRequest? updateLoadBalancerRequest = null)

Update Load Balancer

Update information for a Load Balancer. All attributes are optional. If not set, the attributes will retain their original values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateLoadBalancerExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new LoadBalancerApi(config);
            var loadBalancerId = "loadBalancerId_example";  // string | The [Load Balancer id](#operation/list-load-balancers).
            var updateLoadBalancerRequest = new UpdateLoadBalancerRequest?(); // UpdateLoadBalancerRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Load Balancer
                apiInstance.UpdateLoadBalancer(loadBalancerId, updateLoadBalancerRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LoadBalancerApi.UpdateLoadBalancer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateLoadBalancerWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Load Balancer
    apiInstance.UpdateLoadBalancerWithHttpInfo(loadBalancerId, updateLoadBalancerRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LoadBalancerApi.UpdateLoadBalancerWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **loadBalancerId** | **string** | The [Load Balancer id](#operation/list-load-balancers). |  |
| **updateLoadBalancerRequest** | [**UpdateLoadBalancerRequest?**](UpdateLoadBalancerRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

