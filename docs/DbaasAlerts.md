# Org.OpenAPITools.Model.DbaasAlerts
Managed Database alerts information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Timestamp** | **string** | The date and time in which the alert was sent. | [optional] 
**MessageType** | **string** | The category of alert that was sent. * &#x60;DB MASTER PROMOTION&#x60; * &#x60;MAINTENANCE SCHEDULED&#x60; * &#x60;MISSING MYSQL PRIMARY KEYS&#x60; * &#x60;RESOURCE USAGE DISK&#x60; * &#x60;RESOURCE USAGE OOM KILLED&#x60; * &#x60;RESOURCE USAGE PG REPLICATION SLOTS&#x60; | [optional] 
**Description** | **string** | A verbose description of the associated alert category. | [optional] 
**Recommendation** | **string** | A description of the recommended action the customer should take. Only included for certain alert types. | [optional] 
**MaintenanceScheduled** | **string** | The time in which mandatory maintenance has been scheduled with the associated alert. Only included for certain alert types. | [optional] 
**ResourceType** | **string** | The affected resource related to the associated alert. Only included for certain alert types. | [optional] 
**TableCount** | **int** | The number of affected tables related to the associated alert. Only included for certain alert types. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

