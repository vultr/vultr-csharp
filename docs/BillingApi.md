# Org.OpenAPITools.Api.BillingApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GetInvoice**](BillingApi.md#getinvoice) | **GET** /billing/invoices/{invoice-id} | Get Invoice |
| [**GetInvoiceItems**](BillingApi.md#getinvoiceitems) | **GET** /billing/invoices/{invoice-id}/items | Get Invoice Items |
| [**ListBillingHistory**](BillingApi.md#listbillinghistory) | **GET** /billing/history | List Billing History |
| [**ListInvoices**](BillingApi.md#listinvoices) | **GET** /billing/invoices | List Invoices |

<a id="getinvoice"></a>
# **GetInvoice**
> GetInvoice200Response GetInvoice (string invoiceId)

Get Invoice

Retrieve specified invoice

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInvoiceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingApi(config);
            var invoiceId = "invoiceId_example";  // string | ID of invoice

            try
            {
                // Get Invoice
                GetInvoice200Response result = apiInstance.GetInvoice(invoiceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingApi.GetInvoice: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInvoiceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Invoice
    ApiResponse<GetInvoice200Response> response = apiInstance.GetInvoiceWithHttpInfo(invoiceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingApi.GetInvoiceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **invoiceId** | **string** | ID of invoice |  |

### Return type

[**GetInvoice200Response**](GetInvoice200Response.md)

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

<a id="getinvoiceitems"></a>
# **GetInvoiceItems**
> GetInvoiceItems200Response GetInvoiceItems (string invoiceId)

Get Invoice Items

Retrieve full specified invoice

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInvoiceItemsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingApi(config);
            var invoiceId = "invoiceId_example";  // string | ID of invoice

            try
            {
                // Get Invoice Items
                GetInvoiceItems200Response result = apiInstance.GetInvoiceItems(invoiceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingApi.GetInvoiceItems: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInvoiceItemsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Invoice Items
    ApiResponse<GetInvoiceItems200Response> response = apiInstance.GetInvoiceItemsWithHttpInfo(invoiceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingApi.GetInvoiceItemsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **invoiceId** | **string** | ID of invoice |  |

### Return type

[**GetInvoiceItems200Response**](GetInvoiceItems200Response.md)

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

<a id="listbillinghistory"></a>
# **ListBillingHistory**
> ListBillingHistory200Response ListBillingHistory ()

List Billing History

Retrieve list of billing history

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListBillingHistoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingApi(config);

            try
            {
                // List Billing History
                ListBillingHistory200Response result = apiInstance.ListBillingHistory();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingApi.ListBillingHistory: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListBillingHistoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Billing History
    ApiResponse<ListBillingHistory200Response> response = apiInstance.ListBillingHistoryWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingApi.ListBillingHistoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListBillingHistory200Response**](ListBillingHistory200Response.md)

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

<a id="listinvoices"></a>
# **ListInvoices**
> ListInvoices200Response ListInvoices ()

List Invoices

Retrieve a list of invoices

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInvoicesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new BillingApi(config);

            try
            {
                // List Invoices
                ListInvoices200Response result = apiInstance.ListInvoices();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BillingApi.ListInvoices: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInvoicesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Invoices
    ApiResponse<ListInvoices200Response> response = apiInstance.ListInvoicesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BillingApi.ListInvoicesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListInvoices200Response**](ListInvoices200Response.md)

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

