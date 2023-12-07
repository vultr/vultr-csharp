# Org.OpenAPITools.Model.CreateLoadBalancerRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Region** | **string** | The [Region id](#operation/list-regions) to create this Load Balancer. | 
**BalancingAlgorithm** | **string** | The balancing algorithm.  * roundrobin (default) * leastconn | [optional] 
**SslRedirect** | **bool** | If &#x60;true&#x60;, this will redirect all HTTP traffic to HTTPS. You must have an HTTPS rule and SSL certificate installed on the load balancer to enable this option.  * true * false | [optional] 
**Http2** | **bool** | If &#x60;true&#x60;, this will enable HTTP2 traffic. You must have an HTTPS forwarding rule combo (HTTPS -&gt; HTTPS) to enable this option.  * true * false | [optional] 
**Nodes** | **int** | The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1. | [optional] 
**ProxyProtocol** | **bool** | If &#x60;true&#x60;, you must configure backend nodes to accept Proxy protocol.  * true * false (Default) | [optional] 
**HealthCheck** | [**CreateLoadBalancerRequestHealthCheck**](CreateLoadBalancerRequestHealthCheck.md) |  | [optional] 
**ForwardingRules** | [**List&lt;CreateLoadBalancerRequestForwardingRulesInner&gt;**](CreateLoadBalancerRequestForwardingRulesInner.md) | An array of forwarding rule objects. | [optional] 
**StickySession** | [**CreateLoadBalancerRequestStickySession**](CreateLoadBalancerRequestStickySession.md) |  | [optional] 
**Ssl** | [**CreateLoadBalancerRequestSsl**](CreateLoadBalancerRequestSsl.md) |  | [optional] 
**Label** | **string** | Label for your Load Balancer. | [optional] 
**Instances** | **List&lt;string&gt;** | An array of instances IDs that you want attached to the load balancer. | [optional] 
**FirewallRules** | [**List&lt;CreateLoadBalancerRequestFirewallRulesInner&gt;**](CreateLoadBalancerRequestFirewallRulesInner.md) | An array of firewall rule objects. | [optional] 
**PrivateNetwork** | **string** | Use &#x60;vpc&#x60; instead. ID of the private network you wish to use. If private_network is omitted it will default to the public network. | [optional] 
**Vpc** | **string** | ID of the VPC you wish to use. If a VPC ID is omitted it will default to the public network. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

