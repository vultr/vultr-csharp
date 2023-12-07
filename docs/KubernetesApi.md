# Org.OpenAPITools.Api.KubernetesApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CreateKubernetesCluster**](KubernetesApi.md#createkubernetescluster) | **POST** /kubernetes/clusters | Create Kubernetes Cluster |
| [**CreateNodepools**](KubernetesApi.md#createnodepools) | **POST** /kubernetes/clusters/{vke-id}/node-pools | Create NodePool |
| [**DeleteKubernetesCluster**](KubernetesApi.md#deletekubernetescluster) | **DELETE** /kubernetes/clusters/{vke-id} | Delete Kubernetes Cluster |
| [**DeleteKubernetesClusterVkeIdDeleteWithLinkedResources**](KubernetesApi.md#deletekubernetesclustervkeiddeletewithlinkedresources) | **DELETE** /kubernetes/clusters/{vke-id}/delete-with-linked-resources | Delete VKE Cluster and All Related Resources |
| [**DeleteNodepool**](KubernetesApi.md#deletenodepool) | **DELETE** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Delete Nodepool |
| [**DeleteNodepoolInstance**](KubernetesApi.md#deletenodepoolinstance) | **DELETE** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id} | Delete NodePool Instance |
| [**GetKubernetesAvailableUpgrades**](KubernetesApi.md#getkubernetesavailableupgrades) | **GET** /kubernetes/clusters/{vke-id}/available-upgrades | Get Kubernetes Available Upgrades |
| [**GetKubernetesClusters**](KubernetesApi.md#getkubernetesclusters) | **GET** /kubernetes/clusters/{vke-id} | Get Kubernetes Cluster |
| [**GetKubernetesClustersConfig**](KubernetesApi.md#getkubernetesclustersconfig) | **GET** /kubernetes/clusters/{vke-id}/config | Get Kubernetes Cluster Kubeconfig |
| [**GetKubernetesResources**](KubernetesApi.md#getkubernetesresources) | **GET** /kubernetes/clusters/{vke-id}/resources | Get Kubernetes Resources |
| [**GetKubernetesVersions**](KubernetesApi.md#getkubernetesversions) | **GET** /kubernetes/versions | Get Kubernetes Versions |
| [**GetNodepool**](KubernetesApi.md#getnodepool) | **GET** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Get NodePool |
| [**GetNodepools**](KubernetesApi.md#getnodepools) | **GET** /kubernetes/clusters/{vke-id}/node-pools | List NodePools |
| [**ListKubernetesClusters**](KubernetesApi.md#listkubernetesclusters) | **GET** /kubernetes/clusters | List all Kubernetes Clusters |
| [**RecycleNodepoolInstance**](KubernetesApi.md#recyclenodepoolinstance) | **POST** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}/recycle | Recycle a NodePool Instance |
| [**StartKubernetesClusterUpgrade**](KubernetesApi.md#startkubernetesclusterupgrade) | **POST** /kubernetes/clusters/{vke-id}/upgrades | Start Kubernetes Cluster Upgrade |
| [**UpdateKubernetesCluster**](KubernetesApi.md#updatekubernetescluster) | **PUT** /kubernetes/clusters/{vke-id} | Update Kubernetes Cluster |
| [**UpdateNodepool**](KubernetesApi.md#updatenodepool) | **PATCH** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Update Nodepool |

<a id="createkubernetescluster"></a>
# **CreateKubernetesCluster**
> CreateKubernetesCluster201Response CreateKubernetesCluster (CreateKubernetesClusterRequest? createKubernetesClusterRequest = null)

Create Kubernetes Cluster

Create Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateKubernetesClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var createKubernetesClusterRequest = new CreateKubernetesClusterRequest?(); // CreateKubernetesClusterRequest? | Request Body (optional) 

            try
            {
                // Create Kubernetes Cluster
                CreateKubernetesCluster201Response result = apiInstance.CreateKubernetesCluster(createKubernetesClusterRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.CreateKubernetesCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateKubernetesClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Kubernetes Cluster
    ApiResponse<CreateKubernetesCluster201Response> response = apiInstance.CreateKubernetesClusterWithHttpInfo(createKubernetesClusterRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.CreateKubernetesClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createKubernetesClusterRequest** | [**CreateKubernetesClusterRequest?**](CreateKubernetesClusterRequest?.md) | Request Body | [optional]  |

### Return type

[**CreateKubernetesCluster201Response**](CreateKubernetesCluster201Response.md)

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

<a id="createnodepools"></a>
# **CreateNodepools**
> CreateNodepools201Response CreateNodepools (string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = null)

Create NodePool

Create NodePool for a Existing Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateNodepoolsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var createNodepoolsRequest = new CreateNodepoolsRequest?(); // CreateNodepoolsRequest? | Request Body (optional) 

            try
            {
                // Create NodePool
                CreateNodepools201Response result = apiInstance.CreateNodepools(vkeId, createNodepoolsRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.CreateNodepools: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateNodepoolsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create NodePool
    ApiResponse<CreateNodepools201Response> response = apiInstance.CreateNodepoolsWithHttpInfo(vkeId, createNodepoolsRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.CreateNodepoolsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **createNodepoolsRequest** | [**CreateNodepoolsRequest?**](CreateNodepoolsRequest?.md) | Request Body | [optional]  |

### Return type

[**CreateNodepools201Response**](CreateNodepools201Response.md)

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

<a id="deletekubernetescluster"></a>
# **DeleteKubernetesCluster**
> void DeleteKubernetesCluster (string vkeId)

Delete Kubernetes Cluster

Delete Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteKubernetesClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // Delete Kubernetes Cluster
                apiInstance.DeleteKubernetesCluster(vkeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.DeleteKubernetesCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteKubernetesClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Kubernetes Cluster
    apiInstance.DeleteKubernetesClusterWithHttpInfo(vkeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.DeleteKubernetesClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

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

<a id="deletekubernetesclustervkeiddeletewithlinkedresources"></a>
# **DeleteKubernetesClusterVkeIdDeleteWithLinkedResources**
> void DeleteKubernetesClusterVkeIdDeleteWithLinkedResources (string vkeId)

Delete VKE Cluster and All Related Resources

Delete Kubernetes Cluster and all related resources. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | 

            try
            {
                // Delete VKE Cluster and All Related Resources
                apiInstance.DeleteKubernetesClusterVkeIdDeleteWithLinkedResources(vkeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.DeleteKubernetesClusterVkeIdDeleteWithLinkedResources: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete VKE Cluster and All Related Resources
    apiInstance.DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo(vkeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** |  |  |

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

<a id="deletenodepool"></a>
# **DeleteNodepool**
> void DeleteNodepool (string vkeId, string nodepoolId)

Delete Nodepool

Delete a NodePool from a Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteNodepoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var nodepoolId = "nodepoolId_example";  // string | The [NodePool ID](#operation/get-nodepools).

            try
            {
                // Delete Nodepool
                apiInstance.DeleteNodepool(vkeId, nodepoolId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.DeleteNodepool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteNodepoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Nodepool
    apiInstance.DeleteNodepoolWithHttpInfo(vkeId, nodepoolId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.DeleteNodepoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **nodepoolId** | **string** | The [NodePool ID](#operation/get-nodepools). |  |

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

<a id="deletenodepoolinstance"></a>
# **DeleteNodepoolInstance**
> void DeleteNodepoolInstance (string vkeId, string nodepoolId, string nodeId)

Delete NodePool Instance

Delete a single nodepool instance from a given Nodepool

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteNodepoolInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var nodepoolId = "nodepoolId_example";  // string | The [NodePool ID](#operation/get-nodepools).
            var nodeId = "nodeId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Delete NodePool Instance
                apiInstance.DeleteNodepoolInstance(vkeId, nodepoolId, nodeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.DeleteNodepoolInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteNodepoolInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete NodePool Instance
    apiInstance.DeleteNodepoolInstanceWithHttpInfo(vkeId, nodepoolId, nodeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.DeleteNodepoolInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **nodepoolId** | **string** | The [NodePool ID](#operation/get-nodepools). |  |
| **nodeId** | **string** | The [Instance ID](#operation/list-instances). |  |

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

<a id="getkubernetesavailableupgrades"></a>
# **GetKubernetesAvailableUpgrades**
> GetKubernetesAvailableUpgrades200Response GetKubernetesAvailableUpgrades (string vkeId)

Get Kubernetes Available Upgrades

Get the available upgrades for the specified Kubernetes cluster.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetKubernetesAvailableUpgradesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // Get Kubernetes Available Upgrades
                GetKubernetesAvailableUpgrades200Response result = apiInstance.GetKubernetesAvailableUpgrades(vkeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetKubernetesAvailableUpgrades: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetKubernetesAvailableUpgradesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Kubernetes Available Upgrades
    ApiResponse<GetKubernetesAvailableUpgrades200Response> response = apiInstance.GetKubernetesAvailableUpgradesWithHttpInfo(vkeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetKubernetesAvailableUpgradesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

### Return type

[**GetKubernetesAvailableUpgrades200Response**](GetKubernetesAvailableUpgrades200Response.md)

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

<a id="getkubernetesclusters"></a>
# **GetKubernetesClusters**
> CreateKubernetesCluster201Response GetKubernetesClusters (string vkeId)

Get Kubernetes Cluster

Get Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetKubernetesClustersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // Get Kubernetes Cluster
                CreateKubernetesCluster201Response result = apiInstance.GetKubernetesClusters(vkeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetKubernetesClusters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetKubernetesClustersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Kubernetes Cluster
    ApiResponse<CreateKubernetesCluster201Response> response = apiInstance.GetKubernetesClustersWithHttpInfo(vkeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetKubernetesClustersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

### Return type

[**CreateKubernetesCluster201Response**](CreateKubernetesCluster201Response.md)

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

<a id="getkubernetesclustersconfig"></a>
# **GetKubernetesClustersConfig**
> GetKubernetesClustersConfig200Response GetKubernetesClustersConfig (string vkeId)

Get Kubernetes Cluster Kubeconfig

Get Kubernetes Cluster Kubeconfig

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetKubernetesClustersConfigExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // Get Kubernetes Cluster Kubeconfig
                GetKubernetesClustersConfig200Response result = apiInstance.GetKubernetesClustersConfig(vkeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetKubernetesClustersConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetKubernetesClustersConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Kubernetes Cluster Kubeconfig
    ApiResponse<GetKubernetesClustersConfig200Response> response = apiInstance.GetKubernetesClustersConfigWithHttpInfo(vkeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetKubernetesClustersConfigWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

### Return type

[**GetKubernetesClustersConfig200Response**](GetKubernetesClustersConfig200Response.md)

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

<a id="getkubernetesresources"></a>
# **GetKubernetesResources**
> GetKubernetesResources200Response GetKubernetesResources (string vkeId)

Get Kubernetes Resources

Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetKubernetesResourcesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // Get Kubernetes Resources
                GetKubernetesResources200Response result = apiInstance.GetKubernetesResources(vkeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetKubernetesResources: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetKubernetesResourcesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Kubernetes Resources
    ApiResponse<GetKubernetesResources200Response> response = apiInstance.GetKubernetesResourcesWithHttpInfo(vkeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetKubernetesResourcesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

### Return type

[**GetKubernetesResources200Response**](GetKubernetesResources200Response.md)

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

<a id="getkubernetesversions"></a>
# **GetKubernetesVersions**
> GetKubernetesVersions200Response GetKubernetesVersions ()

Get Kubernetes Versions

Get a list of supported Kubernetes versions

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetKubernetesVersionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            var apiInstance = new KubernetesApi(config);

            try
            {
                // Get Kubernetes Versions
                GetKubernetesVersions200Response result = apiInstance.GetKubernetesVersions();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetKubernetesVersions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetKubernetesVersionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Kubernetes Versions
    ApiResponse<GetKubernetesVersions200Response> response = apiInstance.GetKubernetesVersionsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetKubernetesVersionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**GetKubernetesVersions200Response**](GetKubernetesVersions200Response.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="getnodepool"></a>
# **GetNodepool**
> CreateNodepools201Response GetNodepool (string vkeId, string nodepoolId)

Get NodePool

Get Nodepool from a Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetNodepoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var nodepoolId = "nodepoolId_example";  // string | The [NodePool ID](#operation/get-nodepools).

            try
            {
                // Get NodePool
                CreateNodepools201Response result = apiInstance.GetNodepool(vkeId, nodepoolId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetNodepool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetNodepoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get NodePool
    ApiResponse<CreateNodepools201Response> response = apiInstance.GetNodepoolWithHttpInfo(vkeId, nodepoolId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetNodepoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **nodepoolId** | **string** | The [NodePool ID](#operation/get-nodepools). |  |

### Return type

[**CreateNodepools201Response**](CreateNodepools201Response.md)

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

<a id="getnodepools"></a>
# **GetNodepools**
> GetNodepools200Response GetNodepools (string vkeId)

List NodePools

List all available NodePools on a Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetNodepoolsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).

            try
            {
                // List NodePools
                GetNodepools200Response result = apiInstance.GetNodepools(vkeId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.GetNodepools: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetNodepoolsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List NodePools
    ApiResponse<GetNodepools200Response> response = apiInstance.GetNodepoolsWithHttpInfo(vkeId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.GetNodepoolsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |

### Return type

[**GetNodepools200Response**](GetNodepools200Response.md)

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

<a id="listkubernetesclusters"></a>
# **ListKubernetesClusters**
> ListKubernetesClusters200Response ListKubernetesClusters ()

List all Kubernetes Clusters

List all Kubernetes clusters currently deployed

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListKubernetesClustersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);

            try
            {
                // List all Kubernetes Clusters
                ListKubernetesClusters200Response result = apiInstance.ListKubernetesClusters();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.ListKubernetesClusters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListKubernetesClustersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List all Kubernetes Clusters
    ApiResponse<ListKubernetesClusters200Response> response = apiInstance.ListKubernetesClustersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.ListKubernetesClustersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListKubernetesClusters200Response**](ListKubernetesClusters200Response.md)

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

<a id="recyclenodepoolinstance"></a>
# **RecycleNodepoolInstance**
> void RecycleNodepoolInstance (string vkeId, string nodepoolId, string nodeId)

Recycle a NodePool Instance

Recycle a specific NodePool Instance

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RecycleNodepoolInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var nodepoolId = "nodepoolId_example";  // string | The [NodePool ID](#operation/get-nodepools).
            var nodeId = "nodeId_example";  // string | Node ID

            try
            {
                // Recycle a NodePool Instance
                apiInstance.RecycleNodepoolInstance(vkeId, nodepoolId, nodeId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.RecycleNodepoolInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RecycleNodepoolInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Recycle a NodePool Instance
    apiInstance.RecycleNodepoolInstanceWithHttpInfo(vkeId, nodepoolId, nodeId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.RecycleNodepoolInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **nodepoolId** | **string** | The [NodePool ID](#operation/get-nodepools). |  |
| **nodeId** | **string** | Node ID |  |

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

<a id="startkubernetesclusterupgrade"></a>
# **StartKubernetesClusterUpgrade**
> void StartKubernetesClusterUpgrade (string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = null)

Start Kubernetes Cluster Upgrade

Start a Kubernetes cluster upgrade.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartKubernetesClusterUpgradeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var startKubernetesClusterUpgradeRequest = new StartKubernetesClusterUpgradeRequest?(); // StartKubernetesClusterUpgradeRequest? | Request Body (optional) 

            try
            {
                // Start Kubernetes Cluster Upgrade
                apiInstance.StartKubernetesClusterUpgrade(vkeId, startKubernetesClusterUpgradeRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.StartKubernetesClusterUpgrade: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartKubernetesClusterUpgradeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Kubernetes Cluster Upgrade
    apiInstance.StartKubernetesClusterUpgradeWithHttpInfo(vkeId, startKubernetesClusterUpgradeRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.StartKubernetesClusterUpgradeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **startKubernetesClusterUpgradeRequest** | [**StartKubernetesClusterUpgradeRequest?**](StartKubernetesClusterUpgradeRequest?.md) | Request Body | [optional]  |

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="updatekubernetescluster"></a>
# **UpdateKubernetesCluster**
> void UpdateKubernetesCluster (string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = null)

Update Kubernetes Cluster

Update Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateKubernetesClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var updateKubernetesClusterRequest = new UpdateKubernetesClusterRequest?(); // UpdateKubernetesClusterRequest? | Request Body (optional) 

            try
            {
                // Update Kubernetes Cluster
                apiInstance.UpdateKubernetesCluster(vkeId, updateKubernetesClusterRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.UpdateKubernetesCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateKubernetesClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Kubernetes Cluster
    apiInstance.UpdateKubernetesClusterWithHttpInfo(vkeId, updateKubernetesClusterRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.UpdateKubernetesClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **updateKubernetesClusterRequest** | [**UpdateKubernetesClusterRequest?**](UpdateKubernetesClusterRequest?.md) | Request Body | [optional]  |

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

<a id="updatenodepool"></a>
# **UpdateNodepool**
> CreateNodepools201Response UpdateNodepool (string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = null)

Update Nodepool

Update a Nodepool on a Kubernetes Cluster

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateNodepoolExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new KubernetesApi(config);
            var vkeId = "vkeId_example";  // string | The [VKE ID](#operation/list-kubernetes-clusters).
            var nodepoolId = "nodepoolId_example";  // string | The [NodePool ID](#operation/get-nodepools).
            var updateNodepoolRequest = new UpdateNodepoolRequest?(); // UpdateNodepoolRequest? | Request Body (optional) 

            try
            {
                // Update Nodepool
                CreateNodepools201Response result = apiInstance.UpdateNodepool(vkeId, nodepoolId, updateNodepoolRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling KubernetesApi.UpdateNodepool: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateNodepoolWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Nodepool
    ApiResponse<CreateNodepools201Response> response = apiInstance.UpdateNodepoolWithHttpInfo(vkeId, nodepoolId, updateNodepoolRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling KubernetesApi.UpdateNodepoolWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **vkeId** | **string** | The [VKE ID](#operation/list-kubernetes-clusters). |  |
| **nodepoolId** | **string** | The [NodePool ID](#operation/get-nodepools). |  |
| **updateNodepoolRequest** | [**UpdateNodepoolRequest?**](UpdateNodepoolRequest?.md) | Request Body | [optional]  |

### Return type

[**CreateNodepools201Response**](CreateNodepools201Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

