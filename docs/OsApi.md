# Org.OpenAPITools.Api.OsApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ListOs**](OsApi.md#listos) | **GET** /os | List OS |

<a id="listos"></a>
# **ListOs**
> ListOs200Response ListOs (int? perPage = null, string? cursor = null)

List OS

List the OS images available for installation at Vultr.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListOsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            var apiInstance = new OsApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List OS
                ListOs200Response result = apiInstance.ListOs(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OsApi.ListOs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListOsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List OS
    ApiResponse<ListOs200Response> response = apiInstance.ListOsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OsApi.ListOsWithHttpInfo: " + e.Message);
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

[**ListOs200Response**](ListOs200Response.md)

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

