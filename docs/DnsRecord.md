# Org.OpenAPITools.Model.DnsRecord
DNS Record information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the DNS Record. | [optional] 
**Type** | **string** | The DNS record type.  * A * AAAA * CNAME * NS * MX * SRV * TXT * CAA * SSHFP | [optional] 
**Name** | **string** | The hostname for this DNS record. | [optional] 
**Data** | **string** | The DNS data for this record type. | [optional] 
**Priority** | **int** | DNS priority. Does not apply to all record types. | [optional] 
**Ttl** | **int** | Time to Live in seconds. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

