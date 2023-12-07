# Org.OpenAPITools.Model.CreateVpcRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | Create the VPC in this [Region id](#operation/list-regions). | 
**Description** | **string** | A description of the VPC. | [optional] 
**V4Subnet** | **string** | The IPv4 VPC address. For example: 10.99.0.0&lt;br&gt;&lt;span style&#x3D;\&quot;color: red\&quot;&gt;If v4_subnet_mask is specified then v4_subnet is a required field.&lt;/span&gt; | [optional] 
**V4SubnetMask** | **int** | The number of bits for the netmask in CIDR notation. Example: 24&lt;br&gt;&lt;span style&#x3D;\&quot;color: red\&quot;&gt;If v4_subnet is specified then v4_subnet_mask is a required field.&lt;/span&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

