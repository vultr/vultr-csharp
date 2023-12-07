# Org.OpenAPITools.Model.CreateInstanceRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | The [Region id](#operation/list-regions) where the Instance is located. | 
**Plan** | **string** | The [Plan id](#operation/list-plans) to use when deploying this instance. | 
**OsId** | **int** | The [Operating System id](#operation/list-os) to use when deploying this instance. | [optional] 
**IpxeChainUrl** | **string** | The URL location of the iPXE chainloader. | [optional] 
**IsoId** | **string** | The [ISO id](#operation/list-isos) to use when deploying this instance. | [optional] 
**ScriptId** | **string** | The [Startup Script id](#operation/list-startup-scripts) to use when deploying this instance. | [optional] 
**SnapshotId** | **string** | The [Snapshot id](#operation/list-snapshots) to use when deploying the instance. | [optional] 
**EnableIpv6** | **bool** | Enable IPv6.  * true | [optional] 
**DisablePublicIpv4** | **bool** | Don&#39;t set up a public IPv4 address when IPv6 is enabled. Will not do anything unless &#x60;enable_ipv6&#x60; is also &#x60;true&#x60;.  * true | [optional] 
**AttachPrivateNetwork** | **List&lt;string&gt;** | Use &#x60;attach_vpc&#x60; instead. An array of [Private Network ids](#operation/list-networks) to attach to this Instance. This parameter takes precedence over &#x60;enable_private_network&#x60;. Please choose one parameter. | [optional] 
**AttachVpc** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpcs) to attach to this Instance. This parameter takes precedence over &#x60;enable_vpc&#x60;. Please choose one parameter. | [optional] 
**AttachVpc2** | **List&lt;string&gt;** | An array of [VPC IDs](#operation/list-vpc2) to attach to this Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter. | [optional] 
**Label** | **string** | A user-supplied label for this instance. | [optional] 
**SshkeyId** | **List&lt;string&gt;** | The [SSH Key id](#operation/list-ssh-keys) to install on this instance. | [optional] 
**Backups** | **string** | Enable automatic backups for the instance.  * enabled * disabled | [optional] 
**AppId** | **int** | The [Application id](#operation/list-applications) to use when deploying this instance. | [optional] 
**ImageId** | **string** | The [Application image_id](#operation/list-applications) to use when deploying this instance. | [optional] 
**UserData** | **string** | The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) to attach to this instance. | [optional] 
**DdosProtection** | **bool** | Enable DDoS protection (there is an additional charge for this).  * true * false | [optional] 
**ActivationEmail** | **bool** | Notify by email after deployment.  * true * false (default) | [optional] 
**Hostname** | **string** | The hostname to use when deploying this instance. | [optional] 
**Tag** | **string** | Use &#x60;tags&#x60; instead. The user-supplied tag. | [optional] 
**FirewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups) to attach to this Instance. | [optional] 
**ReservedIpv4** | **string** | ID of the floating IP to use as the main IP of this server. | [optional] 
**EnablePrivateNetwork** | **bool** | Use &#x60;enable_vpc&#x60; instead.  If &#x60;true&#x60;, private networking support will be added to the new server.  This parameter attaches a single network. When no network exists in the region, it will be automatically created.  If there are multiple private networks in the instance&#39;s region, use &#x60;attach_private_network&#x60; instead to specify a network. | [optional] 
**EnableVpc** | **bool** | If &#x60;true&#x60;, VPC support will be added to the new server.  This parameter attaches a single VPC. When no VPC exists in the region, it will be automatically created.  If there are multiple VPCs in the instance&#39;s region, use &#x60;attach_vpc&#x60; instead to specify a network. | [optional] 
**EnableVpc2** | **bool** | If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 network. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a network. | [optional] 
**Tags** | **List&lt;string&gt;** | Tags to apply to the instance | [optional] 
**UserScheme** | **string** | Linux-only: The user scheme used for logging into this instance. By default, the \&quot;root\&quot; user is configured. Alternatively, a limited user with sudo permissions can be selected.  * root * limited | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

