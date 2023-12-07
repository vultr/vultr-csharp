# Org.OpenAPITools.Model.ObjectStorage
Object Storage information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Object Storage. | [optional] 
**DateCreated** | **string** | Date the Object Store was created. | [optional] 
**ClusterId** | **int** | The [Cluster id](#operation/list-object-storage-clusters). | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) for this Object Storage. | [optional] 
**Label** | **string** | The user-supplied label for this Object Storage. | [optional] 
**Status** | **string** | The status of this Object Storage.  * active * pending | [optional] 
**S3Hostname** | **string** | The [Cluster hostname](#operation/list-object-storage-clusters) for this Object Storage. | [optional] 
**S3AccessKey** | **string** | The Object Storage access key. | [optional] 
**S3SecretKey** | **string** | The Object Storage secret key. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

