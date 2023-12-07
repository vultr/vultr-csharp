# Org.OpenAPITools.Model.Instance
Instance information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the VPS Instance. | [optional] 
**Os** | **string** | The [Operating System name](#operation/list-os). | [optional] 
**Ram** | **int** | The amount of RAM in MB. | [optional] 
**Disk** | **int** | The size of the disk in GB. | [optional] 
**MainIp** | **string** | The main IPv4 address. | [optional] 
**VcpuCount** | **int** | Number of vCPUs. | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) where the Instance is located. | [optional] 
**DefaultPassword** | **string** | The default password assigned at deployment. Only available for ten minutes after deployment. | [optional] 
**DateCreated** | **string** | The date this instance was created. | [optional] 
**Status** | **string** | The current status.  * active * pending * suspended * resizing | [optional] 
**PowerStatus** | **string** | The power-on status.  * running * stopped | [optional] 
**ServerStatus** | **string** | The server health status.  * none * locked * installingbooting * ok | [optional] 
**AllowedBandwidth** | **int** | Monthly bandwidth quota in GB. | [optional] 
**NetmaskV4** | **string** | The IPv4 netmask in dot-decimal notation. | [optional] 
**GatewayV4** | **string** | The gateway IP address. | [optional] 
**V6Networks** | [**List&lt;InstanceV6NetworksInner&gt;**](InstanceV6NetworksInner.md) | An array of IPv6 objects. | [optional] 
**Hostname** | **string** | The hostname for this instance. | [optional] 
**Label** | **string** | The user-supplied label for this instance. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag for this instance. | [optional] 
**InternalIp** | **string** | The internal IP used by this instance, if set. Only relevant when a VPC is attached. | [optional] 
**Kvm** | **string** | HTTPS link to the Vultr noVNC Web Console. | [optional] 
**OsId** | **int** | The [Operating System id](#operation/list-os) used by this instance. | [optional] 
**AppId** | **int** | The [Application id](#operation/list-applications) used by this instance. | [optional] 
**ImageId** | **string** | The [Application image_id](#operation/list-applications) used by this instance. | [optional] 
**FirewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups) linked to this Instance. | [optional] 
**Features** | **List&lt;string&gt;** | \&quot;auto_backups\&quot;, \&quot;ipv6\&quot;, \&quot;ddos_protection\&quot; | [optional] 
**Plan** | **string** | A unique ID for the Plan. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance. | [optional] 
**UserScheme** | **string** | The user scheme.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

