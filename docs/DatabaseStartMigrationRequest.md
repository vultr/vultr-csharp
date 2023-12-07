# Org.OpenAPITools.Model.DatabaseStartMigrationRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Host** | **string** | The host name of the source server. | 
**Port** | [**Int**](Int.md) | The connection port of the source server. | 
**Username** | **string** | The username of the source server. Uses &#x60;default&#x60; for Redis if left empty or unset. | 
**Password** | **string** | The password of the source server. | 
**Database** | **string** | The database of the source server. Required for MySQL/PostgreSQL engine types, but excluded for Redis. | [optional] 
**IgnoredDatabases** | **string** | Comma-separated list of ignored databases on the source server. Excluded for Redis engine types. | [optional] 
**Ssl** | **bool** | The true/false value for whether SSL is needed to connect to the source server. | 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

