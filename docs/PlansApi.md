# Org.OpenAPITools.Api.PlansApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ListMetalPlans**](PlansApi.md#listmetalplans) | **GET** /plans-metal | List Bare Metal Plans |
| [**ListPlans**](PlansApi.md#listplans) | **GET** /plans | List Plans |

<a id="listmetalplans"></a>
# **ListMetalPlans**
> ListMetalPlans200Response ListMetalPlans (string? perPage = null, string? cursor = null)

List Bare Metal Plans

Get a list of all Bare Metal plans at Vultr.  The response is an array of JSON `plan` objects, with unique `id` with sub-fields in the general format of:    <type>-<number of cores>-<memory size>-<optional modifier>  For example: `vc2-24c-96gb-sc1`  More about the sub-fields:  * `<type>`: The Vultr type code. For example, `vc2`, `vhf`, `vdc`, etc. * `<number of cores>`: The number of cores, such as `4c` for \"4 cores\", `8c` for \"8 cores\", etc. * `<memory size>`: Size in GB, such as `32gb`. * `<optional modifier>`: Some plans include a modifier for internal identification purposes, such as CPU type or location surcharges.  > Note: This information about plan id format is for general education. Vultr may change the sub-field format or values at any time. You should not attempt to parse the plan ID sub-fields in your code for any specific purpose. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListMetalPlansExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            var apiInstance = new PlansApi(config);
            var perPage = "perPage_example";  // string? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Bare Metal Plans
                ListMetalPlans200Response result = apiInstance.ListMetalPlans(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlansApi.ListMetalPlans: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListMetalPlansWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Bare Metal Plans
    ApiResponse<ListMetalPlans200Response> response = apiInstance.ListMetalPlansWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlansApi.ListMetalPlansWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **perPage** | **string?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListMetalPlans200Response**](ListMetalPlans200Response.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listplans"></a>
# **ListPlans**
> ListPlans200Response ListPlans (string? type = null, int? perPage = null, string? cursor = null, string? os = null)

List Plans

Get a list of all VPS plans at Vultr.  The response is an array of JSON `plan` objects, with unique `id` with sub-fields in the general format of:    <type>-<number of cores>-<memory size>-<optional modifier>  For example: `vc2-24c-96gb-sc1`  More about the sub-fields:  * `<type>`: The Vultr type code. For example, `vc2`, `vhf`, `vdc`, etc. * `<number of cores>`: The number of cores, such as `4c` for \"4 cores\", `8c` for \"8 cores\", etc. * `<memory size>`: Size in GB, such as `32gb`. * `<optional modifier>`: Some plans include a modifier for internal identification purposes, such as CPU type or location surcharges.  > Note: This information about plan id format is for general education. Vultr may change the sub-field format or values at any time. You should not attempt to parse the plan ID sub-fields in your code for any specific purpose. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListPlansExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            var apiInstance = new PlansApi(config);
            var type = "type_example";  // string? | Filter the results by type.  | **Type** | **Description** | |- -- -- -- -- -|- -- -- -- -- -- -- -- --| | all | All available types | | vc2 | Cloud Compute | | vdc | Dedicated Cloud | | vhf | High Frequency Compute | | vhp | High Performance | | voc | All Optimized Cloud types | | voc-g | General Purpose Optimized Cloud | | voc-c | CPU Optimized Cloud | | voc-m | Memory Optimized Cloud | | voc-s | Storage Optimized Cloud | | vcg | Cloud GPU | (optional) 
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 
            var os = "os_example";  // string? | Filter the results by operating system.  |   | Type | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | windows | All available plans that support windows | (optional) 

            try
            {
                // List Plans
                ListPlans200Response result = apiInstance.ListPlans(type, perPage, cursor, os);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlansApi.ListPlans: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListPlansWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Plans
    ApiResponse<ListPlans200Response> response = apiInstance.ListPlansWithHttpInfo(type, perPage, cursor, os);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlansApi.ListPlansWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **type** | **string?** | Filter the results by type.  | **Type** | **Description** | |- -- -- -- -- -|- -- -- -- -- -- -- -- --| | all | All available types | | vc2 | Cloud Compute | | vdc | Dedicated Cloud | | vhf | High Frequency Compute | | vhp | High Performance | | voc | All Optimized Cloud types | | voc-g | General Purpose Optimized Cloud | | voc-c | CPU Optimized Cloud | | voc-m | Memory Optimized Cloud | | voc-s | Storage Optimized Cloud | | vcg | Cloud GPU | | [optional]  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |
| **os** | **string?** | Filter the results by operating system.  |   | Type | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | windows | All available plans that support windows | | [optional]  |

### Return type

[**ListPlans200Response**](ListPlans200Response.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

