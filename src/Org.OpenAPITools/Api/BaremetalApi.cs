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
    public interface IBaremetalApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void AttachBaremetalsVpc2(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0);

        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> AttachBaremetalsVpc2WithHttpInfo(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0);
        /// <summary>
        /// Create Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBaremetal202Response</returns>
        CreateBaremetal202Response CreateBaremetal(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBaremetal202Response</returns>
        ApiResponse<CreateBaremetal202Response> CreateBaremetalWithHttpInfo(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Bare Metal
        /// </summary>
        /// <remarks>
        /// Delete a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Delete Bare Metal
        /// </summary>
        /// <remarks>
        /// Delete a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DetachBaremetalVpc2(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0);

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DetachBaremetalVpc2WithHttpInfo(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0);
        /// <summary>
        /// Bare Metal Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBandwidthBaremetal200Response</returns>
        GetBandwidthBaremetal200Response GetBandwidthBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Bare Metal Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBandwidthBaremetal200Response</returns>
        ApiResponse<GetBandwidthBaremetal200Response> GetBandwidthBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Get Bare Metal User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalUserdata200Response</returns>
        GetBareMetalUserdata200Response GetBareMetalUserdata(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Get Bare Metal User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalUserdata200Response</returns>
        ApiResponse<GetBareMetalUserdata200Response> GetBareMetalUserdataWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Get VNC URL for a Bare Metal
        /// </summary>
        /// <remarks>
        /// Get the VNC URL for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalVnc200Response</returns>
        GetBareMetalVnc200Response GetBareMetalVnc(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Get VNC URL for a Bare Metal
        /// </summary>
        /// <remarks>
        /// Get the VNC URL for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalVnc200Response</returns>
        ApiResponse<GetBareMetalVnc200Response> GetBareMetalVncWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Get Available Bare Metal Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalsUpgrades200Response</returns>
        GetBareMetalsUpgrades200Response GetBareMetalsUpgrades(string baremetalId, string? type = default(string?), int operationIndex = 0);

        /// <summary>
        /// Get Available Bare Metal Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalsUpgrades200Response</returns>
        ApiResponse<GetBareMetalsUpgrades200Response> GetBareMetalsUpgradesWithHttpInfo(string baremetalId, string? type = default(string?), int operationIndex = 0);
        /// <summary>
        /// Get Bare Metal
        /// </summary>
        /// <remarks>
        /// Get information for a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        GetBaremetal200Response GetBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Get Bare Metal
        /// </summary>
        /// <remarks>
        /// Get information for a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        ApiResponse<GetBaremetal200Response> GetBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Bare Metal IPv4 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv4 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv4Baremetal200Response</returns>
        GetIpv4Baremetal200Response GetIpv4Baremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Bare Metal IPv4 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv4 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv4Baremetal200Response</returns>
        ApiResponse<GetIpv4Baremetal200Response> GetIpv4BaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Bare Metal IPv6 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv6Baremetal200Response</returns>
        GetIpv6Baremetal200Response GetIpv6Baremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Bare Metal IPv6 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv6Baremetal200Response</returns>
        ApiResponse<GetIpv6Baremetal200Response> GetIpv6BaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Halt Bare Metal
        /// </summary>
        /// <remarks>
        /// Halt the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void HaltBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Halt Bare Metal
        /// </summary>
        /// <remarks>
        /// Halt the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> HaltBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Halt Bare Metals
        /// </summary>
        /// <remarks>
        /// Halt Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void HaltBaremetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);

        /// <summary>
        /// Halt Bare Metals
        /// </summary>
        /// <remarks>
        /// Halt Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> HaltBaremetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);
        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBaremetalVpc2200Response</returns>
        ListBaremetalVpc2200Response ListBaremetalVpc2(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBaremetalVpc2200Response</returns>
        ApiResponse<ListBaremetalVpc2200Response> ListBaremetalVpc2WithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// List Bare Metal Instances
        /// </summary>
        /// <remarks>
        /// List all Bare Metal instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBaremetals200Response</returns>
        ListBaremetals200Response ListBaremetals(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Bare Metal Instances
        /// </summary>
        /// <remarks>
        /// List all Bare Metal instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBaremetals200Response</returns>
        ApiResponse<ListBaremetals200Response> ListBaremetalsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Reboot Bare Metals
        /// </summary>
        /// <remarks>
        /// Reboot Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RebootBareMetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);

        /// <summary>
        /// Reboot Bare Metals
        /// </summary>
        /// <remarks>
        /// Reboot Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RebootBareMetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);
        /// <summary>
        /// Reboot Bare Metal
        /// </summary>
        /// <remarks>
        /// Reboot the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RebootBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Reboot Bare Metal
        /// </summary>
        /// <remarks>
        /// Reboot the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RebootBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Reinstall Bare Metal
        /// </summary>
        /// <remarks>
        /// Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        GetBaremetal200Response ReinstallBaremetal(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0);

        /// <summary>
        /// Reinstall Bare Metal
        /// </summary>
        /// <remarks>
        /// Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        ApiResponse<GetBaremetal200Response> ReinstallBaremetalWithHttpInfo(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0);
        /// <summary>
        /// Start Bare Metals
        /// </summary>
        /// <remarks>
        /// Start Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void StartBareMetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);

        /// <summary>
        /// Start Bare Metals
        /// </summary>
        /// <remarks>
        /// Start Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> StartBareMetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0);
        /// <summary>
        /// Start Bare Metal
        /// </summary>
        /// <remarks>
        /// Start the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void StartBaremetal(string baremetalId, int operationIndex = 0);

        /// <summary>
        /// Start Bare Metal
        /// </summary>
        /// <remarks>
        /// Start the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> StartBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0);
        /// <summary>
        /// Update Bare Metal
        /// </summary>
        /// <remarks>
        /// Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        GetBaremetal200Response UpdateBaremetal(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Bare Metal
        /// </summary>
        /// <remarks>
        /// Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        ApiResponse<GetBaremetal200Response> UpdateBaremetalWithHttpInfo(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBaremetalApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task AttachBaremetalsVpc2Async(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> AttachBaremetalsVpc2WithHttpInfoAsync(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBaremetal202Response</returns>
        System.Threading.Tasks.Task<CreateBaremetal202Response> CreateBaremetalAsync(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBaremetal202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateBaremetal202Response>> CreateBaremetalWithHttpInfoAsync(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Bare Metal
        /// </summary>
        /// <remarks>
        /// Delete a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Bare Metal
        /// </summary>
        /// <remarks>
        /// Delete a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DetachBaremetalVpc2Async(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DetachBaremetalVpc2WithHttpInfoAsync(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Bare Metal Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBandwidthBaremetal200Response</returns>
        System.Threading.Tasks.Task<GetBandwidthBaremetal200Response> GetBandwidthBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Bare Metal Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBandwidthBaremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBandwidthBaremetal200Response>> GetBandwidthBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Bare Metal User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalUserdata200Response</returns>
        System.Threading.Tasks.Task<GetBareMetalUserdata200Response> GetBareMetalUserdataAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Bare Metal User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalUserdata200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBareMetalUserdata200Response>> GetBareMetalUserdataWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get VNC URL for a Bare Metal
        /// </summary>
        /// <remarks>
        /// Get the VNC URL for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalVnc200Response</returns>
        System.Threading.Tasks.Task<GetBareMetalVnc200Response> GetBareMetalVncAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get VNC URL for a Bare Metal
        /// </summary>
        /// <remarks>
        /// Get the VNC URL for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalVnc200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBareMetalVnc200Response>> GetBareMetalVncWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Available Bare Metal Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalsUpgrades200Response</returns>
        System.Threading.Tasks.Task<GetBareMetalsUpgrades200Response> GetBareMetalsUpgradesAsync(string baremetalId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Available Bare Metal Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for a Bare Metal
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalsUpgrades200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBareMetalsUpgrades200Response>> GetBareMetalsUpgradesWithHttpInfoAsync(string baremetalId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Bare Metal
        /// </summary>
        /// <remarks>
        /// Get information for a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        System.Threading.Tasks.Task<GetBaremetal200Response> GetBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Bare Metal
        /// </summary>
        /// <remarks>
        /// Get information for a Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBaremetal200Response>> GetBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Bare Metal IPv4 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv4 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv4Baremetal200Response</returns>
        System.Threading.Tasks.Task<GetIpv4Baremetal200Response> GetIpv4BaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Bare Metal IPv4 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv4 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv4Baremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetIpv4Baremetal200Response>> GetIpv4BaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Bare Metal IPv6 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv6Baremetal200Response</returns>
        System.Threading.Tasks.Task<GetIpv6Baremetal200Response> GetIpv6BaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Bare Metal IPv6 Addresses
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv6Baremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetIpv6Baremetal200Response>> GetIpv6BaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Halt Bare Metal
        /// </summary>
        /// <remarks>
        /// Halt the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task HaltBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Halt Bare Metal
        /// </summary>
        /// <remarks>
        /// Halt the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> HaltBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Halt Bare Metals
        /// </summary>
        /// <remarks>
        /// Halt Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task HaltBaremetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Halt Bare Metals
        /// </summary>
        /// <remarks>
        /// Halt Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> HaltBaremetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBaremetalVpc2200Response</returns>
        System.Threading.Tasks.Task<ListBaremetalVpc2200Response> ListBaremetalVpc2Async(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for a Bare Metal Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBaremetalVpc2200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListBaremetalVpc2200Response>> ListBaremetalVpc2WithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Bare Metal Instances
        /// </summary>
        /// <remarks>
        /// List all Bare Metal instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBaremetals200Response</returns>
        System.Threading.Tasks.Task<ListBaremetals200Response> ListBaremetalsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Bare Metal Instances
        /// </summary>
        /// <remarks>
        /// List all Bare Metal instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBaremetals200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListBaremetals200Response>> ListBaremetalsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reboot Bare Metals
        /// </summary>
        /// <remarks>
        /// Reboot Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RebootBareMetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reboot Bare Metals
        /// </summary>
        /// <remarks>
        /// Reboot Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RebootBareMetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reboot Bare Metal
        /// </summary>
        /// <remarks>
        /// Reboot the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RebootBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reboot Bare Metal
        /// </summary>
        /// <remarks>
        /// Reboot the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RebootBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reinstall Bare Metal
        /// </summary>
        /// <remarks>
        /// Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        System.Threading.Tasks.Task<GetBaremetal200Response> ReinstallBaremetalAsync(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reinstall Bare Metal
        /// </summary>
        /// <remarks>
        /// Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBaremetal200Response>> ReinstallBaremetalWithHttpInfoAsync(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Bare Metals
        /// </summary>
        /// <remarks>
        /// Start Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task StartBareMetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Bare Metals
        /// </summary>
        /// <remarks>
        /// Start Bare Metals.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> StartBareMetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start Bare Metal
        /// </summary>
        /// <remarks>
        /// Start the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task StartBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start Bare Metal
        /// </summary>
        /// <remarks>
        /// Start the Bare Metal instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> StartBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Bare Metal
        /// </summary>
        /// <remarks>
        /// Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        System.Threading.Tasks.Task<GetBaremetal200Response> UpdateBaremetalAsync(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Bare Metal
        /// </summary>
        /// <remarks>
        /// Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBaremetal200Response>> UpdateBaremetalWithHttpInfoAsync(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBaremetalApi : IBaremetalApiSync, IBaremetalApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class BaremetalApi : IBaremetalApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaremetalApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BaremetalApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaremetalApi"/> class.
        /// </summary>
        /// <returns></returns>
        public BaremetalApi(string basePath)
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
        /// Initializes a new instance of the <see cref="BaremetalApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public BaremetalApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="BaremetalApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public BaremetalApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Attach VPC 2.0 Network to Bare Metal Instance Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void AttachBaremetalsVpc2(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0)
        {
            AttachBaremetalsVpc2WithHttpInfo(baremetalId, attachBaremetalsVpc2Request);
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> AttachBaremetalsVpc2WithHttpInfo(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->AttachBaremetalsVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = attachBaremetalsVpc2Request;

            localVarRequestOptions.Operation = "BaremetalApi.AttachBaremetalsVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/{baremetal-id}/vpc2/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachBaremetalsVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AttachBaremetalsVpc2Async(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await AttachBaremetalsVpc2WithHttpInfoAsync(baremetalId, attachBaremetalsVpc2Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Bare Metal Instance Attach a VPC 2.0 Network to a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="attachBaremetalsVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> AttachBaremetalsVpc2WithHttpInfoAsync(string baremetalId, AttachBaremetalsVpc2Request? attachBaremetalsVpc2Request = default(AttachBaremetalsVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->AttachBaremetalsVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = attachBaremetalsVpc2Request;

            localVarRequestOptions.Operation = "BaremetalApi.AttachBaremetalsVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/{baremetal-id}/vpc2/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachBaremetalsVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Bare Metal Instance Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateBaremetal202Response</returns>
        public CreateBaremetal202Response CreateBaremetal(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBaremetal202Response> localVarResponse = CreateBaremetalWithHttpInfo(createBaremetalRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Bare Metal Instance Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateBaremetal202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateBaremetal202Response> CreateBaremetalWithHttpInfo(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.CreateBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateBaremetal202Response>("/bare-metals", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Bare Metal Instance Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateBaremetal202Response</returns>
        public async System.Threading.Tasks.Task<CreateBaremetal202Response> CreateBaremetalAsync(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateBaremetal202Response> localVarResponse = await CreateBaremetalWithHttpInfoAsync(createBaremetalRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Bare Metal Instance Create a new Bare Metal instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateBaremetal202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateBaremetal202Response>> CreateBaremetalWithHttpInfoAsync(CreateBaremetalRequest? createBaremetalRequest = default(CreateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.CreateBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateBaremetal202Response>("/bare-metals", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Bare Metal Delete a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteBaremetal(string baremetalId, int operationIndex = 0)
        {
            DeleteBaremetalWithHttpInfo(baremetalId);
        }

        /// <summary>
        /// Delete Bare Metal Delete a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->DeleteBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.DeleteBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Bare Metal Delete a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Bare Metal Delete a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->DeleteBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.DeleteBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DetachBaremetalVpc2(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0)
        {
            DetachBaremetalVpc2WithHttpInfo(baremetalId, detachBaremetalVpc2Request);
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DetachBaremetalVpc2WithHttpInfo(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->DetachBaremetalVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = detachBaremetalVpc2Request;

            localVarRequestOptions.Operation = "BaremetalApi.DetachBaremetalVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/{baremetal-id}/vpc2/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachBaremetalVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DetachBaremetalVpc2Async(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DetachBaremetalVpc2WithHttpInfoAsync(baremetalId, detachBaremetalVpc2Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Bare Metal Instance Detach a VPC 2.0 Network from an Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [bare-metal ID](#operation/list-baremetals).</param>
        /// <param name="detachBaremetalVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DetachBaremetalVpc2WithHttpInfoAsync(string baremetalId, DetachBaremetalVpc2Request? detachBaremetalVpc2Request = default(DetachBaremetalVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->DetachBaremetalVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = detachBaremetalVpc2Request;

            localVarRequestOptions.Operation = "BaremetalApi.DetachBaremetalVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/{baremetal-id}/vpc2/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachBaremetalVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal Bandwidth Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBandwidthBaremetal200Response</returns>
        public GetBandwidthBaremetal200Response GetBandwidthBaremetal(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> localVarResponse = GetBandwidthBaremetalWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal Bandwidth Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBandwidthBaremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> GetBandwidthBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBandwidthBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBandwidthBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBandwidthBaremetal200Response>("/bare-metals/{baremetal-id}/bandwidth", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBandwidthBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal Bandwidth Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBandwidthBaremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetBandwidthBaremetal200Response> GetBandwidthBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> localVarResponse = await GetBandwidthBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal Bandwidth Get bandwidth information for the Bare Metal instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. Bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBandwidthBaremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response>> GetBandwidthBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBandwidthBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBandwidthBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBandwidthBaremetal200Response>("/bare-metals/{baremetal-id}/bandwidth", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBandwidthBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Bare Metal User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalUserdata200Response</returns>
        public GetBareMetalUserdata200Response GetBareMetalUserdata(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalUserdata200Response> localVarResponse = GetBareMetalUserdataWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Bare Metal User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalUserdata200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBareMetalUserdata200Response> GetBareMetalUserdataWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalUserdata");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalUserdata";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBareMetalUserdata200Response>("/bare-metals/{baremetal-id}/user-data", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalUserdata", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Bare Metal User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalUserdata200Response</returns>
        public async System.Threading.Tasks.Task<GetBareMetalUserdata200Response> GetBareMetalUserdataAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalUserdata200Response> localVarResponse = await GetBareMetalUserdataWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Bare Metal User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for a Bare Metal.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalUserdata200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBareMetalUserdata200Response>> GetBareMetalUserdataWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalUserdata");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalUserdata";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBareMetalUserdata200Response>("/bare-metals/{baremetal-id}/user-data", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalUserdata", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get VNC URL for a Bare Metal Get the VNC URL for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalVnc200Response</returns>
        public GetBareMetalVnc200Response GetBareMetalVnc(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalVnc200Response> localVarResponse = GetBareMetalVncWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get VNC URL for a Bare Metal Get the VNC URL for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalVnc200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBareMetalVnc200Response> GetBareMetalVncWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalVnc");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalVnc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBareMetalVnc200Response>("/bare-metals/{baremetal-id}/vnc", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalVnc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get VNC URL for a Bare Metal Get the VNC URL for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalVnc200Response</returns>
        public async System.Threading.Tasks.Task<GetBareMetalVnc200Response> GetBareMetalVncAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalVnc200Response> localVarResponse = await GetBareMetalVncWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get VNC URL for a Bare Metal Get the VNC URL for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalVnc200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBareMetalVnc200Response>> GetBareMetalVncWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalVnc");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalVnc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBareMetalVnc200Response>("/bare-metals/{baremetal-id}/vnc", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalVnc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Available Bare Metal Upgrades Get available upgrades for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBareMetalsUpgrades200Response</returns>
        public GetBareMetalsUpgrades200Response GetBareMetalsUpgrades(string baremetalId, string? type = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalsUpgrades200Response> localVarResponse = GetBareMetalsUpgradesWithHttpInfo(baremetalId, type);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Available Bare Metal Upgrades Get available upgrades for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBareMetalsUpgrades200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBareMetalsUpgrades200Response> GetBareMetalsUpgradesWithHttpInfo(string baremetalId, string? type = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalsUpgrades");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalsUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBareMetalsUpgrades200Response>("/bare-metals/{baremetal-id}/upgrades", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalsUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Available Bare Metal Upgrades Get available upgrades for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBareMetalsUpgrades200Response</returns>
        public async System.Threading.Tasks.Task<GetBareMetalsUpgrades200Response> GetBareMetalsUpgradesAsync(string baremetalId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBareMetalsUpgrades200Response> localVarResponse = await GetBareMetalsUpgradesWithHttpInfoAsync(baremetalId, type, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Available Bare Metal Upgrades Get available upgrades for a Bare Metal
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, plans) - applications - os (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBareMetalsUpgrades200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBareMetalsUpgrades200Response>> GetBareMetalsUpgradesWithHttpInfoAsync(string baremetalId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBareMetalsUpgrades");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "BaremetalApi.GetBareMetalsUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBareMetalsUpgrades200Response>("/bare-metals/{baremetal-id}/upgrades", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBareMetalsUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Bare Metal Get information for a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        public GetBaremetal200Response GetBaremetal(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = GetBaremetalWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Bare Metal Get information for a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> GetBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBaremetal200Response>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Bare Metal Get information for a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetBaremetal200Response> GetBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = await GetBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Bare Metal Get information for a Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response>> GetBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBaremetal200Response>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal IPv4 Addresses Get the IPv4 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv4Baremetal200Response</returns>
        public GetIpv4Baremetal200Response GetIpv4Baremetal(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> localVarResponse = GetIpv4BaremetalWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal IPv4 Addresses Get the IPv4 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv4Baremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> GetIpv4BaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetIpv4Baremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetIpv4Baremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetIpv4Baremetal200Response>("/bare-metals/{baremetal-id}/ipv4", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetIpv4Baremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal IPv4 Addresses Get the IPv4 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv4Baremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetIpv4Baremetal200Response> GetIpv4BaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> localVarResponse = await GetIpv4BaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal IPv4 Addresses Get the IPv4 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv4Baremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response>> GetIpv4BaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetIpv4Baremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetIpv4Baremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetIpv4Baremetal200Response>("/bare-metals/{baremetal-id}/ipv4", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetIpv4Baremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal IPv6 Addresses Get the IPv6 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv6Baremetal200Response</returns>
        public GetIpv6Baremetal200Response GetIpv6Baremetal(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> localVarResponse = GetIpv6BaremetalWithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal IPv6 Addresses Get the IPv6 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv6Baremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> GetIpv6BaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetIpv6Baremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetIpv6Baremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetIpv6Baremetal200Response>("/bare-metals/{baremetal-id}/ipv6", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetIpv6Baremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Bare Metal IPv6 Addresses Get the IPv6 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv6Baremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetIpv6Baremetal200Response> GetIpv6BaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> localVarResponse = await GetIpv6BaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Bare Metal IPv6 Addresses Get the IPv6 information for the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv6Baremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response>> GetIpv6BaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->GetIpv6Baremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.GetIpv6Baremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetIpv6Baremetal200Response>("/bare-metals/{baremetal-id}/ipv6", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetIpv6Baremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Bare Metal Halt the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void HaltBaremetal(string baremetalId, int operationIndex = 0)
        {
            HaltBaremetalWithHttpInfo(baremetalId);
        }

        /// <summary>
        /// Halt Bare Metal Halt the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> HaltBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->HaltBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.HaltBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/{baremetal-id}/halt", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Bare Metal Halt the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task HaltBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await HaltBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Halt Bare Metal Halt the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> HaltBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->HaltBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.HaltBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/{baremetal-id}/halt", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Bare Metals Halt Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void HaltBaremetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
            HaltBaremetalsWithHttpInfo(haltBaremetalsRequest);
        }

        /// <summary>
        /// Halt Bare Metals Halt Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> HaltBaremetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.HaltBaremetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/halt", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltBaremetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Bare Metals Halt Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task HaltBaremetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await HaltBaremetalsWithHttpInfoAsync(haltBaremetalsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Halt Bare Metals Halt Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> HaltBaremetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.HaltBaremetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/halt", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltBaremetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks List the VPC 2.0 networks for a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBaremetalVpc2200Response</returns>
        public ListBaremetalVpc2200Response ListBaremetalVpc2(string baremetalId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListBaremetalVpc2200Response> localVarResponse = ListBaremetalVpc2WithHttpInfo(baremetalId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks List the VPC 2.0 networks for a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBaremetalVpc2200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListBaremetalVpc2200Response> ListBaremetalVpc2WithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->ListBaremetalVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.ListBaremetalVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListBaremetalVpc2200Response>("/bare-metals/{baremetal-id}/vpc2", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBaremetalVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks List the VPC 2.0 networks for a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBaremetalVpc2200Response</returns>
        public async System.Threading.Tasks.Task<ListBaremetalVpc2200Response> ListBaremetalVpc2Async(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListBaremetalVpc2200Response> localVarResponse = await ListBaremetalVpc2WithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Bare Metal Instance VPC 2.0 Networks List the VPC 2.0 networks for a Bare Metal Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal ID](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBaremetalVpc2200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListBaremetalVpc2200Response>> ListBaremetalVpc2WithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->ListBaremetalVpc2");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.ListBaremetalVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListBaremetalVpc2200Response>("/bare-metals/{baremetal-id}/vpc2", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBaremetalVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Bare Metal Instances List all Bare Metal instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListBaremetals200Response</returns>
        public ListBaremetals200Response ListBaremetals(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListBaremetals200Response> localVarResponse = ListBaremetalsWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Bare Metal Instances List all Bare Metal instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListBaremetals200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListBaremetals200Response> ListBaremetalsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            localVarRequestOptions.Operation = "BaremetalApi.ListBaremetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListBaremetals200Response>("/bare-metals", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBaremetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Bare Metal Instances List all Bare Metal instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListBaremetals200Response</returns>
        public async System.Threading.Tasks.Task<ListBaremetals200Response> ListBaremetalsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListBaremetals200Response> localVarResponse = await ListBaremetalsWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Bare Metal Instances List all Bare Metal instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListBaremetals200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListBaremetals200Response>> ListBaremetalsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "BaremetalApi.ListBaremetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListBaremetals200Response>("/bare-metals", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListBaremetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Bare Metals Reboot Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RebootBareMetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
            RebootBareMetalsWithHttpInfo(haltBaremetalsRequest);
        }

        /// <summary>
        /// Reboot Bare Metals Reboot Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> RebootBareMetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.RebootBareMetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/reboot", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootBareMetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Bare Metals Reboot Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RebootBareMetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await RebootBareMetalsWithHttpInfoAsync(haltBaremetalsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reboot Bare Metals Reboot Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> RebootBareMetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.RebootBareMetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/reboot", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootBareMetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Bare Metal Reboot the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RebootBaremetal(string baremetalId, int operationIndex = 0)
        {
            RebootBaremetalWithHttpInfo(baremetalId);
        }

        /// <summary>
        /// Reboot Bare Metal Reboot the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> RebootBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->RebootBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.RebootBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/{baremetal-id}/reboot", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Bare Metal Reboot the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RebootBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await RebootBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reboot Bare Metal Reboot the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> RebootBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->RebootBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.RebootBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/{baremetal-id}/reboot", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reinstall Bare Metal Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        public GetBaremetal200Response ReinstallBaremetal(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = ReinstallBaremetalWithHttpInfo(baremetalId, reinstallBaremetalRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Reinstall Bare Metal Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> ReinstallBaremetalWithHttpInfo(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->ReinstallBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = reinstallBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.ReinstallBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<GetBaremetal200Response>("/bare-metals/{baremetal-id}/reinstall", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReinstallBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reinstall Bare Metal Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetBaremetal200Response> ReinstallBaremetalAsync(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = await ReinstallBaremetalWithHttpInfoAsync(baremetalId, reinstallBaremetalRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Reinstall Bare Metal Reinstall the Bare Metal instance using an optional &#x60;hostname&#x60;.   **Note:** This action may take some time to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="reinstallBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response>> ReinstallBaremetalWithHttpInfoAsync(string baremetalId, ReinstallBaremetalRequest? reinstallBaremetalRequest = default(ReinstallBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->ReinstallBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = reinstallBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.ReinstallBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<GetBaremetal200Response>("/bare-metals/{baremetal-id}/reinstall", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReinstallBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Bare Metals Start Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void StartBareMetals(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
            StartBareMetalsWithHttpInfo(haltBaremetalsRequest);
        }

        /// <summary>
        /// Start Bare Metals Start Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> StartBareMetalsWithHttpInfo(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0)
        {
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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.StartBareMetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/start", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartBareMetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Bare Metals Start Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task StartBareMetalsAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await StartBareMetalsWithHttpInfoAsync(haltBaremetalsRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start Bare Metals Start Bare Metals.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltBaremetalsRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> StartBareMetalsWithHttpInfoAsync(HaltBaremetalsRequest? haltBaremetalsRequest = default(HaltBaremetalsRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

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

            localVarRequestOptions.Data = haltBaremetalsRequest;

            localVarRequestOptions.Operation = "BaremetalApi.StartBareMetals";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/start", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartBareMetals", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Bare Metal Start the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void StartBaremetal(string baremetalId, int operationIndex = 0)
        {
            StartBaremetalWithHttpInfo(baremetalId);
        }

        /// <summary>
        /// Start Bare Metal Start the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> StartBaremetalWithHttpInfo(string baremetalId, int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->StartBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.StartBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/bare-metals/{baremetal-id}/start", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start Bare Metal Start the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task StartBaremetalAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await StartBaremetalWithHttpInfoAsync(baremetalId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start Bare Metal Start the Bare Metal instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> StartBaremetalWithHttpInfoAsync(string baremetalId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->StartBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter

            localVarRequestOptions.Operation = "BaremetalApi.StartBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/bare-metals/{baremetal-id}/start", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Bare Metal Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBaremetal200Response</returns>
        public GetBaremetal200Response UpdateBaremetal(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = UpdateBaremetalWithHttpInfo(baremetalId, updateBaremetalRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Bare Metal Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBaremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> UpdateBaremetalWithHttpInfo(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->UpdateBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = updateBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.UpdateBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<GetBaremetal200Response>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Bare Metal Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBaremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetBaremetal200Response> UpdateBaremetalAsync(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response> localVarResponse = await UpdateBaremetalWithHttpInfoAsync(baremetalId, updateBaremetalRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Bare Metal Update a Bare Metal instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="baremetalId">The [Bare Metal id](#operation/list-baremetals).</param>
        /// <param name="updateBaremetalRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBaremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBaremetal200Response>> UpdateBaremetalWithHttpInfoAsync(string baremetalId, UpdateBaremetalRequest? updateBaremetalRequest = default(UpdateBaremetalRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'baremetalId' is set
            if (baremetalId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'baremetalId' when calling BaremetalApi->UpdateBaremetal");
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

            localVarRequestOptions.PathParameters.Add("baremetal-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(baremetalId)); // path parameter
            localVarRequestOptions.Data = updateBaremetalRequest;

            localVarRequestOptions.Operation = "BaremetalApi.UpdateBaremetal";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<GetBaremetal200Response>("/bare-metals/{baremetal-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateBaremetal", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
