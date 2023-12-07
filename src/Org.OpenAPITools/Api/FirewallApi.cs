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
    public interface IFirewallApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create Firewall Group
        /// </summary>
        /// <remarks>
        /// Create a new Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateFirewallGroup201Response</returns>
        CreateFirewallGroup201Response CreateFirewallGroup(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Firewall Group
        /// </summary>
        /// <remarks>
        /// Create a new Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateFirewallGroup201Response</returns>
        ApiResponse<CreateFirewallGroup201Response> CreateFirewallGroupWithHttpInfo(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Firewall Group
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteFirewallGroup(string firewallGroupId, int operationIndex = 0);

        /// <summary>
        /// Delete Firewall Group
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteFirewallGroupWithHttpInfo(string firewallGroupId, int operationIndex = 0);
        /// <summary>
        /// Delete Firewall Rule
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteFirewallGroupRule(string firewallGroupId, string firewallRuleId, int operationIndex = 0);

        /// <summary>
        /// Delete Firewall Rule
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteFirewallGroupRuleWithHttpInfo(string firewallGroupId, string firewallRuleId, int operationIndex = 0);
        /// <summary>
        /// Get Firewall Group
        /// </summary>
        /// <remarks>
        /// Get information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateFirewallGroup201Response</returns>
        CreateFirewallGroup201Response GetFirewallGroup(string firewallGroupId, int operationIndex = 0);

        /// <summary>
        /// Get Firewall Group
        /// </summary>
        /// <remarks>
        /// Get information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateFirewallGroup201Response</returns>
        ApiResponse<CreateFirewallGroup201Response> GetFirewallGroupWithHttpInfo(string firewallGroupId, int operationIndex = 0);
        /// <summary>
        /// Get Firewall Rule
        /// </summary>
        /// <remarks>
        /// Get a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PostFirewallsFirewallGroupIdRules201Response</returns>
        PostFirewallsFirewallGroupIdRules201Response GetFirewallGroupRule(string firewallGroupId, string firewallRuleId, int operationIndex = 0);

        /// <summary>
        /// Get Firewall Rule
        /// </summary>
        /// <remarks>
        /// Get a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PostFirewallsFirewallGroupIdRules201Response</returns>
        ApiResponse<PostFirewallsFirewallGroupIdRules201Response> GetFirewallGroupRuleWithHttpInfo(string firewallGroupId, string firewallRuleId, int operationIndex = 0);
        /// <summary>
        /// List Firewall Rules
        /// </summary>
        /// <remarks>
        /// Get the Firewall Rules for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListFirewallGroupRules200Response</returns>
        ListFirewallGroupRules200Response ListFirewallGroupRules(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Firewall Rules
        /// </summary>
        /// <remarks>
        /// Get the Firewall Rules for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListFirewallGroupRules200Response</returns>
        ApiResponse<ListFirewallGroupRules200Response> ListFirewallGroupRulesWithHttpInfo(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// List Firewall Groups
        /// </summary>
        /// <remarks>
        /// Get a list of all Firewall Groups.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListFirewallGroups200Response</returns>
        ListFirewallGroups200Response ListFirewallGroups(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Firewall Groups
        /// </summary>
        /// <remarks>
        /// Get a list of all Firewall Groups.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListFirewallGroups200Response</returns>
        ApiResponse<ListFirewallGroups200Response> ListFirewallGroupsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Create Firewall Rules
        /// </summary>
        /// <remarks>
        /// Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PostFirewallsFirewallGroupIdRules201Response</returns>
        PostFirewallsFirewallGroupIdRules201Response PostFirewallsFirewallGroupIdRules(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Firewall Rules
        /// </summary>
        /// <remarks>
        /// Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PostFirewallsFirewallGroupIdRules201Response</returns>
        ApiResponse<PostFirewallsFirewallGroupIdRules201Response> PostFirewallsFirewallGroupIdRulesWithHttpInfo(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Firewall Group
        /// </summary>
        /// <remarks>
        /// Update information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateFirewallGroup(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Firewall Group
        /// </summary>
        /// <remarks>
        /// Update information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateFirewallGroupWithHttpInfo(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IFirewallApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create Firewall Group
        /// </summary>
        /// <remarks>
        /// Create a new Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateFirewallGroup201Response</returns>
        System.Threading.Tasks.Task<CreateFirewallGroup201Response> CreateFirewallGroupAsync(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Firewall Group
        /// </summary>
        /// <remarks>
        /// Create a new Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateFirewallGroup201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateFirewallGroup201Response>> CreateFirewallGroupWithHttpInfoAsync(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Firewall Group
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteFirewallGroupAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Firewall Group
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteFirewallGroupWithHttpInfoAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Firewall Rule
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteFirewallGroupRuleAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Firewall Rule
        /// </summary>
        /// <remarks>
        /// Delete a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteFirewallGroupRuleWithHttpInfoAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Firewall Group
        /// </summary>
        /// <remarks>
        /// Get information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateFirewallGroup201Response</returns>
        System.Threading.Tasks.Task<CreateFirewallGroup201Response> GetFirewallGroupAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Firewall Group
        /// </summary>
        /// <remarks>
        /// Get information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateFirewallGroup201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateFirewallGroup201Response>> GetFirewallGroupWithHttpInfoAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Firewall Rule
        /// </summary>
        /// <remarks>
        /// Get a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PostFirewallsFirewallGroupIdRules201Response</returns>
        System.Threading.Tasks.Task<PostFirewallsFirewallGroupIdRules201Response> GetFirewallGroupRuleAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Firewall Rule
        /// </summary>
        /// <remarks>
        /// Get a Firewall Rule.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PostFirewallsFirewallGroupIdRules201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<PostFirewallsFirewallGroupIdRules201Response>> GetFirewallGroupRuleWithHttpInfoAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Firewall Rules
        /// </summary>
        /// <remarks>
        /// Get the Firewall Rules for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListFirewallGroupRules200Response</returns>
        System.Threading.Tasks.Task<ListFirewallGroupRules200Response> ListFirewallGroupRulesAsync(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Firewall Rules
        /// </summary>
        /// <remarks>
        /// Get the Firewall Rules for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListFirewallGroupRules200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListFirewallGroupRules200Response>> ListFirewallGroupRulesWithHttpInfoAsync(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Firewall Groups
        /// </summary>
        /// <remarks>
        /// Get a list of all Firewall Groups.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListFirewallGroups200Response</returns>
        System.Threading.Tasks.Task<ListFirewallGroups200Response> ListFirewallGroupsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Firewall Groups
        /// </summary>
        /// <remarks>
        /// Get a list of all Firewall Groups.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListFirewallGroups200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListFirewallGroups200Response>> ListFirewallGroupsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Firewall Rules
        /// </summary>
        /// <remarks>
        /// Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PostFirewallsFirewallGroupIdRules201Response</returns>
        System.Threading.Tasks.Task<PostFirewallsFirewallGroupIdRules201Response> PostFirewallsFirewallGroupIdRulesAsync(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Firewall Rules
        /// </summary>
        /// <remarks>
        /// Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PostFirewallsFirewallGroupIdRules201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<PostFirewallsFirewallGroupIdRules201Response>> PostFirewallsFirewallGroupIdRulesWithHttpInfoAsync(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Firewall Group
        /// </summary>
        /// <remarks>
        /// Update information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateFirewallGroupAsync(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Firewall Group
        /// </summary>
        /// <remarks>
        /// Update information for a Firewall Group.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateFirewallGroupWithHttpInfoAsync(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IFirewallApi : IFirewallApiSync, IFirewallApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class FirewallApi : IFirewallApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirewallApi"/> class.
        /// </summary>
        /// <returns></returns>
        public FirewallApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FirewallApi"/> class.
        /// </summary>
        /// <returns></returns>
        public FirewallApi(string basePath)
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
        /// Initializes a new instance of the <see cref="FirewallApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public FirewallApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="FirewallApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public FirewallApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Create Firewall Group Create a new Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateFirewallGroup201Response</returns>
        public CreateFirewallGroup201Response CreateFirewallGroup(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> localVarResponse = CreateFirewallGroupWithHttpInfo(createFirewallGroupRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Firewall Group Create a new Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateFirewallGroup201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> CreateFirewallGroupWithHttpInfo(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createFirewallGroupRequest;

            localVarRequestOptions.Operation = "FirewallApi.CreateFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateFirewallGroup201Response>("/firewalls", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Firewall Group Create a new Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateFirewallGroup201Response</returns>
        public async System.Threading.Tasks.Task<CreateFirewallGroup201Response> CreateFirewallGroupAsync(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> localVarResponse = await CreateFirewallGroupWithHttpInfoAsync(createFirewallGroupRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Firewall Group Create a new Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateFirewallGroup201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response>> CreateFirewallGroupWithHttpInfoAsync(CreateFirewallGroupRequest? createFirewallGroupRequest = default(CreateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createFirewallGroupRequest;

            localVarRequestOptions.Operation = "FirewallApi.CreateFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateFirewallGroup201Response>("/firewalls", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Firewall Group Delete a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteFirewallGroup(string firewallGroupId, int operationIndex = 0)
        {
            DeleteFirewallGroupWithHttpInfo(firewallGroupId);
        }

        /// <summary>
        /// Delete Firewall Group Delete a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteFirewallGroupWithHttpInfo(string firewallGroupId, int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->DeleteFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.DeleteFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Firewall Group Delete a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteFirewallGroupAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteFirewallGroupWithHttpInfoAsync(firewallGroupId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Firewall Group Delete a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteFirewallGroupWithHttpInfoAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->DeleteFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.DeleteFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Firewall Rule Delete a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteFirewallGroupRule(string firewallGroupId, string firewallRuleId, int operationIndex = 0)
        {
            DeleteFirewallGroupRuleWithHttpInfo(firewallGroupId, firewallRuleId);
        }

        /// <summary>
        /// Delete Firewall Rule Delete a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteFirewallGroupRuleWithHttpInfo(string firewallGroupId, string firewallRuleId, int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->DeleteFirewallGroupRule");
            }

            // verify the required parameter 'firewallRuleId' is set
            if (firewallRuleId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallRuleId' when calling FirewallApi->DeleteFirewallGroupRule");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.PathParameters.Add("firewall-rule-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallRuleId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.DeleteFirewallGroupRule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/firewalls/{firewall-group-id}/rules/{firewall-rule-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteFirewallGroupRule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Firewall Rule Delete a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteFirewallGroupRuleAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteFirewallGroupRuleWithHttpInfoAsync(firewallGroupId, firewallRuleId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Firewall Rule Delete a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteFirewallGroupRuleWithHttpInfoAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->DeleteFirewallGroupRule");
            }

            // verify the required parameter 'firewallRuleId' is set
            if (firewallRuleId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallRuleId' when calling FirewallApi->DeleteFirewallGroupRule");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.PathParameters.Add("firewall-rule-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallRuleId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.DeleteFirewallGroupRule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/firewalls/{firewall-group-id}/rules/{firewall-rule-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteFirewallGroupRule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Firewall Group Get information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateFirewallGroup201Response</returns>
        public CreateFirewallGroup201Response GetFirewallGroup(string firewallGroupId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> localVarResponse = GetFirewallGroupWithHttpInfo(firewallGroupId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Firewall Group Get information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateFirewallGroup201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> GetFirewallGroupWithHttpInfo(string firewallGroupId, int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->GetFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.GetFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateFirewallGroup201Response>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Firewall Group Get information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateFirewallGroup201Response</returns>
        public async System.Threading.Tasks.Task<CreateFirewallGroup201Response> GetFirewallGroupAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response> localVarResponse = await GetFirewallGroupWithHttpInfoAsync(firewallGroupId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Firewall Group Get information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateFirewallGroup201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateFirewallGroup201Response>> GetFirewallGroupWithHttpInfoAsync(string firewallGroupId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->GetFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.GetFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateFirewallGroup201Response>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Firewall Rule Get a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PostFirewallsFirewallGroupIdRules201Response</returns>
        public PostFirewallsFirewallGroupIdRules201Response GetFirewallGroupRule(string firewallGroupId, string firewallRuleId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> localVarResponse = GetFirewallGroupRuleWithHttpInfo(firewallGroupId, firewallRuleId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Firewall Rule Get a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PostFirewallsFirewallGroupIdRules201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> GetFirewallGroupRuleWithHttpInfo(string firewallGroupId, string firewallRuleId, int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->GetFirewallGroupRule");
            }

            // verify the required parameter 'firewallRuleId' is set
            if (firewallRuleId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallRuleId' when calling FirewallApi->GetFirewallGroupRule");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.PathParameters.Add("firewall-rule-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallRuleId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.GetFirewallGroupRule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<PostFirewallsFirewallGroupIdRules201Response>("/firewalls/{firewall-group-id}/rules/{firewall-rule-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetFirewallGroupRule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Firewall Rule Get a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PostFirewallsFirewallGroupIdRules201Response</returns>
        public async System.Threading.Tasks.Task<PostFirewallsFirewallGroupIdRules201Response> GetFirewallGroupRuleAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> localVarResponse = await GetFirewallGroupRuleWithHttpInfoAsync(firewallGroupId, firewallRuleId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Firewall Rule Get a Firewall Rule.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="firewallRuleId">The [Firewall Rule id](#operation/list-firewall-group-rules).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PostFirewallsFirewallGroupIdRules201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response>> GetFirewallGroupRuleWithHttpInfoAsync(string firewallGroupId, string firewallRuleId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->GetFirewallGroupRule");
            }

            // verify the required parameter 'firewallRuleId' is set
            if (firewallRuleId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallRuleId' when calling FirewallApi->GetFirewallGroupRule");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.PathParameters.Add("firewall-rule-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallRuleId)); // path parameter

            localVarRequestOptions.Operation = "FirewallApi.GetFirewallGroupRule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<PostFirewallsFirewallGroupIdRules201Response>("/firewalls/{firewall-group-id}/rules/{firewall-rule-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetFirewallGroupRule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Firewall Rules Get the Firewall Rules for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListFirewallGroupRules200Response</returns>
        public ListFirewallGroupRules200Response ListFirewallGroupRules(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListFirewallGroupRules200Response> localVarResponse = ListFirewallGroupRulesWithHttpInfo(firewallGroupId, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Firewall Rules Get the Firewall Rules for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListFirewallGroupRules200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListFirewallGroupRules200Response> ListFirewallGroupRulesWithHttpInfo(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->ListFirewallGroupRules");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "FirewallApi.ListFirewallGroupRules";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListFirewallGroupRules200Response>("/firewalls/{firewall-group-id}/rules", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFirewallGroupRules", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Firewall Rules Get the Firewall Rules for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListFirewallGroupRules200Response</returns>
        public async System.Threading.Tasks.Task<ListFirewallGroupRules200Response> ListFirewallGroupRulesAsync(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListFirewallGroupRules200Response> localVarResponse = await ListFirewallGroupRulesWithHttpInfoAsync(firewallGroupId, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Firewall Rules Get the Firewall Rules for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListFirewallGroupRules200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListFirewallGroupRules200Response>> ListFirewallGroupRulesWithHttpInfoAsync(string firewallGroupId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->ListFirewallGroupRules");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "FirewallApi.ListFirewallGroupRules";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListFirewallGroupRules200Response>("/firewalls/{firewall-group-id}/rules", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFirewallGroupRules", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Firewall Groups Get a list of all Firewall Groups.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListFirewallGroups200Response</returns>
        public ListFirewallGroups200Response ListFirewallGroups(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListFirewallGroups200Response> localVarResponse = ListFirewallGroupsWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Firewall Groups Get a list of all Firewall Groups.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListFirewallGroups200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListFirewallGroups200Response> ListFirewallGroupsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            localVarRequestOptions.Operation = "FirewallApi.ListFirewallGroups";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListFirewallGroups200Response>("/firewalls", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFirewallGroups", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Firewall Groups Get a list of all Firewall Groups.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListFirewallGroups200Response</returns>
        public async System.Threading.Tasks.Task<ListFirewallGroups200Response> ListFirewallGroupsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListFirewallGroups200Response> localVarResponse = await ListFirewallGroupsWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Firewall Groups Get a list of all Firewall Groups.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListFirewallGroups200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListFirewallGroups200Response>> ListFirewallGroupsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "FirewallApi.ListFirewallGroups";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListFirewallGroups200Response>("/firewalls", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListFirewallGroups", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Firewall Rules Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>PostFirewallsFirewallGroupIdRules201Response</returns>
        public PostFirewallsFirewallGroupIdRules201Response PostFirewallsFirewallGroupIdRules(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> localVarResponse = PostFirewallsFirewallGroupIdRulesWithHttpInfo(firewallGroupId, postFirewallsFirewallGroupIdRulesRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Firewall Rules Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of PostFirewallsFirewallGroupIdRules201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> PostFirewallsFirewallGroupIdRulesWithHttpInfo(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->PostFirewallsFirewallGroupIdRules");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.Data = postFirewallsFirewallGroupIdRulesRequest;

            localVarRequestOptions.Operation = "FirewallApi.PostFirewallsFirewallGroupIdRules";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<PostFirewallsFirewallGroupIdRules201Response>("/firewalls/{firewall-group-id}/rules", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostFirewallsFirewallGroupIdRules", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Firewall Rules Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PostFirewallsFirewallGroupIdRules201Response</returns>
        public async System.Threading.Tasks.Task<PostFirewallsFirewallGroupIdRules201Response> PostFirewallsFirewallGroupIdRulesAsync(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response> localVarResponse = await PostFirewallsFirewallGroupIdRulesWithHttpInfoAsync(firewallGroupId, postFirewallsFirewallGroupIdRulesRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Firewall Rules Create a Firewall Rule for a Firewall Group. The attributes &#x60;ip_type&#x60;, &#x60;protocol&#x60;, &#x60;subnet&#x60;, and &#x60;subnet_size&#x60; are required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="postFirewallsFirewallGroupIdRulesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PostFirewallsFirewallGroupIdRules201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<PostFirewallsFirewallGroupIdRules201Response>> PostFirewallsFirewallGroupIdRulesWithHttpInfoAsync(string firewallGroupId, PostFirewallsFirewallGroupIdRulesRequest? postFirewallsFirewallGroupIdRulesRequest = default(PostFirewallsFirewallGroupIdRulesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->PostFirewallsFirewallGroupIdRules");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.Data = postFirewallsFirewallGroupIdRulesRequest;

            localVarRequestOptions.Operation = "FirewallApi.PostFirewallsFirewallGroupIdRules";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<PostFirewallsFirewallGroupIdRules201Response>("/firewalls/{firewall-group-id}/rules", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostFirewallsFirewallGroupIdRules", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Firewall Group Update information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateFirewallGroup(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0)
        {
            UpdateFirewallGroupWithHttpInfo(firewallGroupId, updateFirewallGroupRequest);
        }

        /// <summary>
        /// Update Firewall Group Update information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateFirewallGroupWithHttpInfo(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->UpdateFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.Data = updateFirewallGroupRequest;

            localVarRequestOptions.Operation = "FirewallApi.UpdateFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Firewall Group Update information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateFirewallGroupAsync(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateFirewallGroupWithHttpInfoAsync(firewallGroupId, updateFirewallGroupRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Firewall Group Update information for a Firewall Group.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="firewallGroupId">The [Firewall Group id](#operation/list-firewall-groups).</param>
        /// <param name="updateFirewallGroupRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateFirewallGroupWithHttpInfoAsync(string firewallGroupId, UpdateFirewallGroupRequest? updateFirewallGroupRequest = default(UpdateFirewallGroupRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'firewallGroupId' is set
            if (firewallGroupId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'firewallGroupId' when calling FirewallApi->UpdateFirewallGroup");
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

            localVarRequestOptions.PathParameters.Add("firewall-group-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(firewallGroupId)); // path parameter
            localVarRequestOptions.Data = updateFirewallGroupRequest;

            localVarRequestOptions.Operation = "FirewallApi.UpdateFirewallGroup";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/firewalls/{firewall-group-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateFirewallGroup", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
