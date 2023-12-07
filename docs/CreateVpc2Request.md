# Org.OpenAPITools.Model.CreateVpc2Request

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | Create the VPC in this [Region id](#operation/list-regions). | 
**Description** | **string** | A description of the VPC. &lt;/br&gt; Must be no longer than 255 characters and may include only letters, numbers, spaces, underscores and hyphens. | [optional] 
**IpType** | [**Enum**](Enum.md) | Accepted values: * &#x60;v4&#x60; | [optional] 
**IpBlock** | **string** | The VPC subnet IP address. For example: 10.99.0.0&lt;br&gt;&lt;span style&#x3D;\&quot;color: red\&quot;&gt;If a prefix_length is specified then ip_block is a required field.&lt;/span&gt; | [optional] 
**PrefixLength** | **int** | The number of bits for the netmask in CIDR notation. Example: 24&lt;br&gt;&lt;span style&#x3D;\&quot;color: red\&quot;&gt;If an ip_block is specified then prefix_length is a required field.&lt;/span&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

