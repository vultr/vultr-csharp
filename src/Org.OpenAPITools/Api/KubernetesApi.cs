/*
 * Vultr API
 *
 * # Introduction  The Vultr API v2 is a set of HTTP endpoints that adhere to RESTful design principles and CRUD actions with predictable URIs. It uses standard HTTP response codes, authentication, and verbs. The API has consistent and well-formed JSON requests and responses with cursor-based pagination to simplify list handling. Error messages are descriptive and easy to understand. All functions of the Vultr customer portal are accessible via the API, enabling you to script complex unattended scenarios with any tool fluent in HTTP.  ## Requests  Communicate with the API by making an HTTP request at the correct endpoint. The chosen method determines the action taken.  | Method | Usage | | - -- -- - | - -- -- -- -- -- -- | | DELETE | Use the DELETE method to destroy a resource in your account. If it is not found, the operation will return a 4xx error and an appropriate message. | | GET | To retrieve information about a resource, use the GET method. The data is returned as a JSON object. GET methods are read-only and do not affect any resources. | | PATCH | Some resources support partial modification with PATCH, which modifies specific attributes without updating the entire object representation. | | POST | Issue a POST method to create a new object. Include all needed attributes in the request body encoded as JSON. | | PUT | Use the PUT method to update information about a resource. PUT will set new values on the item without regard to their current values. |  **Rate Limit:** Vultr safeguards the API against bursts of incoming traffic based on the request's IP address to ensure stability for all users. If your application sends more than 30 requests per second, the API may return HTTP status code 429.  ## Response Codes  We use standard HTTP response codes to show the success or failure of requests. Response codes in the 2xx range indicate success, while codes in the 4xx range indicate an error, such as an authorization failure or a malformed request. All 4xx errors will return a JSON response object with an `error` attribute explaining the error. Codes in the 5xx range indicate a server-side problem preventing Vultr from fulfilling your request.  | Response | Description | | - -- -- - | - -- -- -- -- -- -- | | 200 OK | The response contains your requested information. | | 201 Created | Your request was accepted. The resource was created. | | 202 Accepted | Your request was accepted. The resource was created or updated. | | 204 No Content | Your request succeeded, there is no additional information returned. | | 400 Bad Request | Your request was malformed. | | 401 Unauthorized | You did not supply valid authentication credentials. | | 403 Forbidden | You are not allowed to perform that action. | | 404 Not Found | No results were found for your request. | | 429 Too Many Requests | Your request exceeded the API rate limit. | | 500 Internal Server Error | We were unable to perform the request due to server-side problems. |  ## Meta and Pagination  Many API calls will return a `meta` object with paging information.  ### Definitions  | Term | Description | | - -- -- - | - -- -- -- -- -- -- | | **List** | The items returned from the database for your request (not necessarily shown in a single response depending on the **cursor** size). | | **Page** | A subset of the full **list** of items. Choose the size of a **page** with the `per_page` parameter. | | **Total** | The `total` attribute indicates the number of items in the full **list**.| | **Cursor** | Use the `cursor` query parameter to select a next or previous **page**. | | **Next** & **Prev** | Use the `next` and `prev` attributes of the `links` meta object as `cursor` values. |  ### How to use Paging  If your result **list** total exceeds the default **cursor** size (the default depends on the route, but is usually 100 records) or the value defined by the `per_page` query param (when present) the response will be returned to you paginated.  ### Paging Example  > These examples have abbreviated attributes and sample values. Your actual `cursor` values will be encoded alphanumeric strings.  To return a **page** with the first two plans in the List:      curl \"https://api.vultr.com/v2/plans?per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  The API returns an object similar to this:      {         \"plans\": [             {                 \"id\": \"vc2-1c-2gb\",                 \"vcpu_count\": 1,                 \"ram\": 2048,                 \"locations\": []             },             {                 \"id\": \"vc2-24c-97gb\",                 \"vcpu_count\": 24,                 \"ram\": 98304,                 \"locations\": []             }         ],         \"meta\": {             \"total\": 19,             \"links\": {                 \"next\": \"WxYzExampleNext\",                 \"prev\": \"WxYzExamplePrev\"             }         }     }  The object contains two plans. The `total` attribute indicates that 19 plans are available in the List. To navigate forward in the **list**, use the `next` value (`WxYzExampleNext` in this example) as your `cursor` query parameter.      curl \"https://api.vultr.com/v2/plans?per_page=2&cursor=WxYzExampleNext\" \\       -X GET       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  Likewise, you can use the example `prev` value `WxYzExamplePrev` to navigate backward.  ## Parameters  You can pass information to the API with three different types of parameters.  ### Path parameters  Some API calls require variable parameters as part of the endpoint path. For example, to retrieve information about a user, supply the `user-id` in the endpoint.      curl \"https://api.vultr.com/v2/users/{user-id}\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Query parameters  Some API calls allow filtering with query parameters. For example, the `/v2/plans` endpoint supports a `type` query parameter. Setting `type=vhf` instructs the API only to return High Frequency Compute plans in the list. You'll find more specifics about valid filters in the endpoint documentation below.      curl \"https://api.vultr.com/v2/plans?type=vhf\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  You can also combine filtering with paging. Use the `per_page` parameter to retreive a subset of vhf plans.      curl \"https://api.vultr.com/v2/plans?type=vhf&per_page=2\" \\       -X GET \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\"  ### Request Body  PUT, POST, and PATCH methods may include an object in the request body with a content type of **application/json**. The documentation for each endpoint below has more information about the expected object.  ## API Example Conventions  The examples in this documentation use `curl`, a command-line HTTP client, to demonstrate useage. Linux and macOS computers usually have curl installed by default, and it's [available for download](https://curl.se/download.html) on all popular platforms including Windows.  Each example is split across multiple lines with the `\\` character, which is compatible with a `bash` terminal. A typical example looks like this:      curl \"https://api.vultr.com/v2/domains\" \\       -X POST \\       -H \"Authorization: Bearer ${VULTR_API_KEY}\" \\       -H \"Content-Type: application/json\" \\       - -data '{         \"domain\" : \"example.com\",         \"ip\" : \"192.0.2.123\"       }'  * The `-X` parameter sets the request method. For consistency, we show the method on all examples, even though it's not explicitly required for GET methods. * The `-H` lines set required HTTP headers. These examples are formatted to expand the VULTR\\_API\\_KEY environment variable for your convenience. * Examples that require a JSON object in the request body pass the required data via the `- -data` parameter.  All values in this guide are examples. Do not rely on the OS or Plan IDs listed in this guide; use the appropriate endpoint to retreive values before creating resources. 
 *
 * The version of the OpenAPI document: 2.0
 * Contact: support@vultr.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IKubernetesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Create Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateKubernetesCluster201Response</returns>
        CreateKubernetesCluster201Response CreateKubernetesCluster(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Create Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateKubernetesCluster201Response</returns>
        ApiResponse<CreateKubernetesCluster201Response> CreateKubernetesClusterWithHttpInfo(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0);
        /// <summary>
        /// Create NodePool
        /// </summary>
        /// <remarks>
        /// Create NodePool for a Existing Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        CreateNodepools201Response CreateNodepools(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0);

        /// <summary>
        /// Create NodePool
        /// </summary>
        /// <remarks>
        /// Create NodePool for a Existing Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        ApiResponse<CreateNodepools201Response> CreateNodepoolsWithHttpInfo(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteKubernetesCluster(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Delete Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteKubernetesClusterWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Delete VKE Cluster and All Related Resources
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster and all related resources. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteKubernetesClusterVkeIdDeleteWithLinkedResources(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Delete VKE Cluster and All Related Resources
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster and all related resources. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Delete Nodepool
        /// </summary>
        /// <remarks>
        /// Delete a NodePool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteNodepool(string vkeId, string nodepoolId, int operationIndex = 0);

        /// <summary>
        /// Delete Nodepool
        /// </summary>
        /// <remarks>
        /// Delete a NodePool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteNodepoolWithHttpInfo(string vkeId, string nodepoolId, int operationIndex = 0);
        /// <summary>
        /// Delete NodePool Instance
        /// </summary>
        /// <remarks>
        /// Delete a single nodepool instance from a given Nodepool
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteNodepoolInstance(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0);

        /// <summary>
        /// Delete NodePool Instance
        /// </summary>
        /// <remarks>
        /// Delete a single nodepool instance from a given Nodepool
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteNodepoolInstanceWithHttpInfo(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0);
        /// <summary>
        /// Get Kubernetes Available Upgrades
        /// </summary>
        /// <remarks>
        /// Get the available upgrades for the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesAvailableUpgrades200Response</returns>
        GetKubernetesAvailableUpgrades200Response GetKubernetesAvailableUpgrades(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Get Kubernetes Available Upgrades
        /// </summary>
        /// <remarks>
        /// Get the available upgrades for the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesAvailableUpgrades200Response</returns>
        ApiResponse<GetKubernetesAvailableUpgrades200Response> GetKubernetesAvailableUpgradesWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Get Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateKubernetesCluster201Response</returns>
        CreateKubernetesCluster201Response GetKubernetesClusters(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Get Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateKubernetesCluster201Response</returns>
        ApiResponse<CreateKubernetesCluster201Response> GetKubernetesClustersWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster Kubeconfig
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesClustersConfig200Response</returns>
        GetKubernetesClustersConfig200Response GetKubernetesClustersConfig(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster Kubeconfig
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesClustersConfig200Response</returns>
        ApiResponse<GetKubernetesClustersConfig200Response> GetKubernetesClustersConfigWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Get Kubernetes Resources
        /// </summary>
        /// <remarks>
        /// Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesResources200Response</returns>
        GetKubernetesResources200Response GetKubernetesResources(string vkeId, int operationIndex = 0);

        /// <summary>
        /// Get Kubernetes Resources
        /// </summary>
        /// <remarks>
        /// Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesResources200Response</returns>
        ApiResponse<GetKubernetesResources200Response> GetKubernetesResourcesWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// Get Kubernetes Versions
        /// </summary>
        /// <remarks>
        /// Get a list of supported Kubernetes versions
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesVersions200Response</returns>
        GetKubernetesVersions200Response GetKubernetesVersions(int operationIndex = 0);

        /// <summary>
        /// Get Kubernetes Versions
        /// </summary>
        /// <remarks>
        /// Get a list of supported Kubernetes versions
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesVersions200Response</returns>
        ApiResponse<GetKubernetesVersions200Response> GetKubernetesVersionsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Get NodePool
        /// </summary>
        /// <remarks>
        /// Get Nodepool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        CreateNodepools201Response GetNodepool(string vkeId, string nodepoolId, int operationIndex = 0);

        /// <summary>
        /// Get NodePool
        /// </summary>
        /// <remarks>
        /// Get Nodepool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        ApiResponse<CreateNodepools201Response> GetNodepoolWithHttpInfo(string vkeId, string nodepoolId, int operationIndex = 0);
        /// <summary>
        /// List NodePools
        /// </summary>
        /// <remarks>
        /// List all available NodePools on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetNodepools200Response</returns>
        GetNodepools200Response GetNodepools(string vkeId, int operationIndex = 0);

        /// <summary>
        /// List NodePools
        /// </summary>
        /// <remarks>
        /// List all available NodePools on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetNodepools200Response</returns>
        ApiResponse<GetNodepools200Response> GetNodepoolsWithHttpInfo(string vkeId, int operationIndex = 0);
        /// <summary>
        /// List all Kubernetes Clusters
        /// </summary>
        /// <remarks>
        /// List all Kubernetes clusters currently deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListKubernetesClusters200Response</returns>
        ListKubernetesClusters200Response ListKubernetesClusters(int operationIndex = 0);

        /// <summary>
        /// List all Kubernetes Clusters
        /// </summary>
        /// <remarks>
        /// List all Kubernetes clusters currently deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListKubernetesClusters200Response</returns>
        ApiResponse<ListKubernetesClusters200Response> ListKubernetesClustersWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// Recycle a NodePool Instance
        /// </summary>
        /// <remarks>
        /// Recycle a specific NodePool Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RecycleNodepoolInstance(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0);

        /// <summary>
        /// Recycle a NodePool Instance
        /// </summary>
        /// <remarks>
        /// Recycle a specific NodePool Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RecycleNodepoolInstanceWithHttpInfo(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0);
        /// <summary>
        /// Start Kubernetes Cluster Upgrade
        /// </summary>
        /// <remarks>
        /// Start a Kubernetes cluster upgrade.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void StartKubernetesClusterUpgrade(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0);

        /// <summary>
        /// Start Kubernetes Cluster Upgrade
        /// </summary>
        /// <remarks>
        /// Start a Kubernetes cluster upgrade.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> StartKubernetesClusterUpgradeWithHttpInfo(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Update Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateKubernetesCluster(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Update Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateKubernetesClusterWithHttpInfo(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Nodepool
        /// </summary>
        /// <remarks>
        /// Update a Nodepool on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        CreateNodepools201Response UpdateNodepool(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Nodepool
        /// </summary>
        /// <remarks>
        /// Update a Nodepool on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        ApiResponse<CreateNodepools201Response> UpdateNodepoolWithHttpInfo(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IKubernetesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Create Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateKubernetesCluster201Response</returns>
        System.Threading.Tasks.Task<CreateKubernetesCluster201Response> CreateKubernetesClusterAsync(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Create Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateKubernetesCluster201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateKubernetesCluster201Response>> CreateKubernetesClusterWithHttpInfoAsync(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create NodePool
        /// </summary>
        /// <remarks>
        /// Create NodePool for a Existing Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        System.Threading.Tasks.Task<CreateNodepools201Response> CreateNodepoolsAsync(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create NodePool
        /// </summary>
        /// <remarks>
        /// Create NodePool for a Existing Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateNodepools201Response>> CreateNodepoolsWithHttpInfoAsync(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteKubernetesClusterAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteKubernetesClusterWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete VKE Cluster and All Related Resources
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster and all related resources. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete VKE Cluster and All Related Resources
        /// </summary>
        /// <remarks>
        /// Delete Kubernetes Cluster and all related resources. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Nodepool
        /// </summary>
        /// <remarks>
        /// Delete a NodePool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteNodepoolAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Nodepool
        /// </summary>
        /// <remarks>
        /// Delete a NodePool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete NodePool Instance
        /// </summary>
        /// <remarks>
        /// Delete a single nodepool instance from a given Nodepool
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteNodepoolInstanceAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete NodePool Instance
        /// </summary>
        /// <remarks>
        /// Delete a single nodepool instance from a given Nodepool
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteNodepoolInstanceWithHttpInfoAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Kubernetes Available Upgrades
        /// </summary>
        /// <remarks>
        /// Get the available upgrades for the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesAvailableUpgrades200Response</returns>
        System.Threading.Tasks.Task<GetKubernetesAvailableUpgrades200Response> GetKubernetesAvailableUpgradesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Kubernetes Available Upgrades
        /// </summary>
        /// <remarks>
        /// Get the available upgrades for the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesAvailableUpgrades200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetKubernetesAvailableUpgrades200Response>> GetKubernetesAvailableUpgradesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateKubernetesCluster201Response</returns>
        System.Threading.Tasks.Task<CreateKubernetesCluster201Response> GetKubernetesClustersAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateKubernetesCluster201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateKubernetesCluster201Response>> GetKubernetesClustersWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster Kubeconfig
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesClustersConfig200Response</returns>
        System.Threading.Tasks.Task<GetKubernetesClustersConfig200Response> GetKubernetesClustersConfigAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <remarks>
        /// Get Kubernetes Cluster Kubeconfig
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesClustersConfig200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetKubernetesClustersConfig200Response>> GetKubernetesClustersConfigWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Kubernetes Resources
        /// </summary>
        /// <remarks>
        /// Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesResources200Response</returns>
        System.Threading.Tasks.Task<GetKubernetesResources200Response> GetKubernetesResourcesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Kubernetes Resources
        /// </summary>
        /// <remarks>
        /// Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesResources200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetKubernetesResources200Response>> GetKubernetesResourcesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Kubernetes Versions
        /// </summary>
        /// <remarks>
        /// Get a list of supported Kubernetes versions
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesVersions200Response</returns>
        System.Threading.Tasks.Task<GetKubernetesVersions200Response> GetKubernetesVersionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Kubernetes Versions
        /// </summary>
        /// <remarks>
        /// Get a list of supported Kubernetes versions
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesVersions200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetKubernetesVersions200Response>> GetKubernetesVersionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get NodePool
        /// </summary>
        /// <remarks>
        /// Get Nodepool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        System.Threading.Tasks.Task<CreateNodepools201Response> GetNodepoolAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get NodePool
        /// </summary>
        /// <remarks>
        /// Get Nodepool from a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateNodepools201Response>> GetNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List NodePools
        /// </summary>
        /// <remarks>
        /// List all available NodePools on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetNodepools200Response</returns>
        System.Threading.Tasks.Task<GetNodepools200Response> GetNodepoolsAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List NodePools
        /// </summary>
        /// <remarks>
        /// List all available NodePools on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetNodepools200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetNodepools200Response>> GetNodepoolsWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List all Kubernetes Clusters
        /// </summary>
        /// <remarks>
        /// List all Kubernetes clusters currently deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListKubernetesClusters200Response</returns>
        System.Threading.Tasks.Task<ListKubernetesClusters200Response> ListKubernetesClustersAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List all Kubernetes Clusters
        /// </summary>
        /// <remarks>
        /// List all Kubernetes clusters currently deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListKubernetesClusters200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListKubernetesClusters200Response>> ListKubernetesClustersWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Recycle a NodePool Instance
        /// </summary>
        /// <remarks>
        /// Recycle a specific NodePool Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RecycleNodepoolInstanceAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Recycle a NodePool Instance
        /// </summary>
        /// <remarks>
        /// Recycle a specific NodePool Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RecycleNodepoolInstanceWithHttpInfoAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Kubernetes Cluster Upgrade
        /// </summary>
        /// <remarks>
        /// Start a Kubernetes cluster upgrade.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task StartKubernetesClusterUpgradeAsync(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Kubernetes Cluster Upgrade
        /// </summary>
        /// <remarks>
        /// Start a Kubernetes cluster upgrade.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> StartKubernetesClusterUpgradeWithHttpInfoAsync(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Update Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateKubernetesClusterAsync(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Kubernetes Cluster
        /// </summary>
        /// <remarks>
        /// Update Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateKubernetesClusterWithHttpInfoAsync(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Nodepool
        /// </summary>
        /// <remarks>
        /// Update a Nodepool on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        System.Threading.Tasks.Task<CreateNodepools201Response> UpdateNodepoolAsync(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Nodepool
        /// </summary>
        /// <remarks>
        /// Update a Nodepool on a Kubernetes Cluster
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateNodepools201Response>> UpdateNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IKubernetesApi : IKubernetesApiSync, IKubernetesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class KubernetesApi : IKubernetesApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public KubernetesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public KubernetesApi(string basePath)
        {
            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                new Org.OpenAPITools.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public KubernetesApi(Org.OpenAPITools.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Org.OpenAPITools.Client.Configuration.MergeConfigurations(
                Org.OpenAPITools.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.OpenAPITools.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KubernetesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public KubernetesApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Org.OpenAPITools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Org.OpenAPITools.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Org.OpenAPITools.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Org.OpenAPITools.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Org.OpenAPITools.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Create Kubernetes Cluster Create Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateKubernetesCluster201Response</returns>
        public CreateKubernetesCluster201Response CreateKubernetesCluster(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> localVarResponse = CreateKubernetesClusterWithHttpInfo(createKubernetesClusterRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Kubernetes Cluster Create Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateKubernetesCluster201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> CreateKubernetesClusterWithHttpInfo(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createKubernetesClusterRequest;

            localVarRequestOptions.Operation = "KubernetesApi.CreateKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateKubernetesCluster201Response>("/kubernetes/clusters", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Kubernetes Cluster Create Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateKubernetesCluster201Response</returns>
        public async System.Threading.Tasks.Task<CreateKubernetesCluster201Response> CreateKubernetesClusterAsync(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> localVarResponse = await CreateKubernetesClusterWithHttpInfoAsync(createKubernetesClusterRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Kubernetes Cluster Create Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateKubernetesCluster201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response>> CreateKubernetesClusterWithHttpInfoAsync(CreateKubernetesClusterRequest? createKubernetesClusterRequest = default(CreateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.Data = createKubernetesClusterRequest;

            localVarRequestOptions.Operation = "KubernetesApi.CreateKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateKubernetesCluster201Response>("/kubernetes/clusters", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create NodePool Create NodePool for a Existing Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        public CreateNodepools201Response CreateNodepools(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = CreateNodepoolsWithHttpInfo(vkeId, createNodepoolsRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create NodePool Create NodePool for a Existing Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> CreateNodepoolsWithHttpInfo(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->CreateNodepools");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = createNodepoolsRequest;

            localVarRequestOptions.Operation = "KubernetesApi.CreateNodepools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateNodepools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create NodePool Create NodePool for a Existing Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        public async System.Threading.Tasks.Task<CreateNodepools201Response> CreateNodepoolsAsync(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = await CreateNodepoolsWithHttpInfoAsync(vkeId, createNodepoolsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create NodePool Create NodePool for a Existing Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="createNodepoolsRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response>> CreateNodepoolsWithHttpInfoAsync(string vkeId, CreateNodepoolsRequest? createNodepoolsRequest = default(CreateNodepoolsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->CreateNodepools");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = createNodepoolsRequest;

            localVarRequestOptions.Operation = "KubernetesApi.CreateNodepools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateNodepools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Kubernetes Cluster Delete Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteKubernetesCluster(string vkeId, int operationIndex = 0)
        {
            DeleteKubernetesClusterWithHttpInfo(vkeId);
        }

        /// <summary>
        /// Delete Kubernetes Cluster Delete Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteKubernetesClusterWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteKubernetesCluster");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Kubernetes Cluster Delete Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteKubernetesClusterAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteKubernetesClusterWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Kubernetes Cluster Delete Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteKubernetesClusterWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteKubernetesCluster");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete VKE Cluster and All Related Resources Delete Kubernetes Cluster and all related resources. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteKubernetesClusterVkeIdDeleteWithLinkedResources(string vkeId, int operationIndex = 0)
        {
            DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo(vkeId);
        }

        /// <summary>
        /// Delete VKE Cluster and All Related Resources Delete Kubernetes Cluster and all related resources. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteKubernetesClusterVkeIdDeleteWithLinkedResources");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteKubernetesClusterVkeIdDeleteWithLinkedResources";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/kubernetes/clusters/{vke-id}/delete-with-linked-resources", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteKubernetesClusterVkeIdDeleteWithLinkedResources", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete VKE Cluster and All Related Resources Delete Kubernetes Cluster and all related resources. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete VKE Cluster and All Related Resources Delete Kubernetes Cluster and all related resources. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId"></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteKubernetesClusterVkeIdDeleteWithLinkedResourcesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteKubernetesClusterVkeIdDeleteWithLinkedResources");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteKubernetesClusterVkeIdDeleteWithLinkedResources";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/kubernetes/clusters/{vke-id}/delete-with-linked-resources", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteKubernetesClusterVkeIdDeleteWithLinkedResources", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Nodepool Delete a NodePool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteNodepool(string vkeId, string nodepoolId, int operationIndex = 0)
        {
            DeleteNodepoolWithHttpInfo(vkeId, nodepoolId);
        }

        /// <summary>
        /// Delete Nodepool Delete a NodePool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteNodepoolWithHttpInfo(string vkeId, string nodepoolId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->DeleteNodepool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Nodepool Delete a NodePool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteNodepoolAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteNodepoolWithHttpInfoAsync(vkeId, nodepoolId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Nodepool Delete a NodePool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->DeleteNodepool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete NodePool Instance Delete a single nodepool instance from a given Nodepool
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteNodepoolInstance(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0)
        {
            DeleteNodepoolInstanceWithHttpInfo(vkeId, nodepoolId, nodeId);
        }

        /// <summary>
        /// Delete NodePool Instance Delete a single nodepool instance from a given Nodepool
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteNodepoolInstanceWithHttpInfo(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteNodepoolInstance");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->DeleteNodepoolInstance");
            }

            // verify the required parameter 'nodeId' is set
            if (nodeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodeId' when calling KubernetesApi->DeleteNodepoolInstance");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.PathParameters.Add("node-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteNodepoolInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteNodepoolInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete NodePool Instance Delete a single nodepool instance from a given Nodepool
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteNodepoolInstanceAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteNodepoolInstanceWithHttpInfoAsync(vkeId, nodepoolId, nodeId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete NodePool Instance Delete a single nodepool instance from a given Nodepool
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteNodepoolInstanceWithHttpInfoAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->DeleteNodepoolInstance");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->DeleteNodepoolInstance");
            }

            // verify the required parameter 'nodeId' is set
            if (nodeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodeId' when calling KubernetesApi->DeleteNodepoolInstance");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.PathParameters.Add("node-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.DeleteNodepoolInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteNodepoolInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Available Upgrades Get the available upgrades for the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesAvailableUpgrades200Response</returns>
        public GetKubernetesAvailableUpgrades200Response GetKubernetesAvailableUpgrades(string vkeId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesAvailableUpgrades200Response> localVarResponse = GetKubernetesAvailableUpgradesWithHttpInfo(vkeId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Available Upgrades Get the available upgrades for the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesAvailableUpgrades200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetKubernetesAvailableUpgrades200Response> GetKubernetesAvailableUpgradesWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesAvailableUpgrades");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesAvailableUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetKubernetesAvailableUpgrades200Response>("/kubernetes/clusters/{vke-id}/available-upgrades", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesAvailableUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Available Upgrades Get the available upgrades for the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesAvailableUpgrades200Response</returns>
        public async System.Threading.Tasks.Task<GetKubernetesAvailableUpgrades200Response> GetKubernetesAvailableUpgradesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesAvailableUpgrades200Response> localVarResponse = await GetKubernetesAvailableUpgradesWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Available Upgrades Get the available upgrades for the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesAvailableUpgrades200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetKubernetesAvailableUpgrades200Response>> GetKubernetesAvailableUpgradesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesAvailableUpgrades");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesAvailableUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetKubernetesAvailableUpgrades200Response>("/kubernetes/clusters/{vke-id}/available-upgrades", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesAvailableUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Cluster Get Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateKubernetesCluster201Response</returns>
        public CreateKubernetesCluster201Response GetKubernetesClusters(string vkeId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> localVarResponse = GetKubernetesClustersWithHttpInfo(vkeId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Cluster Get Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateKubernetesCluster201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> GetKubernetesClustersWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesClusters");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesClusters";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateKubernetesCluster201Response>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesClusters", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Cluster Get Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateKubernetesCluster201Response</returns>
        public async System.Threading.Tasks.Task<CreateKubernetesCluster201Response> GetKubernetesClustersAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response> localVarResponse = await GetKubernetesClustersWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Cluster Get Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateKubernetesCluster201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateKubernetesCluster201Response>> GetKubernetesClustersWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesClusters");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesClusters";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateKubernetesCluster201Response>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesClusters", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesClustersConfig200Response</returns>
        public GetKubernetesClustersConfig200Response GetKubernetesClustersConfig(string vkeId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesClustersConfig200Response> localVarResponse = GetKubernetesClustersConfigWithHttpInfo(vkeId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesClustersConfig200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetKubernetesClustersConfig200Response> GetKubernetesClustersConfigWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesClustersConfig");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesClustersConfig";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetKubernetesClustersConfig200Response>("/kubernetes/clusters/{vke-id}/config", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesClustersConfig", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesClustersConfig200Response</returns>
        public async System.Threading.Tasks.Task<GetKubernetesClustersConfig200Response> GetKubernetesClustersConfigAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesClustersConfig200Response> localVarResponse = await GetKubernetesClustersConfigWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Cluster Kubeconfig Get Kubernetes Cluster Kubeconfig
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesClustersConfig200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetKubernetesClustersConfig200Response>> GetKubernetesClustersConfigWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesClustersConfig");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesClustersConfig";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetKubernetesClustersConfig200Response>("/kubernetes/clusters/{vke-id}/config", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesClustersConfig", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Resources Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesResources200Response</returns>
        public GetKubernetesResources200Response GetKubernetesResources(string vkeId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesResources200Response> localVarResponse = GetKubernetesResourcesWithHttpInfo(vkeId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Resources Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesResources200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetKubernetesResources200Response> GetKubernetesResourcesWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesResources");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesResources";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetKubernetesResources200Response>("/kubernetes/clusters/{vke-id}/resources", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesResources", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Resources Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesResources200Response</returns>
        public async System.Threading.Tasks.Task<GetKubernetesResources200Response> GetKubernetesResourcesAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesResources200Response> localVarResponse = await GetKubernetesResourcesWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Resources Get the block storage volumes and load balancers deployed by the specified Kubernetes cluster.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesResources200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetKubernetesResources200Response>> GetKubernetesResourcesWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetKubernetesResources");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesResources";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetKubernetesResources200Response>("/kubernetes/clusters/{vke-id}/resources", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesResources", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Versions Get a list of supported Kubernetes versions
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetKubernetesVersions200Response</returns>
        public GetKubernetesVersions200Response GetKubernetesVersions(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesVersions200Response> localVarResponse = GetKubernetesVersionsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Versions Get a list of supported Kubernetes versions
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetKubernetesVersions200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetKubernetesVersions200Response> GetKubernetesVersionsWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesVersions";
            localVarRequestOptions.OperationIndex = operationIndex;


            // make the HTTP request
            var localVarResponse = this.Client.Get<GetKubernetesVersions200Response>("/kubernetes/versions", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Kubernetes Versions Get a list of supported Kubernetes versions
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetKubernetesVersions200Response</returns>
        public async System.Threading.Tasks.Task<GetKubernetesVersions200Response> GetKubernetesVersionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetKubernetesVersions200Response> localVarResponse = await GetKubernetesVersionsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Kubernetes Versions Get a list of supported Kubernetes versions
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetKubernetesVersions200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetKubernetesVersions200Response>> GetKubernetesVersionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "KubernetesApi.GetKubernetesVersions";
            localVarRequestOptions.OperationIndex = operationIndex;


            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetKubernetesVersions200Response>("/kubernetes/versions", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetKubernetesVersions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get NodePool Get Nodepool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        public CreateNodepools201Response GetNodepool(string vkeId, string nodepoolId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = GetNodepoolWithHttpInfo(vkeId, nodepoolId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get NodePool Get Nodepool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> GetNodepoolWithHttpInfo(string vkeId, string nodepoolId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->GetNodepool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get NodePool Get Nodepool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        public async System.Threading.Tasks.Task<CreateNodepools201Response> GetNodepoolAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = await GetNodepoolWithHttpInfoAsync(vkeId, nodepoolId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get NodePool Get Nodepool from a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response>> GetNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->GetNodepool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List NodePools List all available NodePools on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetNodepools200Response</returns>
        public GetNodepools200Response GetNodepools(string vkeId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetNodepools200Response> localVarResponse = GetNodepoolsWithHttpInfo(vkeId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List NodePools List all available NodePools on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetNodepools200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetNodepools200Response> GetNodepoolsWithHttpInfo(string vkeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetNodepools");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetNodepools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetNodepools200Response>("/kubernetes/clusters/{vke-id}/node-pools", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetNodepools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List NodePools List all available NodePools on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetNodepools200Response</returns>
        public async System.Threading.Tasks.Task<GetNodepools200Response> GetNodepoolsAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetNodepools200Response> localVarResponse = await GetNodepoolsWithHttpInfoAsync(vkeId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List NodePools List all available NodePools on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetNodepools200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetNodepools200Response>> GetNodepoolsWithHttpInfoAsync(string vkeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->GetNodepools");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.GetNodepools";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetNodepools200Response>("/kubernetes/clusters/{vke-id}/node-pools", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetNodepools", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Kubernetes Clusters List all Kubernetes clusters currently deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListKubernetesClusters200Response</returns>
        public ListKubernetesClusters200Response ListKubernetesClusters(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListKubernetesClusters200Response> localVarResponse = ListKubernetesClustersWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Kubernetes Clusters List all Kubernetes clusters currently deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListKubernetesClusters200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListKubernetesClusters200Response> ListKubernetesClustersWithHttpInfo(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "KubernetesApi.ListKubernetesClusters";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListKubernetesClusters200Response>("/kubernetes/clusters", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListKubernetesClusters", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List all Kubernetes Clusters List all Kubernetes clusters currently deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListKubernetesClusters200Response</returns>
        public async System.Threading.Tasks.Task<ListKubernetesClusters200Response> ListKubernetesClustersAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListKubernetesClusters200Response> localVarResponse = await ListKubernetesClustersWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List all Kubernetes Clusters List all Kubernetes clusters currently deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListKubernetesClusters200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListKubernetesClusters200Response>> ListKubernetesClustersWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }


            localVarRequestOptions.Operation = "KubernetesApi.ListKubernetesClusters";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListKubernetesClusters200Response>("/kubernetes/clusters", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListKubernetesClusters", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Recycle a NodePool Instance Recycle a specific NodePool Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RecycleNodepoolInstance(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0)
        {
            RecycleNodepoolInstanceWithHttpInfo(vkeId, nodepoolId, nodeId);
        }

        /// <summary>
        /// Recycle a NodePool Instance Recycle a specific NodePool Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> RecycleNodepoolInstanceWithHttpInfo(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->RecycleNodepoolInstance");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->RecycleNodepoolInstance");
            }

            // verify the required parameter 'nodeId' is set
            if (nodeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodeId' when calling KubernetesApi->RecycleNodepoolInstance");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.PathParameters.Add("node-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.RecycleNodepoolInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}/recycle", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RecycleNodepoolInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Recycle a NodePool Instance Recycle a specific NodePool Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RecycleNodepoolInstanceAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await RecycleNodepoolInstanceWithHttpInfoAsync(vkeId, nodepoolId, nodeId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Recycle a NodePool Instance Recycle a specific NodePool Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="nodeId">Node ID</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> RecycleNodepoolInstanceWithHttpInfoAsync(string vkeId, string nodepoolId, string nodeId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->RecycleNodepoolInstance");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->RecycleNodepoolInstance");
            }

            // verify the required parameter 'nodeId' is set
            if (nodeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodeId' when calling KubernetesApi->RecycleNodepoolInstance");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.PathParameters.Add("node-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodeId)); // path parameter

            localVarRequestOptions.Operation = "KubernetesApi.RecycleNodepoolInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}/recycle", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RecycleNodepoolInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Kubernetes Cluster Upgrade Start a Kubernetes cluster upgrade.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void StartKubernetesClusterUpgrade(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0)
        {
            StartKubernetesClusterUpgradeWithHttpInfo(vkeId, startKubernetesClusterUpgradeRequest);
        }

        /// <summary>
        /// Start Kubernetes Cluster Upgrade Start a Kubernetes cluster upgrade.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> StartKubernetesClusterUpgradeWithHttpInfo(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->StartKubernetesClusterUpgrade");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = startKubernetesClusterUpgradeRequest;

            localVarRequestOptions.Operation = "KubernetesApi.StartKubernetesClusterUpgrade";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/kubernetes/clusters/{vke-id}/upgrades", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartKubernetesClusterUpgrade", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Kubernetes Cluster Upgrade Start a Kubernetes cluster upgrade.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task StartKubernetesClusterUpgradeAsync(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await StartKubernetesClusterUpgradeWithHttpInfoAsync(vkeId, startKubernetesClusterUpgradeRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start Kubernetes Cluster Upgrade Start a Kubernetes cluster upgrade.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="startKubernetesClusterUpgradeRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> StartKubernetesClusterUpgradeWithHttpInfoAsync(string vkeId, StartKubernetesClusterUpgradeRequest? startKubernetesClusterUpgradeRequest = default(StartKubernetesClusterUpgradeRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->StartKubernetesClusterUpgrade");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = startKubernetesClusterUpgradeRequest;

            localVarRequestOptions.Operation = "KubernetesApi.StartKubernetesClusterUpgrade";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/kubernetes/clusters/{vke-id}/upgrades", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartKubernetesClusterUpgrade", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Kubernetes Cluster Update Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateKubernetesCluster(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0)
        {
            UpdateKubernetesClusterWithHttpInfo(vkeId, updateKubernetesClusterRequest);
        }

        /// <summary>
        /// Update Kubernetes Cluster Update Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateKubernetesClusterWithHttpInfo(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->UpdateKubernetesCluster");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = updateKubernetesClusterRequest;

            localVarRequestOptions.Operation = "KubernetesApi.UpdateKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Kubernetes Cluster Update Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateKubernetesClusterAsync(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateKubernetesClusterWithHttpInfoAsync(vkeId, updateKubernetesClusterRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Kubernetes Cluster Update Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="updateKubernetesClusterRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateKubernetesClusterWithHttpInfoAsync(string vkeId, UpdateKubernetesClusterRequest? updateKubernetesClusterRequest = default(UpdateKubernetesClusterRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->UpdateKubernetesCluster");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.Data = updateKubernetesClusterRequest;

            localVarRequestOptions.Operation = "KubernetesApi.UpdateKubernetesCluster";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/kubernetes/clusters/{vke-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateKubernetesCluster", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Nodepool Update a Nodepool on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateNodepools201Response</returns>
        public CreateNodepools201Response UpdateNodepool(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = UpdateNodepoolWithHttpInfo(vkeId, nodepoolId, updateNodepoolRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Nodepool Update a Nodepool on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateNodepools201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> UpdateNodepoolWithHttpInfo(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->UpdateNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->UpdateNodepool");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json",
                "application/xml"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.Data = updateNodepoolRequest;

            localVarRequestOptions.Operation = "KubernetesApi.UpdateNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Nodepool Update a Nodepool on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateNodepools201Response</returns>
        public async System.Threading.Tasks.Task<CreateNodepools201Response> UpdateNodepoolAsync(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response> localVarResponse = await UpdateNodepoolWithHttpInfoAsync(vkeId, nodepoolId, updateNodepoolRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Nodepool Update a Nodepool on a Kubernetes Cluster
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="vkeId">The [VKE ID](#operation/list-kubernetes-clusters).</param>
        /// <param name="nodepoolId">The [NodePool ID](#operation/get-nodepools).</param>
        /// <param name="updateNodepoolRequest">Request Body (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateNodepools201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateNodepools201Response>> UpdateNodepoolWithHttpInfoAsync(string vkeId, string nodepoolId, UpdateNodepoolRequest? updateNodepoolRequest = default(UpdateNodepoolRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'vkeId' is set
            if (vkeId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'vkeId' when calling KubernetesApi->UpdateNodepool");
            }

            // verify the required parameter 'nodepoolId' is set
            if (nodepoolId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'nodepoolId' when calling KubernetesApi->UpdateNodepool");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json", 
                "application/xml"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localVarContentType = Org.OpenAPITools.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = Org.OpenAPITools.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("vke-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(vkeId)); // path parameter
            localVarRequestOptions.PathParameters.Add("nodepool-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(nodepoolId)); // path parameter
            localVarRequestOptions.Data = updateNodepoolRequest;

            localVarRequestOptions.Operation = "KubernetesApi.UpdateNodepool";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<CreateNodepools201Response>("/kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateNodepool", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
