# Org.OpenAPITools.Api.DnsApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateDnsDomain**](DnsApi.md#creatednsdomain) | **POST** /domains | Create DNS Domain |
| [**CreateDnsDomainRecord**](DnsApi.md#creatednsdomainrecord) | **POST** /domains/{dns-domain}/records | Create Record |
| [**DeleteDnsDomain**](DnsApi.md#deletednsdomain) | **DELETE** /domains/{dns-domain} | Delete Domain |
| [**DeleteDnsDomainRecord**](DnsApi.md#deletednsdomainrecord) | **DELETE** /domains/{dns-domain}/records/{record-id} | Delete Record |
| [**GetDnsDomain**](DnsApi.md#getdnsdomain) | **GET** /domains/{dns-domain} | Get DNS Domain |
| [**GetDnsDomainDnssec**](DnsApi.md#getdnsdomaindnssec) | **GET** /domains/{dns-domain}/dnssec | Get DNSSec Info |
| [**GetDnsDomainRecord**](DnsApi.md#getdnsdomainrecord) | **GET** /domains/{dns-domain}/records/{record-id} | Get Record |
| [**GetDnsDomainSoa**](DnsApi.md#getdnsdomainsoa) | **GET** /domains/{dns-domain}/soa | Get SOA information |
| [**ListDnsDomainRecords**](DnsApi.md#listdnsdomainrecords) | **GET** /domains/{dns-domain}/records | List Records |
| [**ListDnsDomains**](DnsApi.md#listdnsdomains) | **GET** /domains | List DNS Domains |
| [**UpdateDnsDomain**](DnsApi.md#updatednsdomain) | **PUT** /domains/{dns-domain} | Update a DNS Domain |
| [**UpdateDnsDomainRecord**](DnsApi.md#updatednsdomainrecord) | **PATCH** /domains/{dns-domain}/records/{record-id} | Update Record |
| [**UpdateDnsDomainSoa**](DnsApi.md#updatednsdomainsoa) | **PATCH** /domains/{dns-domain}/soa | Update SOA information |

<a id="creatednsdomain"></a>
# **CreateDnsDomain**
> CreateDnsDomain200Response CreateDnsDomain (CreateDnsDomainRequest? createDnsDomainRequest = null)

Create DNS Domain

Create a DNS Domain for `domain`. If no `ip` address is supplied a domain with no records will be created.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDnsDomainExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var createDnsDomainRequest = new CreateDnsDomainRequest?(); // CreateDnsDomainRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create DNS Domain
                CreateDnsDomain200Response result = apiInstance.CreateDnsDomain(createDnsDomainRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.CreateDnsDomain: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDnsDomainWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create DNS Domain
    ApiResponse<CreateDnsDomain200Response> response = apiInstance.CreateDnsDomainWithHttpInfo(createDnsDomainRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.CreateDnsDomainWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createDnsDomainRequest** | [**CreateDnsDomainRequest?**](CreateDnsDomainRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDnsDomain200Response**](CreateDnsDomain200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="creatednsdomainrecord"></a>
# **CreateDnsDomainRecord**
> CreateDnsDomainRecord201Response CreateDnsDomainRecord (string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = null)

Create Record

Create a DNS record.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateDnsDomainRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var createDnsDomainRecordRequest = new CreateDnsDomainRecordRequest?(); // CreateDnsDomainRecordRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Record
                CreateDnsDomainRecord201Response result = apiInstance.CreateDnsDomainRecord(dnsDomain, createDnsDomainRecordRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.CreateDnsDomainRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateDnsDomainRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Record
    ApiResponse<CreateDnsDomainRecord201Response> response = apiInstance.CreateDnsDomainRecordWithHttpInfo(dnsDomain, createDnsDomainRecordRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.CreateDnsDomainRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **createDnsDomainRecordRequest** | [**CreateDnsDomainRecordRequest?**](CreateDnsDomainRecordRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateDnsDomainRecord201Response**](CreateDnsDomainRecord201Response.md)

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
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deletednsdomain"></a>
# **DeleteDnsDomain**
> void DeleteDnsDomain (string dnsDomain)

Delete Domain

Delete the DNS Domain.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDnsDomainExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).

            try
            {
                // Delete Domain
                apiInstance.DeleteDnsDomain(dnsDomain);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.DeleteDnsDomain: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDnsDomainWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Domain
    apiInstance.DeleteDnsDomainWithHttpInfo(dnsDomain);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.DeleteDnsDomainWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |

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

<a id="deletednsdomainrecord"></a>
# **DeleteDnsDomainRecord**
> void DeleteDnsDomainRecord (string dnsDomain, string recordId)

Delete Record

Delete the DNS record.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteDnsDomainRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var recordId = "recordId_example";  // string | The [DNS Record id](#operation/list-dns-domain-records).

            try
            {
                // Delete Record
                apiInstance.DeleteDnsDomainRecord(dnsDomain, recordId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.DeleteDnsDomainRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteDnsDomainRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Record
    apiInstance.DeleteDnsDomainRecordWithHttpInfo(dnsDomain, recordId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.DeleteDnsDomainRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **recordId** | **string** | The [DNS Record id](#operation/list-dns-domain-records). |  |

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

<a id="getdnsdomain"></a>
# **GetDnsDomain**
> CreateDnsDomain200Response GetDnsDomain (string dnsDomain)

Get DNS Domain

Get information for the DNS Domain.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDnsDomainExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).

            try
            {
                // Get DNS Domain
                CreateDnsDomain200Response result = apiInstance.GetDnsDomain(dnsDomain);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.GetDnsDomain: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDnsDomainWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get DNS Domain
    ApiResponse<CreateDnsDomain200Response> response = apiInstance.GetDnsDomainWithHttpInfo(dnsDomain);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.GetDnsDomainWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |

### Return type

[**CreateDnsDomain200Response**](CreateDnsDomain200Response.md)

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

<a id="getdnsdomaindnssec"></a>
# **GetDnsDomainDnssec**
> GetDnsDomainDnssec200Response GetDnsDomainDnssec (string dnsDomain)

Get DNSSec Info

Get the DNSSEC information for the DNS Domain.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDnsDomainDnssecExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).

            try
            {
                // Get DNSSec Info
                GetDnsDomainDnssec200Response result = apiInstance.GetDnsDomainDnssec(dnsDomain);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.GetDnsDomainDnssec: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDnsDomainDnssecWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get DNSSec Info
    ApiResponse<GetDnsDomainDnssec200Response> response = apiInstance.GetDnsDomainDnssecWithHttpInfo(dnsDomain);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.GetDnsDomainDnssecWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |

### Return type

[**GetDnsDomainDnssec200Response**](GetDnsDomainDnssec200Response.md)

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

<a id="getdnsdomainrecord"></a>
# **GetDnsDomainRecord**
> CreateDnsDomainRecord201Response GetDnsDomainRecord (string dnsDomain, string recordId)

Get Record

Get information for a DNS Record.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDnsDomainRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var recordId = "recordId_example";  // string | The [DNS Record id](#operation/list-dns-domain-records).

            try
            {
                // Get Record
                CreateDnsDomainRecord201Response result = apiInstance.GetDnsDomainRecord(dnsDomain, recordId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.GetDnsDomainRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDnsDomainRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Record
    ApiResponse<CreateDnsDomainRecord201Response> response = apiInstance.GetDnsDomainRecordWithHttpInfo(dnsDomain, recordId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.GetDnsDomainRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **recordId** | **string** | The [DNS Record id](#operation/list-dns-domain-records). |  |

### Return type

[**CreateDnsDomainRecord201Response**](CreateDnsDomainRecord201Response.md)

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

<a id="getdnsdomainsoa"></a>
# **GetDnsDomainSoa**
> GetDnsDomainSoa200Response GetDnsDomainSoa (string dnsDomain)

Get SOA information

Get SOA information for the DNS Domain.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetDnsDomainSoaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).

            try
            {
                // Get SOA information
                GetDnsDomainSoa200Response result = apiInstance.GetDnsDomainSoa(dnsDomain);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.GetDnsDomainSoa: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetDnsDomainSoaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get SOA information
    ApiResponse<GetDnsDomainSoa200Response> response = apiInstance.GetDnsDomainSoaWithHttpInfo(dnsDomain);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.GetDnsDomainSoaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |

### Return type

[**GetDnsDomainSoa200Response**](GetDnsDomainSoa200Response.md)

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

<a id="listdnsdomainrecords"></a>
# **ListDnsDomainRecords**
> ListDnsDomainRecords200Response ListDnsDomainRecords (string dnsDomain, int? perPage = null, string? cursor = null)

List Records

Get the DNS records for the Domain.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDnsDomainRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Records
                ListDnsDomainRecords200Response result = apiInstance.ListDnsDomainRecords(dnsDomain, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.ListDnsDomainRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDnsDomainRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Records
    ApiResponse<ListDnsDomainRecords200Response> response = apiInstance.ListDnsDomainRecordsWithHttpInfo(dnsDomain, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.ListDnsDomainRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListDnsDomainRecords200Response**](ListDnsDomainRecords200Response.md)

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

<a id="listdnsdomains"></a>
# **ListDnsDomains**
> ListDnsDomains200Response ListDnsDomains (int? perPage = null, string? cursor = null)

List DNS Domains

List all DNS Domains in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListDnsDomainsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List DNS Domains
                ListDnsDomains200Response result = apiInstance.ListDnsDomains(perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.ListDnsDomains: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListDnsDomainsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List DNS Domains
    ApiResponse<ListDnsDomains200Response> response = apiInstance.ListDnsDomainsWithHttpInfo(perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.ListDnsDomainsWithHttpInfo: " + e.Message);
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

[**ListDnsDomains200Response**](ListDnsDomains200Response.md)

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

<a id="updatednsdomain"></a>
# **UpdateDnsDomain**
> void UpdateDnsDomain (string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = null)

Update a DNS Domain

Update the DNS Domain. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDnsDomainExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var updateDnsDomainRequest = new UpdateDnsDomainRequest?(); // UpdateDnsDomainRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update a DNS Domain
                apiInstance.UpdateDnsDomain(dnsDomain, updateDnsDomainRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.UpdateDnsDomain: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDnsDomainWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update a DNS Domain
    apiInstance.UpdateDnsDomainWithHttpInfo(dnsDomain, updateDnsDomainRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.UpdateDnsDomainWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **updateDnsDomainRequest** | [**UpdateDnsDomainRequest?**](UpdateDnsDomainRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="updatednsdomainrecord"></a>
# **UpdateDnsDomainRecord**
> void UpdateDnsDomainRecord (string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = null)

Update Record

Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDnsDomainRecordExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var recordId = "recordId_example";  // string | The [DNS Record id](#operation/list-dns-domain-records).
            var updateDnsDomainRecordRequest = new UpdateDnsDomainRecordRequest?(); // UpdateDnsDomainRecordRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Record
                apiInstance.UpdateDnsDomainRecord(dnsDomain, recordId, updateDnsDomainRecordRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.UpdateDnsDomainRecord: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDnsDomainRecordWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Record
    apiInstance.UpdateDnsDomainRecordWithHttpInfo(dnsDomain, recordId, updateDnsDomainRecordRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.UpdateDnsDomainRecordWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **recordId** | **string** | The [DNS Record id](#operation/list-dns-domain-records). |  |
| **updateDnsDomainRecordRequest** | [**UpdateDnsDomainRecordRequest?**](UpdateDnsDomainRecordRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatednsdomainsoa"></a>
# **UpdateDnsDomainSoa**
> void UpdateDnsDomainSoa (string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = null)

Update SOA information

Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateDnsDomainSoaExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new DnsApi(config);
            var dnsDomain = "dnsDomain_example";  // string | The [DNS Domain](#operation/list-dns-domains).
            var updateDnsDomainSoaRequest = new UpdateDnsDomainSoaRequest?(); // UpdateDnsDomainSoaRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update SOA information
                apiInstance.UpdateDnsDomainSoa(dnsDomain, updateDnsDomainSoaRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DnsApi.UpdateDnsDomainSoa: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateDnsDomainSoaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update SOA information
    apiInstance.UpdateDnsDomainSoaWithHttpInfo(dnsDomain, updateDnsDomainSoaRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DnsApi.UpdateDnsDomainSoaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dnsDomain** | **string** | The [DNS Domain](#operation/list-dns-domains). |  |
| **updateDnsDomainSoaRequest** | [**UpdateDnsDomainSoaRequest?**](UpdateDnsDomainSoaRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

