# Org.OpenAPITools.Model.CreateLoadBalancerRequestFirewallRulesInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Port** | **int** | Port for this rule. | [optional] 
**Source** | **string** | If the source string is given a value of \&quot;cloudflare\&quot; then cloudflare IPs will be supplied. Otherwise enter a IP address with subnet size that you wish to permit through the firewall.  Possible values:  |   | Value | Description | | - | - -- -- - | - -- -- -- -- -- -- | |   | \&quot;192.168.1.1/16\&quot; | Ip address with a subnet size. | |   | cloudflare | Allow all of Cloudflare&#39;s IP space through the firewall | | [optional] 
**IpType** | **string** | The type of IP rule.  * v4 * v6 | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

