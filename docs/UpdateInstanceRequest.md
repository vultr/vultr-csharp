# Org.OpenAPITools.Model.UpdateInstanceRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AppId** | **int** | Reinstall the instance with this [Application id](#operation/list-applications). | [optional] 
**ImageId** | **string** | Reinstall the instance with this [Application image_id](#operation/list-applications). | [optional] 
**Backups** | **string** | Enable automatic backups for the instance.  * enabled * disabled | [optional] 
**FirewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups) to attach to this Instance. | [optional] 
**EnableIpv6** | **bool** | Enable IPv6.  * true | [optional] 
**OsId** | **string** | Reinstall the instance with this [ISO id](#operation/list-isos). | [optional] 
**UserData** | **string** | The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) to attach to this instance. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag. | [optional] 
**Plan** | **string** | Upgrade the instance with this [Plan id](#operation/list-plans). | [optional] 
**DdosProtection** | **bool** | Enable DDoS Protection (there is an additional charge for this).  * true * false | [optional] 
**AttachPrivateNetwork** | **List&lt;string&gt;** | Use &#x60;attach_vpc&#x60; instead. An array of [Private Network ids](#operation/list-networks) to attach to this Instance. This parameter takes precedence over &#x60;enable_private_network&#x60;. Please choose one parameter. | [optional] 
**AttachVpc** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpcs) to attach to this Instance. This parameter takes precedence over &#x60;enable_vpc&#x60;. Please choose one parameter. | [optional] 
**AttachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to attach to this Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter. | [optional] 
**DetachPrivateNetwork** | **List&lt;string&gt;** | Use &#x60;detach_vpc&#x60; instead. An array of [Private Network ids](#operation/list-networks) to detach from this Instance. This parameter takes precedence over &#x60;enable_private_network&#x60;. | [optional] 
**DetachVpc** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpcs) to detach from this Instance. This parameter takes precedence over &#x60;enable_vpc&#x60;. | [optional] 
**DetachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to detach from this Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. | [optional] 
**EnablePrivateNetwork** | **bool** | Use &#x60;enable_vpc&#x60; instead.  If &#x60;true&#x60;, private networking support will be added to the new server.  This parameter attaches a single network. When no network exists in the region, it will be automatically created.  If there are multiple private networks in the instance&#39;s region, use &#x60;attach_private_network&#x60; instead to specify a network. | [optional] 
**EnableVpc** | **bool** | If &#x60;true&#x60;, VPC support will be added to the new server.  This parameter attaches a single VPC. When no VPC exists in the region, it will be automatically created.  If there are multiple VPCs in the instance&#39;s region, use &#x60;attach_vpc&#x60; instead to specify a VPC. | [optional] 
**EnableVpc2** | **bool** | If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network. | [optional] 
**Label** | **string** | The user supplied label. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance. | [optional] 
**UserScheme** | **string** | Linux-only: The user scheme used for logging into this instance. The instance must be reinstalled for this change to take effect.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

