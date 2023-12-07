# Org.OpenAPITools.Model.CreateNodepoolsRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NodeQuantity** | **int** | Number of instances in this nodepool | 
**Label** | **string** | Label for the nodepool. You cannot change the label after a nodepool is created. You cannot have duplicate node pool labels in the same cluster. | 
**Plan** | **string** | Plan that this nodepool will use | 
**Tag** | **string** | Tag for node pool | [optional] 
**AutoScaler** | **bool** | Option to use the auto scaler with your cluster. Default false. | [optional] 
**MinNodes** | **int** | Auto scaler field for minimum nodes you want for your cluster. Default 1. | [optional] 
**MaxNodes** | **int** | Auto scaler field for maximum nodes you want for your cluster. Default 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

