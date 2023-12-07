# Org.OpenAPITools.Model.PlansMetal
Plans for Bare Metal instances.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Bare Metal Plan. | [optional] 
**CpuCount** | **int** | The number of CPUs in this Plan. | [optional] 
**CpuModel** | **string** | The CPU model type for this instance. | [optional] 
**CpuThreads** | **int** | The numner of supported threads for this instance. | [optional] 
**Ram** | **int** | The amount of RAM in MB. | [optional] 
**Disk** | **string** | The disk size in GB. | [optional] 
**Bandwidth** | **int** | The monthly bandwidth quota in GB. | [optional] 
**Locations** | **List&lt;string&gt;** | An array of Regions where this plan is valid for use. | [optional] 
**Type** | **string** | The plan type.  * SSD | [optional] 
**MonthlyCost** | **int** | The monthly cost in US Dollars. | [optional] 
**DiskCount** | **int** | The number of disks that this plan offers. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

