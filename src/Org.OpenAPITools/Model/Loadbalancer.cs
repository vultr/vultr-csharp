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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.OpenAPITools.Client.OpenAPIDateConverter;

namespace Org.OpenAPITools.Model
{
    /// <summary>
    /// Load Balancer information.
    /// </summary>
    [DataContract(Name = "loadbalancer")]
    public partial class Loadbalancer : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Loadbalancer" /> class.
        /// </summary>
        /// <param name="id">A unique ID for the Load Balancer..</param>
        /// <param name="dateCreated">Date this Load Balancer was created..</param>
        /// <param name="region">The [Region id](#operation/list-regions) where the Load Balancer is located..</param>
        /// <param name="label">The user-supplied label for this load-balancer..</param>
        /// <param name="status">The current status.  * active.</param>
        /// <param name="ipv4">The IPv4 address of this Load Balancer..</param>
        /// <param name="ipv6">The IPv6 address of this Load Balancer..</param>
        /// <param name="genericInfo">genericInfo.</param>
        /// <param name="healthCheck">healthCheck.</param>
        /// <param name="hasSsl">Indicates if this Load Balancer has an SSL certificate installed..</param>
        /// <param name="http2">Indicates if this Load Balancer has HTTP2 enabled..</param>
        /// <param name="nodes">The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1..</param>
        /// <param name="forwardRules">An array of forwarding rule objects..</param>
        /// <param name="instances">Array of [Instance ids](#operation/list-instances) attached to this Load Balancer..</param>
        /// <param name="firewallRules">An array of firewall rule objects..</param>
        public Loadbalancer(string id = default(string), string dateCreated = default(string), string region = default(string), string label = default(string), string status = default(string), string ipv4 = default(string), string ipv6 = default(string), LoadbalancerGenericInfo genericInfo = default(LoadbalancerGenericInfo), LoadbalancerHealthCheck healthCheck = default(LoadbalancerHealthCheck), bool hasSsl = default(bool), bool http2 = default(bool), int nodes = default(int), List<LoadbalancerForwardRulesInner> forwardRules = default(List<LoadbalancerForwardRulesInner>), List<string> instances = default(List<string>), List<LoadbalancerFirewallRulesInner> firewallRules = default(List<LoadbalancerFirewallRulesInner>))
        {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.Region = region;
            this.Label = label;
            this.Status = status;
            this.Ipv4 = ipv4;
            this.Ipv6 = ipv6;
            this.GenericInfo = genericInfo;
            this.HealthCheck = healthCheck;
            this.HasSsl = hasSsl;
            this.Http2 = http2;
            this.Nodes = nodes;
            this.ForwardRules = forwardRules;
            this.Instances = instances;
            this.FirewallRules = firewallRules;
        }

        /// <summary>
        /// A unique ID for the Load Balancer.
        /// </summary>
        /// <value>A unique ID for the Load Balancer.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Date this Load Balancer was created.
        /// </summary>
        /// <value>Date this Load Balancer was created.</value>
        [DataMember(Name = "date_created", EmitDefaultValue = false)]
        public string DateCreated { get; set; }

        /// <summary>
        /// The [Region id](#operation/list-regions) where the Load Balancer is located.
        /// </summary>
        /// <value>The [Region id](#operation/list-regions) where the Load Balancer is located.</value>
        [DataMember(Name = "region", EmitDefaultValue = false)]
        public string Region { get; set; }

        /// <summary>
        /// The user-supplied label for this load-balancer.
        /// </summary>
        /// <value>The user-supplied label for this load-balancer.</value>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// The current status.  * active
        /// </summary>
        /// <value>The current status.  * active</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// The IPv4 address of this Load Balancer.
        /// </summary>
        /// <value>The IPv4 address of this Load Balancer.</value>
        [DataMember(Name = "ipv4", EmitDefaultValue = false)]
        public string Ipv4 { get; set; }

        /// <summary>
        /// The IPv6 address of this Load Balancer.
        /// </summary>
        /// <value>The IPv6 address of this Load Balancer.</value>
        [DataMember(Name = "ipv6", EmitDefaultValue = false)]
        public string Ipv6 { get; set; }

        /// <summary>
        /// Gets or Sets GenericInfo
        /// </summary>
        [DataMember(Name = "generic_info", EmitDefaultValue = false)]
        public LoadbalancerGenericInfo GenericInfo { get; set; }

        /// <summary>
        /// Gets or Sets HealthCheck
        /// </summary>
        [DataMember(Name = "health_check", EmitDefaultValue = false)]
        public LoadbalancerHealthCheck HealthCheck { get; set; }

        /// <summary>
        /// Indicates if this Load Balancer has an SSL certificate installed.
        /// </summary>
        /// <value>Indicates if this Load Balancer has an SSL certificate installed.</value>
        [DataMember(Name = "has_ssl", EmitDefaultValue = true)]
        public bool HasSsl { get; set; }

        /// <summary>
        /// Indicates if this Load Balancer has HTTP2 enabled.
        /// </summary>
        /// <value>Indicates if this Load Balancer has HTTP2 enabled.</value>
        [DataMember(Name = "http2", EmitDefaultValue = true)]
        public bool Http2 { get; set; }

        /// <summary>
        /// The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1.
        /// </summary>
        /// <value>The number of nodes to add to the load balancer (1-99), must be an odd number. This defaults to 1.</value>
        [DataMember(Name = "nodes", EmitDefaultValue = false)]
        public int Nodes { get; set; }

        /// <summary>
        /// An array of forwarding rule objects.
        /// </summary>
        /// <value>An array of forwarding rule objects.</value>
        [DataMember(Name = "forward_rules", EmitDefaultValue = false)]
        public List<LoadbalancerForwardRulesInner> ForwardRules { get; set; }

        /// <summary>
        /// Array of [Instance ids](#operation/list-instances) attached to this Load Balancer.
        /// </summary>
        /// <value>Array of [Instance ids](#operation/list-instances) attached to this Load Balancer.</value>
        [DataMember(Name = "instances", EmitDefaultValue = false)]
        public List<string> Instances { get; set; }

        /// <summary>
        /// An array of firewall rule objects.
        /// </summary>
        /// <value>An array of firewall rule objects.</value>
        [DataMember(Name = "firewall_rules", EmitDefaultValue = false)]
        public List<LoadbalancerFirewallRulesInner> FirewallRules { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Loadbalancer {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  DateCreated: ").Append(DateCreated).Append("\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Ipv4: ").Append(Ipv4).Append("\n");
            sb.Append("  Ipv6: ").Append(Ipv6).Append("\n");
            sb.Append("  GenericInfo: ").Append(GenericInfo).Append("\n");
            sb.Append("  HealthCheck: ").Append(HealthCheck).Append("\n");
            sb.Append("  HasSsl: ").Append(HasSsl).Append("\n");
            sb.Append("  Http2: ").Append(Http2).Append("\n");
            sb.Append("  Nodes: ").Append(Nodes).Append("\n");
            sb.Append("  ForwardRules: ").Append(ForwardRules).Append("\n");
            sb.Append("  Instances: ").Append(Instances).Append("\n");
            sb.Append("  FirewallRules: ").Append(FirewallRules).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
