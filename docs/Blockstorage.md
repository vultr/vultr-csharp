# Org.OpenAPITools.Model.Blockstorage
Block Storage information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Block Storage. | [optional] 
**Cost** | **int** | The monthly cost of this Block Storage. | [optional] 
**Status** | **string** | The current status of this Block Storage.  * active | [optional] 
**SizeGb** | **int** | Size of the Block Storage in GB. | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) where the Block Storage is located. | [optional] 
**AttachedToInstance** | **string** | The [Instance id](#operation/list-instances) with this Block Storage attached. | [optional] 
**DateCreated** | **string** | The date this Block Storage was created. | [optional] 
**Label** | **string** | The user-supplied label. | [optional] 
**MountId** | **string** | An ID associated with the instance, when mounted the ID can be found in /dev/disk/by-id prefixed with virtio. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

