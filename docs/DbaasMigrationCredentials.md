# Org.OpenAPITools.Model.DbaasMigrationCredentials
Associated list of connection details for the source database server.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Host** | **string** | The host name of the source server. | [optional] 
**Port** | [**Int**](Int.md) | The connection port of the source server. | [optional] 
**Username** | **string** | The username of the source server. | [optional] 
**Password** | **string** | The password of the source server. | [optional] 
**Database** | **string** | The database of the source server. Excluded for Redis engine types. | [optional] 
**IgnoredDatabases** | **string** | Comma-separated list of ignored databases on the source server. Excluded for Redis engine types. | [optional] 
**Ssl** | **bool** | The true/false value for whether SSL is needed to connect to the source server. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

