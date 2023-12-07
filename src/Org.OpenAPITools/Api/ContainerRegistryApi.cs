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
    public interface IContainerRegistryApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Container Registry
        /// </summary>
        /// <remarks>
        /// Create a new Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        Registry CreateRegistry(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Container Registry
        /// </summary>
        /// <remarks>
        /// Create a new Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        ApiResponse<Registry> CreateRegistryWithHttpInfo(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Docker Credentials
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryDockerCredentials</returns>
        RegistryDockerCredentials CreateRegistryDockerCredentials(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0);

        /// <summary>
        /// Create Docker Credentials
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryDockerCredentials</returns>
        ApiResponse<RegistryDockerCredentials> CreateRegistryDockerCredentialsWithHttpInfo(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0);
        /// <summary>
        /// Create Docker Credentials for Kubernetes
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryKubernetesDockerCredentials</returns>
        RegistryKubernetesDockerCredentials CreateRegistryKubernetesDockerCredentials(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0);

        /// <summary>
        /// Create Docker Credentials for Kubernetes
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryKubernetesDockerCredentials</returns>
        ApiResponse<RegistryKubernetesDockerCredentials> CreateRegistryKubernetesDockerCredentialsWithHttpInfo(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0);
        /// <summary>
        /// Delete Container Registry
        /// </summary>
        /// <remarks>
        /// Deletes a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteRegistry(string registryId, int operationIndex = 0);

        /// <summary>
        /// Delete Container Registry
        /// </summary>
        /// <remarks>
        /// Deletes a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteRegistryWithHttpInfo(string registryId, int operationIndex = 0);
        /// <summary>
        /// Delete Repository
        /// </summary>
        /// <remarks>
        /// Deletes a Repository from a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteRepository(string registryId, string repositoryImage, int operationIndex = 0);

        /// <summary>
        /// Delete Repository
        /// </summary>
        /// <remarks>
        /// Deletes a Repository from a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteRepositoryWithHttpInfo(string registryId, string repositoryImage, int operationIndex = 0);
        /// <summary>
        /// List Container Registries
        /// </summary>
        /// <remarks>
        /// List All Container Registry Subscriptions for this account
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistries200Response</returns>
        ListRegistries200Response ListRegistries(int operationIndex = 0);

        /// <summary>
        /// List Container Registries
        /// </summary>
        /// <remarks>
        /// List All Container Registry Subscriptions for this account
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistries200Response</returns>
        ApiResponse<ListRegistries200Response> ListRegistriesWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Registry Plans
        /// </summary>
        /// <remarks>
        /// List All Plans to help choose which one is the best fit for your Container Registry
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryPlans200Response</returns>
        ListRegistryPlans200Response ListRegistryPlans(int operationIndex = 0);

        /// <summary>
        /// List Registry Plans
        /// </summary>
        /// <remarks>
        /// List All Plans to help choose which one is the best fit for your Container Registry
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryPlans200Response</returns>
        ApiResponse<ListRegistryPlans200Response> ListRegistryPlansWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Registry Regions
        /// </summary>
        /// <remarks>
        /// List All Regions where a Container Registry can be deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryRegions200Response</returns>
        ListRegistryRegions200Response ListRegistryRegions(int operationIndex = 0);

        /// <summary>
        /// List Registry Regions
        /// </summary>
        /// <remarks>
        /// List All Regions where a Container Registry can be deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryRegions200Response</returns>
        ApiResponse<ListRegistryRegions200Response> ListRegistryRegionsWithHttpInfo(int operationIndex = 0);
        /// <summary>
        /// List Repositories
        /// </summary>
        /// <remarks>
        /// List All Repositories in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryRepositories200Response</returns>
        ListRegistryRepositories200Response ListRegistryRepositories(string registryId, int operationIndex = 0);

        /// <summary>
        /// List Repositories
        /// </summary>
        /// <remarks>
        /// List All Repositories in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryRepositories200Response</returns>
        ApiResponse<ListRegistryRepositories200Response> ListRegistryRepositoriesWithHttpInfo(string registryId, int operationIndex = 0);
        /// <summary>
        /// Read Container Registry
        /// </summary>
        /// <remarks>
        /// Get a single Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        Registry ReadRegistry(string registryId, int operationIndex = 0);

        /// <summary>
        /// Read Container Registry
        /// </summary>
        /// <remarks>
        /// Get a single Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        ApiResponse<Registry> ReadRegistryWithHttpInfo(string registryId, int operationIndex = 0);
        /// <summary>
        /// Read Repository
        /// </summary>
        /// <remarks>
        /// Get a single Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryRepository</returns>
        RegistryRepository ReadRegistryRepository(string registryId, string repositoryImage, int operationIndex = 0);

        /// <summary>
        /// Read Repository
        /// </summary>
        /// <remarks>
        /// Get a single Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryRepository</returns>
        ApiResponse<RegistryRepository> ReadRegistryRepositoryWithHttpInfo(string registryId, string repositoryImage, int operationIndex = 0);
        /// <summary>
        /// Update Container Registry
        /// </summary>
        /// <remarks>
        /// Update a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        Registry UpdateRegistry(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Container Registry
        /// </summary>
        /// <remarks>
        /// Update a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        ApiResponse<Registry> UpdateRegistryWithHttpInfo(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Repository
        /// </summary>
        /// <remarks>
        /// Update a Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryRepository</returns>
        RegistryRepository UpdateRepository(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Repository
        /// </summary>
        /// <remarks>
        /// Update a Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryRepository</returns>
        ApiResponse<RegistryRepository> UpdateRepositoryWithHttpInfo(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IContainerRegistryApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Container Registry
        /// </summary>
        /// <remarks>
        /// Create a new Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        System.Threading.Tasks.Task<Registry> CreateRegistryAsync(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Container Registry
        /// </summary>
        /// <remarks>
        /// Create a new Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        System.Threading.Tasks.Task<ApiResponse<Registry>> CreateRegistryWithHttpInfoAsync(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Docker Credentials
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryDockerCredentials</returns>
        System.Threading.Tasks.Task<RegistryDockerCredentials> CreateRegistryDockerCredentialsAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Docker Credentials
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryDockerCredentials)</returns>
        System.Threading.Tasks.Task<ApiResponse<RegistryDockerCredentials>> CreateRegistryDockerCredentialsWithHttpInfoAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Docker Credentials for Kubernetes
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryKubernetesDockerCredentials</returns>
        System.Threading.Tasks.Task<RegistryKubernetesDockerCredentials> CreateRegistryKubernetesDockerCredentialsAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Docker Credentials for Kubernetes
        /// </summary>
        /// <remarks>
        /// Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryKubernetesDockerCredentials)</returns>
        System.Threading.Tasks.Task<ApiResponse<RegistryKubernetesDockerCredentials>> CreateRegistryKubernetesDockerCredentialsWithHttpInfoAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Container Registry
        /// </summary>
        /// <remarks>
        /// Deletes a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteRegistryAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Container Registry
        /// </summary>
        /// <remarks>
        /// Deletes a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteRegistryWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Repository
        /// </summary>
        /// <remarks>
        /// Deletes a Repository from a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteRepositoryAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Repository
        /// </summary>
        /// <remarks>
        /// Deletes a Repository from a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Container Registries
        /// </summary>
        /// <remarks>
        /// List All Container Registry Subscriptions for this account
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistries200Response</returns>
        System.Threading.Tasks.Task<ListRegistries200Response> ListRegistriesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Container Registries
        /// </summary>
        /// <remarks>
        /// List All Container Registry Subscriptions for this account
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistries200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListRegistries200Response>> ListRegistriesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Registry Plans
        /// </summary>
        /// <remarks>
        /// List All Plans to help choose which one is the best fit for your Container Registry
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryPlans200Response</returns>
        System.Threading.Tasks.Task<ListRegistryPlans200Response> ListRegistryPlansAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Registry Plans
        /// </summary>
        /// <remarks>
        /// List All Plans to help choose which one is the best fit for your Container Registry
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryPlans200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListRegistryPlans200Response>> ListRegistryPlansWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Registry Regions
        /// </summary>
        /// <remarks>
        /// List All Regions where a Container Registry can be deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryRegions200Response</returns>
        System.Threading.Tasks.Task<ListRegistryRegions200Response> ListRegistryRegionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Registry Regions
        /// </summary>
        /// <remarks>
        /// List All Regions where a Container Registry can be deployed
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryRegions200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListRegistryRegions200Response>> ListRegistryRegionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Repositories
        /// </summary>
        /// <remarks>
        /// List All Repositories in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryRepositories200Response</returns>
        System.Threading.Tasks.Task<ListRegistryRepositories200Response> ListRegistryRepositoriesAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Repositories
        /// </summary>
        /// <remarks>
        /// List All Repositories in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryRepositories200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListRegistryRepositories200Response>> ListRegistryRepositoriesWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Read Container Registry
        /// </summary>
        /// <remarks>
        /// Get a single Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        System.Threading.Tasks.Task<Registry> ReadRegistryAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Read Container Registry
        /// </summary>
        /// <remarks>
        /// Get a single Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        System.Threading.Tasks.Task<ApiResponse<Registry>> ReadRegistryWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Read Repository
        /// </summary>
        /// <remarks>
        /// Get a single Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryRepository</returns>
        System.Threading.Tasks.Task<RegistryRepository> ReadRegistryRepositoryAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Read Repository
        /// </summary>
        /// <remarks>
        /// Get a single Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryRepository)</returns>
        System.Threading.Tasks.Task<ApiResponse<RegistryRepository>> ReadRegistryRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Container Registry
        /// </summary>
        /// <remarks>
        /// Update a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        System.Threading.Tasks.Task<Registry> UpdateRegistryAsync(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Container Registry
        /// </summary>
        /// <remarks>
        /// Update a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        System.Threading.Tasks.Task<ApiResponse<Registry>> UpdateRegistryWithHttpInfoAsync(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Repository
        /// </summary>
        /// <remarks>
        /// Update a Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryRepository</returns>
        System.Threading.Tasks.Task<RegistryRepository> UpdateRepositoryAsync(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Repository
        /// </summary>
        /// <remarks>
        /// Update a Repository in a Container Registry Subscription
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryRepository)</returns>
        System.Threading.Tasks.Task<ApiResponse<RegistryRepository>> UpdateRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IContainerRegistryApi : IContainerRegistryApiSync, IContainerRegistryApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class ContainerRegistryApi : IContainerRegistryApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerRegistryApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ContainerRegistryApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerRegistryApi"/> class.
        /// </summary>
        /// <returns></returns>
        public ContainerRegistryApi(string basePath)
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
        /// Initializes a new instance of the <see cref="ContainerRegistryApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public ContainerRegistryApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="ContainerRegistryApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public ContainerRegistryApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Create Container Registry Create a new Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        public Registry CreateRegistry(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = CreateRegistryWithHttpInfo(createRegistryRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Container Registry Create a new Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        public Org.OpenAPITools.Client.ApiResponse<Registry> CreateRegistryWithHttpInfo(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createRegistryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Registry>("/registry", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Container Registry Create a new Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        public async System.Threading.Tasks.Task<Registry> CreateRegistryAsync(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = await CreateRegistryWithHttpInfoAsync(createRegistryRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Container Registry Create a new Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Registry>> CreateRegistryWithHttpInfoAsync(CreateRegistryRequest? createRegistryRequest = default(CreateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createRegistryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Registry>("/registry", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Docker Credentials Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryDockerCredentials</returns>
        public RegistryDockerCredentials CreateRegistryDockerCredentials(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryDockerCredentials> localVarResponse = CreateRegistryDockerCredentialsWithHttpInfo(registryId, expirySeconds, readWrite);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Docker Credentials Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryDockerCredentials</returns>
        public Org.OpenAPITools.Client.ApiResponse<RegistryDockerCredentials> CreateRegistryDockerCredentialsWithHttpInfo(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->CreateRegistryDockerCredentials");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            if (expirySeconds != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "expiry_seconds", expirySeconds));
            }
            if (readWrite != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "read_write", readWrite));
            }

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistryDockerCredentials";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Options<RegistryDockerCredentials>("/registry/{registry-id}/docker-credentials?expiry_seconds=0&read_write=false", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistryDockerCredentials", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Docker Credentials Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryDockerCredentials</returns>
        public async System.Threading.Tasks.Task<RegistryDockerCredentials> CreateRegistryDockerCredentialsAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryDockerCredentials> localVarResponse = await CreateRegistryDockerCredentialsWithHttpInfoAsync(registryId, expirySeconds, readWrite, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Docker Credentials Create a fresh set of Docker Credentials for this Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryDockerCredentials)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<RegistryDockerCredentials>> CreateRegistryDockerCredentialsWithHttpInfoAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->CreateRegistryDockerCredentials");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            if (expirySeconds != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "expiry_seconds", expirySeconds));
            }
            if (readWrite != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "read_write", readWrite));
            }

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistryDockerCredentials";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.OptionsAsync<RegistryDockerCredentials>("/registry/{registry-id}/docker-credentials?expiry_seconds=0&read_write=false", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistryDockerCredentials", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Docker Credentials for Kubernetes Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryKubernetesDockerCredentials</returns>
        public RegistryKubernetesDockerCredentials CreateRegistryKubernetesDockerCredentials(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryKubernetesDockerCredentials> localVarResponse = CreateRegistryKubernetesDockerCredentialsWithHttpInfo(registryId, expirySeconds, readWrite, base64Encode);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Docker Credentials for Kubernetes Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryKubernetesDockerCredentials</returns>
        public Org.OpenAPITools.Client.ApiResponse<RegistryKubernetesDockerCredentials> CreateRegistryKubernetesDockerCredentialsWithHttpInfo(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->CreateRegistryKubernetesDockerCredentials");
            }

            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/yaml"
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            if (expirySeconds != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "expiry_seconds", expirySeconds));
            }
            if (readWrite != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "read_write", readWrite));
            }
            if (base64Encode != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "base64_encode", base64Encode));
            }

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistryKubernetesDockerCredentials";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Options<RegistryKubernetesDockerCredentials>("/registry/{registry-id}/docker-credentials/kubernetes?expiry_seconds=0&read_write=false&base64_encode=false", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistryKubernetesDockerCredentials", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Docker Credentials for Kubernetes Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryKubernetesDockerCredentials</returns>
        public async System.Threading.Tasks.Task<RegistryKubernetesDockerCredentials> CreateRegistryKubernetesDockerCredentialsAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryKubernetesDockerCredentials> localVarResponse = await CreateRegistryKubernetesDockerCredentialsWithHttpInfoAsync(registryId, expirySeconds, readWrite, base64Encode, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Docker Credentials for Kubernetes Create a fresh set of Docker Credentials for this Container Registry Subscription and return them in a Kubernetes friendly YAML format
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="expirySeconds">The seconds until these credentials expire. When set to 0, credentials do not expire. The default value is 0 (optional)</param>
        /// <param name="readWrite">Whether these credentials will have only PULL access or PUSH access as well. If true these credentials can PUSH to repos in this registry. If false, these credentials can only PULL. Default is false. (optional)</param>
        /// <param name="base64Encode">Whether this YAML will be returned in a base64 encoded string for ease of downloading. If true, the response will be a base64 encoded string. Default is false. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryKubernetesDockerCredentials)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<RegistryKubernetesDockerCredentials>> CreateRegistryKubernetesDockerCredentialsWithHttpInfoAsync(string registryId, int? expirySeconds = default(int?), bool? readWrite = default(bool?), bool? base64Encode = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->CreateRegistryKubernetesDockerCredentials");
            }


            Org.OpenAPITools.Client.RequestOptions localVarRequestOptions = new Org.OpenAPITools.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/yaml"
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            if (expirySeconds != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "expiry_seconds", expirySeconds));
            }
            if (readWrite != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "read_write", readWrite));
            }
            if (base64Encode != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "base64_encode", base64Encode));
            }

            localVarRequestOptions.Operation = "ContainerRegistryApi.CreateRegistryKubernetesDockerCredentials";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.OptionsAsync<RegistryKubernetesDockerCredentials>("/registry/{registry-id}/docker-credentials/kubernetes?expiry_seconds=0&read_write=false&base64_encode=false", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateRegistryKubernetesDockerCredentials", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Container Registry Deletes a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteRegistry(string registryId, int operationIndex = 0)
        {
            DeleteRegistryWithHttpInfo(registryId);
        }

        /// <summary>
        /// Delete Container Registry Deletes a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteRegistryWithHttpInfo(string registryId, int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->DeleteRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.DeleteRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/registry/{registry-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Container Registry Deletes a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteRegistryAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteRegistryWithHttpInfoAsync(registryId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Container Registry Deletes a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteRegistryWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->DeleteRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.DeleteRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/registry/{registry-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Repository Deletes a Repository from a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteRepository(string registryId, string repositoryImage, int operationIndex = 0)
        {
            DeleteRepositoryWithHttpInfo(registryId, repositoryImage);
        }

        /// <summary>
        /// Delete Repository Deletes a Repository from a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteRepositoryWithHttpInfo(string registryId, string repositoryImage, int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->DeleteRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->DeleteRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.DeleteRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Repository Deletes a Repository from a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteRepositoryAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteRepositoryWithHttpInfoAsync(registryId, repositoryImage, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Repository Deletes a Repository from a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->DeleteRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->DeleteRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.DeleteRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Container Registries List All Container Registry Subscriptions for this account
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistries200Response</returns>
        public ListRegistries200Response ListRegistries(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistries200Response> localVarResponse = ListRegistriesWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Container Registries List All Container Registry Subscriptions for this account
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistries200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListRegistries200Response> ListRegistriesWithHttpInfo(int operationIndex = 0)
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistries";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListRegistries200Response>("/registries", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistries", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Container Registries List All Container Registry Subscriptions for this account
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistries200Response</returns>
        public async System.Threading.Tasks.Task<ListRegistries200Response> ListRegistriesAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistries200Response> localVarResponse = await ListRegistriesWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Container Registries List All Container Registry Subscriptions for this account
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistries200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListRegistries200Response>> ListRegistriesWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistries";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListRegistries200Response>("/registries", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistries", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Registry Plans List All Plans to help choose which one is the best fit for your Container Registry
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryPlans200Response</returns>
        public ListRegistryPlans200Response ListRegistryPlans(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryPlans200Response> localVarResponse = ListRegistryPlansWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Registry Plans List All Plans to help choose which one is the best fit for your Container Registry
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryPlans200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListRegistryPlans200Response> ListRegistryPlansWithHttpInfo(int operationIndex = 0)
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryPlans";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListRegistryPlans200Response>("/registry/plan/list", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryPlans", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Registry Plans List All Plans to help choose which one is the best fit for your Container Registry
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryPlans200Response</returns>
        public async System.Threading.Tasks.Task<ListRegistryPlans200Response> ListRegistryPlansAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryPlans200Response> localVarResponse = await ListRegistryPlansWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Registry Plans List All Plans to help choose which one is the best fit for your Container Registry
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryPlans200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListRegistryPlans200Response>> ListRegistryPlansWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryPlans";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListRegistryPlans200Response>("/registry/plan/list", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryPlans", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Registry Regions List All Regions where a Container Registry can be deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryRegions200Response</returns>
        public ListRegistryRegions200Response ListRegistryRegions(int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryRegions200Response> localVarResponse = ListRegistryRegionsWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Registry Regions List All Regions where a Container Registry can be deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryRegions200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListRegistryRegions200Response> ListRegistryRegionsWithHttpInfo(int operationIndex = 0)
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryRegions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListRegistryRegions200Response>("/registry/region/list", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryRegions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Registry Regions List All Regions where a Container Registry can be deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryRegions200Response</returns>
        public async System.Threading.Tasks.Task<ListRegistryRegions200Response> ListRegistryRegionsAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryRegions200Response> localVarResponse = await ListRegistryRegionsWithHttpInfoAsync(operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Registry Regions List All Regions where a Container Registry can be deployed
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryRegions200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListRegistryRegions200Response>> ListRegistryRegionsWithHttpInfoAsync(int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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


            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryRegions";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListRegistryRegions200Response>("/registry/region/list", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryRegions", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Repositories List All Repositories in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListRegistryRepositories200Response</returns>
        public ListRegistryRepositories200Response ListRegistryRepositories(string registryId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryRepositories200Response> localVarResponse = ListRegistryRepositoriesWithHttpInfo(registryId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Repositories List All Repositories in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListRegistryRepositories200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListRegistryRepositories200Response> ListRegistryRepositoriesWithHttpInfo(string registryId, int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ListRegistryRepositories");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryRepositories";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListRegistryRepositories200Response>("/registry/{registry-id}/repositories", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryRepositories", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Repositories List All Repositories in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListRegistryRepositories200Response</returns>
        public async System.Threading.Tasks.Task<ListRegistryRepositories200Response> ListRegistryRepositoriesAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListRegistryRepositories200Response> localVarResponse = await ListRegistryRepositoriesWithHttpInfoAsync(registryId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Repositories List All Repositories in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListRegistryRepositories200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListRegistryRepositories200Response>> ListRegistryRepositoriesWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ListRegistryRepositories");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ListRegistryRepositories";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListRegistryRepositories200Response>("/registry/{registry-id}/repositories", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListRegistryRepositories", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read Container Registry Get a single Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        public Registry ReadRegistry(string registryId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = ReadRegistryWithHttpInfo(registryId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read Container Registry Get a single Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        public Org.OpenAPITools.Client.ApiResponse<Registry> ReadRegistryWithHttpInfo(string registryId, int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ReadRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ReadRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Registry>("/registry/{registry-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read Container Registry Get a single Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        public async System.Threading.Tasks.Task<Registry> ReadRegistryAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = await ReadRegistryWithHttpInfoAsync(registryId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read Container Registry Get a single Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Registry>> ReadRegistryWithHttpInfoAsync(string registryId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ReadRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ReadRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Registry>("/registry/{registry-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read Repository Get a single Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryRepository</returns>
        public RegistryRepository ReadRegistryRepository(string registryId, string repositoryImage, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryRepository> localVarResponse = ReadRegistryRepositoryWithHttpInfo(registryId, repositoryImage);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read Repository Get a single Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryRepository</returns>
        public Org.OpenAPITools.Client.ApiResponse<RegistryRepository> ReadRegistryRepositoryWithHttpInfo(string registryId, string repositoryImage, int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ReadRegistryRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->ReadRegistryRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ReadRegistryRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<RegistryRepository>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadRegistryRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Read Repository Get a single Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryRepository</returns>
        public async System.Threading.Tasks.Task<RegistryRepository> ReadRegistryRepositoryAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryRepository> localVarResponse = await ReadRegistryRepositoryWithHttpInfoAsync(registryId, repositoryImage, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Read Repository Get a single Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryRepository)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<RegistryRepository>> ReadRegistryRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->ReadRegistryRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->ReadRegistryRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter

            localVarRequestOptions.Operation = "ContainerRegistryApi.ReadRegistryRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<RegistryRepository>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReadRegistryRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Container Registry Update a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Registry</returns>
        public Registry UpdateRegistry(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = UpdateRegistryWithHttpInfo(registryId, updateRegistryRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Container Registry Update a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Registry</returns>
        public Org.OpenAPITools.Client.ApiResponse<Registry> UpdateRegistryWithHttpInfo(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->UpdateRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.Data = updateRegistryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.UpdateRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Registry>("/registry/{registry-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Container Registry Update a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Registry</returns>
        public async System.Threading.Tasks.Task<Registry> UpdateRegistryAsync(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<Registry> localVarResponse = await UpdateRegistryWithHttpInfoAsync(registryId, updateRegistryRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Container Registry Update a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="updateRegistryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Registry)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Registry>> UpdateRegistryWithHttpInfoAsync(string registryId, UpdateRegistryRequest? updateRegistryRequest = default(UpdateRegistryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->UpdateRegistry");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.Data = updateRegistryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.UpdateRegistry";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Registry>("/registry/{registry-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateRegistry", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Repository Update a Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RegistryRepository</returns>
        public RegistryRepository UpdateRepository(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryRepository> localVarResponse = UpdateRepositoryWithHttpInfo(registryId, repositoryImage, updateRepositoryRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Repository Update a Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RegistryRepository</returns>
        public Org.OpenAPITools.Client.ApiResponse<RegistryRepository> UpdateRepositoryWithHttpInfo(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->UpdateRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->UpdateRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter
            localVarRequestOptions.Data = updateRepositoryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.UpdateRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<RegistryRepository>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Repository Update a Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RegistryRepository</returns>
        public async System.Threading.Tasks.Task<RegistryRepository> UpdateRepositoryAsync(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<RegistryRepository> localVarResponse = await UpdateRepositoryWithHttpInfoAsync(registryId, repositoryImage, updateRepositoryRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Repository Update a Repository in a Container Registry Subscription
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="registryId">The [Registry ID](#components/schemas/registry/properties/id). Which can be found by [List Registries](#operation/list-registries).</param>
        /// <param name="repositoryImage">The [Repository Image](#components/schemas/registry-repository/properties/image). Which can be found by [List Repositories](#operation/list-registry-repositories).</param>
        /// <param name="updateRepositoryRequest"> (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RegistryRepository)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<RegistryRepository>> UpdateRepositoryWithHttpInfoAsync(string registryId, string repositoryImage, UpdateRepositoryRequest? updateRepositoryRequest = default(UpdateRepositoryRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'registryId' is set
            if (registryId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'registryId' when calling ContainerRegistryApi->UpdateRepository");
            }

            // verify the required parameter 'repositoryImage' is set
            if (repositoryImage == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'repositoryImage' when calling ContainerRegistryApi->UpdateRepository");
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

            localVarRequestOptions.PathParameters.Add("registry-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(registryId)); // path parameter
            localVarRequestOptions.PathParameters.Add("repository-image", Org.OpenAPITools.Client.ClientUtils.ParameterToString(repositoryImage)); // path parameter
            localVarRequestOptions.Data = updateRepositoryRequest;

            localVarRequestOptions.Operation = "ContainerRegistryApi.UpdateRepository";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<RegistryRepository>("/registry/{registry-id}/repository/{repository-image}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateRepository", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
