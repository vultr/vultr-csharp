# Org.OpenAPITools.Model.DbaasPlan
Managed Database plan information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the plan. | [optional] 
**NumberOfNodes** | **int** | The total number of nodes for this plan. | [optional] 
**Type** | **string** | The type of plan this is. | [optional] 
**VcpuCount** | **int** | Number of vCPUs. | [optional] 
**Ram** | **int** | The amount of RAM in MB. | [optional] 
**Disk** | **int** | The size of the disk in GB (excluded for Redis engine types). | [optional] 
**MonthlyCost** | **int** | The monthly cost of this Managed Database plan. | [optional] 
**SupportedEngines** | **Object** | A list of key/value pairs with database engine types and boolean values. | [optional] 
**MaxConnections** | **Object** | A list of key/value pairs with database engine types (excluding Redis) and integers of max connection values. | [optional] 
**Locations** | **List&lt;string&gt;** | A list of available regions in which this plan is currently available. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

