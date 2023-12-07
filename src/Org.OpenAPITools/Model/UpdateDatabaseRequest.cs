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
    /// UpdateDatabaseRequest
    /// </summary>
    [DataContract(Name = "update_database_request")]
    public partial class UpdateDatabaseRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDatabaseRequest" /> class.
        /// </summary>
        /// <param name="region">The [Region id](#operation/list-regions) where the Managed Database is located..</param>
        /// <param name="plan">The [Plan id](#operation/list-database-plans) for this Managed Database..</param>
        /// <param name="label">A user-supplied label for this Managed Database..</param>
        /// <param name="tag">The user-supplied tag for this Managed Database..</param>
        /// <param name="vpcId">The [VPC id](#operation/list-vpcs) for this Managed Database..</param>
        /// <param name="maintenanceDow">The day of week for routine maintenance updates. * &#x60;monday&#x60; * &#x60;tuesday&#x60; * &#x60;wednesday&#x60; * &#x60;thursday&#x60; * &#x60;friday&#x60; * &#x60;saturday&#x60; * &#x60;sunday&#x60;.</param>
        /// <param name="maintenanceTime">The preferred time (UTC) for routine maintenance updates to occur in 24-hour HH:00 format (e.g. &#x60;01:00&#x60;, &#x60;13:00&#x60;, &#x60;23:00&#x60;, etc.)..</param>
        /// <param name="clusterTimeZone">The configured time zone for the Managed Database in TZ database format (e.g. &#x60;UTC&#x60;, &#x60;America/New_York&#x60;, &#x60;Europe/London&#x60;, etc.)..</param>
        /// <param name="trustedIps">A list of IP addresses allowed to access the Managed Database in CIDR notation (defaults to /32 if excluded)..</param>
        /// <param name="mysqlSqlModes">A list of SQL modes to enable on the Managed Database (MySQL engine type only). * &#x60;ALLOW_INVALID_DATES&#x60; * &#x60;ANSI&#x60; (Combination Mode) * &#x60;ANSI_QUOTES&#x60; * &#x60;ERROR_FOR_DIVISION_BY_ZERO&#x60; * &#x60;HIGH_NOT_PRECEDENCE&#x60; * &#x60;IGNORE_SPACE&#x60; * &#x60;NO_AUTO_VALUE_ON_ZERO&#x60; * &#x60;NO_DIR_IN_CREATE&#x60; * &#x60;NO_ENGINE_SUBSTITUTION&#x60; * &#x60;NO_UNSIGNED_SUBTRACTION&#x60; * &#x60;NO_ZERO_DATE&#x60; * &#x60;NO_ZERO_IN_DATE&#x60; * &#x60;ONLY_FULL_GROUP_BY&#x60; * &#x60;PIPES_AS_CONCAT&#x60; * &#x60;REAL_AS_FLOAT&#x60; * &#x60;STRICT_ALL_TABLES&#x60; * &#x60;STRICT_TRANS_TABLES&#x60; * &#x60;TIME_TRUNCATE_FRACTIONAL&#x60; * &#x60;TRADITIONAL&#x60; (Combination Mode).</param>
        /// <param name="mysqlRequirePrimaryKey">Require a primary key for all tables on the Managed Database (MySQL engine type only)..</param>
        /// <param name="mysqlSlowQueryLog">Enable or disable slow query logging on the Managed Database (MySQL engine type only)..</param>
        /// <param name="mysqlLongQueryTime">The number of seconds to denote a slow query when logging is enabled (MySQL engine type only)..</param>
        /// <param name="redisEvictionPolicy">Set the data eviction policy for the Managed Database (Redis engine type only).</param>
        public UpdateDatabaseRequest(string region = default(string), string plan = default(string), string label = default(string), string tag = default(string), string vpcId = default(string), string maintenanceDow = default(string), string maintenanceTime = default(string), string clusterTimeZone = default(string), List<string> trustedIps = default(List<string>), List<string> mysqlSqlModes = default(List<string>), bool mysqlRequirePrimaryKey = default(bool), bool mysqlSlowQueryLog = default(bool), int mysqlLongQueryTime = default(int), string redisEvictionPolicy = default(string))
        {
            this.Region = region;
            this.Plan = plan;
            this.Label = label;
            this.Tag = tag;
            this.VpcId = vpcId;
            this.MaintenanceDow = maintenanceDow;
            this.MaintenanceTime = maintenanceTime;
            this.ClusterTimeZone = clusterTimeZone;
            this.TrustedIps = trustedIps;
            this.MysqlSqlModes = mysqlSqlModes;
            this.MysqlRequirePrimaryKey = mysqlRequirePrimaryKey;
            this.MysqlSlowQueryLog = mysqlSlowQueryLog;
            this.MysqlLongQueryTime = mysqlLongQueryTime;
            this.RedisEvictionPolicy = redisEvictionPolicy;
        }

        /// <summary>
        /// The [Region id](#operation/list-regions) where the Managed Database is located.
        /// </summary>
        /// <value>The [Region id](#operation/list-regions) where the Managed Database is located.</value>
        [DataMember(Name = "region", EmitDefaultValue = false)]
        public string Region { get; set; }

        /// <summary>
        /// The [Plan id](#operation/list-database-plans) for this Managed Database.
        /// </summary>
        /// <value>The [Plan id](#operation/list-database-plans) for this Managed Database.</value>
        [DataMember(Name = "plan", EmitDefaultValue = false)]
        public string Plan { get; set; }

        /// <summary>
        /// A user-supplied label for this Managed Database.
        /// </summary>
        /// <value>A user-supplied label for this Managed Database.</value>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// The user-supplied tag for this Managed Database.
        /// </summary>
        /// <value>The user-supplied tag for this Managed Database.</value>
        [DataMember(Name = "tag", EmitDefaultValue = false)]
        public string Tag { get; set; }

        /// <summary>
        /// The [VPC id](#operation/list-vpcs) for this Managed Database.
        /// </summary>
        /// <value>The [VPC id](#operation/list-vpcs) for this Managed Database.</value>
        [DataMember(Name = "vpc_id", EmitDefaultValue = false)]
        public string VpcId { get; set; }

        /// <summary>
        /// The day of week for routine maintenance updates. * &#x60;monday&#x60; * &#x60;tuesday&#x60; * &#x60;wednesday&#x60; * &#x60;thursday&#x60; * &#x60;friday&#x60; * &#x60;saturday&#x60; * &#x60;sunday&#x60;
        /// </summary>
        /// <value>The day of week for routine maintenance updates. * &#x60;monday&#x60; * &#x60;tuesday&#x60; * &#x60;wednesday&#x60; * &#x60;thursday&#x60; * &#x60;friday&#x60; * &#x60;saturday&#x60; * &#x60;sunday&#x60;</value>
        [DataMember(Name = "maintenance_dow", EmitDefaultValue = false)]
        public string MaintenanceDow { get; set; }

        /// <summary>
        /// The preferred time (UTC) for routine maintenance updates to occur in 24-hour HH:00 format (e.g. &#x60;01:00&#x60;, &#x60;13:00&#x60;, &#x60;23:00&#x60;, etc.).
        /// </summary>
        /// <value>The preferred time (UTC) for routine maintenance updates to occur in 24-hour HH:00 format (e.g. &#x60;01:00&#x60;, &#x60;13:00&#x60;, &#x60;23:00&#x60;, etc.).</value>
        [DataMember(Name = "maintenance_time", EmitDefaultValue = false)]
        public string MaintenanceTime { get; set; }

        /// <summary>
        /// The configured time zone for the Managed Database in TZ database format (e.g. &#x60;UTC&#x60;, &#x60;America/New_York&#x60;, &#x60;Europe/London&#x60;, etc.).
        /// </summary>
        /// <value>The configured time zone for the Managed Database in TZ database format (e.g. &#x60;UTC&#x60;, &#x60;America/New_York&#x60;, &#x60;Europe/London&#x60;, etc.).</value>
        [DataMember(Name = "cluster_time_zone", EmitDefaultValue = false)]
        public string ClusterTimeZone { get; set; }

        /// <summary>
        /// A list of IP addresses allowed to access the Managed Database in CIDR notation (defaults to /32 if excluded).
        /// </summary>
        /// <value>A list of IP addresses allowed to access the Managed Database in CIDR notation (defaults to /32 if excluded).</value>
        [DataMember(Name = "trusted_ips", EmitDefaultValue = false)]
        public List<string> TrustedIps { get; set; }

        /// <summary>
        /// A list of SQL modes to enable on the Managed Database (MySQL engine type only). * &#x60;ALLOW_INVALID_DATES&#x60; * &#x60;ANSI&#x60; (Combination Mode) * &#x60;ANSI_QUOTES&#x60; * &#x60;ERROR_FOR_DIVISION_BY_ZERO&#x60; * &#x60;HIGH_NOT_PRECEDENCE&#x60; * &#x60;IGNORE_SPACE&#x60; * &#x60;NO_AUTO_VALUE_ON_ZERO&#x60; * &#x60;NO_DIR_IN_CREATE&#x60; * &#x60;NO_ENGINE_SUBSTITUTION&#x60; * &#x60;NO_UNSIGNED_SUBTRACTION&#x60; * &#x60;NO_ZERO_DATE&#x60; * &#x60;NO_ZERO_IN_DATE&#x60; * &#x60;ONLY_FULL_GROUP_BY&#x60; * &#x60;PIPES_AS_CONCAT&#x60; * &#x60;REAL_AS_FLOAT&#x60; * &#x60;STRICT_ALL_TABLES&#x60; * &#x60;STRICT_TRANS_TABLES&#x60; * &#x60;TIME_TRUNCATE_FRACTIONAL&#x60; * &#x60;TRADITIONAL&#x60; (Combination Mode)
        /// </summary>
        /// <value>A list of SQL modes to enable on the Managed Database (MySQL engine type only). * &#x60;ALLOW_INVALID_DATES&#x60; * &#x60;ANSI&#x60; (Combination Mode) * &#x60;ANSI_QUOTES&#x60; * &#x60;ERROR_FOR_DIVISION_BY_ZERO&#x60; * &#x60;HIGH_NOT_PRECEDENCE&#x60; * &#x60;IGNORE_SPACE&#x60; * &#x60;NO_AUTO_VALUE_ON_ZERO&#x60; * &#x60;NO_DIR_IN_CREATE&#x60; * &#x60;NO_ENGINE_SUBSTITUTION&#x60; * &#x60;NO_UNSIGNED_SUBTRACTION&#x60; * &#x60;NO_ZERO_DATE&#x60; * &#x60;NO_ZERO_IN_DATE&#x60; * &#x60;ONLY_FULL_GROUP_BY&#x60; * &#x60;PIPES_AS_CONCAT&#x60; * &#x60;REAL_AS_FLOAT&#x60; * &#x60;STRICT_ALL_TABLES&#x60; * &#x60;STRICT_TRANS_TABLES&#x60; * &#x60;TIME_TRUNCATE_FRACTIONAL&#x60; * &#x60;TRADITIONAL&#x60; (Combination Mode)</value>
        [DataMember(Name = "mysql_sql_modes", EmitDefaultValue = false)]
        public List<string> MysqlSqlModes { get; set; }

        /// <summary>
        /// Require a primary key for all tables on the Managed Database (MySQL engine type only).
        /// </summary>
        /// <value>Require a primary key for all tables on the Managed Database (MySQL engine type only).</value>
        [DataMember(Name = "mysql_require_primary_key", EmitDefaultValue = true)]
        public bool MysqlRequirePrimaryKey { get; set; }

        /// <summary>
        /// Enable or disable slow query logging on the Managed Database (MySQL engine type only).
        /// </summary>
        /// <value>Enable or disable slow query logging on the Managed Database (MySQL engine type only).</value>
        [DataMember(Name = "mysql_slow_query_log", EmitDefaultValue = true)]
        public bool MysqlSlowQueryLog { get; set; }

        /// <summary>
        /// The number of seconds to denote a slow query when logging is enabled (MySQL engine type only).
        /// </summary>
        /// <value>The number of seconds to denote a slow query when logging is enabled (MySQL engine type only).</value>
        [DataMember(Name = "mysql_long_query_time", EmitDefaultValue = false)]
        public int MysqlLongQueryTime { get; set; }

        /// <summary>
        /// Set the data eviction policy for the Managed Database (Redis engine type only)
        /// </summary>
        /// <value>Set the data eviction policy for the Managed Database (Redis engine type only)</value>
        [DataMember(Name = "redis_eviction_policy", EmitDefaultValue = false)]
        public string RedisEvictionPolicy { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateDatabaseRequest {\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  Plan: ").Append(Plan).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  VpcId: ").Append(VpcId).Append("\n");
            sb.Append("  MaintenanceDow: ").Append(MaintenanceDow).Append("\n");
            sb.Append("  MaintenanceTime: ").Append(MaintenanceTime).Append("\n");
            sb.Append("  ClusterTimeZone: ").Append(ClusterTimeZone).Append("\n");
            sb.Append("  TrustedIps: ").Append(TrustedIps).Append("\n");
            sb.Append("  MysqlSqlModes: ").Append(MysqlSqlModes).Append("\n");
            sb.Append("  MysqlRequirePrimaryKey: ").Append(MysqlRequirePrimaryKey).Append("\n");
            sb.Append("  MysqlSlowQueryLog: ").Append(MysqlSlowQueryLog).Append("\n");
            sb.Append("  MysqlLongQueryTime: ").Append(MysqlLongQueryTime).Append("\n");
            sb.Append("  RedisEvictionPolicy: ").Append(RedisEvictionPolicy).Append("\n");
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
