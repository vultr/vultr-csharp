# Org.OpenAPITools.Model.Account
Account information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | Your user name. | [optional] 
**Email** | **string** | Your email address. | [optional] 
**Acls** | **List&lt;string&gt;** | An array of permission granted. * manage\\_users * subscriptions_view * subscriptions * billing * support * provisioning * dns * abuse * upgrade * firewall * alerts * objstore * loadbalancer | [optional] 
**Balance** | **decimal** | Your current account balance. | [optional] 
**PendingCharges** | **decimal** | Unbilled charges for this month. | [optional] 
**LastPaymentDate** | **string** | Date of your last payment. | [optional] 
**LastPaymentAmount** | **decimal** | The amount of your last payment. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

