# Org.OpenAPITools.Model.ForwardingRule
Forwarding Rule information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Forwarding Rule. | [optional] 
**FrontendProtocol** | **string** | The protocol on the Load Balancer to forward to the backend.  * HTTP * HTTPS * TCP | [optional] 
**FrontendPort** | **int** | The port number on the Load Balancer to forward to the backend. | [optional] 
**BackendProtocol** | **string** | The protocol destination on the backend server.  * HTTP * HTTPS * TCP | [optional] 
**BackendPort** | **int** | The port number destination on the backend server. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

