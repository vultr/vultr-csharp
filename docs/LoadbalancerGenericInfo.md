# Org.OpenAPITools.Model.LoadbalancerGenericInfo
An object containing additional options.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**BalancingAlgorithm** | **string** | The balancing algorithm.  * roundrobin (default) * leastconn | [optional] 
**SslRedirect** | **bool** | If &#x60;true&#x60;, this will redirect all HTTP traffic to HTTPS. You must have an HTTPS rule and SSL certificate installed on the load balancer to enable this option.  * true * false | [optional] 
**StickySessions** | [**LoadbalancerGenericInfoStickySessions**](LoadbalancerGenericInfoStickySessions.md) |  | [optional] 
**ProxyProtocol** | **bool** | \&quot;If &#x60;true&#x60;, you must configure backend nodes to accept Proxy protocol. \\n\\n* true\\n* false (Default)\&quot; | [optional] 
**PrivateNetwork** | **string** | Use &#x60;vpc&#x60; instead. ID of the private network you wish to use. If private_network is omitted it will default to the public network. | [optional] 
**Vpc** | **string** | ID of the VPC you wish to use. If a VPC ID is omitted it will default to the public network. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

