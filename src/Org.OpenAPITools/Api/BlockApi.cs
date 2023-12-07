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
    public interface IBlockApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Attach Block Storage
        /// </summary>
        /// <remarks>
        /// Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void AttachBlock(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0);

        /// <summary>
        /// Attach Block Storage
        /// </summary>
        /// <remarks>
        /// Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> AttachBlockWithHttpInfo(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Block Storage
        /// </summary>
        /// <remarks>
        /// Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBlock202Response</returns>
        CreateBlock202Response CreateBlock(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Block Storage
        /// </summary>
        /// <remarks>
        /// Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBlock202Response</returns>
        ApiResponse<CreateBlock202Response> CreateBlockWithHttpInfo(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Block Storage
        /// </summary>
        /// <remarks>
        /// Delete Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteBlock(string blockId, int operationIndex = 0);

        /// <summary>
        /// Delete Block Storage
        /// </summary>
        /// <remarks>
        /// Delete Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteBlockWithHttpInfo(string blockId, int operationIndex = 0);
        /// <summary>
        /// Detach Block Storage
        /// </summary>
        /// <remarks>
        /// Detach Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DetachBlock(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0);

        /// <summary>
        /// Detach Block Storage
        /// </summary>
        /// <remarks>
        /// Detach Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DetachBlockWithHttpInfo(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0);
        /// <summary>
        /// Get Block Storage
        /// </summary>
        /// <remarks>
        /// Get information for Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBlock202Response</returns>
        CreateBlock202Response GetBlock(string blockId, int operationIndex = 0);

        /// <summary>
        /// Get Block Storage
        /// </summary>
        /// <remarks>
        /// Get information for Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBlock202Response</returns>
        ApiResponse<CreateBlock202Response> GetBlockWithHttpInfo(string blockId, int operationIndex = 0);
        /// <summary>
        /// List Block storages
        /// </summary>
        /// <remarks>
        /// List all Block Storage in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBlocks200Response</returns>
        ListBlocks200Response ListBlocks(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Block storages
        /// </summary>
        /// <remarks>
        /// List all Block Storage in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBlocks200Response</returns>
        ApiResponse<ListBlocks200Response> ListBlocksWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Update Block Storage
        /// </summary>
        /// <remarks>
        /// Update information for Block Storage. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateBlock(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Block Storage
        /// </summary>
        /// <remarks>
        /// Update information for Block Storage. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateBlockWithHttpInfo(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBlockApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Attach Block Storage
        /// </summary>
        /// <remarks>
        /// Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task AttachBlockAsync(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach Block Storage
        /// </summary>
        /// <remarks>
        /// Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> AttachBlockWithHttpInfoAsync(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Block Storage
        /// </summary>
        /// <remarks>
        /// Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBlock202Response</returns>
        System.Threading.Tasks.Task<CreateBlock202Response> CreateBlockAsync(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Block Storage
        /// </summary>
        /// <remarks>
        /// Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBlock202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateBlock202Response>> CreateBlockWithHttpInfoAsync(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Block Storage
        /// </summary>
        /// <remarks>
        /// Delete Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteBlockAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Block Storage
        /// </summary>
        /// <remarks>
        /// Delete Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteBlockWithHttpInfoAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach Block Storage
        /// </summary>
        /// <remarks>
        /// Detach Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DetachBlockAsync(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach Block Storage
        /// </summary>
        /// <remarks>
        /// Detach Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DetachBlockWithHttpInfoAsync(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Block Storage
        /// </summary>
        /// <remarks>
        /// Get information for Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBlock202Response</returns>
        System.Threading.Tasks.Task<CreateBlock202Response> GetBlockAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Block Storage
        /// </summary>
        /// <remarks>
        /// Get information for Block Storage.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBlock202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateBlock202Response>> GetBlockWithHttpInfoAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Block storages
        /// </summary>
        /// <remarks>
        /// List all Block Storage in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBlocks200Response</returns>
        System.Threading.Tasks.Task<ListBlocks200Response> ListBlocksAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Block storages
        /// </summary>
        /// <remarks>
        /// List all Block Storage in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBlocks200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListBlocks200Response>> ListBlocksWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Block Storage
        /// </summary>
        /// <remarks>
        /// Update information for Block Storage. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateBlockAsync(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Block Storage
        /// </summary>
        /// <remarks>
        /// Update information for Block Storage. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateBlockWithHttpInfoAsync(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBlockApi : IBlockApiSync, IBlockApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class BlockApi : IBlockApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BlockApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BlockApi(string basePath)
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
        /// Initializes a new instance of the <see cref="BlockApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public BlockApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="BlockApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public BlockApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Attach Block Storage Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void AttachBlock(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0)
        {
            AttachBlockWithHttpInfo(blockId, attachBlockRequest);
        }

        /// <summary>
        /// Attach Block Storage Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> AttachBlockWithHttpInfo(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->AttachBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = attachBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.AttachBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/blocks/{block-id}/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach Block Storage Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AttachBlockAsync(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await AttachBlockWithHttpInfoAsync(blockId, attachBlockRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Attach Block Storage Attach Block Storage to Instance &#x60;instance_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="attachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> AttachBlockWithHttpInfoAsync(string blockId, AttachBlockRequest? attachBlockRequest = default(AttachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->AttachBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = attachBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.AttachBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/blocks/{block-id}/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Block Storage Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBlock202Response</returns>
        public CreateBlock202Response CreateBlock(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> localVarResponse = CreateBlockWithHttpInfo(createBlockRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Block Storage Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBlock202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> CreateBlockWithHttpInfo(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.CreateBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateBlock202Response>("/blocks", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Block Storage Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBlock202Response</returns>
        public async System.Threading.Tasks.Task<CreateBlock202Response> CreateBlockAsync(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> localVarResponse = await CreateBlockWithHttpInfoAsync(createBlockRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Block Storage Create new Block Storage in a &#x60;region&#x60; with a size of &#x60;size_gb&#x60;. Size may range between 10 and 40000 depending on the &#x60;block_type&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBlock202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response>> CreateBlockWithHttpInfoAsync(CreateBlockRequest? createBlockRequest = default(CreateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.CreateBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateBlock202Response>("/blocks", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Block Storage Delete Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteBlock(string blockId, int operationIndex = 0)
        {
            DeleteBlockWithHttpInfo(blockId);
        }

        /// <summary>
        /// Delete Block Storage Delete Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteBlockWithHttpInfo(string blockId, int operationIndex = 0)
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->DeleteBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter

            localVarRequestOptions.Operation = "BlockApi.DeleteBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/blocks/{block-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Block Storage Delete Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteBlockAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteBlockWithHttpInfoAsync(blockId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Block Storage Delete Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteBlockWithHttpInfoAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->DeleteBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter

            localVarRequestOptions.Operation = "BlockApi.DeleteBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/blocks/{block-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Block Storage Detach Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DetachBlock(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0)
        {
            DetachBlockWithHttpInfo(blockId, detachBlockRequest);
        }

        /// <summary>
        /// Detach Block Storage Detach Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DetachBlockWithHttpInfo(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->DetachBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = detachBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.DetachBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/blocks/{block-id}/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Block Storage Detach Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DetachBlockAsync(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DetachBlockWithHttpInfoAsync(blockId, detachBlockRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach Block Storage Detach Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="detachBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DetachBlockWithHttpInfoAsync(string blockId, DetachBlockRequest? detachBlockRequest = default(DetachBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->DetachBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = detachBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.DetachBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/blocks/{block-id}/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Block Storage Get information for Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBlock202Response</returns>
        public CreateBlock202Response GetBlock(string blockId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> localVarResponse = GetBlockWithHttpInfo(blockId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Block Storage Get information for Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBlock202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> GetBlockWithHttpInfo(string blockId, int operationIndex = 0)
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->GetBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter

            localVarRequestOptions.Operation = "BlockApi.GetBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateBlock202Response>("/blocks/{block-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Block Storage Get information for Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBlock202Response</returns>
        public async System.Threading.Tasks.Task<CreateBlock202Response> GetBlockAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response> localVarResponse = await GetBlockWithHttpInfoAsync(blockId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Block Storage Get information for Block Storage.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBlock202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateBlock202Response>> GetBlockWithHttpInfoAsync(string blockId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->GetBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter

            localVarRequestOptions.Operation = "BlockApi.GetBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateBlock202Response>("/blocks/{block-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Block storages List all Block Storage in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBlocks200Response</returns>
        public ListBlocks200Response ListBlocks(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListBlocks200Response> localVarResponse = ListBlocksWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Block storages List all Block Storage in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBlocks200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListBlocks200Response> ListBlocksWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            localVarRequestOptions.Operation = "BlockApi.ListBlocks";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListBlocks200Response>("/blocks", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBlocks", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Block storages List all Block Storage in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBlocks200Response</returns>
        public async System.Threading.Tasks.Task<ListBlocks200Response> ListBlocksAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListBlocks200Response> localVarResponse = await ListBlocksWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Block storages List all Block Storage in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBlocks200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListBlocks200Response>> ListBlocksWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "BlockApi.ListBlocks";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListBlocks200Response>("/blocks", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBlocks", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Block Storage Update information for Block Storage. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateBlock(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0)
        {
            UpdateBlockWithHttpInfo(blockId, updateBlockRequest);
        }

        /// <summary>
        /// Update Block Storage Update information for Block Storage. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateBlockWithHttpInfo(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->UpdateBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = updateBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.UpdateBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<Object>("/blocks/{block-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Block Storage Update information for Block Storage. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateBlockAsync(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateBlockWithHttpInfoAsync(blockId, updateBlockRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Block Storage Update information for Block Storage. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="blockId">The [Block Storage id](#operation/list-blocks).</param>
        /// <param name="updateBlockRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateBlockWithHttpInfoAsync(string blockId, UpdateBlockRequest? updateBlockRequest = default(UpdateBlockRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'blockId' is set
            if (blockId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'blockId' when calling BlockApi->UpdateBlock");
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

            localVarRequestOptions.PathParameters.Add("block-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(blockId)); // path parameter
            localVarRequestOptions.Data = updateBlockRequest;

            localVarRequestOptions.Operation = "BlockApi.UpdateBlock";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<Object>("/blocks/{block-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateBlock", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
