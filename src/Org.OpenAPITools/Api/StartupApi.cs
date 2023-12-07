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
    public interface IStartupApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Startup Script
        /// </summary>
        /// <remarks>
        /// Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetStartupScript200Response</returns>
        GetStartupScript200Response CreateStartupScript(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Startup Script
        /// </summary>
        /// <remarks>
        /// Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetStartupScript200Response</returns>
        ApiResponse<GetStartupScript200Response> CreateStartupScriptWithHttpInfo(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Startup Script
        /// </summary>
        /// <remarks>
        /// Delete a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteStartupScript(string startupId, int operationIndex = 0);

        /// <summary>
        /// Delete Startup Script
        /// </summary>
        /// <remarks>
        /// Delete a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteStartupScriptWithHttpInfo(string startupId, int operationIndex = 0);
        /// <summary>
        /// Get Startup Script
        /// </summary>
        /// <remarks>
        /// Get information for a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetStartupScript200Response</returns>
        GetStartupScript200Response GetStartupScript(string startupId, int operationIndex = 0);

        /// <summary>
        /// Get Startup Script
        /// </summary>
        /// <remarks>
        /// Get information for a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetStartupScript200Response</returns>
        ApiResponse<GetStartupScript200Response> GetStartupScriptWithHttpInfo(string startupId, int operationIndex = 0);
        /// <summary>
        /// List Startup Scripts
        /// </summary>
        /// <remarks>
        /// Get a list of all Startup Scripts.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListStartupScripts200Response</returns>
        ListStartupScripts200Response ListStartupScripts(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Startup Scripts
        /// </summary>
        /// <remarks>
        /// Get a list of all Startup Scripts.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListStartupScripts200Response</returns>
        ApiResponse<ListStartupScripts200Response> ListStartupScriptsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Update Startup Script
        /// </summary>
        /// <remarks>
        /// Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateStartupScript(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Startup Script
        /// </summary>
        /// <remarks>
        /// Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateStartupScriptWithHttpInfo(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IStartupApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Startup Script
        /// </summary>
        /// <remarks>
        /// Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetStartupScript200Response</returns>
        System.Threading.Tasks.Task<GetStartupScript200Response> CreateStartupScriptAsync(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Startup Script
        /// </summary>
        /// <remarks>
        /// Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetStartupScript200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetStartupScript200Response>> CreateStartupScriptWithHttpInfoAsync(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Startup Script
        /// </summary>
        /// <remarks>
        /// Delete a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteStartupScriptAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Startup Script
        /// </summary>
        /// <remarks>
        /// Delete a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteStartupScriptWithHttpInfoAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Startup Script
        /// </summary>
        /// <remarks>
        /// Get information for a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetStartupScript200Response</returns>
        System.Threading.Tasks.Task<GetStartupScript200Response> GetStartupScriptAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Startup Script
        /// </summary>
        /// <remarks>
        /// Get information for a Startup Script.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetStartupScript200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetStartupScript200Response>> GetStartupScriptWithHttpInfoAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Startup Scripts
        /// </summary>
        /// <remarks>
        /// Get a list of all Startup Scripts.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListStartupScripts200Response</returns>
        System.Threading.Tasks.Task<ListStartupScripts200Response> ListStartupScriptsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Startup Scripts
        /// </summary>
        /// <remarks>
        /// Get a list of all Startup Scripts.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListStartupScripts200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListStartupScripts200Response>> ListStartupScriptsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Startup Script
        /// </summary>
        /// <remarks>
        /// Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateStartupScriptAsync(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Startup Script
        /// </summary>
        /// <remarks>
        /// Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateStartupScriptWithHttpInfoAsync(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IStartupApi : IStartupApiSync, IStartupApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class StartupApi : IStartupApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupApi"/> class.
        /// </summary>
        /// <returns></returns>
        public StartupApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupApi"/> class.
        /// </summary>
        /// <returns></returns>
        public StartupApi(string basePath)
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
        /// Initializes a new instance of the <see cref="StartupApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public StartupApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="StartupApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public StartupApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Create Startup Script Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetStartupScript200Response</returns>
        public GetStartupScript200Response CreateStartupScript(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> localVarResponse = CreateStartupScriptWithHttpInfo(createStartupScriptRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Startup Script Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetStartupScript200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> CreateStartupScriptWithHttpInfo(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createStartupScriptRequest;

            localVarRequestOptions.Operation = "StartupApi.CreateStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<GetStartupScript200Response>("/startup-scripts", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Startup Script Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetStartupScript200Response</returns>
        public async System.Threading.Tasks.Task<GetStartupScript200Response> CreateStartupScriptAsync(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> localVarResponse = await CreateStartupScriptWithHttpInfoAsync(createStartupScriptRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Startup Script Create a new Startup Script. The &#x60;name&#x60; and &#x60;script&#x60; attributes are required, and scripts are base-64 encoded.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetStartupScript200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response>> CreateStartupScriptWithHttpInfoAsync(CreateStartupScriptRequest? createStartupScriptRequest = default(CreateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createStartupScriptRequest;

            localVarRequestOptions.Operation = "StartupApi.CreateStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<GetStartupScript200Response>("/startup-scripts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Startup Script Delete a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteStartupScript(string startupId, int operationIndex = 0)
        {
            DeleteStartupScriptWithHttpInfo(startupId);
        }

        /// <summary>
        /// Delete Startup Script Delete a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteStartupScriptWithHttpInfo(string startupId, int operationIndex = 0)
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->DeleteStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter

            localVarRequestOptions.Operation = "StartupApi.DeleteStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Startup Script Delete a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteStartupScriptAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteStartupScriptWithHttpInfoAsync(startupId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Startup Script Delete a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteStartupScriptWithHttpInfoAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->DeleteStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter

            localVarRequestOptions.Operation = "StartupApi.DeleteStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Startup Script Get information for a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetStartupScript200Response</returns>
        public GetStartupScript200Response GetStartupScript(string startupId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> localVarResponse = GetStartupScriptWithHttpInfo(startupId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Startup Script Get information for a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetStartupScript200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> GetStartupScriptWithHttpInfo(string startupId, int operationIndex = 0)
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->GetStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter

            localVarRequestOptions.Operation = "StartupApi.GetStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetStartupScript200Response>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Startup Script Get information for a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetStartupScript200Response</returns>
        public async System.Threading.Tasks.Task<GetStartupScript200Response> GetStartupScriptAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response> localVarResponse = await GetStartupScriptWithHttpInfoAsync(startupId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Startup Script Get information for a Startup Script.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetStartupScript200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetStartupScript200Response>> GetStartupScriptWithHttpInfoAsync(string startupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->GetStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter

            localVarRequestOptions.Operation = "StartupApi.GetStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetStartupScript200Response>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Startup Scripts Get a list of all Startup Scripts.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListStartupScripts200Response</returns>
        public ListStartupScripts200Response ListStartupScripts(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListStartupScripts200Response> localVarResponse = ListStartupScriptsWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Startup Scripts Get a list of all Startup Scripts.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListStartupScripts200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListStartupScripts200Response> ListStartupScriptsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            localVarRequestOptions.Operation = "StartupApi.ListStartupScripts";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListStartupScripts200Response>("/startup-scripts", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListStartupScripts", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Startup Scripts Get a list of all Startup Scripts.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListStartupScripts200Response</returns>
        public async System.Threading.Tasks.Task<ListStartupScripts200Response> ListStartupScriptsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListStartupScripts200Response> localVarResponse = await ListStartupScriptsWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Startup Scripts Get a list of all Startup Scripts.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListStartupScripts200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListStartupScripts200Response>> ListStartupScriptsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "StartupApi.ListStartupScripts";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListStartupScripts200Response>("/startup-scripts", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListStartupScripts", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Startup Script Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateStartupScript(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0)
        {
            UpdateStartupScriptWithHttpInfo(startupId, updateStartupScriptRequest);
        }

        /// <summary>
        /// Update Startup Script Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateStartupScriptWithHttpInfo(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->UpdateStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter
            localVarRequestOptions.Data = updateStartupScriptRequest;

            localVarRequestOptions.Operation = "StartupApi.UpdateStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<Object>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Startup Script Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateStartupScriptAsync(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateStartupScriptWithHttpInfoAsync(startupId, updateStartupScriptRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Startup Script Update a Startup Script. The attributes &#x60;name&#x60; and &#x60;script&#x60; are optional. If not set, the attributes will retain their original values. The &#x60;script&#x60; attribute is base-64 encoded. New deployments will use the updated script, but this action does not update previously deployed instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startupId">The [Startup Script id](#operation/list-startup-scripts).</param>
        /// <param name="updateStartupScriptRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateStartupScriptWithHttpInfoAsync(string startupId, UpdateStartupScriptRequest? updateStartupScriptRequest = default(UpdateStartupScriptRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'startupId' is set
            if (startupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'startupId' when calling StartupApi->UpdateStartupScript");
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

            localVarRequestOptions.PathParameters.Add("startup-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(startupId)); // path parameter
            localVarRequestOptions.Data = updateStartupScriptRequest;

            localVarRequestOptions.Operation = "StartupApi.UpdateStartupScript";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<Object>("/startup-scripts/{startup-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateStartupScript", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
