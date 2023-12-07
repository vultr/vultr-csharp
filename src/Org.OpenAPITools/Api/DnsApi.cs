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
    public interface IDnsApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Create DNS Domain
        /// </summary>
        /// <remarks>
        /// Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomain200Response</returns>
        CreateDnsDomain200Response CreateDnsDomain(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0);

        /// <summary>
        /// Create DNS Domain
        /// </summary>
        /// <remarks>
        /// Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomain200Response</returns>
        ApiResponse<CreateDnsDomain200Response> CreateDnsDomainWithHttpInfo(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0);
        /// <summary>
        /// Create Record
        /// </summary>
        /// <remarks>
        /// Create a DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomainRecord201Response</returns>
        CreateDnsDomainRecord201Response CreateDnsDomainRecord(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0);

        /// <summary>
        /// Create Record
        /// </summary>
        /// <remarks>
        /// Create a DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomainRecord201Response</returns>
        ApiResponse<CreateDnsDomainRecord201Response> CreateDnsDomainRecordWithHttpInfo(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0);
        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <remarks>
        /// Delete the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteDnsDomain(string dnsDomain, int operationIndex = 0);

        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <remarks>
        /// Delete the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteDnsDomainWithHttpInfo(string dnsDomain, int operationIndex = 0);
        /// <summary>
        /// Delete Record
        /// </summary>
        /// <remarks>
        /// Delete the DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void DeleteDnsDomainRecord(string dnsDomain, string recordId, int operationIndex = 0);

        /// <summary>
        /// Delete Record
        /// </summary>
        /// <remarks>
        /// Delete the DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> DeleteDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, int operationIndex = 0);
        /// <summary>
        /// Get DNS Domain
        /// </summary>
        /// <remarks>
        /// Get information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomain200Response</returns>
        CreateDnsDomain200Response GetDnsDomain(string dnsDomain, int operationIndex = 0);

        /// <summary>
        /// Get DNS Domain
        /// </summary>
        /// <remarks>
        /// Get information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomain200Response</returns>
        ApiResponse<CreateDnsDomain200Response> GetDnsDomainWithHttpInfo(string dnsDomain, int operationIndex = 0);
        /// <summary>
        /// Get DNSSec Info
        /// </summary>
        /// <remarks>
        /// Get the DNSSEC information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDnsDomainDnssec200Response</returns>
        GetDnsDomainDnssec200Response GetDnsDomainDnssec(string dnsDomain, int operationIndex = 0);

        /// <summary>
        /// Get DNSSec Info
        /// </summary>
        /// <remarks>
        /// Get the DNSSEC information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDnsDomainDnssec200Response</returns>
        ApiResponse<GetDnsDomainDnssec200Response> GetDnsDomainDnssecWithHttpInfo(string dnsDomain, int operationIndex = 0);
        /// <summary>
        /// Get Record
        /// </summary>
        /// <remarks>
        /// Get information for a DNS Record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomainRecord201Response</returns>
        CreateDnsDomainRecord201Response GetDnsDomainRecord(string dnsDomain, string recordId, int operationIndex = 0);

        /// <summary>
        /// Get Record
        /// </summary>
        /// <remarks>
        /// Get information for a DNS Record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomainRecord201Response</returns>
        ApiResponse<CreateDnsDomainRecord201Response> GetDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, int operationIndex = 0);
        /// <summary>
        /// Get SOA information
        /// </summary>
        /// <remarks>
        /// Get SOA information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDnsDomainSoa200Response</returns>
        GetDnsDomainSoa200Response GetDnsDomainSoa(string dnsDomain, int operationIndex = 0);

        /// <summary>
        /// Get SOA information
        /// </summary>
        /// <remarks>
        /// Get SOA information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDnsDomainSoa200Response</returns>
        ApiResponse<GetDnsDomainSoa200Response> GetDnsDomainSoaWithHttpInfo(string dnsDomain, int operationIndex = 0);
        /// <summary>
        /// List Records
        /// </summary>
        /// <remarks>
        /// Get the DNS records for the Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDnsDomainRecords200Response</returns>
        ListDnsDomainRecords200Response ListDnsDomainRecords(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List Records
        /// </summary>
        /// <remarks>
        /// Get the DNS records for the Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDnsDomainRecords200Response</returns>
        ApiResponse<ListDnsDomainRecords200Response> ListDnsDomainRecordsWithHttpInfo(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// List DNS Domains
        /// </summary>
        /// <remarks>
        /// List all DNS Domains in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDnsDomains200Response</returns>
        ListDnsDomains200Response ListDnsDomains(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);

        /// <summary>
        /// List DNS Domains
        /// </summary>
        /// <remarks>
        /// List all DNS Domains in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDnsDomains200Response</returns>
        ApiResponse<ListDnsDomains200Response> ListDnsDomainsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0);
        /// <summary>
        /// Update a DNS Domain
        /// </summary>
        /// <remarks>
        /// Update the DNS Domain. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateDnsDomain(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0);

        /// <summary>
        /// Update a DNS Domain
        /// </summary>
        /// <remarks>
        /// Update the DNS Domain. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateDnsDomainWithHttpInfo(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0);
        /// <summary>
        /// Update Record
        /// </summary>
        /// <remarks>
        /// Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateDnsDomainRecord(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0);

        /// <summary>
        /// Update Record
        /// </summary>
        /// <remarks>
        /// Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0);
        /// <summary>
        /// Update SOA information
        /// </summary>
        /// <remarks>
        /// Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        void UpdateDnsDomainSoa(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0);

        /// <summary>
        /// Update SOA information
        /// </summary>
        /// <remarks>
        /// Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        ApiResponse<Object> UpdateDnsDomainSoaWithHttpInfo(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDnsApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Create DNS Domain
        /// </summary>
        /// <remarks>
        /// Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomain200Response</returns>
        System.Threading.Tasks.Task<CreateDnsDomain200Response> CreateDnsDomainAsync(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create DNS Domain
        /// </summary>
        /// <remarks>
        /// Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomain200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDnsDomain200Response>> CreateDnsDomainWithHttpInfoAsync(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create Record
        /// </summary>
        /// <remarks>
        /// Create a DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomainRecord201Response</returns>
        System.Threading.Tasks.Task<CreateDnsDomainRecord201Response> CreateDnsDomainRecordAsync(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create Record
        /// </summary>
        /// <remarks>
        /// Create a DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomainRecord201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDnsDomainRecord201Response>> CreateDnsDomainRecordWithHttpInfoAsync(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <remarks>
        /// Delete the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteDnsDomainAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Domain
        /// </summary>
        /// <remarks>
        /// Delete the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDnsDomainWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete Record
        /// </summary>
        /// <remarks>
        /// Delete the DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task DeleteDnsDomainRecordAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete Record
        /// </summary>
        /// <remarks>
        /// Delete the DNS record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> DeleteDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get DNS Domain
        /// </summary>
        /// <remarks>
        /// Get information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomain200Response</returns>
        System.Threading.Tasks.Task<CreateDnsDomain200Response> GetDnsDomainAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get DNS Domain
        /// </summary>
        /// <remarks>
        /// Get information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomain200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDnsDomain200Response>> GetDnsDomainWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get DNSSec Info
        /// </summary>
        /// <remarks>
        /// Get the DNSSEC information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDnsDomainDnssec200Response</returns>
        System.Threading.Tasks.Task<GetDnsDomainDnssec200Response> GetDnsDomainDnssecAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get DNSSec Info
        /// </summary>
        /// <remarks>
        /// Get the DNSSEC information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDnsDomainDnssec200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetDnsDomainDnssec200Response>> GetDnsDomainDnssecWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get Record
        /// </summary>
        /// <remarks>
        /// Get information for a DNS Record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomainRecord201Response</returns>
        System.Threading.Tasks.Task<CreateDnsDomainRecord201Response> GetDnsDomainRecordAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Record
        /// </summary>
        /// <remarks>
        /// Get information for a DNS Record.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomainRecord201Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<CreateDnsDomainRecord201Response>> GetDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Get SOA information
        /// </summary>
        /// <remarks>
        /// Get SOA information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDnsDomainSoa200Response</returns>
        System.Threading.Tasks.Task<GetDnsDomainSoa200Response> GetDnsDomainSoaAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get SOA information
        /// </summary>
        /// <remarks>
        /// Get SOA information for the DNS Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDnsDomainSoa200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<GetDnsDomainSoa200Response>> GetDnsDomainSoaWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List Records
        /// </summary>
        /// <remarks>
        /// Get the DNS records for the Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDnsDomainRecords200Response</returns>
        System.Threading.Tasks.Task<ListDnsDomainRecords200Response> ListDnsDomainRecordsAsync(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List Records
        /// </summary>
        /// <remarks>
        /// Get the DNS records for the Domain.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDnsDomainRecords200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDnsDomainRecords200Response>> ListDnsDomainRecordsWithHttpInfoAsync(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// List DNS Domains
        /// </summary>
        /// <remarks>
        /// List all DNS Domains in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDnsDomains200Response</returns>
        System.Threading.Tasks.Task<ListDnsDomains200Response> ListDnsDomainsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// List DNS Domains
        /// </summary>
        /// <remarks>
        /// List all DNS Domains in your account.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDnsDomains200Response)</returns>
        System.Threading.Tasks.Task<ApiResponse<ListDnsDomains200Response>> ListDnsDomainsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update a DNS Domain
        /// </summary>
        /// <remarks>
        /// Update the DNS Domain. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateDnsDomainAsync(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update a DNS Domain
        /// </summary>
        /// <remarks>
        /// Update the DNS Domain. 
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateDnsDomainWithHttpInfoAsync(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update Record
        /// </summary>
        /// <remarks>
        /// Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateDnsDomainRecordAsync(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update Record
        /// </summary>
        /// <remarks>
        /// Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Update SOA information
        /// </summary>
        /// <remarks>
        /// Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        System.Threading.Tasks.Task UpdateDnsDomainSoaAsync(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Update SOA information
        /// </summary>
        /// <remarks>
        /// Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </remarks>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<Object>> UpdateDnsDomainSoaWithHttpInfoAsync(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDnsApi : IDnsApiSync, IDnsApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class DnsApi : IDnsApi
    {
        private Org.OpenAPITools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DnsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DnsApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DnsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DnsApi(string basePath)
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
        /// Initializes a new instance of the <see cref="DnsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public DnsApi(Org.OpenAPITools.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="DnsApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public DnsApi(Org.OpenAPITools.Client.ISynchronousClient client, Org.OpenAPITools.Client.IAsynchronousClient asyncClient, Org.OpenAPITools.Client.IReadableConfiguration configuration)
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
        /// Create DNS Domain Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomain200Response</returns>
        public CreateDnsDomain200Response CreateDnsDomain(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> localVarResponse = CreateDnsDomainWithHttpInfo(createDnsDomainRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create DNS Domain Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomain200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> CreateDnsDomainWithHttpInfo(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0)
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

            localVarRequestOptions.Data = createDnsDomainRequest;

            localVarRequestOptions.Operation = "DnsApi.CreateDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDnsDomain200Response>("/domains", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create DNS Domain Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomain200Response</returns>
        public async System.Threading.Tasks.Task<CreateDnsDomain200Response> CreateDnsDomainAsync(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> localVarResponse = await CreateDnsDomainWithHttpInfoAsync(createDnsDomainRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create DNS Domain Create a DNS Domain for &#x60;domain&#x60;. If no &#x60;ip&#x60; address is supplied a domain with no records will be created.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="createDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomain200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response>> CreateDnsDomainWithHttpInfoAsync(CreateDnsDomainRequest? createDnsDomainRequest = default(CreateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Data = createDnsDomainRequest;

            localVarRequestOptions.Operation = "DnsApi.CreateDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDnsDomain200Response>("/domains", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Record Create a DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomainRecord201Response</returns>
        public CreateDnsDomainRecord201Response CreateDnsDomainRecord(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> localVarResponse = CreateDnsDomainRecordWithHttpInfo(dnsDomain, createDnsDomainRecordRequest);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Record Create a DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomainRecord201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> CreateDnsDomainRecordWithHttpInfo(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->CreateDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = createDnsDomainRecordRequest;

            localVarRequestOptions.Operation = "DnsApi.CreateDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<CreateDnsDomainRecord201Response>("/domains/{dns-domain}/records", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create Record Create a DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomainRecord201Response</returns>
        public async System.Threading.Tasks.Task<CreateDnsDomainRecord201Response> CreateDnsDomainRecordAsync(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> localVarResponse = await CreateDnsDomainRecordWithHttpInfoAsync(dnsDomain, createDnsDomainRecordRequest, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create Record Create a DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="createDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomainRecord201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response>> CreateDnsDomainRecordWithHttpInfoAsync(string dnsDomain, CreateDnsDomainRecordRequest? createDnsDomainRecordRequest = default(CreateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->CreateDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = createDnsDomainRecordRequest;

            localVarRequestOptions.Operation = "DnsApi.CreateDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<CreateDnsDomainRecord201Response>("/domains/{dns-domain}/records", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("CreateDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Domain Delete the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteDnsDomain(string dnsDomain, int operationIndex = 0)
        {
            DeleteDnsDomainWithHttpInfo(dnsDomain);
        }

        /// <summary>
        /// Delete Domain Delete the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteDnsDomainWithHttpInfo(string dnsDomain, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->DeleteDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.DeleteDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Domain Delete the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteDnsDomainAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteDnsDomainWithHttpInfoAsync(dnsDomain, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Domain Delete the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteDnsDomainWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->DeleteDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.DeleteDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Record Delete the DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void DeleteDnsDomainRecord(string dnsDomain, string recordId, int operationIndex = 0)
        {
            DeleteDnsDomainRecordWithHttpInfo(dnsDomain, recordId);
        }

        /// <summary>
        /// Delete Record Delete the DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> DeleteDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->DeleteDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->DeleteDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.DeleteDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Delete<Object>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete Record Delete the DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteDnsDomainRecordAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await DeleteDnsDomainRecordWithHttpInfoAsync(dnsDomain, recordId, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete Record Delete the DNS record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> DeleteDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->DeleteDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->DeleteDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.DeleteDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.DeleteAsync<Object>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("DeleteDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get DNS Domain Get information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomain200Response</returns>
        public CreateDnsDomain200Response GetDnsDomain(string dnsDomain, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> localVarResponse = GetDnsDomainWithHttpInfo(dnsDomain);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get DNS Domain Get information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomain200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> GetDnsDomainWithHttpInfo(string dnsDomain, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateDnsDomain200Response>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get DNS Domain Get information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomain200Response</returns>
        public async System.Threading.Tasks.Task<CreateDnsDomain200Response> GetDnsDomainAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response> localVarResponse = await GetDnsDomainWithHttpInfoAsync(dnsDomain, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get DNS Domain Get information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomain200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDnsDomain200Response>> GetDnsDomainWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateDnsDomain200Response>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get DNSSec Info Get the DNSSEC information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDnsDomainDnssec200Response</returns>
        public GetDnsDomainDnssec200Response GetDnsDomainDnssec(string dnsDomain, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetDnsDomainDnssec200Response> localVarResponse = GetDnsDomainDnssecWithHttpInfo(dnsDomain);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get DNSSec Info Get the DNSSEC information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDnsDomainDnssec200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetDnsDomainDnssec200Response> GetDnsDomainDnssecWithHttpInfo(string dnsDomain, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainDnssec");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainDnssec";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetDnsDomainDnssec200Response>("/domains/{dns-domain}/dnssec", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainDnssec", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get DNSSec Info Get the DNSSEC information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDnsDomainDnssec200Response</returns>
        public async System.Threading.Tasks.Task<GetDnsDomainDnssec200Response> GetDnsDomainDnssecAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetDnsDomainDnssec200Response> localVarResponse = await GetDnsDomainDnssecWithHttpInfoAsync(dnsDomain, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get DNSSec Info Get the DNSSEC information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDnsDomainDnssec200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetDnsDomainDnssec200Response>> GetDnsDomainDnssecWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainDnssec");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainDnssec";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetDnsDomainDnssec200Response>("/domains/{dns-domain}/dnssec", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainDnssec", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Record Get information for a DNS Record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>CreateDnsDomainRecord201Response</returns>
        public CreateDnsDomainRecord201Response GetDnsDomainRecord(string dnsDomain, string recordId, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> localVarResponse = GetDnsDomainRecordWithHttpInfo(dnsDomain, recordId);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Record Get information for a DNS Record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of CreateDnsDomainRecord201Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> GetDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->GetDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<CreateDnsDomainRecord201Response>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Record Get information for a DNS Record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of CreateDnsDomainRecord201Response</returns>
        public async System.Threading.Tasks.Task<CreateDnsDomainRecord201Response> GetDnsDomainRecordAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response> localVarResponse = await GetDnsDomainRecordWithHttpInfoAsync(dnsDomain, recordId, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Record Get information for a DNS Record.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (CreateDnsDomainRecord201Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<CreateDnsDomainRecord201Response>> GetDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->GetDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<CreateDnsDomainRecord201Response>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get SOA information Get SOA information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>GetDnsDomainSoa200Response</returns>
        public GetDnsDomainSoa200Response GetDnsDomainSoa(string dnsDomain, int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<GetDnsDomainSoa200Response> localVarResponse = GetDnsDomainSoaWithHttpInfo(dnsDomain);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get SOA information Get SOA information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of GetDnsDomainSoa200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<GetDnsDomainSoa200Response> GetDnsDomainSoaWithHttpInfo(string dnsDomain, int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainSoa");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainSoa";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<GetDnsDomainSoa200Response>("/domains/{dns-domain}/soa", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainSoa", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get SOA information Get SOA information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of GetDnsDomainSoa200Response</returns>
        public async System.Threading.Tasks.Task<GetDnsDomainSoa200Response> GetDnsDomainSoaAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<GetDnsDomainSoa200Response> localVarResponse = await GetDnsDomainSoaWithHttpInfoAsync(dnsDomain, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get SOA information Get SOA information for the DNS Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (GetDnsDomainSoa200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<GetDnsDomainSoa200Response>> GetDnsDomainSoaWithHttpInfoAsync(string dnsDomain, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->GetDnsDomainSoa");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter

            localVarRequestOptions.Operation = "DnsApi.GetDnsDomainSoa";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<GetDnsDomainSoa200Response>("/domains/{dns-domain}/soa", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("GetDnsDomainSoa", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Records Get the DNS records for the Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDnsDomainRecords200Response</returns>
        public ListDnsDomainRecords200Response ListDnsDomainRecords(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDnsDomainRecords200Response> localVarResponse = ListDnsDomainRecordsWithHttpInfo(dnsDomain, perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Records Get the DNS records for the Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDnsDomainRecords200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDnsDomainRecords200Response> ListDnsDomainRecordsWithHttpInfo(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->ListDnsDomainRecords");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "DnsApi.ListDnsDomainRecords";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDnsDomainRecords200Response>("/domains/{dns-domain}/records", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDnsDomainRecords", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List Records Get the DNS records for the Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDnsDomainRecords200Response</returns>
        public async System.Threading.Tasks.Task<ListDnsDomainRecords200Response> ListDnsDomainRecordsAsync(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDnsDomainRecords200Response> localVarResponse = await ListDnsDomainRecordsWithHttpInfoAsync(dnsDomain, perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List Records Get the DNS records for the Domain.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500. (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDnsDomainRecords200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDnsDomainRecords200Response>> ListDnsDomainRecordsWithHttpInfoAsync(string dnsDomain, int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->ListDnsDomainRecords");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            if (perPage != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "per_page", perPage));
            }
            if (cursor != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.OpenAPITools.Client.ClientUtils.ParameterToMultiMap("", "cursor", cursor));
            }

            localVarRequestOptions.Operation = "DnsApi.ListDnsDomainRecords";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDnsDomainRecords200Response>("/domains/{dns-domain}/records", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDnsDomainRecords", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List DNS Domains List all DNS Domains in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ListDnsDomains200Response</returns>
        public ListDnsDomains200Response ListDnsDomains(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
        {
            Org.OpenAPITools.Client.ApiResponse<ListDnsDomains200Response> localVarResponse = ListDnsDomainsWithHttpInfo(perPage, cursor);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List DNS Domains List all DNS Domains in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of ListDnsDomains200Response</returns>
        public Org.OpenAPITools.Client.ApiResponse<ListDnsDomains200Response> ListDnsDomainsWithHttpInfo(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0)
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

            localVarRequestOptions.Operation = "DnsApi.ListDnsDomains";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<ListDnsDomains200Response>("/domains", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDnsDomains", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List DNS Domains List all DNS Domains in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ListDnsDomains200Response</returns>
        public async System.Threading.Tasks.Task<ListDnsDomains200Response> ListDnsDomainsAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.OpenAPITools.Client.ApiResponse<ListDnsDomains200Response> localVarResponse = await ListDnsDomainsWithHttpInfoAsync(perPage, cursor, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List DNS Domains List all DNS Domains in your account.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="perPage">Number of items requested per page. Default is 100 and Max is 500.  (optional)</param>
        /// <param name="cursor">Cursor for paging. See [Meta and Pagination](#section/Introduction/Meta-and-Pagination). (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (ListDnsDomains200Response)</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<ListDnsDomains200Response>> ListDnsDomainsWithHttpInfoAsync(int? perPage = default(int?), string? cursor = default(string?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
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

            localVarRequestOptions.Operation = "DnsApi.ListDnsDomains";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<ListDnsDomains200Response>("/domains", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("ListDnsDomains", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a DNS Domain Update the DNS Domain. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateDnsDomain(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0)
        {
            UpdateDnsDomainWithHttpInfo(dnsDomain, updateDnsDomainRequest);
        }

        /// <summary>
        /// Update a DNS Domain Update the DNS Domain. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateDnsDomainWithHttpInfo(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update a DNS Domain Update the DNS Domain. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateDnsDomainAsync(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateDnsDomainWithHttpInfoAsync(dnsDomain, updateDnsDomainRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a DNS Domain Update the DNS Domain. 
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateDnsDomainWithHttpInfoAsync(string dnsDomain, UpdateDnsDomainRequest? updateDnsDomainRequest = default(UpdateDnsDomainRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomain");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomain";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/domains/{dns-domain}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomain", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Record Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateDnsDomainRecord(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0)
        {
            UpdateDnsDomainRecordWithHttpInfo(dnsDomain, recordId, updateDnsDomainRecordRequest);
        }

        /// <summary>
        /// Update Record Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateDnsDomainRecordWithHttpInfo(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->UpdateDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainRecordRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<Object>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update Record Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateDnsDomainRecordAsync(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateDnsDomainRecordWithHttpInfoAsync(dnsDomain, recordId, updateDnsDomainRecordRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update Record Update the information for a DNS record. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="recordId">The [DNS Record id](#operation/list-dns-domain-records).</param>
        /// <param name="updateDnsDomainRecordRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateDnsDomainRecordWithHttpInfoAsync(string dnsDomain, string recordId, UpdateDnsDomainRecordRequest? updateDnsDomainRecordRequest = default(UpdateDnsDomainRecordRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomainRecord");
            }

            // verify the required parameter 'recordId' is set
            if (recordId == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'recordId' when calling DnsApi->UpdateDnsDomainRecord");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.PathParameters.Add("record-id", Org.OpenAPITools.Client.ClientUtils.ParameterToString(recordId)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainRecordRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomainRecord";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<Object>("/domains/{dns-domain}/records/{record-id}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomainRecord", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update SOA information Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns></returns>
        public void UpdateDnsDomainSoa(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0)
        {
            UpdateDnsDomainSoaWithHttpInfo(dnsDomain, updateDnsDomainSoaRequest);
        }

        /// <summary>
        /// Update SOA information Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        public Org.OpenAPITools.Client.ApiResponse<Object> UpdateDnsDomainSoaWithHttpInfo(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0)
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomainSoa");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainSoaRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomainSoa";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Patch<Object>("/domains/{dns-domain}/soa", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomainSoa", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Update SOA information Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task UpdateDnsDomainSoaAsync(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            await UpdateDnsDomainSoaWithHttpInfoAsync(dnsDomain, updateDnsDomainSoaRequest, operationIndex, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update SOA information Update the SOA information for the DNS Domain. All attributes are optional. If not set, the attributes will retain their original values.
        /// </summary>
        /// <exception cref="Org.OpenAPITools.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="dnsDomain">The [DNS Domain](#operation/list-dns-domains).</param>
        /// <param name="updateDnsDomainSoaRequest">Include a JSON object in the request body with a content type of **application/json**. (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<Org.OpenAPITools.Client.ApiResponse<Object>> UpdateDnsDomainSoaWithHttpInfoAsync(string dnsDomain, UpdateDnsDomainSoaRequest? updateDnsDomainSoaRequest = default(UpdateDnsDomainSoaRequest?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'dnsDomain' is set
            if (dnsDomain == null)
            {
                throw new Org.OpenAPITools.Client.ApiException(400, "Missing required parameter 'dnsDomain' when calling DnsApi->UpdateDnsDomainSoa");
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

            localVarRequestOptions.PathParameters.Add("dns-domain", Org.OpenAPITools.Client.ClientUtils.ParameterToString(dnsDomain)); // path parameter
            localVarRequestOptions.Data = updateDnsDomainSoaRequest;

            localVarRequestOptions.Operation = "DnsApi.UpdateDnsDomainSoa";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (API Key) required
            // bearer authentication required
            if (!string.IsNullOrEmpty(this.Configuration.AccessToken) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PatchAsync<Object>("/domains/{dns-domain}/soa", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("UpdateDnsDomainSoa", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
