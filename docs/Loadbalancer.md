# Org.OpenAPITools.Model.Loadbalancer
Load Balancer information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Load Balancer. | [optional] 
**DateCreated** | **string** | Date this Load Balancer was created. | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) where the Load Balancer is located. | [optional] 
**Label** | **string** | The user-supplied label for this load-balancer. | [optional] 
**Status** | **string** | The current status.  * active | [optional] 
**Ipv4** | **string** | The IPv4 address of this Load Balancer. | [optional] 
**Ipv6** | **string** | The IPv6 address of this Load Balancer. | [optional] 
**GenericInfo** | [**LoadbalancerGenericInfo**](LoadbalancerGenericInfo.md) |  | [optional] 
**HealthCheck** | [**LoadbalancerHealthCheck**](LoadbalancerHealthCheck.md) |  | [optional] 
**HasSsl** | **bool** | Indicates if this Load Balancer has an SSL certificate installed. | [optional] 
**Http2** | **bool** | Indicates if this Load Balancer has HTTP2 enabled. | [optional] 
**Nodes** | **int** | The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1. | [optional] 
**ForwardRules** | [**List&lt;LoadbalancerForwardRulesInner&gt;**](LoadbalancerForwardRulesInner.md) | An array of forwarding rule objects. | [optional] 
**Instances** | **List&lt;string&gt;** | Array of [Instance ids](#operation/list-instances) attached to this Load Balancer. | [optional] 
**FirewallRules** | [**List&lt;LoadbalancerFirewallRulesInner&gt;**](LoadbalancerFirewallRulesInner.md) | An array of firewall rule objects. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

