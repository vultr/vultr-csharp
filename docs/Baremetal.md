# Org.OpenAPITools.Model.Baremetal
Bare Metal information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Bare Metal instance. | [optional] 
**Os** | **string** | The [Operating System name](#operation/list-os). | [optional] 
**Ram** | **string** | Text description of the instances&#39; RAM. | [optional] 
**Disk** | **string** | Text description of the instances&#39; disk configuration. | [optional] 
**MainIp** | **string** | The main IPv4 address. | [optional] 
**CpuCount** | **int** | Number of CPUs. | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) where the instance is located. | [optional] 
**DefaultPassword** | **string** | The default password assigned at deployment. Only available for ten minutes after deployment. | [optional] 
**DateCreated** | **string** | The date this instance was created. | [optional] 
**Status** | **string** | The current status.  * active * pending * suspended | [optional] 
**NetmaskV4** | **string** | The IPv4 netmask in dot-decimal notation. | [optional] 
**GatewayV4** | **string** | The IPv4 gateway address. | [optional] 
**Plan** | **string** | The [Bare Metal Plan id](#operation/list-metal-plans) used by this instance. | [optional] 
**Label** | **string** | The user-supplied label for this instance. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag for this instance. | [optional] 
**OsId** | **int** | The [Operating System id](#operation/list-os). | [optional] 
**AppId** | **int** | The [Application id](#operation/list-applications). | [optional] 
**ImageId** | **string** | The [Application image_id](#operation/list-applications). | [optional] 
**V6Network** | **string** | The IPv6 network size in bits. | [optional] 
**V6MainIp** | **string** | The main IPv6 network address. | [optional] 
**V6NetworkSize** | **int** | The IPv6 subnet. | [optional] 
**MacAddress** | **int** | The MAC address for a Bare Metal server. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance. | [optional] 
**UserScheme** | **string** | The user scheme.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

