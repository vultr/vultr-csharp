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
    public interface ISshApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create SSH key
        /// </summary>
        /// <remarks>
        /// Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetSshKey200Response</returns>
        GetSshKey200Response CreateSshKey(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0);

        /// <summary>
        /// Create SSH key
        /// </summary>
        /// <remarks>
        /// Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetSshKey200Response</returns>
        ApiResponse<GetSshKey200Response> CreateSshKeyWithHttpInfo(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete SSH Key
        /// </summary>
        /// <remarks>
        /// Delete an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteSshKey(string sshKeyId, int operationIndex = 0);

        /// <summary>
        /// Delete SSH Key
        /// </summary>
        /// <remarks>
        /// Delete an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteSshKeyWithHttpInfo(string sshKeyId, int operationIndex = 0);
        /// <summary>
        /// Get SSH Key
        /// </summary>
        /// <remarks>
        /// Get information about an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetSshKey200Response</returns>
        GetSshKey200Response GetSshKey(string sshKeyId, int operationIndex = 0);

        /// <summary>
        /// Get SSH Key
        /// </summary>
        /// <remarks>
        /// Get information about an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetSshKey200Response</returns>
        ApiResponse<GetSshKey200Response> GetSshKeyWithHttpInfo(string sshKeyId, int operationIndex = 0);
        /// <summary>
        /// List SSH Keys
        /// </summary>
        /// <remarks>
        /// List all SSH Keys in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListSshKeys200Response</returns>
        ListSshKeys200Response ListSshKeys(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List SSH Keys
        /// </summary>
        /// <remarks>
        /// List all SSH Keys in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListSshKeys200Response</returns>
        ApiResponse<ListSshKeys200Response> ListSshKeysWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Update SSH Key
        /// </summary>
        /// <remarks>
        /// Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateSshKey(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0);

        /// <summary>
        /// Update SSH Key
        /// </summary>
        /// <remarks>
        /// Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateSshKeyWithHttpInfo(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISshApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create SSH key
        /// </summary>
        /// <remarks>
        /// Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetSshKey200Response</returns>
        System.Threading.Tasks.Task<GetSshKey200Response> CreateSshKeyAsync(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create SSH key
        /// </summary>
        /// <remarks>
        /// Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetSshKey200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetSshKey200Response>> CreateSshKeyWithHttpInfoAsync(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete SSH Key
        /// </summary>
        /// <remarks>
        /// Delete an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteSshKeyAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete SSH Key
        /// </summary>
        /// <remarks>
        /// Delete an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteSshKeyWithHttpInfoAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get SSH Key
        /// </summary>
        /// <remarks>
        /// Get information about an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetSshKey200Response</returns>
        System.Threading.Tasks.Task<GetSshKey200Response> GetSshKeyAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get SSH Key
        /// </summary>
        /// <remarks>
        /// Get information about an SSH Key.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetSshKey200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetSshKey200Response>> GetSshKeyWithHttpInfoAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List SSH Keys
        /// </summary>
        /// <remarks>
        /// List all SSH Keys in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListSshKeys200Response</returns>
        System.Threading.Tasks.Task<ListSshKeys200Response> ListSshKeysAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List SSH Keys
        /// </summary>
        /// <remarks>
        /// List all SSH Keys in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListSshKeys200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListSshKeys200Response>> ListSshKeysWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update SSH Key
        /// </summary>
        /// <remarks>
        /// Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateSshKeyAsync(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update SSH Key
        /// </summary>
        /// <remarks>
        /// Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateSshKeyWithHttpInfoAsync(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISshApi : ISshApiSync, ISshApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class SshApi : ISshApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SshApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SshApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SshApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SshApi(string basePath)
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
        /// Initializes a new instance of the <see cref="SshApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public SshApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="SshApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public SshApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Create SSH key Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetSshKey200Response</returns>
        public GetSshKey200Response CreateSshKey(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> localVarResponse = CreateSshKeyWithHttpInfo(createSshKeyRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create SSH key Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetSshKey200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> CreateSshKeyWithHttpInfo(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createSshKeyRequest;

            localVarRequestOptions.Operation = "SshApi.CreateSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<GetSshKey200Response>("/ssh-keys", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create SSH key Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetSshKey200Response</returns>
        public async System.Threading.Tasks.Task<GetSshKey200Response> CreateSshKeyAsync(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> localVarResponse = await CreateSshKeyWithHttpInfoAsync(createSshKeyRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create SSH key Create a new SSH Key for use with future instances. This does not update any running instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetSshKey200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response>> CreateSshKeyWithHttpInfoAsync(CreateSshKeyRequest? createSshKeyRequest = default(CreateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createSshKeyRequest;

            localVarRequestOptions.Operation = "SshApi.CreateSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<GetSshKey200Response>("/ssh-keys", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete SSH Key Delete an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteSshKey(string sshKeyId, int operationIndex = 0)
        {
            DeleteSshKeyWithHttpInfo(sshKeyId);
        }

        /// <summary>
        /// Delete SSH Key Delete an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteSshKeyWithHttpInfo(string sshKeyId, int operationIndex = 0)
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->DeleteSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter

            localVarRequestOptions.Operation = "SshApi.DeleteSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete SSH Key Delete an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteSshKeyAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteSshKeyWithHttpInfoAsync(sshKeyId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete SSH Key Delete an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteSshKeyWithHttpInfoAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->DeleteSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter

            localVarRequestOptions.Operation = "SshApi.DeleteSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get SSH Key Get information about an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetSshKey200Response</returns>
        public GetSshKey200Response GetSshKey(string sshKeyId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> localVarResponse = GetSshKeyWithHttpInfo(sshKeyId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get SSH Key Get information about an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetSshKey200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> GetSshKeyWithHttpInfo(string sshKeyId, int operationIndex = 0)
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->GetSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter

            localVarRequestOptions.Operation = "SshApi.GetSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetSshKey200Response>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get SSH Key Get information about an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetSshKey200Response</returns>
        public async System.Threading.Tasks.Task<GetSshKey200Response> GetSshKeyAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response> localVarResponse = await GetSshKeyWithHttpInfoAsync(sshKeyId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get SSH Key Get information about an SSH Key.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetSshKey200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetSshKey200Response>> GetSshKeyWithHttpInfoAsync(string sshKeyId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->GetSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter

            localVarRequestOptions.Operation = "SshApi.GetSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetSshKey200Response>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List SSH Keys List all SSH Keys in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListSshKeys200Response</returns>
        public ListSshKeys200Response ListSshKeys(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListSshKeys200Response> localVarResponse = ListSshKeysWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List SSH Keys List all SSH Keys in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListSshKeys200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListSshKeys200Response> ListSshKeysWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "SshApi.ListSshKeys";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListSshKeys200Response>("/ssh-keys", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListSshKeys", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List SSH Keys List all SSH Keys in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListSshKeys200Response</returns>
        public async System.Threading.Tasks.Task<ListSshKeys200Response> ListSshKeysAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListSshKeys200Response> localVarResponse = await ListSshKeysWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List SSH Keys List all SSH Keys in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListSshKeys200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListSshKeys200Response>> ListSshKeysWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "SshApi.ListSshKeys";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListSshKeys200Response>("/ssh-keys", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListSshKeys", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update SSH Key Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateSshKey(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0)
        {
            UpdateSshKeyWithHttpInfo(sshKeyId, updateSshKeyRequest);
        }

        /// <summary>
        /// Update SSH Key Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateSshKeyWithHttpInfo(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->UpdateSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter
            localVarRequestOptions.Data = updateSshKeyRequest;

            localVarRequestOptions.Operation = "SshApi.UpdateSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<Object>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update SSH Key Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateSshKeyAsync(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateSshKeyWithHttpInfoAsync(sshKeyId, updateSshKeyRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update SSH Key Update an SSH Key. The attributes &#x60;name&#x60; and &#x60;ssh_key&#x60; are optional. If not set, the attributes will retain their original values. New deployments will use the updated key, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="sshKeyId">The [SSH Key id](#operation/list-ssh-keys).</param>
        /// <param name="updateSshKeyRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateSshKeyWithHttpInfoAsync(string sshKeyId, UpdateSshKeyRequest? updateSshKeyRequest = default(UpdateSshKeyRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'sshKeyId' is set
            if (sshKeyId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'sshKeyId' when calling SshApi->UpdateSshKey");
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

            localVarRequestOptions.PathParameters.Add("ssh-key-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(sshKeyId)); // path parameter
            localVarRequestOptions.Data = updateSshKeyRequest;

            localVarRequestOptions.Operation = "SshApi.UpdateSshKey";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<Object>("/ssh-keys/{ssh-key-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateSshKey", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
