# Org.OpenAPITools.Model.CreateBaremetalRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | The [Region id](#operation/list-regions) to create the instance. | 
**Plan** | **string** | The [Bare Metal plan id](#operation/list-metal-plans) to use for this instance. | 
**ScriptId** | **string** | The [Startup Script id](#operation/list-startup-scripts) to use for this instance. | [optional] 
**EnableIpv6** | **bool** | Enable IPv6.  * true | [optional] 
**SshkeyId** | **List&lt;string&gt;** | The [SSH Key id](#operation/list-ssh-keys) to install on this instance. | [optional] 
**UserData** | **string** | The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for this Instance. | [optional] 
**Label** | **string** | The user-supplied label. | [optional] 
**ActivationEmail** | **bool** | Notify by email after deployment.  * true * false (default) | [optional] 
**Hostname** | **string** | The user-supplied hostname to use when deploying this instance. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag. | [optional] 
**ReservedIpv4** | **string** | The [Reserved IP id](#operation/list-reserved-ips) for this instance. | [optional] 
**OsId** | **int** | If supplied, deploy the instance using this [Operating System id](#operation/list-os). | [optional] 
**SnapshotId** | **string** | If supplied, deploy the instance using this [Snapshot ID](#operation/list-snapshots). | [optional] 
**AppId** | **int** | If supplied, deploy the instance using this [Application id](#operation/list-applications). | [optional] 
**ImageId** | **string** | If supplied, deploy the instance using this [Application image_id](#operation/list-applications). | [optional] 
**PersistentPxe** | **bool** | Enable persistent PXE.  * true * false (default) | [optional] 
**AttachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to attach to this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter. | [optional] 
**DetachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to detach from this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. | [optional] 
**EnableVpc2** | **bool** | If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance. | [optional] 
**UserScheme** | **string** | Linux-only: The user scheme used for logging into this instance. By default, the \&quot;root\&quot; user is configured. Alternatively, a limited user with sudo permissions can be selected.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

