# Org.OpenAPITools.Model.DatabaseForkRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Label** | **string** | A user-supplied label for this Managed Database. | 
**Region** | **string** | The [Region id](#operation/list-regions) where the Managed Database is located. | 
**Plan** | **string** | The [Plan id](#operation/list-database-plans) to use when deploying this Managed Database. | 
**VpcId** | **string** | The [VPC id](#operation/list-vpcs) to use when deploying this Managed Database. It can also be set to &#x60;new&#x60; to configure a new VPC network with this deployment. | [optional] 
**Type** | **string** | The type of backup restoration to use for this Managed Database. * &#x60;pitr&#x60;: Point-in-time recovery * &#x60;basebackup&#x60;: Latest backup (default if omitted) | [optional] 
**Date** | **string** | The [backup date](#operation/get-backup-information) to use when restoring the Managed Database in YYYY-MM-DD date format. Required for &#x60;pitr&#x60; type requests. | [optional] 
**Time** | **string** | The [backup time](#operation/get-backup-information) to use when restoring the Managed Database in HH-MM-SS time format (24-hour UTC). Required for &#x60;pitr&#x60; type requests. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

