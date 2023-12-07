# Org.OpenAPITools.Model.DbaasAvailableOptions
Managed Database PostgreSQL advanced configuration options.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | The name of the configuration option. | [optional] 
**Type** | **string** | The type of the configuration option. * &#x60;int&#x60; * &#x60;float&#x60; * &#x60;bool&#x60; * &#x60;enum&#x60; | [optional] 
**Enumerals** | **List&lt;string&gt;** | Valid enumerals for &#x60;enum&#x60; type configuration options only. | [optional] 
**MinValue** | [**Mixed**](Mixed.md) | The smallest value accepted for the configuration option. | [optional] 
**MaxValue** | [**Mixed**](Mixed.md) | The largest value accepted for the configuration option. | [optional] 
**AltValues** | [**List&lt;Mixed&gt;**](Mixed.md) | Any alternate value accepted for the configuration option. | [optional] 
**Units** | **string** | The units associated with the configuration option. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

