# Org.OpenAPITools.Api.ContainerRegistryApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateRegistry**](ContainerRegistryApi.md#createregistry) | **POST** /registry | Create Container Registry |
| [**CreateRegistryDockerCredentials**](ContainerRegistryApi.md#createregistrydockercredentials) | **OPTIONS** /registry/{registry-id}/docker-credentials?expiry_seconds&#x3D;0&amp;read_write&#x3D;false | Create Docker Credentials |
| [**CreateRegistryKubernetesDockerCredentials**](ContainerRegistryApi.md#createregistrykubernetesdockercredentials) | **OPTIONS** /registry/{registry-id}/docker-credentials/kubernetes?expiry_seconds&#x3D;0&amp;read_write&#x3D;false&amp;base64_encode&#x3D;false | Create Docker Credentials for Kubernetes |
| [**DeleteRegistry**](ContainerRegistryApi.md#deleteregistry) | **DELETE** /registry/{registry-id} | Delete Container Registry |
| [**DeleteRepository**](ContainerRegistryApi.md#deleterepository) | **DELETE** /registry/{registry-id}/repository/{repository-image} | Delete Repository |
| [**ListRegistries**](ContainerRegistryApi.md#listregistries) | **GET** /registries | List Container Registries |
| [**ListRegistryPlans**](ContainerRegistryApi.md#listregistryplans) | **GET** /registry/plan/list | List Registry Plans |
| [**ListRegistryRegions**](ContainerRegistryApi.md#listregistryregions) | **GET** /registry/region/list | List Registry Regions |
| [**ListRegistryRepositories**](ContainerRegistryApi.md#listregistryrepositories) | **GET** /registry/{registry-id}/repositories | List Repositories |
| [**ReadRegistry**](ContainerRegistryApi.md#readregistry) | **GET** /registry/{registry-id} | Read Container Registry |
| [**ReadRegistryRepository**](ContainerRegistryApi.md#readregistryrepository) | **GET** /registry/{registry-id}/repository/{repository-image} | Read Repository |
| [**UpdateRegistry**](ContainerRegistryApi.md#updateregistry) | **PUT** /registry/{registry-id} | Update Container Registry |
| [**UpdateRepository**](ContainerRegistryApi.md#updaterepository) | **PUT** /registry/{registry-id}/repository/{repository-image} | Update Repository |

<a id="createregistry"></a>
# **CreateRegistry**
> Registry CreateRegistry (CreateRegistryRequest? createRegistryRequest = null)

Create Container Registry

Create a new Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateRegistryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var createRegistryRequest = new CreateRegistryRequest?(); // CreateRegistryRequest? |  (optional) 

            try
            {
                // Create Container Registry
                Registry result = apiInstance.CreateRegistry(createRegistryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistry: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateRegistryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Container Registry
    ApiResponse<Registry> response = apiInstance.CreateRegistryWithHttpInfo(createRegistryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createRegistryRequest** | [**CreateRegistryRequest?**](CreateRegistryRequest?.md) |  | [optional]  |

### Return type

[**Registry**](Registry.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | OK |  -  |
| **401** | Unauthorized |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createregistrydockercredentials"></a>
# **CreateRegistryDockerCredentials**
> RegistryDockerCredentials CreateRegistryDockerCredentials (string registryId, int? expirySeconds = null, bool? readWrite = null)

Create Docker Credentials

Create a fresh set of Docker Credentials for this Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateRegistryDockerCredentialsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var expirySeconds = 56;  // int? | The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional) 
            var readWrite = true;  // bool? | Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional) 

            try
            {
                // Create Docker Credentials
                RegistryDockerCredentials result = apiInstance.CreateRegistryDockerCredentials(registryId, expirySeconds, readWrite);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistryDockerCredentials: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateRegistryDockerCredentialsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Docker Credentials
    ApiResponse<RegistryDockerCredentials> response = apiInstance.CreateRegistryDockerCredentialsWithHttpInfo(registryId, expirySeconds, readWrite);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistryDockerCredentialsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **expirySeconds** | **int?** | The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 | [optional]  |
| **readWrite** | **bool?** | Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. | [optional]  |

### Return type

[**RegistryDockerCredentials**](RegistryDockerCredentials.md)

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
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createregistrykubernetesdockercredentials"></a>
# **CreateRegistryKubernetesDockerCredentials**
> RegistryKubernetesDockerCredentials CreateRegistryKubernetesDockerCredentials (string registryId, int? expirySeconds = null, bool? readWrite = null, bool? base64Encode = null)

Create Docker Credentials for Kubernetes

Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateRegistryKubernetesDockerCredentialsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var expirySeconds = 56;  // int? | The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional) 
            var readWrite = true;  // bool? | Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional) 
            var base64Encode = true;  // bool? | Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional) 

            try
            {
                // Create Docker Credentials for Kubernetes
                RegistryKubernetesDockerCredentials result = apiInstance.CreateRegistryKubernetesDockerCredentials(registryId, expirySeconds, readWrite, base64Encode);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistryKubernetesDockerCredentials: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateRegistryKubernetesDockerCredentialsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Docker Credentials for Kubernetes
    ApiResponse<RegistryKubernetesDockerCredentials> response = apiInstance.CreateRegistryKubernetesDockerCredentialsWithHttpInfo(registryId, expirySeconds, readWrite, base64Encode);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.CreateRegistryKubernetesDockerCredentialsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **expirySeconds** | **int?** | The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 | [optional]  |
| **readWrite** | **bool?** | Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. | [optional]  |
| **base64Encode** | **bool?** | Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. | [optional]  |

### Return type

[**RegistryKubernetesDockerCredentials**](RegistryKubernetesDockerCredentials.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/yaml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteregistry"></a>
# **DeleteRegistry**
> void DeleteRegistry (string registryId)

Delete Container Registry

Deletes a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteRegistryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).

            try
            {
                // Delete Container Registry
                apiInstance.DeleteRegistry(registryId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.DeleteRegistry: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteRegistryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Container Registry
    apiInstance.DeleteRegistryWithHttpInfo(registryId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.DeleteRegistryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |

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
| **204** | No Content - Successfully Deleted |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleterepository"></a>
# **DeleteRepository**
> void DeleteRepository (string registryId, string repositoryImage)

Delete Repository

Deletes a Repository from a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteRepositoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var repositoryImage = "repositoryImage_example";  // string | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).

            try
            {
                // Delete Repository
                apiInstance.DeleteRepository(registryId, repositoryImage);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.DeleteRepository: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteRepositoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Repository
    apiInstance.DeleteRepositoryWithHttpInfo(registryId, repositoryImage);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.DeleteRepositoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **repositoryImage** | **string** | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories). |  |

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
| **204** | No Content - Successfully Deleted |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listregistries"></a>
# **ListRegistries**
> ListRegistries200Response ListRegistries ()

List Container Registries

List All Container Registry Subscriptions for this account

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListRegistriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);

            try
            {
                // List Container Registries
                ListRegistries200Response result = apiInstance.ListRegistries();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ListRegistries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListRegistriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Container Registries
    ApiResponse<ListRegistries200Response> response = apiInstance.ListRegistriesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ListRegistriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListRegistries200Response**](ListRegistries200Response.md)

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
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listregistryplans"></a>
# **ListRegistryPlans**
> ListRegistryPlans200Response ListRegistryPlans ()

List Registry Plans

List All Plans to help choose which one is the best fit for your Container Registry

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListRegistryPlansExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);

            try
            {
                // List Registry Plans
                ListRegistryPlans200Response result = apiInstance.ListRegistryPlans();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryPlans: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListRegistryPlansWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Registry Plans
    ApiResponse<ListRegistryPlans200Response> response = apiInstance.ListRegistryPlansWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryPlansWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListRegistryPlans200Response**](ListRegistryPlans200Response.md)

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
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listregistryregions"></a>
# **ListRegistryRegions**
> ListRegistryRegions200Response ListRegistryRegions ()

List Registry Regions

List All Regions where a Container Registry can be deployed

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListRegistryRegionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);

            try
            {
                // List Registry Regions
                ListRegistryRegions200Response result = apiInstance.ListRegistryRegions();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryRegions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListRegistryRegionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Registry Regions
    ApiResponse<ListRegistryRegions200Response> response = apiInstance.ListRegistryRegionsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryRegionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListRegistryRegions200Response**](ListRegistryRegions200Response.md)

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
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listregistryrepositories"></a>
# **ListRegistryRepositories**
> ListRegistryRepositories200Response ListRegistryRepositories (string registryId)

List Repositories

List All Repositories in a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListRegistryRepositoriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).

            try
            {
                // List Repositories
                ListRegistryRepositories200Response result = apiInstance.ListRegistryRepositories(registryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryRepositories: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListRegistryRepositoriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Repositories
    ApiResponse<ListRegistryRepositories200Response> response = apiInstance.ListRegistryRepositoriesWithHttpInfo(registryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ListRegistryRepositoriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |

### Return type

[**ListRegistryRepositories200Response**](ListRegistryRepositories200Response.md)

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
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="readregistry"></a>
# **ReadRegistry**
> Registry ReadRegistry (string registryId)

Read Container Registry

Get a single Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ReadRegistryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).

            try
            {
                // Read Container Registry
                Registry result = apiInstance.ReadRegistry(registryId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ReadRegistry: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReadRegistryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read Container Registry
    ApiResponse<Registry> response = apiInstance.ReadRegistryWithHttpInfo(registryId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ReadRegistryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |

### Return type

[**Registry**](Registry.md)

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
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="readregistryrepository"></a>
# **ReadRegistryRepository**
> RegistryRepository ReadRegistryRepository (string registryId, string repositoryImage)

Read Repository

Get a single Repository in a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ReadRegistryRepositoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var repositoryImage = "repositoryImage_example";  // string | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).

            try
            {
                // Read Repository
                RegistryRepository result = apiInstance.ReadRegistryRepository(registryId, repositoryImage);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.ReadRegistryRepository: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReadRegistryRepositoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Read Repository
    ApiResponse<RegistryRepository> response = apiInstance.ReadRegistryRepositoryWithHttpInfo(registryId, repositoryImage);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.ReadRegistryRepositoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **repositoryImage** | **string** | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories). |  |

### Return type

[**RegistryRepository**](RegistryRepository.md)

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
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updateregistry"></a>
# **UpdateRegistry**
> Registry UpdateRegistry (string registryId, UpdateRegistryRequest? updateRegistryRequest = null)

Update Container Registry

Update a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateRegistryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var updateRegistryRequest = new UpdateRegistryRequest?(); // UpdateRegistryRequest? |  (optional) 

            try
            {
                // Update Container Registry
                Registry result = apiInstance.UpdateRegistry(registryId, updateRegistryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.UpdateRegistry: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateRegistryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Container Registry
    ApiResponse<Registry> response = apiInstance.UpdateRegistryWithHttpInfo(registryId, updateRegistryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.UpdateRegistryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **updateRegistryRequest** | [**UpdateRegistryRequest?**](UpdateRegistryRequest?.md) |  | [optional]  |

### Return type

[**Registry**](Registry.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updaterepository"></a>
# **UpdateRepository**
> RegistryRepository UpdateRepository (string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = null)

Update Repository

Update a Repository in a Container Registry Subscription

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateRepositoryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new ContainerRegistryApi(config);
            var registryId = "registryId_example";  // string | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).
            var repositoryImage = "repositoryImage_example";  // string | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).
            var updateRepositoryRequest = new UpdateRepositoryRequest?(); // UpdateRepositoryRequest? |  (optional) 

            try
            {
                // Update Repository
                RegistryRepository result = apiInstance.UpdateRepository(registryId, repositoryImage, updateRepositoryRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ContainerRegistryApi.UpdateRepository: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateRepositoryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Repository
    ApiResponse<RegistryRepository> response = apiInstance.UpdateRepositoryWithHttpInfo(registryId, repositoryImage, updateRepositoryRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ContainerRegistryApi.UpdateRepositoryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **registryId** | **string** | The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries). |  |
| **repositoryImage** | **string** | The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories). |  |
| **updateRepositoryRequest** | [**UpdateRepositoryRequest?**](UpdateRepositoryRequest?.md) |  | [optional]  |

### Return type

[**RegistryRepository**](RegistryRepository.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |
| **422** | Bad Request |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

