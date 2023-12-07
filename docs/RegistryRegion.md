# Org.OpenAPITools.Model.RegistryRegion
Container Registry Region Entity

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** | The Numeric ID of this region. | [optional] 
**Name** | **string** | The unique name of this region, this is what you will use to specify a region when creating your subscription | [optional] 
**Urn** | **string** | The base URN (the URL without the scheme [i.e. http:// or https://]) of this region | [optional] 
**BaseUrl** | **string** | The base URL of this region | [optional] 
**VarPublic** | **bool** | If true, this is a publically accessible region allowing any customer to create new subscriptions on this region. If false, this region is not generally available yet | [optional] 
**AddedAt** | **string** | The date this Region was added | [optional] 
**UpdatedAt** | **string** | The date this Region was last updated | [optional] 
**DataCenter** | **Object** | Information on the datacenter this region resides in | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

