# Org.OpenAPITools.Model.PostFirewallsFirewallGroupIdRulesRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**IpType** | **string** | The type of IP rule.  * v4 * v6 | 
**Protocol** | **string** | The protocol for this rule.  * ICMP * TCP * UDP * GRE * ESP * AH  | 
**Subnet** | **string** | IP address representing a subnet. The IP address format must match with the \&quot;ip_type\&quot; parameter value. | 
**SubnetSize** | **int** | The number of bits for the netmask in CIDR notation. Example: 32 | 
**Port** | **string** | TCP/UDP only. This field can be a specific port or a colon separated port range. | [optional] 
**Source** | **string** | If the source string is given a value of \&quot;cloudflare\&quot; subnet and subnet_size will both be ignored. Possible values:  |   | Value | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | \&quot;\&quot; | Use the value from &#x60;subnet&#x60; and &#x60;subnet_size&#x60;. | |   | cloudflare | Allow all of Cloudflare&#39;s IP space through the firewall | |   | [Load Balancer id](#operation/list-load-balancers) | Provide a load balancer ID to use its IPs |  | [optional] 
**Notes** | **string** | User-supplied notes for this rule. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

