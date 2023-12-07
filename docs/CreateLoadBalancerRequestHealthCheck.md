# Org.OpenAPITools.Model.CreateLoadBalancerRequestHealthCheck
The health check configuration. See [Load Balancer documentation](https://www.vultr.com/docs/vultr-load-balancers/#Load_Balancer_Configuration).

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Protocol** | **string** | The protocol to use for health checks.  * HTTPS * HTTP * TCP | [optional] 
**Port** | **int** | The port to use for health checks. | [optional] 
**Path** | **string** | HTTP Path to check. Only applies if protocol is HTTP, or HTTPS. | [optional] 
**CheckInterval** | **int** | Interval between health checks. | [optional] 
**ResponseTimeout** | **int** | Timeout before health check fails. | [optional] 
**UnhealthyThreshold** | **int** | Number times a check must fail before becoming unhealthy. | [optional] 
**HealthyThreshold** | **int** | Number of times a check must succeed before returning to healthy status. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

