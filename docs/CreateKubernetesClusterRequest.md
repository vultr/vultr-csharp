# Org.OpenAPITools.Model.CreateKubernetesClusterRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Label** | **string** | The label for your Kubernetes cluster. | [optional] 
**Region** | **string** | Region you want to deploy VKE in. See [Regions](#tag/region) for more information. | 
**VarVersion** | **string** | Version of Kubernetes you want to deploy. | 
**HaControlplanes** | **bool** | Whether a highly available control planes configuration should be deployed * true * false (default) | [optional] 
**EnableFirewall** | **bool** | Whether a [Firewall Group](#tag/firewall) should be deployed and managed by this cluster * true * false (default) | [optional] 
**NodePools** | [**List&lt;CreateKubernetesClusterRequestNodePoolsInner&gt;**](CreateKubernetesClusterRequestNodePoolsInner.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

