# Org.OpenAPITools.Model.DatabaseRestoreFromBackupRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Label** | **string** | A user-supplied label for this Managed Database. | 
**Type** | **string** | The type of backup restoration to use for this Managed Database. * &#x60;pitr&#x60;: Point-in-time recovery * &#x60;basebackup&#x60;: Latest backup (default if omitted) | [optional] 
**Date** | **string** | The [backup date](#operation/get-backup-information) to use when restoring the Managed Database in YYYY-MM-DD date format. Required for &#x60;pitr&#x60; type requests. | [optional] 
**Time** | **string** | The [backup time](#operation/get-backup-information) to use when restoring the Managed Database in HH-MM-SS time format (24-hour UTC). Required for &#x60;pitr&#x60; type requests. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

