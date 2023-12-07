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
    /// CreateBaremetalRequest
    /// </summary>
    [DataContract(Name = "create_baremetal_request")]
    public partial class CreateBaremetalRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBaremetalRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected CreateBaremetalRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBaremetalRequest" /> class.
        /// </summary>
        /// <param name="region">The [Region id](#operation/list-regions) to create the instance. (required).</param>
        /// <param name="plan">The [Bare Metal plan id](#operation/list-metal-plans) to use for this instance. (required).</param>
        /// <param name="scriptId">The [Startup Script id](#operation/list-startup-scripts) to use for this instance..</param>
        /// <param name="enableIpv6">Enable IPv6.  * true.</param>
        /// <param name="sshkeyId">The [SSH Key id](#operation/list-ssh-keys) to install on this instance..</param>
        /// <param name="userData">The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for this Instance..</param>
        /// <param name="label">The user-supplied label..</param>
        /// <param name="activationEmail">Notify by email after deployment.  * true * false (default).</param>
        /// <param name="hostname">The user-supplied hostname to use when deploying this instance..</param>
        /// <param name="tag">Use &#x60;tags&#x60; instead. The user-supplied tag..</param>
        /// <param name="reservedIpv4">The [Reserved IP id](#operation/list-reserved-ips) for this instance..</param>
        /// <param name="osId">If supplied, deploy the instance using this [Operating System id](#operation/list-os)..</param>
        /// <param name="snapshotId">If supplied, deploy the instance using this [Snapshot ID](#operation/list-snapshots)..</param>
        /// <param name="appId">If supplied, deploy the instance using this [Application id](#operation/list-applications)..</param>
        /// <param name="imageId">If supplied, deploy the instance using this [Application image_id](#operation/list-applications)..</param>
        /// <param name="persistentPxe">Enable persistent PXE.  * true * false (default).</param>
        /// <param name="attachVpc2">An array of [VPC IDs](#operation/list-vpc2) to attach to this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter..</param>
        /// <param name="detachVpc2">An array of [VPC IDs](#operation/list-vpc2) to detach from this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;..</param>
        /// <param name="enableVpc2">If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network..</param>
        /// <param name="tags">Tags to apply to the instance..</param>
        /// <param name="userScheme">Linux-only: The user scheme used for logging into this instance. By default, the \&quot;root\&quot; user is configured. Alternatively, a limited user with sudo permissions can be selected.  * root * limited.</param>
        public CreateBaremetalRequest(string region = default(string), string plan = default(string), string scriptId = default(string), bool enableIpv6 = default(bool), List<string> sshkeyId = default(List<string>), string userData = default(string), string label = default(string), bool activationEmail = default(bool), string hostname = default(string), string tag = default(string), string reservedIpv4 = default(string), int osId = default(int), string snapshotId = default(string), int appId = default(int), string imageId = default(string), bool persistentPxe = default(bool), List<string> attachVpc2 = default(List<string>), List<string> detachVpc2 = default(List<string>), bool enableVpc2 = default(bool), List<string> tags = default(List<string>), string userScheme = default(string))
        {
            // to ensure "region" is required (not null)
            if (region == null)
            {
                throw new ArgumentNullException("region is a required property for CreateBaremetalRequest and cannot be null");
            }
            this.Region = region;
            // to ensure "plan" is required (not null)
            if (plan == null)
            {
                throw new ArgumentNullException("plan is a required property for CreateBaremetalRequest and cannot be null");
            }
            this.Plan = plan;
            this.ScriptId = scriptId;
            this.EnableIpv6 = enableIpv6;
            this.SshkeyId = sshkeyId;
            this.UserData = userData;
            this.Label = label;
            this.ActivationEmail = activationEmail;
            this.Hostname = hostname;
            this.Tag = tag;
            this.ReservedIpv4 = reservedIpv4;
            this.OsId = osId;
            this.SnapshotId = snapshotId;
            this.AppId = appId;
            this.ImageId = imageId;
            this.PersistentPxe = persistentPxe;
            this.AttachVpc2 = attachVpc2;
            this.DetachVpc2 = detachVpc2;
            this.EnableVpc2 = enableVpc2;
            this.Tags = tags;
            this.UserScheme = userScheme;
        }

        /// <summary>
        /// The [Region id](#operation/list-regions) to create the instance.
        /// </summary>
        /// <value>The [Region id](#operation/list-regions) to create the instance.</value>
        [DataMember(Name = "region", IsRequired = true, EmitDefaultValue = true)]
        public string Region { get; set; }

        /// <summary>
        /// The [Bare Metal plan id](#operation/list-metal-plans) to use for this instance.
        /// </summary>
        /// <value>The [Bare Metal plan id](#operation/list-metal-plans) to use for this instance.</value>
        [DataMember(Name = "plan", IsRequired = true, EmitDefaultValue = true)]
        public string Plan { get; set; }

        /// <summary>
        /// The [Startup Script id](#operation/list-startup-scripts) to use for this instance.
        /// </summary>
        /// <value>The [Startup Script id](#operation/list-startup-scripts) to use for this instance.</value>
        [DataMember(Name = "script_id", EmitDefaultValue = false)]
        public string ScriptId { get; set; }

        /// <summary>
        /// Enable IPv6.  * true
        /// </summary>
        /// <value>Enable IPv6.  * true</value>
        [DataMember(Name = "enable_ipv6", EmitDefaultValue = true)]
        public bool EnableIpv6 { get; set; }

        /// <summary>
        /// The [SSH Key id](#operation/list-ssh-keys) to install on this instance.
        /// </summary>
        /// <value>The [SSH Key id](#operation/list-ssh-keys) to install on this instance.</value>
        [DataMember(Name = "sshkey_id", EmitDefaultValue = false)]
        public List<string> SshkeyId { get; set; }

        /// <summary>
        /// The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for this Instance.
        /// </summary>
        /// <value>The user-supplied, base64 encoded [user data](https://www.vultr.com/docs/manage-instance-user-data-with-the-vultr-metadata-api/) for this Instance.</value>
        [DataMember(Name = "user_data", EmitDefaultValue = false)]
        public string UserData { get; set; }

        /// <summary>
        /// The user-supplied label.
        /// </summary>
        /// <value>The user-supplied label.</value>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// Notify by email after deployment.  * true * false (default)
        /// </summary>
        /// <value>Notify by email after deployment.  * true * false (default)</value>
        [DataMember(Name = "activation_email", EmitDefaultValue = true)]
        public bool ActivationEmail { get; set; }

        /// <summary>
        /// The user-supplied hostname to use when deploying this instance.
        /// </summary>
        /// <value>The user-supplied hostname to use when deploying this instance.</value>
        [DataMember(Name = "hostname", EmitDefaultValue = false)]
        public string Hostname { get; set; }

        /// <summary>
        /// Use &#x60;tags&#x60; instead. The user-supplied tag.
        /// </summary>
        /// <value>Use &#x60;tags&#x60; instead. The user-supplied tag.</value>
        [DataMember(Name = "tag", EmitDefaultValue = false)]
        [Obsolete]
        public string Tag { get; set; }

        /// <summary>
        /// The [Reserved IP id](#operation/list-reserved-ips) for this instance.
        /// </summary>
        /// <value>The [Reserved IP id](#operation/list-reserved-ips) for this instance.</value>
        [DataMember(Name = "reserved_ipv4", EmitDefaultValue = false)]
        public string ReservedIpv4 { get; set; }

        /// <summary>
        /// If supplied, deploy the instance using this [Operating System id](#operation/list-os).
        /// </summary>
        /// <value>If supplied, deploy the instance using this [Operating System id](#operation/list-os).</value>
        [DataMember(Name = "os_id", EmitDefaultValue = false)]
        public int OsId { get; set; }

        /// <summary>
        /// If supplied, deploy the instance using this [Snapshot ID](#operation/list-snapshots).
        /// </summary>
        /// <value>If supplied, deploy the instance using this [Snapshot ID](#operation/list-snapshots).</value>
        [DataMember(Name = "snapshot_id", EmitDefaultValue = false)]
        public string SnapshotId { get; set; }

        /// <summary>
        /// If supplied, deploy the instance using this [Application id](#operation/list-applications).
        /// </summary>
        /// <value>If supplied, deploy the instance using this [Application id](#operation/list-applications).</value>
        [DataMember(Name = "app_id", EmitDefaultValue = false)]
        public int AppId { get; set; }

        /// <summary>
        /// If supplied, deploy the instance using this [Application image_id](#operation/list-applications).
        /// </summary>
        /// <value>If supplied, deploy the instance using this [Application image_id](#operation/list-applications).</value>
        [DataMember(Name = "image_id", EmitDefaultValue = false)]
        public string ImageId { get; set; }

        /// <summary>
        /// Enable persistent PXE.  * true * false (default)
        /// </summary>
        /// <value>Enable persistent PXE.  * true * false (default)</value>
        [DataMember(Name = "persistent_pxe", EmitDefaultValue = true)]
        public bool PersistentPxe { get; set; }

        /// <summary>
        /// An array of [VPC IDs](#operation/list-vpc2) to attach to this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter.
        /// </summary>
        /// <value>An array of [VPC IDs](#operation/list-vpc2) to attach to this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;. Please choose one parameter.</value>
        [DataMember(Name = "attach_vpc2", EmitDefaultValue = false)]
        public List<string> AttachVpc2 { get; set; }

        /// <summary>
        /// An array of [VPC IDs](#operation/list-vpc2) to detach from this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;.
        /// </summary>
        /// <value>An array of [VPC IDs](#operation/list-vpc2) to detach from this Bare Metal Instance. This parameter takes precedence over &#x60;enable_vpc2&#x60;.</value>
        [DataMember(Name = "detach_vpc2", EmitDefaultValue = false)]
        public List<string> DetachVpc2 { get; set; }

        /// <summary>
        /// If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network.
        /// </summary>
        /// <value>If &#x60;true&#x60;, VPC 2.0 support will be added to the new server.  This parameter attaches a single VPC 2.0 netowrk. When no VPC 2.0 network exists in the region, it will be automatically created.  If there are multiple VPC 2.0 networks in the instance&#39;s region, use &#x60;attach_vpc2&#x60; instead to specify a VPC 2.0 network.</value>
        [DataMember(Name = "enable_vpc2", EmitDefaultValue = true)]
        public bool EnableVpc2 { get; set; }

        /// <summary>
        /// Tags to apply to the instance.
        /// </summary>
        /// <value>Tags to apply to the instance.</value>
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Linux-only: The user scheme used for logging into this instance. By default, the \&quot;root\&quot; user is configured. Alternatively, a limited user with sudo permissions can be selected.  * root * limited
        /// </summary>
        /// <value>Linux-only: The user scheme used for logging into this instance. By default, the \&quot;root\&quot; user is configured. Alternatively, a limited user with sudo permissions can be selected.  * root * limited</value>
        [DataMember(Name = "user_scheme", EmitDefaultValue = false)]
        public string UserScheme { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class CreateBaremetalRequest {\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  Plan: ").Append(Plan).Append("\n");
            sb.Append("  ScriptId: ").Append(ScriptId).Append("\n");
            sb.Append("  EnableIpv6: ").Append(EnableIpv6).Append("\n");
            sb.Append("  SshkeyId: ").Append(SshkeyId).Append("\n");
            sb.Append("  UserData: ").Append(UserData).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  ActivationEmail: ").Append(ActivationEmail).Append("\n");
            sb.Append("  Hostname: ").Append(Hostname).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  ReservedIpv4: ").Append(ReservedIpv4).Append("\n");
            sb.Append("  OsId: ").Append(OsId).Append("\n");
            sb.Append("  SnapshotId: ").Append(SnapshotId).Append("\n");
            sb.Append("  AppId: ").Append(AppId).Append("\n");
            sb.Append("  ImageId: ").Append(ImageId).Append("\n");
            sb.Append("  PersistentPxe: ").Append(PersistentPxe).Append("\n");
            sb.Append("  AttachVpc2: ").Append(AttachVpc2).Append("\n");
            sb.Append("  DetachVpc2: ").Append(DetachVpc2).Append("\n");
            sb.Append("  EnableVpc2: ").Append(EnableVpc2).Append("\n");
            sb.Append("  Tags: ").Append(Tags).Append("\n");
            sb.Append("  UserScheme: ").Append(UserScheme).Append("\n");
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
