# Org.OpenAPITools.Model.UpdateBaremetalRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**UserData** | **string** | The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) to attach to this instance. | [optional] 
**Label** | **string** | The user-supplied label. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag. | [optional] 
**OsId** | **int** | If supplied, reinstall the instance using this [Operating System id](#operation/list-os). | [optional] 
**AppId** | **int** | If supplied, reinstall the instance using this [Application id](#operation/list-applications). | [optional] 
**ImageId** | **string** | If supplied, reinstall the instance using this [Application image_id](#operation/list-applications). | [optional] 
**EnableIpv6** | **bool** | Enable IPv6.  * true | [optional] 
**AttachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to attach to this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter. | [optional] 
**DetachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to detach from this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. | [optional] 
**EnableVpc2** | **bool** | If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance. | [optional] 
**UserScheme** | **string** | Linux-only: The user scheme used for logging into this instance. The instance must be reinstalled for this change to take effect.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

