# Org.OpenAPITools.Model.Nodepools
NodePool

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | The [NodePool ID](#operation/get-nodepools). | [optional] 
**DateCreated** | **string** | Date of creation | [optional] 
**Label** | **string** | Label for nodepool | [optional] 
**Tag** | **string** | Tag for node pool | [optional] 
**Plan** | **string** | Plan used for nodepool | [optional] 
**Status** | **string** | Status for nodepool | [optional] 
**NodeQuantity** | **int** | Number of nodes in nodepool | [optional] 
**Nodes** | [**List&lt;NodepoolInstances&gt;**](NodepoolInstances.md) |  | [optional] 
**DateUpdated** | **string** | Date the nodepool was updated. | [optional] 
**AutoScaler** | **bool** | Displays if the auto scaler is enabled or disabled for your cluster. | [optional] 
**MinNodes** | **int** | Auto scaler field that displays the minimum nodes you want for your cluster. | [optional] 
**MaxNodes** | **int** | Auto scaler field that displays the maximum nodes you want for your cluster. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

