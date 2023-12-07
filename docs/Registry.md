# Org.OpenAPITools.Model.Registry
Container Registry Entity

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The UUID to reference this registry | [optional] 
**Name** | **string** | The globally unique name to reference this registry | [optional] 
**Urn** | **string** | The base URN (the URL without the scheme [i.e. http:// or https://]) of this registry | [optional] 
**Storage** | [**RegistryStorage**](RegistryStorage.md) |  | [optional] 
**DateCreated** | **string** | The date this Registry Subscription was created | [optional] 
**VarPublic** | **bool** | If true, this is a publically accessible registry allowing anyone to pull from it. If false, this registry is completely private | [optional] 
**RootUser** | [**RegistryUser**](RegistryUser.md) |  | [optional] 
**Metadata** | [**RegistryMetadata**](RegistryMetadata.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

