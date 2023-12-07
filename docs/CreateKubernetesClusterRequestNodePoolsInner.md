# Org.OpenAPITools.Model.CreateKubernetesClusterRequestNodePoolsInner

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**NodeQuantity** | **int** | Number of instances to deploy in this nodepool. Minimum of 1 node required, but at least 3 is recommended. | 
**Label** | **string** | Label for this nodepool. You cannot change the label after a nodepool is created. You cannot have duplicate node pool labels in the same cluster. | 
**Plan** | **string** | Plan you want this nodepool to use. Note: minimum plan must be $10 | 
**Tag** | **string** | Tag for node pool | [optional] 
**AutoScaler** | **bool** | Option to use the auto scaler with your cluster. Default false. | [optional] 
**MinNodes** | **int** | Auto scaler field for minimum nodes you want for your cluster. Default 1. | [optional] 
**MaxNodes** | **int** | Auto scaler field for maximum nodes you want for your cluster. Default 1. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

