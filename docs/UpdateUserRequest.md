# Org.OpenAPITools.Model.UpdateUserRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Email** | **string** | The User&#39;s email address. | [optional] 
**Name** | **string** | The User&#39;s name. | [optional] 
**Password** | **string** | The User&#39;s password. | [optional] 
**ApiEnabled** | **bool** | API access is permitted for this User.  * true (default) * false | [optional] 
**Acls** | **List&lt;string&gt;** | An array of permission granted. Valid values:  * abuse * alerts * billing * dns * firewall * loadbalancer * manage\\_users * objstore * provisioning * subscriptions * subscriptions\\_view * support * upgrade | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

