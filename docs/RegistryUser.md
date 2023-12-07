# Org.OpenAPITools.Model.RegistryUser
Container Registry User Entity

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** | The Numeric ID of this user. | [optional] 
**Username** | **string** | The globally unique name of this user. | [optional] 
**Password** | **string** | The password this user will use to authenticate. | [optional] 
**Root** | **bool** | If true, this is a root user/registry owner meaning it cannot be deleted or renamed. If false, this is an additional user added to this registry that can be modified | [optional] 
**AddedAt** | **string** | The date this User was added | [optional] 
**UpdatedAt** | **string** | The date this User was last updated | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

