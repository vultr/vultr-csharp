# Org.OpenAPITools.Model.VkeCluster
VKE Cluster

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | ID for the VKE cluster | [optional] 
**FirewallGroupId** | **string** | The [Firewall Group id](#operation/list-firewall-groups) linked to this cluster. | [optional] 
**Label** | **string** | Label for your cluster | [optional] 
**DateCreated** | **string** | Date of creation | [optional] 
**ClusterSubnet** | **string** | IP range that your pods will run on in this cluster | [optional] 
**ServiceSubnet** | **string** | IP range that services will run on this cluster | [optional] 
**Ip** | **string** | IP for your Kubernetes Clusters Control Plane | [optional] 
**Endpoint** | **string** | Domain for your Kubernetes Clusters Control Plane | [optional] 
**VarVersion** | **string** | Version of Kubernetes this cluster is running on | [optional] 
**Region** | **string** | Region this Kubernetes Cluster is running in | [optional] 
**Status** | **string** | Status for VKE cluster | [optional] 
**HaControlplanes** | **bool** | Whether a highly available control planes configuration has been deployed * true * false (default) | [optional] 
**NodePools** | [**List&lt;Nodepools&gt;**](Nodepools.md) | NodePools in this cluster | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

