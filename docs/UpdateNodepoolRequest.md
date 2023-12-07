# Org.OpenAPITools.Model.UpdateNodepoolRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NodeQuantity** | **int** | Number of instances in the NodePool. Minimum of 1 is required, but at least 3 is recommended. | [optional] 
**Tag** | **string** | Tag for your node pool | [optional] 
**AutoScaler** | **bool** | Option to use the auto scaler for your cluster. Default false. | [optional] 
**MinNodes** | **int** | Auto scaler field for minimum nodes you want for your cluster. Default 1. | [optional] 
**MaxNodes** | **int** | Auto scaler field for maximum nodes you want for your cluster. Default 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

