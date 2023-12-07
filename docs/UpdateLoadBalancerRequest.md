# Org.OpenAPITools.Model.UpdateLoadBalancerRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Ssl** | [**CreateLoadBalancerRequestSsl**](CreateLoadBalancerRequestSsl.md) |  | [optional] 
**StickySession** | [**CreateLoadBalancerRequestStickySession**](CreateLoadBalancerRequestStickySession.md) |  | [optional] 
**ForwardingRules** | [**List&lt;CreateLoadBalancerRequestForwardingRulesInner&gt;**](CreateLoadBalancerRequestForwardingRulesInner.md) | An array of forwarding rule objects. | [optional] 
**HealthCheck** | [**UpdateLoadBalancerRequestHealthCheck**](UpdateLoadBalancerRequestHealthCheck.md) |  | [optional] 
**ProxyProtocol** | **bool** | If &#x60;true&#x60;, you must configure backend nodes to accept Proxy protocol.  * true * false (Default) | [optional] 
**SslRedirect** | **bool** | If &#x60;true&#x60;, this will redirect all HTTP traffic to HTTPS. You must have an HTTPS rule and SSL certificate installed on the load balancer to enable this option.  * true * false | [optional] 
**Http2** | **bool** | If &#x60;true&#x60;, this will enable HTTP2 traffic. You must have an HTTPS forwarding rule combo (HTTPS -&gt; HTTPS) to enable this option.  * true * false | [optional] 
**Nodes** | **int** | The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1. | [optional] 
**BalancingAlgorithm** | **string** | The balancing algorithm.  * roundrobin (default) * leastconn | [optional] 
**Instances** | **List&lt;string&gt;** | Send the complete array of Instances IDs that should be attached to this Load Balancer. Instances will be attached or detached to match your array. For example, if Instances **X**, **Y**, and **Z** are currently attached, and you send [A,B,Z], then Instance **A** and **B** will be attached,  **X** and **Y** will be detached, and **Z** will remain attached. | [optional] 
**Label** | **string** | The label for your Load Balancer | [optional] 
**PrivateNetwork** | **string** | Use &#x60;vpc&#x60; instead. ID of the private network you wish to use. If private_network is omitted it will default to the public network. | [optional] 
**Vpc** | **string** | ID of the VPC you wish to use. If a VPC ID is omitted it will default to the public network. | [optional] 
**FirewallRules** | [**List&lt;CreateLoadBalancerRequestFirewallRulesInner&gt;**](CreateLoadBalancerRequestFirewallRulesInner.md) | An array of firewall rule objects. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

