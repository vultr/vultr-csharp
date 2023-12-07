# Org.OpenAPITools.Api.InstancesApi

All URIs are relative to *https://api.vultr.com/v2*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AttachInstanceIso**](InstancesApi.md#attachinstanceiso) | **POST** /instances/{instance-id}/iso/attach | Attach ISO to Instance |
| [**AttachInstanceNetwork**](InstancesApi.md#attachinstancenetwork) | **POST** /instances/{instance-id}/private-networks/attach | Attach Private Network to Instance |
| [**AttachInstanceVpc**](InstancesApi.md#attachinstancevpc) | **POST** /instances/{instance-id}/vpcs/attach | Attach VPC to Instance |
| [**AttachInstanceVpc2**](InstancesApi.md#attachinstancevpc2) | **POST** /instances/{instance-id}/vpc2/attach | Attach VPC 2.0 Network to Instance |
| [**CreateInstance**](InstancesApi.md#createinstance) | **POST** /instances | Create Instance |
| [**CreateInstanceBackupSchedule**](InstancesApi.md#createinstancebackupschedule) | **POST** /instances/{instance-id}/backup-schedule | Set Instance Backup Schedule |
| [**CreateInstanceIpv4**](InstancesApi.md#createinstanceipv4) | **POST** /instances/{instance-id}/ipv4 | Create IPv4 |
| [**CreateInstanceReverseIpv4**](InstancesApi.md#createinstancereverseipv4) | **POST** /instances/{instance-id}/ipv4/reverse | Create Instance Reverse IPv4 |
| [**CreateInstanceReverseIpv6**](InstancesApi.md#createinstancereverseipv6) | **POST** /instances/{instance-id}/ipv6/reverse | Create Instance Reverse IPv6 |
| [**DeleteInstance**](InstancesApi.md#deleteinstance) | **DELETE** /instances/{instance-id} | Delete Instance |
| [**DeleteInstanceIpv4**](InstancesApi.md#deleteinstanceipv4) | **DELETE** /instances/{instance-id}/ipv4/{ipv4} | Delete IPv4 Address |
| [**DeleteInstanceReverseIpv6**](InstancesApi.md#deleteinstancereverseipv6) | **DELETE** /instances/{instance-id}/ipv6/reverse/{ipv6} | Delete Instance Reverse IPv6 |
| [**DetachInstanceIso**](InstancesApi.md#detachinstanceiso) | **POST** /instances/{instance-id}/iso/detach | Detach ISO from instance |
| [**DetachInstanceNetwork**](InstancesApi.md#detachinstancenetwork) | **POST** /instances/{instance-id}/private-networks/detach | Detach Private Network from Instance. |
| [**DetachInstanceVpc**](InstancesApi.md#detachinstancevpc) | **POST** /instances/{instance-id}/vpcs/detach | Detach VPC from Instance |
| [**DetachInstanceVpc2**](InstancesApi.md#detachinstancevpc2) | **POST** /instances/{instance-id}/vpc2/detach | Detach VPC 2.0 Network from Instance |
| [**GetInstance**](InstancesApi.md#getinstance) | **GET** /instances/{instance-id} | Get Instance |
| [**GetInstanceBackupSchedule**](InstancesApi.md#getinstancebackupschedule) | **GET** /instances/{instance-id}/backup-schedule | Get Instance Backup Schedule |
| [**GetInstanceBandwidth**](InstancesApi.md#getinstancebandwidth) | **GET** /instances/{instance-id}/bandwidth | Instance Bandwidth |
| [**GetInstanceIpv4**](InstancesApi.md#getinstanceipv4) | **GET** /instances/{instance-id}/ipv4 | List Instance IPv4 Information |
| [**GetInstanceIpv6**](InstancesApi.md#getinstanceipv6) | **GET** /instances/{instance-id}/ipv6 | Get Instance IPv6 Information |
| [**GetInstanceIsoStatus**](InstancesApi.md#getinstanceisostatus) | **GET** /instances/{instance-id}/iso | Get Instance ISO Status |
| [**GetInstanceNeighbors**](InstancesApi.md#getinstanceneighbors) | **GET** /instances/{instance-id}/neighbors | Get Instance neighbors |
| [**GetInstanceUpgrades**](InstancesApi.md#getinstanceupgrades) | **GET** /instances/{instance-id}/upgrades | Get Available Instance Upgrades |
| [**GetInstanceUserdata**](InstancesApi.md#getinstanceuserdata) | **GET** /instances/{instance-id}/user-data | Get Instance User Data |
| [**HaltInstance**](InstancesApi.md#haltinstance) | **POST** /instances/{instance-id}/halt | Halt Instance |
| [**HaltInstances**](InstancesApi.md#haltinstances) | **POST** /instances/halt | Halt Instances |
| [**ListInstanceIpv6Reverse**](InstancesApi.md#listinstanceipv6reverse) | **GET** /instances/{instance-id}/ipv6/reverse | List Instance IPv6 Reverse |
| [**ListInstancePrivateNetworks**](InstancesApi.md#listinstanceprivatenetworks) | **GET** /instances/{instance-id}/private-networks | List instance Private Networks |
| [**ListInstanceVpc2**](InstancesApi.md#listinstancevpc2) | **GET** /instances/{instance-id}/vpc2 | List Instance VPC 2.0 Networks |
| [**ListInstanceVpcs**](InstancesApi.md#listinstancevpcs) | **GET** /instances/{instance-id}/vpcs | List instance VPCs |
| [**ListInstances**](InstancesApi.md#listinstances) | **GET** /instances | List Instances |
| [**PostInstancesInstanceIdIpv4ReverseDefault**](InstancesApi.md#postinstancesinstanceidipv4reversedefault) | **POST** /instances/{instance-id}/ipv4/reverse/default | Set Default Reverse DNS Entry |
| [**RebootInstance**](InstancesApi.md#rebootinstance) | **POST** /instances/{instance-id}/reboot | Reboot Instance |
| [**RebootInstances**](InstancesApi.md#rebootinstances) | **POST** /instances/reboot | Reboot instances |
| [**ReinstallInstance**](InstancesApi.md#reinstallinstance) | **POST** /instances/{instance-id}/reinstall | Reinstall Instance |
| [**RestoreInstance**](InstancesApi.md#restoreinstance) | **POST** /instances/{instance-id}/restore | Restore Instance |
| [**StartInstance**](InstancesApi.md#startinstance) | **POST** /instances/{instance-id}/start | Start instance |
| [**StartInstances**](InstancesApi.md#startinstances) | **POST** /instances/start | Start instances |
| [**UpdateInstance**](InstancesApi.md#updateinstance) | **PATCH** /instances/{instance-id} | Update Instance |

<a id="attachinstanceiso"></a>
# **AttachInstanceIso**
> AttachInstanceIso202Response AttachInstanceIso (string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = null)

Attach ISO to Instance

Attach an ISO to an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachInstanceIsoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | 
            var attachInstanceIsoRequest = new AttachInstanceIsoRequest?(); // AttachInstanceIsoRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach ISO to Instance
                AttachInstanceIso202Response result = apiInstance.AttachInstanceIso(instanceId, attachInstanceIsoRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.AttachInstanceIso: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachInstanceIsoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach ISO to Instance
    ApiResponse<AttachInstanceIso202Response> response = apiInstance.AttachInstanceIsoWithHttpInfo(instanceId, attachInstanceIsoRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.AttachInstanceIsoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** |  |  |
| **attachInstanceIsoRequest** | [**AttachInstanceIsoRequest?**](AttachInstanceIsoRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**AttachInstanceIso202Response**](AttachInstanceIso202Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="attachinstancenetwork"></a>
# **AttachInstanceNetwork**
> void AttachInstanceNetwork (string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = null)

Attach Private Network to Instance

Attach Private Network to an Instance.<br><br>**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachInstanceNetworkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var attachInstanceNetworkRequest = new AttachInstanceNetworkRequest?(); // AttachInstanceNetworkRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach Private Network to Instance
                apiInstance.AttachInstanceNetwork(instanceId, attachInstanceNetworkRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.AttachInstanceNetwork: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachInstanceNetworkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach Private Network to Instance
    apiInstance.AttachInstanceNetworkWithHttpInfo(instanceId, attachInstanceNetworkRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.AttachInstanceNetworkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **attachInstanceNetworkRequest** | [**AttachInstanceNetworkRequest?**](AttachInstanceNetworkRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="attachinstancevpc"></a>
# **AttachInstanceVpc**
> void AttachInstanceVpc (string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = null)

Attach VPC to Instance

Attach a VPC to an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachInstanceVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var attachInstanceVpcRequest = new AttachInstanceVpcRequest?(); // AttachInstanceVpcRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach VPC to Instance
                apiInstance.AttachInstanceVpc(instanceId, attachInstanceVpcRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.AttachInstanceVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachInstanceVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach VPC to Instance
    apiInstance.AttachInstanceVpcWithHttpInfo(instanceId, attachInstanceVpcRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.AttachInstanceVpcWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **attachInstanceVpcRequest** | [**AttachInstanceVpcRequest?**](AttachInstanceVpcRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="attachinstancevpc2"></a>
# **AttachInstanceVpc2**
> void AttachInstanceVpc2 (string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = null)

Attach VPC 2.0 Network to Instance

Attach a VPC 2.0 Network to an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class AttachInstanceVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var attachInstanceVpc2Request = new AttachInstanceVpc2Request?(); // AttachInstanceVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Attach VPC 2.0 Network to Instance
                apiInstance.AttachInstanceVpc2(instanceId, attachInstanceVpc2Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.AttachInstanceVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AttachInstanceVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Attach VPC 2.0 Network to Instance
    apiInstance.AttachInstanceVpc2WithHttpInfo(instanceId, attachInstanceVpc2Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.AttachInstanceVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **attachInstanceVpc2Request** | [**AttachInstanceVpc2Request?**](AttachInstanceVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="createinstance"></a>
# **CreateInstance**
> CreateInstance202Response CreateInstance (CreateInstanceRequest? createInstanceRequest = null)

Create Instance

Create a new VPS Instance in a `region` with the desired `plan`. Choose one of the following to deploy the instance:  * `os_id` * `iso_id` * `snapshot_id` * `app_id` * `image_id`  Supply other attributes as desired.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var createInstanceRequest = new CreateInstanceRequest?(); // CreateInstanceRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Instance
                CreateInstance202Response result = apiInstance.CreateInstance(createInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.CreateInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Instance
    ApiResponse<CreateInstance202Response> response = apiInstance.CreateInstanceWithHttpInfo(createInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.CreateInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createInstanceRequest** | [**CreateInstanceRequest?**](CreateInstanceRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateInstance202Response**](CreateInstance202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Created |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **403** | Forbidden |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createinstancebackupschedule"></a>
# **CreateInstanceBackupSchedule**
> void CreateInstanceBackupSchedule (string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = null)

Set Instance Backup Schedule

Set the backup schedule for an Instance in UTC. The `type` is required.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstanceBackupScheduleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var createInstanceBackupScheduleRequest = new CreateInstanceBackupScheduleRequest?(); // CreateInstanceBackupScheduleRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Set Instance Backup Schedule
                apiInstance.CreateInstanceBackupSchedule(instanceId, createInstanceBackupScheduleRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.CreateInstanceBackupSchedule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateInstanceBackupScheduleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Instance Backup Schedule
    apiInstance.CreateInstanceBackupScheduleWithHttpInfo(instanceId, createInstanceBackupScheduleRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.CreateInstanceBackupScheduleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **createInstanceBackupScheduleRequest** | [**CreateInstanceBackupScheduleRequest?**](CreateInstanceBackupScheduleRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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
| **200** | OK |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createinstanceipv4"></a>
# **CreateInstanceIpv4**
> Object CreateInstanceIpv4 (string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = null)

Create IPv4

Create an IPv4 address for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstanceIpv4Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var createInstanceIpv4Request = new CreateInstanceIpv4Request?(); // CreateInstanceIpv4Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create IPv4
                Object result = apiInstance.CreateInstanceIpv4(instanceId, createInstanceIpv4Request);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.CreateInstanceIpv4: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateInstanceIpv4WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create IPv4
    ApiResponse<Object> response = apiInstance.CreateInstanceIpv4WithHttpInfo(instanceId, createInstanceIpv4Request);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.CreateInstanceIpv4WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **createInstanceIpv4Request** | [**CreateInstanceIpv4Request?**](CreateInstanceIpv4Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

**Object**

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="createinstancereverseipv4"></a>
# **CreateInstanceReverseIpv4**
> void CreateInstanceReverseIpv4 (string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = null)

Create Instance Reverse IPv4

Create a reverse IPv4 entry for an Instance. The `ip` and `reverse` attributes are required. 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstanceReverseIpv4Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var createInstanceReverseIpv4Request = new CreateInstanceReverseIpv4Request?(); // CreateInstanceReverseIpv4Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Instance Reverse IPv4
                apiInstance.CreateInstanceReverseIpv4(instanceId, createInstanceReverseIpv4Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.CreateInstanceReverseIpv4: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateInstanceReverseIpv4WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Instance Reverse IPv4
    apiInstance.CreateInstanceReverseIpv4WithHttpInfo(instanceId, createInstanceReverseIpv4Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.CreateInstanceReverseIpv4WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **createInstanceReverseIpv4Request** | [**CreateInstanceReverseIpv4Request?**](CreateInstanceReverseIpv4Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="createinstancereverseipv6"></a>
# **CreateInstanceReverseIpv6**
> void CreateInstanceReverseIpv6 (string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = null)

Create Instance Reverse IPv6

Create a reverse IPv6 entry for an Instance. The `ip` and `reverse` attributes are required. IP address must be in full, expanded format.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class CreateInstanceReverseIpv6Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var createInstanceReverseIpv6Request = new CreateInstanceReverseIpv6Request?(); // CreateInstanceReverseIpv6Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Create Instance Reverse IPv6
                apiInstance.CreateInstanceReverseIpv6(instanceId, createInstanceReverseIpv6Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.CreateInstanceReverseIpv6: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CreateInstanceReverseIpv6WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Instance Reverse IPv6
    apiInstance.CreateInstanceReverseIpv6WithHttpInfo(instanceId, createInstanceReverseIpv6Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.CreateInstanceReverseIpv6WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **createInstanceReverseIpv6Request** | [**CreateInstanceReverseIpv6Request?**](CreateInstanceReverseIpv6Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="deleteinstance"></a>
# **DeleteInstance**
> void DeleteInstance (string instanceId)

Delete Instance

Delete an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Delete Instance
                apiInstance.DeleteInstance(instanceId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DeleteInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Instance
    apiInstance.DeleteInstanceWithHttpInfo(instanceId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DeleteInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

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

<a id="deleteinstanceipv4"></a>
# **DeleteInstanceIpv4**
> void DeleteInstanceIpv4 (string instanceId, string ipv4)

Delete IPv4 Address

Delete an IPv4 address from an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteInstanceIpv4Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var ipv4 = "ipv4_example";  // string | The IPv4 address.

            try
            {
                // Delete IPv4 Address
                apiInstance.DeleteInstanceIpv4(instanceId, ipv4);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DeleteInstanceIpv4: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteInstanceIpv4WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete IPv4 Address
    apiInstance.DeleteInstanceIpv4WithHttpInfo(instanceId, ipv4);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DeleteInstanceIpv4WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **ipv4** | **string** | The IPv4 address. |  |

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
| **403** | Forbidden |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="deleteinstancereverseipv6"></a>
# **DeleteInstanceReverseIpv6**
> void DeleteInstanceReverseIpv6 (string instanceId, string ipv6)

Delete Instance Reverse IPv6

Delete the reverse IPv6 for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DeleteInstanceReverseIpv6Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var ipv6 = "ipv6_example";  // string | The IPv6 address.

            try
            {
                // Delete Instance Reverse IPv6
                apiInstance.DeleteInstanceReverseIpv6(instanceId, ipv6);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DeleteInstanceReverseIpv6: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DeleteInstanceReverseIpv6WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Instance Reverse IPv6
    apiInstance.DeleteInstanceReverseIpv6WithHttpInfo(instanceId, ipv6);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DeleteInstanceReverseIpv6WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **ipv6** | **string** | The IPv6 address. |  |

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
| **403** | Forbidden |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="detachinstanceiso"></a>
# **DetachInstanceIso**
> DetachInstanceIso202Response DetachInstanceIso (string instanceId)

Detach ISO from instance

Detach the ISO from an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachInstanceIsoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Detach ISO from instance
                DetachInstanceIso202Response result = apiInstance.DetachInstanceIso(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DetachInstanceIso: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachInstanceIsoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach ISO from instance
    ApiResponse<DetachInstanceIso202Response> response = apiInstance.DetachInstanceIsoWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DetachInstanceIsoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**DetachInstanceIso202Response**](DetachInstanceIso202Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **202** | Accepted |  -  |
| **400** | Bad Request |  -  |
| **401** | Unauthorized |  -  |
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="detachinstancenetwork"></a>
# **DetachInstanceNetwork**
> void DetachInstanceNetwork (string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = null)

Detach Private Network from Instance.

Detach Private Network from an Instance.<br><br>**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachInstanceNetworkExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var detachInstanceNetworkRequest = new DetachInstanceNetworkRequest?(); // DetachInstanceNetworkRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Detach Private Network from Instance.
                apiInstance.DetachInstanceNetwork(instanceId, detachInstanceNetworkRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DetachInstanceNetwork: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachInstanceNetworkWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach Private Network from Instance.
    apiInstance.DetachInstanceNetworkWithHttpInfo(instanceId, detachInstanceNetworkRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DetachInstanceNetworkWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **detachInstanceNetworkRequest** | [**DetachInstanceNetworkRequest?**](DetachInstanceNetworkRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="detachinstancevpc"></a>
# **DetachInstanceVpc**
> void DetachInstanceVpc (string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = null)

Detach VPC from Instance

Detach a VPC from an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachInstanceVpcExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var detachInstanceVpcRequest = new DetachInstanceVpcRequest?(); // DetachInstanceVpcRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Detach VPC from Instance
                apiInstance.DetachInstanceVpc(instanceId, detachInstanceVpcRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DetachInstanceVpc: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachInstanceVpcWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach VPC from Instance
    apiInstance.DetachInstanceVpcWithHttpInfo(instanceId, detachInstanceVpcRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DetachInstanceVpcWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **detachInstanceVpcRequest** | [**DetachInstanceVpcRequest?**](DetachInstanceVpcRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="detachinstancevpc2"></a>
# **DetachInstanceVpc2**
> void DetachInstanceVpc2 (string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = null)

Detach VPC 2.0 Network from Instance

Detach a VPC 2.0 Network from an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class DetachInstanceVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var detachInstanceVpc2Request = new DetachInstanceVpc2Request?(); // DetachInstanceVpc2Request? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Detach VPC 2.0 Network from Instance
                apiInstance.DetachInstanceVpc2(instanceId, detachInstanceVpc2Request);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.DetachInstanceVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DetachInstanceVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Detach VPC 2.0 Network from Instance
    apiInstance.DetachInstanceVpc2WithHttpInfo(instanceId, detachInstanceVpc2Request);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.DetachInstanceVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **detachInstanceVpc2Request** | [**DetachInstanceVpc2Request?**](DetachInstanceVpc2Request?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="getinstance"></a>
# **GetInstance**
> CreateInstance202Response GetInstance (string instanceId)

Get Instance

Get information about an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance
                CreateInstance202Response result = apiInstance.GetInstance(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance
    ApiResponse<CreateInstance202Response> response = apiInstance.GetInstanceWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**CreateInstance202Response**](CreateInstance202Response.md)

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

<a id="getinstancebackupschedule"></a>
# **GetInstanceBackupSchedule**
> GetInstanceBackupSchedule200Response GetInstanceBackupSchedule (string instanceId)

Get Instance Backup Schedule

Get the backup schedule for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceBackupScheduleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance Backup Schedule
                GetInstanceBackupSchedule200Response result = apiInstance.GetInstanceBackupSchedule(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceBackupSchedule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceBackupScheduleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance Backup Schedule
    ApiResponse<GetInstanceBackupSchedule200Response> response = apiInstance.GetInstanceBackupScheduleWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceBackupScheduleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**GetInstanceBackupSchedule200Response**](GetInstanceBackupSchedule200Response.md)

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

<a id="getinstancebandwidth"></a>
# **GetInstanceBandwidth**
> GetBandwidthBaremetal200Response GetInstanceBandwidth (string instanceId, int? dateRange = null)

Instance Bandwidth

Get bandwidth information about an Instance.<br><br>The `bandwidth` object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceBandwidthExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var dateRange = 56;  // int? | The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional) 

            try
            {
                // Instance Bandwidth
                GetBandwidthBaremetal200Response result = apiInstance.GetInstanceBandwidth(instanceId, dateRange);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceBandwidth: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceBandwidthWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Instance Bandwidth
    ApiResponse<GetBandwidthBaremetal200Response> response = apiInstance.GetInstanceBandwidthWithHttpInfo(instanceId, dateRange);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceBandwidthWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **dateRange** | **int?** | The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. | [optional]  |

### Return type

[**GetBandwidthBaremetal200Response**](GetBandwidthBaremetal200Response.md)

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

<a id="getinstanceipv4"></a>
# **GetInstanceIpv4**
> GetIpv4Baremetal200Response GetInstanceIpv4 (string instanceId, bool? publicNetwork = null, int? perPage = null, string? cursor = null)

List Instance IPv4 Information

List the IPv4 information for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceIpv4Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var publicNetwork = true;  // bool? | If `true`, includes information about the public network adapter (such as MAC address) with the `main_ip` entry. (optional) 
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500.  (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Instance IPv4 Information
                GetIpv4Baremetal200Response result = apiInstance.GetInstanceIpv4(instanceId, publicNetwork, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceIpv4: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceIpv4WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Instance IPv4 Information
    ApiResponse<GetIpv4Baremetal200Response> response = apiInstance.GetInstanceIpv4WithHttpInfo(instanceId, publicNetwork, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceIpv4WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **publicNetwork** | **bool?** | If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. | [optional]  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500.  | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**GetIpv4Baremetal200Response**](GetIpv4Baremetal200Response.md)

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

<a id="getinstanceipv6"></a>
# **GetInstanceIpv6**
> GetIpv6Baremetal200Response GetInstanceIpv6 (string instanceId)

Get Instance IPv6 Information

Get the IPv6 information for an VPS Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceIpv6Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance IPv6 Information
                GetIpv6Baremetal200Response result = apiInstance.GetInstanceIpv6(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceIpv6: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceIpv6WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance IPv6 Information
    ApiResponse<GetIpv6Baremetal200Response> response = apiInstance.GetInstanceIpv6WithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceIpv6WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**GetIpv6Baremetal200Response**](GetIpv6Baremetal200Response.md)

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

<a id="getinstanceisostatus"></a>
# **GetInstanceIsoStatus**
> GetInstanceIsoStatus200Response GetInstanceIsoStatus (string instanceId)

Get Instance ISO Status

Get the ISO status for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceIsoStatusExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance ISO Status
                GetInstanceIsoStatus200Response result = apiInstance.GetInstanceIsoStatus(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceIsoStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceIsoStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance ISO Status
    ApiResponse<GetInstanceIsoStatus200Response> response = apiInstance.GetInstanceIsoStatusWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceIsoStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**GetInstanceIsoStatus200Response**](GetInstanceIsoStatus200Response.md)

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

<a id="getinstanceneighbors"></a>
# **GetInstanceNeighbors**
> GetInstanceNeighbors200Response GetInstanceNeighbors (string instanceId)

Get Instance neighbors

Get a list of other instances in the same location as this Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceNeighborsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance neighbors
                GetInstanceNeighbors200Response result = apiInstance.GetInstanceNeighbors(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceNeighbors: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceNeighborsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance neighbors
    ApiResponse<GetInstanceNeighbors200Response> response = apiInstance.GetInstanceNeighborsWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceNeighborsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**GetInstanceNeighbors200Response**](GetInstanceNeighbors200Response.md)

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

<a id="getinstanceupgrades"></a>
# **GetInstanceUpgrades**
> GetInstanceUpgrades200Response GetInstanceUpgrades (string instanceId, string? type = null)

Get Available Instance Upgrades

Get available upgrades for an Instance

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceUpgradesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var type = "type_example";  // string? | Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional) 

            try
            {
                // Get Available Instance Upgrades
                GetInstanceUpgrades200Response result = apiInstance.GetInstanceUpgrades(instanceId, type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceUpgrades: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceUpgradesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Available Instance Upgrades
    ApiResponse<GetInstanceUpgrades200Response> response = apiInstance.GetInstanceUpgradesWithHttpInfo(instanceId, type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceUpgradesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **type** | **string?** | Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans | [optional]  |

### Return type

[**GetInstanceUpgrades200Response**](GetInstanceUpgrades200Response.md)

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

<a id="getinstanceuserdata"></a>
# **GetInstanceUserdata**
> GetInstanceUserdata200Response GetInstanceUserdata (string instanceId)

Get Instance User Data

Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class GetInstanceUserdataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Get Instance User Data
                GetInstanceUserdata200Response result = apiInstance.GetInstanceUserdata(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.GetInstanceUserdata: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetInstanceUserdataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Instance User Data
    ApiResponse<GetInstanceUserdata200Response> response = apiInstance.GetInstanceUserdataWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.GetInstanceUserdataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**GetInstanceUserdata200Response**](GetInstanceUserdata200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="haltinstance"></a>
# **HaltInstance**
> void HaltInstance (string instanceId)

Halt Instance

Halt an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class HaltInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Halt Instance
                apiInstance.HaltInstance(instanceId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.HaltInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the HaltInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Halt Instance
    apiInstance.HaltInstanceWithHttpInfo(instanceId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.HaltInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

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

<a id="haltinstances"></a>
# **HaltInstances**
> void HaltInstances (HaltInstancesRequest? haltInstancesRequest = null)

Halt Instances

Halt Instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class HaltInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var haltInstancesRequest = new HaltInstancesRequest?(); // HaltInstancesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Halt Instances
                apiInstance.HaltInstances(haltInstancesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.HaltInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the HaltInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Halt Instances
    apiInstance.HaltInstancesWithHttpInfo(haltInstancesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.HaltInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **haltInstancesRequest** | [**HaltInstancesRequest?**](HaltInstancesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="listinstanceipv6reverse"></a>
# **ListInstanceIpv6Reverse**
> ListInstanceIpv6Reverse200Response ListInstanceIpv6Reverse (string instanceId)

List Instance IPv6 Reverse

List the reverse IPv6 information for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstanceIpv6ReverseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // List Instance IPv6 Reverse
                ListInstanceIpv6Reverse200Response result = apiInstance.ListInstanceIpv6Reverse(instanceId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ListInstanceIpv6Reverse: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInstanceIpv6ReverseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Instance IPv6 Reverse
    ApiResponse<ListInstanceIpv6Reverse200Response> response = apiInstance.ListInstanceIpv6ReverseWithHttpInfo(instanceId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ListInstanceIpv6ReverseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

### Return type

[**ListInstanceIpv6Reverse200Response**](ListInstanceIpv6Reverse200Response.md)

### Authorization

[API Key](../README.md#API Key)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="listinstanceprivatenetworks"></a>
# **ListInstancePrivateNetworks**
> ListInstancePrivateNetworks200Response ListInstancePrivateNetworks (string instanceId, int? perPage = null, string? cursor = null)

List instance Private Networks

**Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.<br><br>List the private networks for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstancePrivateNetworksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List instance Private Networks
                ListInstancePrivateNetworks200Response result = apiInstance.ListInstancePrivateNetworks(instanceId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ListInstancePrivateNetworks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInstancePrivateNetworksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List instance Private Networks
    ApiResponse<ListInstancePrivateNetworks200Response> response = apiInstance.ListInstancePrivateNetworksWithHttpInfo(instanceId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ListInstancePrivateNetworksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListInstancePrivateNetworks200Response**](ListInstancePrivateNetworks200Response.md)

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

<a id="listinstancevpc2"></a>
# **ListInstanceVpc2**
> ListInstanceVpc2200Response ListInstanceVpc2 (string instanceId, int? perPage = null, string? cursor = null)

List Instance VPC 2.0 Networks

List the VPC 2.0 networks for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstanceVpc2Example
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List Instance VPC 2.0 Networks
                ListInstanceVpc2200Response result = apiInstance.ListInstanceVpc2(instanceId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ListInstanceVpc2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInstanceVpc2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Instance VPC 2.0 Networks
    ApiResponse<ListInstanceVpc2200Response> response = apiInstance.ListInstanceVpc2WithHttpInfo(instanceId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ListInstanceVpc2WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListInstanceVpc2200Response**](ListInstanceVpc2200Response.md)

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

<a id="listinstancevpcs"></a>
# **ListInstanceVpcs**
> ListInstanceVpcs200Response ListInstanceVpcs (string instanceId, int? perPage = null, string? cursor = null)

List instance VPCs

List the VPCs for an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstanceVpcsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 

            try
            {
                // List instance VPCs
                ListInstanceVpcs200Response result = apiInstance.ListInstanceVpcs(instanceId, perPage, cursor);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ListInstanceVpcs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInstanceVpcsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List instance VPCs
    ApiResponse<ListInstanceVpcs200Response> response = apiInstance.ListInstanceVpcsWithHttpInfo(instanceId, perPage, cursor);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ListInstanceVpcsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |

### Return type

[**ListInstanceVpcs200Response**](ListInstanceVpcs200Response.md)

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

<a id="listinstances"></a>
# **ListInstances**
> ListInstances200Response ListInstances (int? perPage = null, string? cursor = null, string? tag = null, string? label = null, string? mainIp = null, string? region = null, string? firewallGroupId = null)

List Instances

List all VPS instances in your account.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ListInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var perPage = 56;  // int? | Number of items requested per page. Default is 100 and Max is 500. (optional) 
            var cursor = "cursor_example";  // string? | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional) 
            var tag = "tag_example";  // string? | Filter by specific tag. (optional) 
            var label = "label_example";  // string? | Filter by label. (optional) 
            var mainIp = "mainIp_example";  // string? | Filter by main ip address. (optional) 
            var region = "region_example";  // string? | Filter by [Region id](#operation/list-regions). (optional) 
            var firewallGroupId = "firewallGroupId_example";  // string? | Filter by [Firewall group id](#operation/list-firewall-groups). (optional) 

            try
            {
                // List Instances
                ListInstances200Response result = apiInstance.ListInstances(perPage, cursor, tag, label, mainIp, region, firewallGroupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ListInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ListInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Instances
    ApiResponse<ListInstances200Response> response = apiInstance.ListInstancesWithHttpInfo(perPage, cursor, tag, label, mainIp, region, firewallGroupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ListInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **perPage** | **int?** | Number of items requested per page. Default is 100 and Max is 500. | [optional]  |
| **cursor** | **string?** | Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). | [optional]  |
| **tag** | **string?** | Filter by specific tag. | [optional]  |
| **label** | **string?** | Filter by label. | [optional]  |
| **mainIp** | **string?** | Filter by main ip address. | [optional]  |
| **region** | **string?** | Filter by [Region id](#operation/list-regions). | [optional]  |
| **firewallGroupId** | **string?** | Filter by [Firewall group id](#operation/list-firewall-groups). | [optional]  |

### Return type

[**ListInstances200Response**](ListInstances200Response.md)

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
| **422** | Validation Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="postinstancesinstanceidipv4reversedefault"></a>
# **PostInstancesInstanceIdIpv4ReverseDefault**
> void PostInstancesInstanceIdIpv4ReverseDefault (string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = null)

Set Default Reverse DNS Entry

Set a reverse DNS entry for an IPv4 address

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class PostInstancesInstanceIdIpv4ReverseDefaultExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var postInstancesInstanceIdIpv4ReverseDefaultRequest = new PostInstancesInstanceIdIpv4ReverseDefaultRequest?(); // PostInstancesInstanceIdIpv4ReverseDefaultRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Set Default Reverse DNS Entry
                apiInstance.PostInstancesInstanceIdIpv4ReverseDefault(instanceId, postInstancesInstanceIdIpv4ReverseDefaultRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.PostInstancesInstanceIdIpv4ReverseDefault: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Default Reverse DNS Entry
    apiInstance.PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo(instanceId, postInstancesInstanceIdIpv4ReverseDefaultRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **postInstancesInstanceIdIpv4ReverseDefaultRequest** | [**PostInstancesInstanceIdIpv4ReverseDefaultRequest?**](PostInstancesInstanceIdIpv4ReverseDefaultRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="rebootinstance"></a>
# **RebootInstance**
> void RebootInstance (string instanceId)

Reboot Instance

Reboot an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RebootInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Reboot Instance
                apiInstance.RebootInstance(instanceId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.RebootInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RebootInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reboot Instance
    apiInstance.RebootInstanceWithHttpInfo(instanceId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.RebootInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

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

<a id="rebootinstances"></a>
# **RebootInstances**
> void RebootInstances (RebootInstancesRequest? rebootInstancesRequest = null)

Reboot instances

Reboot Instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RebootInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var rebootInstancesRequest = new RebootInstancesRequest?(); // RebootInstancesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Reboot instances
                apiInstance.RebootInstances(rebootInstancesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.RebootInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RebootInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reboot instances
    apiInstance.RebootInstancesWithHttpInfo(rebootInstancesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.RebootInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **rebootInstancesRequest** | [**RebootInstancesRequest?**](RebootInstancesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="reinstallinstance"></a>
# **ReinstallInstance**
> CreateInstance202Response ReinstallInstance (string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = null)

Reinstall Instance

Reinstall an Instance using an optional `hostname`.  **Note:** This action may take a few extra seconds to complete.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class ReinstallInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var reinstallInstanceRequest = new ReinstallInstanceRequest?(); // ReinstallInstanceRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Reinstall Instance
                CreateInstance202Response result = apiInstance.ReinstallInstance(instanceId, reinstallInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.ReinstallInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ReinstallInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Reinstall Instance
    ApiResponse<CreateInstance202Response> response = apiInstance.ReinstallInstanceWithHttpInfo(instanceId, reinstallInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.ReinstallInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **reinstallInstanceRequest** | [**ReinstallInstanceRequest?**](ReinstallInstanceRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateInstance202Response**](CreateInstance202Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="restoreinstance"></a>
# **RestoreInstance**
> RestoreInstance202Response RestoreInstance (string instanceId, RestoreInstanceRequest? restoreInstanceRequest = null)

Restore Instance

Restore an Instance from either `backup_id` or `snapshot_id`.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class RestoreInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var restoreInstanceRequest = new RestoreInstanceRequest?(); // RestoreInstanceRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Restore Instance
                RestoreInstance202Response result = apiInstance.RestoreInstance(instanceId, restoreInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.RestoreInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RestoreInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Restore Instance
    ApiResponse<RestoreInstance202Response> response = apiInstance.RestoreInstanceWithHttpInfo(instanceId, restoreInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.RestoreInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **restoreInstanceRequest** | [**RestoreInstanceRequest?**](RestoreInstanceRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**RestoreInstance202Response**](RestoreInstance202Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="startinstance"></a>
# **StartInstance**
> void StartInstance (string instanceId)

Start instance

Start an Instance.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).

            try
            {
                // Start instance
                apiInstance.StartInstance(instanceId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.StartInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start instance
    apiInstance.StartInstanceWithHttpInfo(instanceId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.StartInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |

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

<a id="startinstances"></a>
# **StartInstances**
> void StartInstances (StartInstancesRequest? startInstancesRequest = null)

Start instances

Start Instances.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class StartInstancesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var startInstancesRequest = new StartInstancesRequest?(); // StartInstancesRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Start instances
                apiInstance.StartInstances(startInstancesRequest);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.StartInstances: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the StartInstancesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start instances
    apiInstance.StartInstancesWithHttpInfo(startInstancesRequest);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.StartInstancesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **startInstancesRequest** | [**StartInstancesRequest?**](StartInstancesRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

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

<a id="updateinstance"></a>
# **UpdateInstance**
> CreateInstance202Response UpdateInstance (string instanceId, UpdateInstanceRequest? updateInstanceRequest = null)

Update Instance

Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing `os_id`, `app_id` or `image_id` may take a few extra seconds to complete.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class UpdateInstanceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new InstancesApi(config);
            var instanceId = "instanceId_example";  // string | The [Instance ID](#operation/list-instances).
            var updateInstanceRequest = new UpdateInstanceRequest?(); // UpdateInstanceRequest? | Include a JSON object in the request body with a content type of **application/json**. (optional) 

            try
            {
                // Update Instance
                CreateInstance202Response result = apiInstance.UpdateInstance(instanceId, updateInstanceRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling InstancesApi.UpdateInstance: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UpdateInstanceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Instance
    ApiResponse<CreateInstance202Response> response = apiInstance.UpdateInstanceWithHttpInfo(instanceId, updateInstanceRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling InstancesApi.UpdateInstanceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **instanceId** | **string** | The [Instance ID](#operation/list-instances). |  |
| **updateInstanceRequest** | [**UpdateInstanceRequest?**](UpdateInstanceRequest?.md) | Include a JSON object in the request body with a content type of **application/json**. | [optional]  |

### Return type

[**CreateInstance202Response**](CreateInstance202Response.md)

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
| **404** | Not Found |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

