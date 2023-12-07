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
    public interface IInstancesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Attach ISO to Instance
        /// </summary>
        /// <remarks>
        /// Attach an ISO to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AttachInstanceIso202Response</returns>
        AttachInstanceIso202Response AttachInstanceIso(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0);

        /// <summary>
        /// Attach ISO to Instance
        /// </summary>
        /// <remarks>
        /// Attach an ISO to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AttachInstanceIso202Response</returns>
        ApiResponse<AttachInstanceIso202Response> AttachInstanceIsoWithHttpInfo(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0);
        /// <summary>
        /// Attach Private Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        [Obsolete]
        void AttachInstanceNetwork(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0);

        /// <summary>
        /// Attach Private Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        [Obsolete]
        ApiResponse<Object> AttachInstanceNetworkWithHttpInfo(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0);
        /// <summary>
        /// Attach VPC to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void AttachInstanceVpc(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0);

        /// <summary>
        /// Attach VPC to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> AttachInstanceVpcWithHttpInfo(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0);
        /// <summary>
        /// Attach VPC 2.0 Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void AttachInstanceVpc2(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0);

        /// <summary>
        /// Attach VPC 2.0 Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> AttachInstanceVpc2WithHttpInfo(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0);
        /// <summary>
        /// Create Instance
        /// </summary>
        /// <remarks>
        /// Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        CreateInstance202Response CreateInstance(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Instance
        /// </summary>
        /// <remarks>
        /// Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        ApiResponse<CreateInstance202Response> CreateInstanceWithHttpInfo(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0);
        /// <summary>
        /// Set Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void CreateInstanceBackupSchedule(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0);

        /// <summary>
        /// Set Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> CreateInstanceBackupScheduleWithHttpInfo(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0);
        /// <summary>
        /// Create IPv4
        /// </summary>
        /// <remarks>
        /// Create an IPv4 address for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Object</returns>
        Object CreateInstanceIpv4(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0);

        /// <summary>
        /// Create IPv4
        /// </summary>
        /// <remarks>
        /// Create an IPv4 address for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object</returns>
        ApiResponse<Object> CreateInstanceIpv4WithHttpInfo(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0);
        /// <summary>
        /// Create Instance Reverse IPv4
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void CreateInstanceReverseIpv4(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0);

        /// <summary>
        /// Create Instance Reverse IPv4
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> CreateInstanceReverseIpv4WithHttpInfo(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0);
        /// <summary>
        /// Create Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void CreateInstanceReverseIpv6(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0);

        /// <summary>
        /// Create Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> CreateInstanceReverseIpv6WithHttpInfo(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0);
        /// <summary>
        /// Delete Instance
        /// </summary>
        /// <remarks>
        /// Delete an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteInstance(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Delete Instance
        /// </summary>
        /// <remarks>
        /// Delete an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteInstanceWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Delete IPv4 Address
        /// </summary>
        /// <remarks>
        /// Delete an IPv4 address from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteInstanceIpv4(string instanceId, string ipv4, int operationIndex = 0);

        /// <summary>
        /// Delete IPv4 Address
        /// </summary>
        /// <remarks>
        /// Delete an IPv4 address from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteInstanceIpv4WithHttpInfo(string instanceId, string ipv4, int operationIndex = 0);
        /// <summary>
        /// Delete Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Delete the reverse IPv6 for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteInstanceReverseIpv6(string instanceId, string ipv6, int operationIndex = 0);

        /// <summary>
        /// Delete Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Delete the reverse IPv6 for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteInstanceReverseIpv6WithHttpInfo(string instanceId, string ipv6, int operationIndex = 0);
        /// <summary>
        /// Detach ISO from instance
        /// </summary>
        /// <remarks>
        /// Detach the ISO from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>DetachInstanceIso202Response</returns>
        DetachInstanceIso202Response DetachInstanceIso(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Detach ISO from instance
        /// </summary>
        /// <remarks>
        /// Detach the ISO from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of DetachInstanceIso202Response</returns>
        ApiResponse<DetachInstanceIso202Response> DetachInstanceIsoWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Detach Private Network from Instance.
        /// </summary>
        /// <remarks>
        /// Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        [Obsolete]
        void DetachInstanceNetwork(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0);

        /// <summary>
        /// Detach Private Network from Instance.
        /// </summary>
        /// <remarks>
        /// Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        [Obsolete]
        ApiResponse<Object> DetachInstanceNetworkWithHttpInfo(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0);
        /// <summary>
        /// Detach VPC from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DetachInstanceVpc(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0);

        /// <summary>
        /// Detach VPC from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DetachInstanceVpcWithHttpInfo(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0);
        /// <summary>
        /// Detach VPC 2.0 Network from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DetachInstanceVpc2(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0);

        /// <summary>
        /// Detach VPC 2.0 Network from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DetachInstanceVpc2WithHttpInfo(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0);
        /// <summary>
        /// Get Instance
        /// </summary>
        /// <remarks>
        /// Get information about an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        CreateInstance202Response GetInstance(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <remarks>
        /// Get information about an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        ApiResponse<CreateInstance202Response> GetInstanceWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Get Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Get the backup schedule for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceBackupSchedule200Response</returns>
        GetInstanceBackupSchedule200Response GetInstanceBackupSchedule(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Get the backup schedule for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceBackupSchedule200Response</returns>
        ApiResponse<GetInstanceBackupSchedule200Response> GetInstanceBackupScheduleWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Instance Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBandwidthBaremetal200Response</returns>
        GetBandwidthBaremetal200Response GetInstanceBandwidth(string instanceId, int? dateRange = default(int?), int operationIndex = 0);

        /// <summary>
        /// Instance Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBandwidthBaremetal200Response</returns>
        ApiResponse<GetBandwidthBaremetal200Response> GetInstanceBandwidthWithHttpInfo(string instanceId, int? dateRange = default(int?), int operationIndex = 0);
        /// <summary>
        /// List Instance IPv4 Information
        /// </summary>
        /// <remarks>
        /// List the IPv4 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv4Baremetal200Response</returns>
        GetIpv4Baremetal200Response GetInstanceIpv4(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Instance IPv4 Information
        /// </summary>
        /// <remarks>
        /// List the IPv4 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv4Baremetal200Response</returns>
        ApiResponse<GetIpv4Baremetal200Response> GetInstanceIpv4WithHttpInfo(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Get Instance IPv6 Information
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for an VPS Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv6Baremetal200Response</returns>
        GetIpv6Baremetal200Response GetInstanceIpv6(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance IPv6 Information
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for an VPS Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv6Baremetal200Response</returns>
        ApiResponse<GetIpv6Baremetal200Response> GetInstanceIpv6WithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Get Instance ISO Status
        /// </summary>
        /// <remarks>
        /// Get the ISO status for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceIsoStatus200Response</returns>
        GetInstanceIsoStatus200Response GetInstanceIsoStatus(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance ISO Status
        /// </summary>
        /// <remarks>
        /// Get the ISO status for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceIsoStatus200Response</returns>
        ApiResponse<GetInstanceIsoStatus200Response> GetInstanceIsoStatusWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Get Instance neighbors
        /// </summary>
        /// <remarks>
        /// Get a list of other instances in the same location as this Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceNeighbors200Response</returns>
        GetInstanceNeighbors200Response GetInstanceNeighbors(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance neighbors
        /// </summary>
        /// <remarks>
        /// Get a list of other instances in the same location as this Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceNeighbors200Response</returns>
        ApiResponse<GetInstanceNeighbors200Response> GetInstanceNeighborsWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Get Available Instance Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for an Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceUpgrades200Response</returns>
        GetInstanceUpgrades200Response GetInstanceUpgrades(string instanceId, string? type = default(string?), int operationIndex = 0);

        /// <summary>
        /// Get Available Instance Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for an Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceUpgrades200Response</returns>
        ApiResponse<GetInstanceUpgrades200Response> GetInstanceUpgradesWithHttpInfo(string instanceId, string? type = default(string?), int operationIndex = 0);
        /// <summary>
        /// Get Instance User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceUserdata200Response</returns>
        GetInstanceUserdata200Response GetInstanceUserdata(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Get Instance User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceUserdata200Response</returns>
        ApiResponse<GetInstanceUserdata200Response> GetInstanceUserdataWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Halt Instance
        /// </summary>
        /// <remarks>
        /// Halt an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void HaltInstance(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Halt Instance
        /// </summary>
        /// <remarks>
        /// Halt an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> HaltInstanceWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Halt Instances
        /// </summary>
        /// <remarks>
        /// Halt Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void HaltInstances(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0);

        /// <summary>
        /// Halt Instances
        /// </summary>
        /// <remarks>
        /// Halt Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> HaltInstancesWithHttpInfo(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0);
        /// <summary>
        /// List Instance IPv6 Reverse
        /// </summary>
        /// <remarks>
        /// List the reverse IPv6 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceIpv6Reverse200Response</returns>
        ListInstanceIpv6Reverse200Response ListInstanceIpv6Reverse(string instanceId, int operationIndex = 0);

        /// <summary>
        /// List Instance IPv6 Reverse
        /// </summary>
        /// <remarks>
        /// List the reverse IPv6 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceIpv6Reverse200Response</returns>
        ApiResponse<ListInstanceIpv6Reverse200Response> ListInstanceIpv6ReverseWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// List instance Private Networks
        /// </summary>
        /// <remarks>
        /// **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        ListInstancePrivateNetworks200Response ListInstancePrivateNetworks(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List instance Private Networks
        /// </summary>
        /// <remarks>
        /// **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        ApiResponse<ListInstancePrivateNetworks200Response> ListInstancePrivateNetworksWithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// List Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceVpc2200Response</returns>
        ListInstanceVpc2200Response ListInstanceVpc2(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceVpc2200Response</returns>
        ApiResponse<ListInstanceVpc2200Response> ListInstanceVpc2WithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// List instance VPCs
        /// </summary>
        /// <remarks>
        /// List the VPCs for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceVpcs200Response</returns>
        ListInstanceVpcs200Response ListInstanceVpcs(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List instance VPCs
        /// </summary>
        /// <remarks>
        /// List the VPCs for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceVpcs200Response</returns>
        ApiResponse<ListInstanceVpcs200Response> ListInstanceVpcsWithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// List Instances
        /// </summary>
        /// <remarks>
        /// List all VPS instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstances200Response</returns>
        ListInstances200Response ListInstances(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Instances
        /// </summary>
        /// <remarks>
        /// List all VPS instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstances200Response</returns>
        ApiResponse<ListInstances200Response> ListInstancesWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0);
        /// <summary>
        /// Set Default Reverse DNS Entry
        /// </summary>
        /// <remarks>
        /// Set a reverse DNS entry for an IPv4 address
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void PostInstancesInstanceIdIpv4ReverseDefault(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0);

        /// <summary>
        /// Set Default Reverse DNS Entry
        /// </summary>
        /// <remarks>
        /// Set a reverse DNS entry for an IPv4 address
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0);
        /// <summary>
        /// Reboot Instance
        /// </summary>
        /// <remarks>
        /// Reboot an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RebootInstance(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Reboot Instance
        /// </summary>
        /// <remarks>
        /// Reboot an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RebootInstanceWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Reboot instances
        /// </summary>
        /// <remarks>
        /// Reboot Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void RebootInstances(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0);

        /// <summary>
        /// Reboot instances
        /// </summary>
        /// <remarks>
        /// Reboot Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> RebootInstancesWithHttpInfo(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0);
        /// <summary>
        /// Reinstall Instance
        /// </summary>
        /// <remarks>
        /// Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        CreateInstance202Response ReinstallInstance(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0);

        /// <summary>
        /// Reinstall Instance
        /// </summary>
        /// <remarks>
        /// Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        ApiResponse<CreateInstance202Response> ReinstallInstanceWithHttpInfo(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0);
        /// <summary>
        /// Restore Instance
        /// </summary>
        /// <remarks>
        /// Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RestoreInstance202Response</returns>
        RestoreInstance202Response RestoreInstance(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0);

        /// <summary>
        /// Restore Instance
        /// </summary>
        /// <remarks>
        /// Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RestoreInstance202Response</returns>
        ApiResponse<RestoreInstance202Response> RestoreInstanceWithHttpInfo(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0);
        /// <summary>
        /// Start instance
        /// </summary>
        /// <remarks>
        /// Start an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void StartInstance(string instanceId, int operationIndex = 0);

        /// <summary>
        /// Start instance
        /// </summary>
        /// <remarks>
        /// Start an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> StartInstanceWithHttpInfo(string instanceId, int operationIndex = 0);
        /// <summary>
        /// Start instances
        /// </summary>
        /// <remarks>
        /// Start Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void StartInstances(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0);

        /// <summary>
        /// Start instances
        /// </summary>
        /// <remarks>
        /// Start Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> StartInstancesWithHttpInfo(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Instance
        /// </summary>
        /// <remarks>
        /// Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        CreateInstance202Response UpdateInstance(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Instance
        /// </summary>
        /// <remarks>
        /// Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        ApiResponse<CreateInstance202Response> UpdateInstanceWithHttpInfo(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IInstancesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Attach ISO to Instance
        /// </summary>
        /// <remarks>
        /// Attach an ISO to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AttachInstanceIso202Response</returns>
        System.Threading.Tasks.Task<AttachInstanceIso202Response> AttachInstanceIsoAsync(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach ISO to Instance
        /// </summary>
        /// <remarks>
        /// Attach an ISO to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AttachInstanceIso202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<AttachInstanceIso202Response>> AttachInstanceIsoWithHttpInfoAsync(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Attach Private Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        [Obsolete]
        System.Threading.Tasks.Task AttachInstanceNetworkAsync(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach Private Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        [Obsolete]
        System.Threading.Tasks.Task<ApiResponse<Object>> AttachInstanceNetworkWithHttpInfoAsync(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Attach VPC to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task AttachInstanceVpcAsync(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach VPC to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> AttachInstanceVpcWithHttpInfoAsync(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Attach VPC 2.0 Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task AttachInstanceVpc2Async(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Attach VPC 2.0 Network to Instance
        /// </summary>
        /// <remarks>
        /// Attach a VPC 2.0 Network to an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> AttachInstanceVpc2WithHttpInfoAsync(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Instance
        /// </summary>
        /// <remarks>
        /// Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        System.Threading.Tasks.Task<CreateInstance202Response> CreateInstanceAsync(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Instance
        /// </summary>
        /// <remarks>
        /// Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateInstance202Response>> CreateInstanceWithHttpInfoAsync(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Set Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task CreateInstanceBackupScheduleAsync(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Set Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateInstanceBackupScheduleWithHttpInfoAsync(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create IPv4
        /// </summary>
        /// <remarks>
        /// Create an IPv4 address for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        System.Threading.Tasks.Task<Object> CreateInstanceIpv4Async(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create IPv4
        /// </summary>
        /// <remarks>
        /// Create an IPv4 address for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateInstanceIpv4WithHttpInfoAsync(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Instance Reverse IPv4
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task CreateInstanceReverseIpv4Async(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Instance Reverse IPv4
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateInstanceReverseIpv4WithHttpInfoAsync(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task CreateInstanceReverseIpv6Async(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> CreateInstanceReverseIpv6WithHttpInfoAsync(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Instance
        /// </summary>
        /// <remarks>
        /// Delete an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Instance
        /// </summary>
        /// <remarks>
        /// Delete an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete IPv4 Address
        /// </summary>
        /// <remarks>
        /// Delete an IPv4 address from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteInstanceIpv4Async(string instanceId, string ipv4, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete IPv4 Address
        /// </summary>
        /// <remarks>
        /// Delete an IPv4 address from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteInstanceIpv4WithHttpInfoAsync(string instanceId, string ipv4, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Delete the reverse IPv6 for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteInstanceReverseIpv6Async(string instanceId, string ipv6, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Instance Reverse IPv6
        /// </summary>
        /// <remarks>
        /// Delete the reverse IPv6 for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteInstanceReverseIpv6WithHttpInfoAsync(string instanceId, string ipv6, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach ISO from instance
        /// </summary>
        /// <remarks>
        /// Detach the ISO from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DetachInstanceIso202Response</returns>
        System.Threading.Tasks.Task<DetachInstanceIso202Response> DetachInstanceIsoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach ISO from instance
        /// </summary>
        /// <remarks>
        /// Detach the ISO from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DetachInstanceIso202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<DetachInstanceIso202Response>> DetachInstanceIsoWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach Private Network from Instance.
        /// </summary>
        /// <remarks>
        /// Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        [Obsolete]
        System.Threading.Tasks.Task DetachInstanceNetworkAsync(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach Private Network from Instance.
        /// </summary>
        /// <remarks>
        /// Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        [Obsolete]
        System.Threading.Tasks.Task<ApiResponse<Object>> DetachInstanceNetworkWithHttpInfoAsync(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach VPC from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DetachInstanceVpcAsync(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach VPC from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DetachInstanceVpcWithHttpInfoAsync(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Detach VPC 2.0 Network from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DetachInstanceVpc2Async(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Detach VPC 2.0 Network from Instance
        /// </summary>
        /// <remarks>
        /// Detach a VPC 2.0 Network from an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DetachInstanceVpc2WithHttpInfoAsync(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance
        /// </summary>
        /// <remarks>
        /// Get information about an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        System.Threading.Tasks.Task<CreateInstance202Response> GetInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance
        /// </summary>
        /// <remarks>
        /// Get information about an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateInstance202Response>> GetInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Get the backup schedule for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceBackupSchedule200Response</returns>
        System.Threading.Tasks.Task<GetInstanceBackupSchedule200Response> GetInstanceBackupScheduleAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance Backup Schedule
        /// </summary>
        /// <remarks>
        /// Get the backup schedule for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceBackupSchedule200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetInstanceBackupSchedule200Response>> GetInstanceBackupScheduleWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Instance Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBandwidthBaremetal200Response</returns>
        System.Threading.Tasks.Task<GetBandwidthBaremetal200Response> GetInstanceBandwidthAsync(string instanceId, int? dateRange = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Instance Bandwidth
        /// </summary>
        /// <remarks>
        /// Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBandwidthBaremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetBandwidthBaremetal200Response>> GetInstanceBandwidthWithHttpInfoAsync(string instanceId, int? dateRange = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Instance IPv4 Information
        /// </summary>
        /// <remarks>
        /// List the IPv4 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv4Baremetal200Response</returns>
        System.Threading.Tasks.Task<GetIpv4Baremetal200Response> GetInstanceIpv4Async(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Instance IPv4 Information
        /// </summary>
        /// <remarks>
        /// List the IPv4 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv4Baremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetIpv4Baremetal200Response>> GetInstanceIpv4WithHttpInfoAsync(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance IPv6 Information
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for an VPS Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv6Baremetal200Response</returns>
        System.Threading.Tasks.Task<GetIpv6Baremetal200Response> GetInstanceIpv6Async(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance IPv6 Information
        /// </summary>
        /// <remarks>
        /// Get the IPv6 information for an VPS Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv6Baremetal200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetIpv6Baremetal200Response>> GetInstanceIpv6WithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance ISO Status
        /// </summary>
        /// <remarks>
        /// Get the ISO status for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceIsoStatus200Response</returns>
        System.Threading.Tasks.Task<GetInstanceIsoStatus200Response> GetInstanceIsoStatusAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance ISO Status
        /// </summary>
        /// <remarks>
        /// Get the ISO status for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceIsoStatus200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetInstanceIsoStatus200Response>> GetInstanceIsoStatusWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance neighbors
        /// </summary>
        /// <remarks>
        /// Get a list of other instances in the same location as this Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceNeighbors200Response</returns>
        System.Threading.Tasks.Task<GetInstanceNeighbors200Response> GetInstanceNeighborsAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance neighbors
        /// </summary>
        /// <remarks>
        /// Get a list of other instances in the same location as this Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceNeighbors200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetInstanceNeighbors200Response>> GetInstanceNeighborsWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Available Instance Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for an Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceUpgrades200Response</returns>
        System.Threading.Tasks.Task<GetInstanceUpgrades200Response> GetInstanceUpgradesAsync(string instanceId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Available Instance Upgrades
        /// </summary>
        /// <remarks>
        /// Get available upgrades for an Instance
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceUpgrades200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetInstanceUpgrades200Response>> GetInstanceUpgradesWithHttpInfoAsync(string instanceId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Instance User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceUserdata200Response</returns>
        System.Threading.Tasks.Task<GetInstanceUserdata200Response> GetInstanceUserdataAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Instance User Data
        /// </summary>
        /// <remarks>
        /// Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceUserdata200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetInstanceUserdata200Response>> GetInstanceUserdataWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Halt Instance
        /// </summary>
        /// <remarks>
        /// Halt an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task HaltInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Halt Instance
        /// </summary>
        /// <remarks>
        /// Halt an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> HaltInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Halt Instances
        /// </summary>
        /// <remarks>
        /// Halt Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task HaltInstancesAsync(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Halt Instances
        /// </summary>
        /// <remarks>
        /// Halt Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> HaltInstancesWithHttpInfoAsync(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Instance IPv6 Reverse
        /// </summary>
        /// <remarks>
        /// List the reverse IPv6 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceIpv6Reverse200Response</returns>
        System.Threading.Tasks.Task<ListInstanceIpv6Reverse200Response> ListInstanceIpv6ReverseAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Instance IPv6 Reverse
        /// </summary>
        /// <remarks>
        /// List the reverse IPv6 information for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceIpv6Reverse200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListInstanceIpv6Reverse200Response>> ListInstanceIpv6ReverseWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List instance Private Networks
        /// </summary>
        /// <remarks>
        /// **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        System.Threading.Tasks.Task<ListInstancePrivateNetworks200Response> ListInstancePrivateNetworksAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List instance Private Networks
        /// </summary>
        /// <remarks>
        /// **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstancePrivateNetworks200Response)</returns>
        [Obsolete]
        System.Threading.Tasks.Task<ApiResponse<ListInstancePrivateNetworks200Response>> ListInstancePrivateNetworksWithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceVpc2200Response</returns>
        System.Threading.Tasks.Task<ListInstanceVpc2200Response> ListInstanceVpc2Async(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Instance VPC 2.0 Networks
        /// </summary>
        /// <remarks>
        /// List the VPC 2.0 networks for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceVpc2200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListInstanceVpc2200Response>> ListInstanceVpc2WithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List instance VPCs
        /// </summary>
        /// <remarks>
        /// List the VPCs for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceVpcs200Response</returns>
        System.Threading.Tasks.Task<ListInstanceVpcs200Response> ListInstanceVpcsAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List instance VPCs
        /// </summary>
        /// <remarks>
        /// List the VPCs for an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceVpcs200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListInstanceVpcs200Response>> ListInstanceVpcsWithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Instances
        /// </summary>
        /// <remarks>
        /// List all VPS instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstances200Response</returns>
        System.Threading.Tasks.Task<ListInstances200Response> ListInstancesAsync(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Instances
        /// </summary>
        /// <remarks>
        /// List all VPS instances in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstances200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListInstances200Response>> ListInstancesWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Set Default Reverse DNS Entry
        /// </summary>
        /// <remarks>
        /// Set a reverse DNS entry for an IPv4 address
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task PostInstancesInstanceIdIpv4ReverseDefaultAsync(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Set Default Reverse DNS Entry
        /// </summary>
        /// <remarks>
        /// Set a reverse DNS entry for an IPv4 address
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfoAsync(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reboot Instance
        /// </summary>
        /// <remarks>
        /// Reboot an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RebootInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reboot Instance
        /// </summary>
        /// <remarks>
        /// Reboot an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RebootInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reboot instances
        /// </summary>
        /// <remarks>
        /// Reboot Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task RebootInstancesAsync(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reboot instances
        /// </summary>
        /// <remarks>
        /// Reboot Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> RebootInstancesWithHttpInfoAsync(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Reinstall Instance
        /// </summary>
        /// <remarks>
        /// Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        System.Threading.Tasks.Task<CreateInstance202Response> ReinstallInstanceAsync(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Reinstall Instance
        /// </summary>
        /// <remarks>
        /// Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateInstance202Response>> ReinstallInstanceWithHttpInfoAsync(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Restore Instance
        /// </summary>
        /// <remarks>
        /// Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RestoreInstance202Response</returns>
        System.Threading.Tasks.Task<RestoreInstance202Response> RestoreInstanceAsync(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Restore Instance
        /// </summary>
        /// <remarks>
        /// Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RestoreInstance202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<RestoreInstance202Response>> RestoreInstanceWithHttpInfoAsync(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start instance
        /// </summary>
        /// <remarks>
        /// Start an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task StartInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start instance
        /// </summary>
        /// <remarks>
        /// Start an Instance.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> StartInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Start instances
        /// </summary>
        /// <remarks>
        /// Start Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task StartInstancesAsync(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Start instances
        /// </summary>
        /// <remarks>
        /// Start Instances.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> StartInstancesWithHttpInfoAsync(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Instance
        /// </summary>
        /// <remarks>
        /// Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        System.Threading.Tasks.Task<CreateInstance202Response> UpdateInstanceAsync(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Instance
        /// </summary>
        /// <remarks>
        /// Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateInstance202Response>> UpdateInstanceWithHttpInfoAsync(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IInstancesApi : IInstancesApiSync, IInstancesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class InstancesApi : IInstancesApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstancesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public InstancesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstancesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public InstancesApi(string basePath)
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
        /// Initializes a new instance of the <see cref="InstancesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public InstancesApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="InstancesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public InstancesApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Attach ISO to Instance Attach an ISO to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>AttachInstanceIso202Response</returns>
        public AttachInstanceIso202Response AttachInstanceIso(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<AttachInstanceIso202Response> localVarResponse = AttachInstanceIsoWithHttpInfo(instanceId, attachInstanceIsoRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Attach ISO to Instance Attach an ISO to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of AttachInstanceIso202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<AttachInstanceIso202Response> AttachInstanceIsoWithHttpInfo(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceIso");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceIsoRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceIso";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<AttachInstanceIso202Response>("/instances/{instance-id}/iso/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceIso", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach ISO to Instance Attach an ISO to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of AttachInstanceIso202Response</returns>
        public async System.Threading.Tasks.Task<AttachInstanceIso202Response> AttachInstanceIsoAsync(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<AttachInstanceIso202Response> localVarResponse = await AttachInstanceIsoWithHttpInfoAsync(instanceId, attachInstanceIsoRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Attach ISO to Instance Attach an ISO to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId"></param>
        /// <param name="attachInstanceIsoRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (AttachInstanceIso202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<AttachInstanceIso202Response>> AttachInstanceIsoWithHttpInfoAsync(string instanceId, AttachInstanceIsoRequest? attachInstanceIsoRequest = default(AttachInstanceIsoRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceIso");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceIsoRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceIso";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<AttachInstanceIso202Response>("/instances/{instance-id}/iso/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceIso", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach Private Network to Instance Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        [Obsolete]
        public void AttachInstanceNetwork(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0)
        {
            AttachInstanceNetworkWithHttpInfo(instanceId, attachInstanceNetworkRequest);
        }

        /// <summary>
        /// Attach Private Network to Instance Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        [Obsolete]
        public Org.OpenAPITools.Client.ApiResponse<Object> AttachInstanceNetworkWithHttpInfo(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceNetwork");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceNetworkRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceNetwork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/private-networks/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceNetwork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach Private Network to Instance Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task AttachInstanceNetworkAsync(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await AttachInstanceNetworkWithHttpInfoAsync(instanceId, attachInstanceNetworkRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Attach Private Network to Instance Attach Private Network to an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Attach VPC to Instance](#operation/attach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> AttachInstanceNetworkWithHttpInfoAsync(string instanceId, AttachInstanceNetworkRequest? attachInstanceNetworkRequest = default(AttachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceNetwork");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceNetworkRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceNetwork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/private-networks/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceNetwork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach VPC to Instance Attach a VPC to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void AttachInstanceVpc(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0)
        {
            AttachInstanceVpcWithHttpInfo(instanceId, attachInstanceVpcRequest);
        }

        /// <summary>
        /// Attach VPC to Instance Attach a VPC to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> AttachInstanceVpcWithHttpInfo(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceVpc");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceVpcRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceVpc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/vpcs/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceVpc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach VPC to Instance Attach a VPC to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AttachInstanceVpcAsync(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await AttachInstanceVpcWithHttpInfoAsync(instanceId, attachInstanceVpcRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Attach VPC to Instance Attach a VPC to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> AttachInstanceVpcWithHttpInfoAsync(string instanceId, AttachInstanceVpcRequest? attachInstanceVpcRequest = default(AttachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceVpc");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceVpcRequest;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceVpc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/vpcs/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceVpc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Instance Attach a VPC 2.0 Network to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void AttachInstanceVpc2(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0)
        {
            AttachInstanceVpc2WithHttpInfo(instanceId, attachInstanceVpc2Request);
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Instance Attach a VPC 2.0 Network to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> AttachInstanceVpc2WithHttpInfo(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceVpc2Request;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/vpc2/attach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Instance Attach a VPC 2.0 Network to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task AttachInstanceVpc2Async(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await AttachInstanceVpc2WithHttpInfoAsync(instanceId, attachInstanceVpc2Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Attach VPC 2.0 Network to Instance Attach a VPC 2.0 Network to an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="attachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> AttachInstanceVpc2WithHttpInfoAsync(string instanceId, AttachInstanceVpc2Request? attachInstanceVpc2Request = default(AttachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->AttachInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = attachInstanceVpc2Request;

            localVarRequestOptions.Operation = "InstancesApi.AttachInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/vpc2/attach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("AttachInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        public CreateInstance202Response CreateInstance(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = CreateInstanceWithHttpInfo(createInstanceRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Instance Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> CreateInstanceWithHttpInfo(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateInstance202Response>("/instances", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        public async System.Threading.Tasks.Task<CreateInstance202Response> CreateInstanceAsync(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = await CreateInstanceWithHttpInfoAsync(createInstanceRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Instance Create a new VPS Instance in a &#x60;region&#x60; with the desired &#x60;plan&#x60;. Choose one of the following to deploy the instance:  * &#x60;os_id&#x60; * &#x60;iso_id&#x60; * &#x60;snapshot_id&#x60; * &#x60;app_id&#x60; * &#x60;image_id&#x60;  Supply other attributes as desired.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response>> CreateInstanceWithHttpInfoAsync(CreateInstanceRequest? createInstanceRequest = default(CreateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateInstance202Response>("/instances", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Instance Backup Schedule Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void CreateInstanceBackupSchedule(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0)
        {
            CreateInstanceBackupScheduleWithHttpInfo(instanceId, createInstanceBackupScheduleRequest);
        }

        /// <summary>
        /// Set Instance Backup Schedule Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> CreateInstanceBackupScheduleWithHttpInfo(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceBackupSchedule");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceBackupScheduleRequest;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceBackupSchedule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/backup-schedule", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceBackupSchedule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Instance Backup Schedule Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task CreateInstanceBackupScheduleAsync(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await CreateInstanceBackupScheduleWithHttpInfoAsync(instanceId, createInstanceBackupScheduleRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set Instance Backup Schedule Set the backup schedule for an Instance in UTC. The &#x60;type&#x60; is required.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceBackupScheduleRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> CreateInstanceBackupScheduleWithHttpInfoAsync(string instanceId, CreateInstanceBackupScheduleRequest? createInstanceBackupScheduleRequest = default(CreateInstanceBackupScheduleRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceBackupSchedule");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceBackupScheduleRequest;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceBackupSchedule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/backup-schedule", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceBackupSchedule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create IPv4 Create an IPv4 address for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Object</returns>
        public Object CreateInstanceIpv4(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<Object> localVarResponse = CreateInstanceIpv4WithHttpInfo(instanceId, createInstanceIpv4Request);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create IPv4 Create an IPv4 address for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> CreateInstanceIpv4WithHttpInfo(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceIpv4Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/ipv4", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create IPv4 Create an IPv4 address for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Object</returns>
        public async System.Threading.Tasks.Task<Object> CreateInstanceIpv4Async(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<Object> localVarResponse = await CreateInstanceIpv4WithHttpInfoAsync(instanceId, createInstanceIpv4Request, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create IPv4 Create an IPv4 address for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Object)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> CreateInstanceIpv4WithHttpInfoAsync(string instanceId, CreateInstanceIpv4Request? createInstanceIpv4Request = default(CreateInstanceIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceIpv4Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/ipv4", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Reverse IPv4 Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void CreateInstanceReverseIpv4(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0)
        {
            CreateInstanceReverseIpv4WithHttpInfo(instanceId, createInstanceReverseIpv4Request);
        }

        /// <summary>
        /// Create Instance Reverse IPv4 Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> CreateInstanceReverseIpv4WithHttpInfo(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceReverseIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceReverseIpv4Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceReverseIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/ipv4/reverse", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceReverseIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Reverse IPv4 Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task CreateInstanceReverseIpv4Async(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await CreateInstanceReverseIpv4WithHttpInfoAsync(instanceId, createInstanceReverseIpv4Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create Instance Reverse IPv4 Create a reverse IPv4 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv4Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> CreateInstanceReverseIpv4WithHttpInfoAsync(string instanceId, CreateInstanceReverseIpv4Request? createInstanceReverseIpv4Request = default(CreateInstanceReverseIpv4Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceReverseIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceReverseIpv4Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceReverseIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/ipv4/reverse", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceReverseIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Reverse IPv6 Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void CreateInstanceReverseIpv6(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0)
        {
            CreateInstanceReverseIpv6WithHttpInfo(instanceId, createInstanceReverseIpv6Request);
        }

        /// <summary>
        /// Create Instance Reverse IPv6 Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> CreateInstanceReverseIpv6WithHttpInfo(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceReverseIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceReverseIpv6Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceReverseIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/ipv6/reverse", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceReverseIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Instance Reverse IPv6 Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task CreateInstanceReverseIpv6Async(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await CreateInstanceReverseIpv6WithHttpInfoAsync(instanceId, createInstanceReverseIpv6Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create Instance Reverse IPv6 Create a reverse IPv6 entry for an Instance. The &#x60;ip&#x60; and &#x60;reverse&#x60; attributes are required. IP address must be in full, expanded format.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="createInstanceReverseIpv6Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> CreateInstanceReverseIpv6WithHttpInfoAsync(string instanceId, CreateInstanceReverseIpv6Request? createInstanceReverseIpv6Request = default(CreateInstanceReverseIpv6Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->CreateInstanceReverseIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = createInstanceReverseIpv6Request;

            localVarRequestOptions.Operation = "InstancesApi.CreateInstanceReverseIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/ipv6/reverse", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateInstanceReverseIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Instance Delete an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteInstance(string instanceId, int operationIndex = 0)
        {
            DeleteInstanceWithHttpInfo(instanceId);
        }

        /// <summary>
        /// Delete Instance Delete an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteInstanceWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/instances/{instance-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Instance Delete an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteInstanceWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Instance Delete an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/instances/{instance-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete IPv4 Address Delete an IPv4 address from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteInstanceIpv4(string instanceId, string ipv4, int operationIndex = 0)
        {
            DeleteInstanceIpv4WithHttpInfo(instanceId, ipv4);
        }

        /// <summary>
        /// Delete IPv4 Address Delete an IPv4 address from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteInstanceIpv4WithHttpInfo(string instanceId, string ipv4, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstanceIpv4");
            }

            // verify the required parameter 'ipv4' is set
            if (ipv4 == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'ipv4' when calling InstancesApi->DeleteInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("ipv4", Org.OpenAPITools.Client.ClientUtils.ParameterToString(ipv4)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/instances/{instance-id}/ipv4/{ipv4}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete IPv4 Address Delete an IPv4 address from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteInstanceIpv4Async(string instanceId, string ipv4, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteInstanceIpv4WithHttpInfoAsync(instanceId, ipv4, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete IPv4 Address Delete an IPv4 address from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv4">The IPv4 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteInstanceIpv4WithHttpInfoAsync(string instanceId, string ipv4, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstanceIpv4");
            }

            // verify the required parameter 'ipv4' is set
            if (ipv4 == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'ipv4' when calling InstancesApi->DeleteInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("ipv4", Org.OpenAPITools.Client.ClientUtils.ParameterToString(ipv4)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/instances/{instance-id}/ipv4/{ipv4}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Instance Reverse IPv6 Delete the reverse IPv6 for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteInstanceReverseIpv6(string instanceId, string ipv6, int operationIndex = 0)
        {
            DeleteInstanceReverseIpv6WithHttpInfo(instanceId, ipv6);
        }

        /// <summary>
        /// Delete Instance Reverse IPv6 Delete the reverse IPv6 for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteInstanceReverseIpv6WithHttpInfo(string instanceId, string ipv6, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstanceReverseIpv6");
            }

            // verify the required parameter 'ipv6' is set
            if (ipv6 == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'ipv6' when calling InstancesApi->DeleteInstanceReverseIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("ipv6", Org.OpenAPITools.Client.ClientUtils.ParameterToString(ipv6)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstanceReverseIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/instances/{instance-id}/ipv6/reverse/{ipv6}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstanceReverseIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Instance Reverse IPv6 Delete the reverse IPv6 for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteInstanceReverseIpv6Async(string instanceId, string ipv6, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteInstanceReverseIpv6WithHttpInfoAsync(instanceId, ipv6, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Instance Reverse IPv6 Delete the reverse IPv6 for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="ipv6">The IPv6 address.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteInstanceReverseIpv6WithHttpInfoAsync(string instanceId, string ipv6, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DeleteInstanceReverseIpv6");
            }

            // verify the required parameter 'ipv6' is set
            if (ipv6 == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'ipv6' when calling InstancesApi->DeleteInstanceReverseIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.PathParameters.Add("ipv6", Org.OpenAPITools.Client.ClientUtils.ParameterToString(ipv6)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DeleteInstanceReverseIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/instances/{instance-id}/ipv6/reverse/{ipv6}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteInstanceReverseIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach ISO from instance Detach the ISO from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>DetachInstanceIso202Response</returns>
        public DetachInstanceIso202Response DetachInstanceIso(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<DetachInstanceIso202Response> localVarResponse = DetachInstanceIsoWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Detach ISO from instance Detach the ISO from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of DetachInstanceIso202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<DetachInstanceIso202Response> DetachInstanceIsoWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceIso");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceIso";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<DetachInstanceIso202Response>("/instances/{instance-id}/iso/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceIso", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach ISO from instance Detach the ISO from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of DetachInstanceIso202Response</returns>
        public async System.Threading.Tasks.Task<DetachInstanceIso202Response> DetachInstanceIsoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<DetachInstanceIso202Response> localVarResponse = await DetachInstanceIsoWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Detach ISO from instance Detach the ISO from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (DetachInstanceIso202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<DetachInstanceIso202Response>> DetachInstanceIsoWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceIso");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceIso";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<DetachInstanceIso202Response>("/instances/{instance-id}/iso/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceIso", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Private Network from Instance. Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        [Obsolete]
        public void DetachInstanceNetwork(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0)
        {
            DetachInstanceNetworkWithHttpInfo(instanceId, detachInstanceNetworkRequest);
        }

        /// <summary>
        /// Detach Private Network from Instance. Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        [Obsolete]
        public Org.OpenAPITools.Client.ApiResponse<Object> DetachInstanceNetworkWithHttpInfo(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceNetwork");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceNetworkRequest;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceNetwork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/private-networks/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceNetwork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach Private Network from Instance. Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task DetachInstanceNetworkAsync(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DetachInstanceNetworkWithHttpInfoAsync(instanceId, detachInstanceNetworkRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach Private Network from Instance. Detach Private Network from an Instance.&lt;br&gt;&lt;br&gt;**Deprecated**: use [Detach VPC from Instance](#operation/detach-instance-vpc) instead.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceNetworkRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DetachInstanceNetworkWithHttpInfoAsync(string instanceId, DetachInstanceNetworkRequest? detachInstanceNetworkRequest = default(DetachInstanceNetworkRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceNetwork");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceNetworkRequest;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceNetwork";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/private-networks/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceNetwork", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC from Instance Detach a VPC from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DetachInstanceVpc(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0)
        {
            DetachInstanceVpcWithHttpInfo(instanceId, detachInstanceVpcRequest);
        }

        /// <summary>
        /// Detach VPC from Instance Detach a VPC from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DetachInstanceVpcWithHttpInfo(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceVpc");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceVpcRequest;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceVpc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/vpcs/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceVpc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC from Instance Detach a VPC from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DetachInstanceVpcAsync(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DetachInstanceVpcWithHttpInfoAsync(instanceId, detachInstanceVpcRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach VPC from Instance Detach a VPC from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpcRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DetachInstanceVpcWithHttpInfoAsync(string instanceId, DetachInstanceVpcRequest? detachInstanceVpcRequest = default(DetachInstanceVpcRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceVpc");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceVpcRequest;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceVpc";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/vpcs/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceVpc", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Instance Detach a VPC 2.0 Network from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DetachInstanceVpc2(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0)
        {
            DetachInstanceVpc2WithHttpInfo(instanceId, detachInstanceVpc2Request);
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Instance Detach a VPC 2.0 Network from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DetachInstanceVpc2WithHttpInfo(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceVpc2Request;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/vpc2/detach", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Instance Detach a VPC 2.0 Network from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DetachInstanceVpc2Async(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DetachInstanceVpc2WithHttpInfoAsync(instanceId, detachInstanceVpc2Request, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Detach VPC 2.0 Network from Instance Detach a VPC 2.0 Network from an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="detachInstanceVpc2Request">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DetachInstanceVpc2WithHttpInfoAsync(string instanceId, DetachInstanceVpc2Request? detachInstanceVpc2Request = default(DetachInstanceVpc2Request?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->DetachInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = detachInstanceVpc2Request;

            localVarRequestOptions.Operation = "InstancesApi.DetachInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/vpc2/detach", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DetachInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance Get information about an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        public CreateInstance202Response GetInstance(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = GetInstanceWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance Get information about an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> GetInstanceWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateInstance202Response>("/instances/{instance-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance Get information about an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        public async System.Threading.Tasks.Task<CreateInstance202Response> GetInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = await GetInstanceWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance Get information about an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response>> GetInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateInstance202Response>("/instances/{instance-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance Backup Schedule Get the backup schedule for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceBackupSchedule200Response</returns>
        public GetInstanceBackupSchedule200Response GetInstanceBackupSchedule(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceBackupSchedule200Response> localVarResponse = GetInstanceBackupScheduleWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance Backup Schedule Get the backup schedule for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceBackupSchedule200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetInstanceBackupSchedule200Response> GetInstanceBackupScheduleWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceBackupSchedule");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceBackupSchedule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetInstanceBackupSchedule200Response>("/instances/{instance-id}/backup-schedule", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceBackupSchedule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance Backup Schedule Get the backup schedule for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceBackupSchedule200Response</returns>
        public async System.Threading.Tasks.Task<GetInstanceBackupSchedule200Response> GetInstanceBackupScheduleAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceBackupSchedule200Response> localVarResponse = await GetInstanceBackupScheduleWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance Backup Schedule Get the backup schedule for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceBackupSchedule200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetInstanceBackupSchedule200Response>> GetInstanceBackupScheduleWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceBackupSchedule");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceBackupSchedule";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetInstanceBackupSchedule200Response>("/instances/{instance-id}/backup-schedule", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceBackupSchedule", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Instance Bandwidth Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetBandwidthBaremetal200Response</returns>
        public GetBandwidthBaremetal200Response GetInstanceBandwidth(string instanceId, int? dateRange = default(int?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> localVarResponse = GetInstanceBandwidthWithHttpInfo(instanceId, dateRange);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Instance Bandwidth Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetBandwidthBaremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> GetInstanceBandwidthWithHttpInfo(string instanceId, int? dateRange = default(int?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceBandwidth");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (dateRange != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "date_range", dateRange));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceBandwidth";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetBandwidthBaremetal200Response>("/instances/{instance-id}/bandwidth", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceBandwidth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Instance Bandwidth Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetBandwidthBaremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetBandwidthBaremetal200Response> GetInstanceBandwidthAsync(string instanceId, int? dateRange = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response> localVarResponse = await GetInstanceBandwidthWithHttpInfoAsync(instanceId, dateRange, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Instance Bandwidth Get bandwidth information about an Instance.&lt;br&gt;&lt;br&gt;The &#x60;bandwidth&#x60; object in a successful response contains objects representing a day in the month. The date is denoted by the nested object keys. Days begin and end in the UTC timezone. The bandwidth utilization data contained within the date object is refreshed periodically. We do not recommend using this endpoint to gather real-time metrics.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="dateRange">The range of days to include, represented as the number of days relative to the current date. Default 30, Minimum 1 and Max 60. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetBandwidthBaremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetBandwidthBaremetal200Response>> GetInstanceBandwidthWithHttpInfoAsync(string instanceId, int? dateRange = default(int?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceBandwidth");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (dateRange != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "date_range", dateRange));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceBandwidth";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetBandwidthBaremetal200Response>("/instances/{instance-id}/bandwidth", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceBandwidth", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance IPv4 Information List the IPv4 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv4Baremetal200Response</returns>
        public GetIpv4Baremetal200Response GetInstanceIpv4(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> localVarResponse = GetInstanceIpv4WithHttpInfo(instanceId, publicNetwork, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance IPv4 Information List the IPv4 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv4Baremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> GetInstanceIpv4WithHttpInfo(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (publicNetwork != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "public_network", publicNetwork));
            }
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetIpv4Baremetal200Response>("/instances/{instance-id}/ipv4", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance IPv4 Information List the IPv4 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv4Baremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetIpv4Baremetal200Response> GetInstanceIpv4Async(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response> localVarResponse = await GetInstanceIpv4WithHttpInfoAsync(instanceId, publicNetwork, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance IPv4 Information List the IPv4 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="publicNetwork">If &#x60;true&#x60;, includes information about the public network adapter (such as MAC address) with the &#x60;main_ip&#x60; entry. (optional)</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv4Baremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetIpv4Baremetal200Response>> GetInstanceIpv4WithHttpInfoAsync(string instanceId, bool? publicNetwork = default(bool?), int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIpv4");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (publicNetwork != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "public_network", publicNetwork));
            }
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIpv4";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetIpv4Baremetal200Response>("/instances/{instance-id}/ipv4", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIpv4", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance IPv6 Information Get the IPv6 information for an VPS Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetIpv6Baremetal200Response</returns>
        public GetIpv6Baremetal200Response GetInstanceIpv6(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> localVarResponse = GetInstanceIpv6WithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance IPv6 Information Get the IPv6 information for an VPS Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetIpv6Baremetal200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> GetInstanceIpv6WithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetIpv6Baremetal200Response>("/instances/{instance-id}/ipv6", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance IPv6 Information Get the IPv6 information for an VPS Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetIpv6Baremetal200Response</returns>
        public async System.Threading.Tasks.Task<GetIpv6Baremetal200Response> GetInstanceIpv6Async(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response> localVarResponse = await GetInstanceIpv6WithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance IPv6 Information Get the IPv6 information for an VPS Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetIpv6Baremetal200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetIpv6Baremetal200Response>> GetInstanceIpv6WithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIpv6");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIpv6";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetIpv6Baremetal200Response>("/instances/{instance-id}/ipv6", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIpv6", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance ISO Status Get the ISO status for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceIsoStatus200Response</returns>
        public GetInstanceIsoStatus200Response GetInstanceIsoStatus(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceIsoStatus200Response> localVarResponse = GetInstanceIsoStatusWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance ISO Status Get the ISO status for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceIsoStatus200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetInstanceIsoStatus200Response> GetInstanceIsoStatusWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIsoStatus");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIsoStatus";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetInstanceIsoStatus200Response>("/instances/{instance-id}/iso", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIsoStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance ISO Status Get the ISO status for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceIsoStatus200Response</returns>
        public async System.Threading.Tasks.Task<GetInstanceIsoStatus200Response> GetInstanceIsoStatusAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceIsoStatus200Response> localVarResponse = await GetInstanceIsoStatusWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance ISO Status Get the ISO status for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceIsoStatus200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetInstanceIsoStatus200Response>> GetInstanceIsoStatusWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceIsoStatus");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceIsoStatus";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetInstanceIsoStatus200Response>("/instances/{instance-id}/iso", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceIsoStatus", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance neighbors Get a list of other instances in the same location as this Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceNeighbors200Response</returns>
        public GetInstanceNeighbors200Response GetInstanceNeighbors(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceNeighbors200Response> localVarResponse = GetInstanceNeighborsWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance neighbors Get a list of other instances in the same location as this Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceNeighbors200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetInstanceNeighbors200Response> GetInstanceNeighborsWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceNeighbors");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceNeighbors";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetInstanceNeighbors200Response>("/instances/{instance-id}/neighbors", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceNeighbors", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance neighbors Get a list of other instances in the same location as this Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceNeighbors200Response</returns>
        public async System.Threading.Tasks.Task<GetInstanceNeighbors200Response> GetInstanceNeighborsAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceNeighbors200Response> localVarResponse = await GetInstanceNeighborsWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance neighbors Get a list of other instances in the same location as this Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceNeighbors200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetInstanceNeighbors200Response>> GetInstanceNeighborsWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceNeighbors");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceNeighbors";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetInstanceNeighbors200Response>("/instances/{instance-id}/neighbors", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceNeighbors", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Available Instance Upgrades Get available upgrades for an Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceUpgrades200Response</returns>
        public GetInstanceUpgrades200Response GetInstanceUpgrades(string instanceId, string? type = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceUpgrades200Response> localVarResponse = GetInstanceUpgradesWithHttpInfo(instanceId, type);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Available Instance Upgrades Get available upgrades for an Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceUpgrades200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetInstanceUpgrades200Response> GetInstanceUpgradesWithHttpInfo(string instanceId, string? type = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceUpgrades");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetInstanceUpgrades200Response>("/instances/{instance-id}/upgrades", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Available Instance Upgrades Get available upgrades for an Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceUpgrades200Response</returns>
        public async System.Threading.Tasks.Task<GetInstanceUpgrades200Response> GetInstanceUpgradesAsync(string instanceId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceUpgrades200Response> localVarResponse = await GetInstanceUpgradesWithHttpInfoAsync(instanceId, type, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Available Instance Upgrades Get available upgrades for an Instance
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="type">Filter upgrade by type:  - all (applications, os, plans) - applications - os - plans (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceUpgrades200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetInstanceUpgrades200Response>> GetInstanceUpgradesWithHttpInfoAsync(string instanceId, string? type = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceUpgrades");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceUpgrades";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetInstanceUpgrades200Response>("/instances/{instance-id}/upgrades", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceUpgrades", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetInstanceUserdata200Response</returns>
        public GetInstanceUserdata200Response GetInstanceUserdata(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceUserdata200Response> localVarResponse = GetInstanceUserdataWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetInstanceUserdata200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetInstanceUserdata200Response> GetInstanceUserdataWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceUserdata");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceUserdata";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetInstanceUserdata200Response>("/instances/{instance-id}/user-data", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceUserdata", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Instance User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetInstanceUserdata200Response</returns>
        public async System.Threading.Tasks.Task<GetInstanceUserdata200Response> GetInstanceUserdataAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetInstanceUserdata200Response> localVarResponse = await GetInstanceUserdataWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Instance User Data Get the user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetInstanceUserdata200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetInstanceUserdata200Response>> GetInstanceUserdataWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->GetInstanceUserdata");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.GetInstanceUserdata";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetInstanceUserdata200Response>("/instances/{instance-id}/user-data", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetInstanceUserdata", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Instance Halt an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void HaltInstance(string instanceId, int operationIndex = 0)
        {
            HaltInstanceWithHttpInfo(instanceId);
        }

        /// <summary>
        /// Halt Instance Halt an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> HaltInstanceWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->HaltInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.HaltInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/halt", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Instance Halt an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task HaltInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await HaltInstanceWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Halt Instance Halt an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> HaltInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->HaltInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.HaltInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/halt", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Instances Halt Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void HaltInstances(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0)
        {
            HaltInstancesWithHttpInfo(haltInstancesRequest);
        }

        /// <summary>
        /// Halt Instances Halt Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> HaltInstancesWithHttpInfo(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = haltInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.HaltInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/halt", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Halt Instances Halt Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task HaltInstancesAsync(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await HaltInstancesWithHttpInfoAsync(haltInstancesRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Halt Instances Halt Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="haltInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> HaltInstancesWithHttpInfoAsync(HaltInstancesRequest? haltInstancesRequest = default(HaltInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = haltInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.HaltInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/halt", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("HaltInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance IPv6 Reverse List the reverse IPv6 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceIpv6Reverse200Response</returns>
        public ListInstanceIpv6Reverse200Response ListInstanceIpv6Reverse(string instanceId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceIpv6Reverse200Response> localVarResponse = ListInstanceIpv6ReverseWithHttpInfo(instanceId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance IPv6 Reverse List the reverse IPv6 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceIpv6Reverse200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListInstanceIpv6Reverse200Response> ListInstanceIpv6ReverseWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceIpv6Reverse");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceIpv6Reverse";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListInstanceIpv6Reverse200Response>("/instances/{instance-id}/ipv6/reverse", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceIpv6Reverse", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance IPv6 Reverse List the reverse IPv6 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceIpv6Reverse200Response</returns>
        public async System.Threading.Tasks.Task<ListInstanceIpv6Reverse200Response> ListInstanceIpv6ReverseAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceIpv6Reverse200Response> localVarResponse = await ListInstanceIpv6ReverseWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance IPv6 Reverse List the reverse IPv6 information for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceIpv6Reverse200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListInstanceIpv6Reverse200Response>> ListInstanceIpv6ReverseWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceIpv6Reverse");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceIpv6Reverse";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListInstanceIpv6Reverse200Response>("/instances/{instance-id}/ipv6/reverse", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceIpv6Reverse", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List instance Private Networks **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        public ListInstancePrivateNetworks200Response ListInstancePrivateNetworks(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstancePrivateNetworks200Response> localVarResponse = ListInstancePrivateNetworksWithHttpInfo(instanceId, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List instance Private Networks **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        public Org.OpenAPITools.Client.ApiResponse<ListInstancePrivateNetworks200Response> ListInstancePrivateNetworksWithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstancePrivateNetworks");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstancePrivateNetworks";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListInstancePrivateNetworks200Response>("/instances/{instance-id}/private-networks", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstancePrivateNetworks", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List instance Private Networks **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstancePrivateNetworks200Response</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task<ListInstancePrivateNetworks200Response> ListInstancePrivateNetworksAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstancePrivateNetworks200Response> localVarResponse = await ListInstancePrivateNetworksWithHttpInfoAsync(instanceId, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List instance Private Networks **Deprecated**: use [List Instance VPCs](#operation/list-instance-vpcs) instead.&lt;br&gt;&lt;br&gt;List the private networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstancePrivateNetworks200Response)</returns>
        [Obsolete]
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListInstancePrivateNetworks200Response>> ListInstancePrivateNetworksWithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstancePrivateNetworks");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstancePrivateNetworks";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListInstancePrivateNetworks200Response>("/instances/{instance-id}/private-networks", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstancePrivateNetworks", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance VPC 2.0 Networks List the VPC 2.0 networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceVpc2200Response</returns>
        public ListInstanceVpc2200Response ListInstanceVpc2(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceVpc2200Response> localVarResponse = ListInstanceVpc2WithHttpInfo(instanceId, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance VPC 2.0 Networks List the VPC 2.0 networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceVpc2200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListInstanceVpc2200Response> ListInstanceVpc2WithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListInstanceVpc2200Response>("/instances/{instance-id}/vpc2", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instance VPC 2.0 Networks List the VPC 2.0 networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceVpc2200Response</returns>
        public async System.Threading.Tasks.Task<ListInstanceVpc2200Response> ListInstanceVpc2Async(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceVpc2200Response> localVarResponse = await ListInstanceVpc2WithHttpInfoAsync(instanceId, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instance VPC 2.0 Networks List the VPC 2.0 networks for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceVpc2200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListInstanceVpc2200Response>> ListInstanceVpc2WithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceVpc2");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceVpc2";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListInstanceVpc2200Response>("/instances/{instance-id}/vpc2", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceVpc2", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List instance VPCs List the VPCs for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstanceVpcs200Response</returns>
        public ListInstanceVpcs200Response ListInstanceVpcs(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceVpcs200Response> localVarResponse = ListInstanceVpcsWithHttpInfo(instanceId, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List instance VPCs List the VPCs for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstanceVpcs200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListInstanceVpcs200Response> ListInstanceVpcsWithHttpInfo(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceVpcs");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceVpcs";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListInstanceVpcs200Response>("/instances/{instance-id}/vpcs", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceVpcs", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List instance VPCs List the VPCs for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstanceVpcs200Response</returns>
        public async System.Threading.Tasks.Task<ListInstanceVpcs200Response> ListInstanceVpcsAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstanceVpcs200Response> localVarResponse = await ListInstanceVpcsWithHttpInfoAsync(instanceId, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List instance VPCs List the VPCs for an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstanceVpcs200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListInstanceVpcs200Response>> ListInstanceVpcsWithHttpInfoAsync(string instanceId, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ListInstanceVpcs");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstanceVpcs";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListInstanceVpcs200Response>("/instances/{instance-id}/vpcs", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstanceVpcs", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instances List all VPS instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListInstances200Response</returns>
        public ListInstances200Response ListInstances(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstances200Response> localVarResponse = ListInstancesWithHttpInfo(perPage, cursor, tag, label, mainIp, region, firewallGroupId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instances List all VPS instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListInstances200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListInstances200Response> ListInstancesWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0)
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
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (label != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "label", label));
            }
            if (mainIp != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "main_ip", mainIp));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }
            if (firewallGroupId != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "firewall_group_id", firewallGroupId));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListInstances200Response>("/instances", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Instances List all VPS instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListInstances200Response</returns>
        public async System.Threading.Tasks.Task<ListInstances200Response> ListInstancesAsync(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListInstances200Response> localVarResponse = await ListInstancesWithHttpInfoAsync(perPage, cursor, tag, label, mainIp, region, firewallGroupId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Instances List all VPS instances in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="tag">Filter by specific tag. (optional) (deprecated)</param>
        /// <param name="label">Filter by label. (optional)</param>
        /// <param name="mainIp">Filter by main ip address. (optional)</param>
        /// <param name="region">Filter by [Region id](#operation/list-regions). (optional)</param>
        /// <param name="firewallGroupId">Filter by [Firewall group id](#operation/list-firewall-groups). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListInstances200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListInstances200Response>> ListInstancesWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), string? tag = default(string?), string? label = default(string?), string? mainIp = default(string?), string? region = default(string?), string? firewallGroupId = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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
            if (tag != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "tag", tag));
            }
            if (label != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "label", label));
            }
            if (mainIp != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "main_ip", mainIp));
            }
            if (region != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "region", region));
            }
            if (firewallGroupId != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "firewall_group_id", firewallGroupId));
            }

            localVarRequestOptions.Operation = "InstancesApi.ListInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListInstances200Response>("/instances", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Default Reverse DNS Entry Set a reverse DNS entry for an IPv4 address
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void PostInstancesInstanceIdIpv4ReverseDefault(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0)
        {
            PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo(instanceId, postInstancesInstanceIdIpv4ReverseDefaultRequest);
        }

        /// <summary>
        /// Set Default Reverse DNS Entry Set a reverse DNS entry for an IPv4 address
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfo(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->PostInstancesInstanceIdIpv4ReverseDefault");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = postInstancesInstanceIdIpv4ReverseDefaultRequest;

            localVarRequestOptions.Operation = "InstancesApi.PostInstancesInstanceIdIpv4ReverseDefault";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/ipv4/reverse/default", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostInstancesInstanceIdIpv4ReverseDefault", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Set Default Reverse DNS Entry Set a reverse DNS entry for an IPv4 address
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task PostInstancesInstanceIdIpv4ReverseDefaultAsync(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfoAsync(instanceId, postInstancesInstanceIdIpv4ReverseDefaultRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Set Default Reverse DNS Entry Set a reverse DNS entry for an IPv4 address
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="postInstancesInstanceIdIpv4ReverseDefaultRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> PostInstancesInstanceIdIpv4ReverseDefaultWithHttpInfoAsync(string instanceId, PostInstancesInstanceIdIpv4ReverseDefaultRequest? postInstancesInstanceIdIpv4ReverseDefaultRequest = default(PostInstancesInstanceIdIpv4ReverseDefaultRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->PostInstancesInstanceIdIpv4ReverseDefault");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = postInstancesInstanceIdIpv4ReverseDefaultRequest;

            localVarRequestOptions.Operation = "InstancesApi.PostInstancesInstanceIdIpv4ReverseDefault";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/ipv4/reverse/default", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PostInstancesInstanceIdIpv4ReverseDefault", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Instance Reboot an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RebootInstance(string instanceId, int operationIndex = 0)
        {
            RebootInstanceWithHttpInfo(instanceId);
        }

        /// <summary>
        /// Reboot Instance Reboot an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> RebootInstanceWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->RebootInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.RebootInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/reboot", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot Instance Reboot an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RebootInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await RebootInstanceWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reboot Instance Reboot an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> RebootInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->RebootInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.RebootInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/reboot", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot instances Reboot Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void RebootInstances(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0)
        {
            RebootInstancesWithHttpInfo(rebootInstancesRequest);
        }

        /// <summary>
        /// Reboot instances Reboot Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> RebootInstancesWithHttpInfo(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = rebootInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.RebootInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/reboot", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reboot instances Reboot Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task RebootInstancesAsync(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await RebootInstancesWithHttpInfoAsync(rebootInstancesRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reboot instances Reboot Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="rebootInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> RebootInstancesWithHttpInfoAsync(RebootInstancesRequest? rebootInstancesRequest = default(RebootInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = rebootInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.RebootInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/reboot", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RebootInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reinstall Instance Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        public CreateInstance202Response ReinstallInstance(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = ReinstallInstanceWithHttpInfo(instanceId, reinstallInstanceRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Reinstall Instance Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> ReinstallInstanceWithHttpInfo(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ReinstallInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = reinstallInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.ReinstallInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateInstance202Response>("/instances/{instance-id}/reinstall", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReinstallInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Reinstall Instance Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        public async System.Threading.Tasks.Task<CreateInstance202Response> ReinstallInstanceAsync(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = await ReinstallInstanceWithHttpInfoAsync(instanceId, reinstallInstanceRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Reinstall Instance Reinstall an Instance using an optional &#x60;hostname&#x60;.  **Note:** This action may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="reinstallInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response>> ReinstallInstanceWithHttpInfoAsync(string instanceId, ReinstallInstanceRequest? reinstallInstanceRequest = default(ReinstallInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->ReinstallInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = reinstallInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.ReinstallInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateInstance202Response>("/instances/{instance-id}/reinstall", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ReinstallInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Restore Instance Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>RestoreInstance202Response</returns>
        public RestoreInstance202Response RestoreInstance(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<RestoreInstance202Response> localVarResponse = RestoreInstanceWithHttpInfo(instanceId, restoreInstanceRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Restore Instance Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of RestoreInstance202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<RestoreInstance202Response> RestoreInstanceWithHttpInfo(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->RestoreInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = restoreInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.RestoreInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<RestoreInstance202Response>("/instances/{instance-id}/restore", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RestoreInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Restore Instance Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of RestoreInstance202Response</returns>
        public async System.Threading.Tasks.Task<RestoreInstance202Response> RestoreInstanceAsync(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<RestoreInstance202Response> localVarResponse = await RestoreInstanceWithHttpInfoAsync(instanceId, restoreInstanceRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Restore Instance Restore an Instance from either &#x60;backup_id&#x60; or &#x60;snapshot_id&#x60;.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="restoreInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (RestoreInstance202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<RestoreInstance202Response>> RestoreInstanceWithHttpInfoAsync(string instanceId, RestoreInstanceRequest? restoreInstanceRequest = default(RestoreInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->RestoreInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = restoreInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.RestoreInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<RestoreInstance202Response>("/instances/{instance-id}/restore", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RestoreInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start instance Start an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void StartInstance(string instanceId, int operationIndex = 0)
        {
            StartInstanceWithHttpInfo(instanceId);
        }

        /// <summary>
        /// Start instance Start an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> StartInstanceWithHttpInfo(string instanceId, int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->StartInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.StartInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/{instance-id}/start", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start instance Start an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task StartInstanceAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await StartInstanceWithHttpInfoAsync(instanceId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start instance Start an Instance.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> StartInstanceWithHttpInfoAsync(string instanceId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->StartInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter

            localVarRequestOptions.Operation = "InstancesApi.StartInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/{instance-id}/start", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start instances Start Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void StartInstances(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0)
        {
            StartInstancesWithHttpInfo(startInstancesRequest);
        }

        /// <summary>
        /// Start instances Start Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> StartInstancesWithHttpInfo(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = startInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.StartInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>("/instances/start", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Start instances Start Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task StartInstancesAsync(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await StartInstancesWithHttpInfoAsync(startInstancesRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Start instances Start Instances.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="startInstancesRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> StartInstancesWithHttpInfoAsync(StartInstancesRequest? startInstancesRequest = default(StartInstancesRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = startInstancesRequest;

            localVarRequestOptions.Operation = "InstancesApi.StartInstances";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>("/instances/start", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("StartInstances", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Instance Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateInstance202Response</returns>
        public CreateInstance202Response UpdateInstance(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = UpdateInstanceWithHttpInfo(instanceId, updateInstanceRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Instance Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateInstance202Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> UpdateInstanceWithHttpInfo(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->UpdateInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = updateInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.UpdateInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<CreateInstance202Response>("/instances/{instance-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Instance Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateInstance202Response</returns>
        public async System.Threading.Tasks.Task<CreateInstance202Response> UpdateInstanceAsync(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response> localVarResponse = await UpdateInstanceWithHttpInfoAsync(instanceId, updateInstanceRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Update Instance Update information for an Instance. All attributes are optional. If not set, the attributes will retain their original values.  **Note:** Changing &#x60;os_id&#x60;, &#x60;app_id&#x60; or &#x60;image_id&#x60; may take a few extra seconds to complete.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="instanceId">The [Instance ID](#operation/list-instances).</param>
        /// <param name="updateInstanceRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateInstance202Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateInstance202Response>> UpdateInstanceWithHttpInfoAsync(string instanceId, UpdateInstanceRequest? updateInstanceRequest = default(UpdateInstanceRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'instanceId' is set
            if (instanceId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'instanceId' when calling InstancesApi->UpdateInstance");
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

            localVarRequestOptions.PathParameters.Add("instance-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(instanceId)); // path parameter
            localVarRequestOptions.Data = updateInstanceRequest;

            localVarRequestOptions.Operation = "InstancesApi.UpdateInstance";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<CreateInstance202Response>("/instances/{instance-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateInstance", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
