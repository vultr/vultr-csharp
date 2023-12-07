# Org.OpenAPITools.Model.Plans
Plans for VPS instances.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Plan. | [optional] 
**Name** | **string** | The Plan name. | [optional] 
**VcpuCount** | **int** | The number of vCPUs in this Plan. | [optional] 
**Ram** | **int** | The amount of RAM in MB. | [optional] 
**Disk** | **int** | The disk size in GB. | [optional] 
**Bandwidth** | **int** | The monthly bandwidth quota in GB. | [optional] 
**MonthlyCost** | **int** | The monthly cost in US Dollars. | [optional] 
**Type** | **string** | The plan type.  |   | Type | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | vc2 | Cloud Compute | |   | vhf | High Frequency Compute | |   | vdc | Dedicated Cloud | | [optional] 
**Locations** | **List&lt;string&gt;** | An array of Regions where this plan is valid for use. | [optional] 
**DiskCount** | **int** | The number of disks that this plan offers. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

