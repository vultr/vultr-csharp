# Org.OpenAPITools.Model.CreateDnsDomainRecordRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The hostname for this DNS record. | 
**Type** | **string** | The DNS record type.  * A * AAAA * CNAME * NS * MX * SRV * TXT * CAA * SSHFP | 
**Data** | **string** | The DNS data for this record type. | 
**Ttl** | **int** | Time to Live in seconds. | [optional] 
**Priority** | **int** | DNS priority. Does not apply to all record types. (Only required for MX and SRV) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

