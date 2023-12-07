# Org.OpenAPITools.Model.CreateBlockRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | The [Region id](#operation/list-regions) where the Block Storage will be created. | 
**SizeGb** | **int** | Size in GB may range between 10 and 40000, depending on the &#x60;block_type&#x60;. | 
**Label** | **string** | The user-supplied label. | [optional] 
**BlockType** | **string** | An optional parameter, that determines on the type of block storage volume that will be created. Soon to become a required parameter.  * &#x60;high_perf&#x60; from 10GB to 10,000GB * &#x60;storage_opt&#x60; from 40GB to 40,000GB | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

