# Org.OpenAPITools.Model.FirewallRule
Firewall rule information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** | A unique ID for the Firewall Rule. | [optional] 
**Type** | **string** | This field is deprecated. Use &#x60;ip_type&#x60; instead.  The type of IP rule.  * v4 * v6 | [optional] 
**IpType** | **string** | The type of IP rule.  * v4 * v6 | [optional] 
**Action** | **string** | Action to take when this rule is met.  * accept | [optional] 
**Protocol** | **string** | The protocol for this rule.  * ICMP * TCP * UDP * GRE  | [optional] 
**Port** | **string** | Port or port range for this rule. | [optional] 
**Subnet** | **string** | IP address representing a subnet. The IP address format must match with the \&quot;ip_type\&quot; parameter value. | [optional] 
**SubnetSize** | **int** | The number of bits for the netmask in CIDR notation. Example: 24 | [optional] 
**Source** | **string** | If the source string is given a value of \&quot;cloudflare\&quot; subnet and subnet_size will both be ignored. Possible values:  |   | Value | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | \&quot;\&quot; | Use the value from &#x60;subnet&#x60; and &#x60;subnet_size&#x60;. | |   | cloudflare | Allow all of Cloudflare&#39;s IP space through the firewall | | [optional] 
**Notes** | **string** | User-supplied notes for this rule. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

