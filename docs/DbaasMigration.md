# Org.OpenAPITools.Model.DbaasMigration
Managed Database migration information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Status** | **string** | The current status of the attached migration. * &#x60;complete&#x60; * &#x60;error&#x60; * &#x60;pending&#x60; * &#x60;running&#x60; | [optional] 
**Method** | **string** | The type of migration performed (dump or replication). Only shows if status is &#x60;complete&#x60;. | [optional] 
**Error** | **string** | The verbose error message output for migrations with an &#x60;error&#x60; status. | [optional] 
**Credentials** | [**DbaasMigrationCredentials**](DbaasMigrationCredentials.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

