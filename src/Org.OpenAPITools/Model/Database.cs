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
    /// Managed Database information.
    /// </summary>
    [DataContract(Name = "database")]
    public partial class Database : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Database" /> class.
        /// </summary>
        /// <param name="id">A unique ID for the Managed Database..</param>
        /// <param name="dateCreated">The date this database was created..</param>
        /// <param name="plan">The name of the Managed Database plan..</param>
        /// <param name="planDisk">The size of the disk in GB (excluded for Redis engine types)..</param>
        /// <param name="planRam">The amount of RAM in MB..</param>
        /// <param name="planVcpus">Number of vCPUs..</param>
        /// <param name="planReplicas">Number of replica nodes..</param>
        /// <param name="region">The [Region id](#operation/list-regions) where the Managed Database is located..</param>
        /// <param name="databaseEngine">The database engine type (MySQL, PostgreSQL, FerretDB + PostgreSQL, Redis)..</param>
        /// <param name="databaseEngineVersion">The version number of the database engine in use..</param>
        /// <param name="vpcId">The ID of the [VPC Network](#operation/get-vpc) attached to the Managed Database..</param>
        /// <param name="status">The current status.  * Rebuilding * Rebalancing * Running.</param>
        /// <param name="label">The user-supplied label for this managed database..</param>
        /// <param name="tag">The user-supplied tag for this managed database..</param>
        /// <param name="dbname">The default database name..</param>
        /// <param name="ferretdbCredentials">ferretdbCredentials.</param>
        /// <param name="host">The public hostname for database connections, or private hostname if this managed database is attached to a VPC network..</param>
        /// <param name="publicHost">The public hostname for database connections. Only visible when the managed database is attached to a VPC network..</param>
        /// <param name="user">The default user configured on creation..</param>
        /// <param name="password">The default user&#39;s secure password generated on creation..</param>
        /// <param name="port">The assigned port for connecting to the Managed Database..</param>
        /// <param name="maintenanceDow">The chosen date of week for routine maintenance updates..</param>
        /// <param name="maintenanceTime">The chosen hour for routine maintenance updates..</param>
        /// <param name="latestBackup">The date for the latest backup stored on the Managed Database..</param>
        /// <param name="trustedIps">A list of trusted IP addresses for connecting to this Managed Database (in CIDR notation)..</param>
        /// <param name="mysqlSqlModes">A list names of enabled SQL Modes for MySQL engine types only..</param>
        /// <param name="mysqlRequirePrimaryKey">Configuration value for requiring table primary keys for MySQL engine types only..</param>
        /// <param name="mysqlSlowQueryLog">Configuration value for slow query logging on the Managed Database for MySQL engine types only..</param>
        /// <param name="mysqlLongQueryTime">The number of seconds to denote a slow query when logging is enabled for MySQL engine types only..</param>
        /// <param name="pgAvailableExtensions">A list of objects containing names and versions (when applicable) of available extensions for PostgreSQL engine types only..</param>
        /// <param name="redisEvictionPolicy">The current configured data eviction policy for Redis engine types only..</param>
        /// <param name="clusterTimeZone">The configured time zone of the Managed Database in TZ database format..</param>
        /// <param name="readReplicas">A list of database objects containing details for all attached read-only replica nodes..</param>
        public Database(string id = default(string), string dateCreated = default(string), string plan = default(string), int planDisk = default(int), int planRam = default(int), int planVcpus = default(int), int planReplicas = default(int), string region = default(string), string databaseEngine = default(string), string databaseEngineVersion = default(string), string vpcId = default(string), string status = default(string), string label = default(string), string tag = default(string), string dbname = default(string), DatabaseFerretdbCredentials ferretdbCredentials = default(DatabaseFerretdbCredentials), string host = default(string), string publicHost = default(string), string user = default(string), string password = default(string), string port = default(string), string maintenanceDow = default(string), string maintenanceTime = default(string), string latestBackup = default(string), List<string> trustedIps = default(List<string>), List<string> mysqlSqlModes = default(List<string>), bool mysqlRequirePrimaryKey = default(bool), bool mysqlSlowQueryLog = default(bool), int mysqlLongQueryTime = default(int), List<Object> pgAvailableExtensions = default(List<Object>), string redisEvictionPolicy = default(string), string clusterTimeZone = default(string), List<Object> readReplicas = default(List<Object>))
        {
            this.Id = id;
            this.DateCreated = dateCreated;
            this.Plan = plan;
            this.PlanDisk = planDisk;
            this.PlanRam = planRam;
            this.PlanVcpus = planVcpus;
            this.PlanReplicas = planReplicas;
            this.Region = region;
            this.DatabaseEngine = databaseEngine;
            this.DatabaseEngineVersion = databaseEngineVersion;
            this.VpcId = vpcId;
            this.Status = status;
            this.Label = label;
            this.Tag = tag;
            this.Dbname = dbname;
            this.FerretdbCredentials = ferretdbCredentials;
            this.Host = host;
            this.PublicHost = publicHost;
            this.User = user;
            this.Password = password;
            this.Port = port;
            this.MaintenanceDow = maintenanceDow;
            this.MaintenanceTime = maintenanceTime;
            this.LatestBackup = latestBackup;
            this.TrustedIps = trustedIps;
            this.MysqlSqlModes = mysqlSqlModes;
            this.MysqlRequirePrimaryKey = mysqlRequirePrimaryKey;
            this.MysqlSlowQueryLog = mysqlSlowQueryLog;
            this.MysqlLongQueryTime = mysqlLongQueryTime;
            this.PgAvailableExtensions = pgAvailableExtensions;
            this.RedisEvictionPolicy = redisEvictionPolicy;
            this.ClusterTimeZone = clusterTimeZone;
            this.ReadReplicas = readReplicas;
        }

        /// <summary>
        /// A unique ID for the Managed Database.
        /// </summary>
        /// <value>A unique ID for the Managed Database.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The date this database was created.
        /// </summary>
        /// <value>The date this database was created.</value>
        [DataMember(Name = "date_created", EmitDefaultValue = false)]
        public string DateCreated { get; set; }

        /// <summary>
        /// The name of the Managed Database plan.
        /// </summary>
        /// <value>The name of the Managed Database plan.</value>
        [DataMember(Name = "plan", EmitDefaultValue = false)]
        public string Plan { get; set; }

        /// <summary>
        /// The size of the disk in GB (excluded for Redis engine types).
        /// </summary>
        /// <value>The size of the disk in GB (excluded for Redis engine types).</value>
        [DataMember(Name = "plan_disk", EmitDefaultValue = false)]
        public int PlanDisk { get; set; }

        /// <summary>
        /// The amount of RAM in MB.
        /// </summary>
        /// <value>The amount of RAM in MB.</value>
        [DataMember(Name = "plan_ram", EmitDefaultValue = false)]
        public int PlanRam { get; set; }

        /// <summary>
        /// Number of vCPUs.
        /// </summary>
        /// <value>Number of vCPUs.</value>
        [DataMember(Name = "plan_vcpus", EmitDefaultValue = false)]
        public int PlanVcpus { get; set; }

        /// <summary>
        /// Number of replica nodes.
        /// </summary>
        /// <value>Number of replica nodes.</value>
        [DataMember(Name = "plan_replicas", EmitDefaultValue = false)]
        public int PlanReplicas { get; set; }

        /// <summary>
        /// The [Region id](#operation/list-regions) where the Managed Database is located.
        /// </summary>
        /// <value>The [Region id](#operation/list-regions) where the Managed Database is located.</value>
        [DataMember(Name = "region", EmitDefaultValue = false)]
        public string Region { get; set; }

        /// <summary>
        /// The database engine type (MySQL, PostgreSQL, FerretDB + PostgreSQL, Redis).
        /// </summary>
        /// <value>The database engine type (MySQL, PostgreSQL, FerretDB + PostgreSQL, Redis).</value>
        [DataMember(Name = "database_engine", EmitDefaultValue = false)]
        public string DatabaseEngine { get; set; }

        /// <summary>
        /// The version number of the database engine in use.
        /// </summary>
        /// <value>The version number of the database engine in use.</value>
        [DataMember(Name = "database_engine_version", EmitDefaultValue = false)]
        public string DatabaseEngineVersion { get; set; }

        /// <summary>
        /// The ID of the [VPC Network](#operation/get-vpc) attached to the Managed Database.
        /// </summary>
        /// <value>The ID of the [VPC Network](#operation/get-vpc) attached to the Managed Database.</value>
        [DataMember(Name = "vpc_id", EmitDefaultValue = false)]
        public string VpcId { get; set; }

        /// <summary>
        /// The current status.  * Rebuilding * Rebalancing * Running
        /// </summary>
        /// <value>The current status.  * Rebuilding * Rebalancing * Running</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// The user-supplied label for this managed database.
        /// </summary>
        /// <value>The user-supplied label for this managed database.</value>
        [DataMember(Name = "label", EmitDefaultValue = false)]
        public string Label { get; set; }

        /// <summary>
        /// The user-supplied tag for this managed database.
        /// </summary>
        /// <value>The user-supplied tag for this managed database.</value>
        [DataMember(Name = "tag", EmitDefaultValue = false)]
        public string Tag { get; set; }

        /// <summary>
        /// The default database name.
        /// </summary>
        /// <value>The default database name.</value>
        [DataMember(Name = "dbname", EmitDefaultValue = false)]
        public string Dbname { get; set; }

        /// <summary>
        /// Gets or Sets FerretdbCredentials
        /// </summary>
        [DataMember(Name = "ferretdb_credentials", EmitDefaultValue = false)]
        public DatabaseFerretdbCredentials FerretdbCredentials { get; set; }

        /// <summary>
        /// The public hostname for database connections, or private hostname if this managed database is attached to a VPC network.
        /// </summary>
        /// <value>The public hostname for database connections, or private hostname if this managed database is attached to a VPC network.</value>
        [DataMember(Name = "host", EmitDefaultValue = false)]
        public string Host { get; set; }

        /// <summary>
        /// The public hostname for database connections. Only visible when the managed database is attached to a VPC network.
        /// </summary>
        /// <value>The public hostname for database connections. Only visible when the managed database is attached to a VPC network.</value>
        [DataMember(Name = "public_host", EmitDefaultValue = false)]
        public string PublicHost { get; set; }

        /// <summary>
        /// The default user configured on creation.
        /// </summary>
        /// <value>The default user configured on creation.</value>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        public string User { get; set; }

        /// <summary>
        /// The default user&#39;s secure password generated on creation.
        /// </summary>
        /// <value>The default user&#39;s secure password generated on creation.</value>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        /// The assigned port for connecting to the Managed Database.
        /// </summary>
        /// <value>The assigned port for connecting to the Managed Database.</value>
        [DataMember(Name = "port", EmitDefaultValue = false)]
        public string Port { get; set; }

        /// <summary>
        /// The chosen date of week for routine maintenance updates.
        /// </summary>
        /// <value>The chosen date of week for routine maintenance updates.</value>
        [DataMember(Name = "maintenance_dow", EmitDefaultValue = false)]
        public string MaintenanceDow { get; set; }

        /// <summary>
        /// The chosen hour for routine maintenance updates.
        /// </summary>
        /// <value>The chosen hour for routine maintenance updates.</value>
        [DataMember(Name = "maintenance_time", EmitDefaultValue = false)]
        public string MaintenanceTime { get; set; }

        /// <summary>
        /// The date for the latest backup stored on the Managed Database.
        /// </summary>
        /// <value>The date for the latest backup stored on the Managed Database.</value>
        [DataMember(Name = "latest_backup", EmitDefaultValue = false)]
        public string LatestBackup { get; set; }

        /// <summary>
        /// A list of trusted IP addresses for connecting to this Managed Database (in CIDR notation).
        /// </summary>
        /// <value>A list of trusted IP addresses for connecting to this Managed Database (in CIDR notation).</value>
        [DataMember(Name = "trusted_ips", EmitDefaultValue = false)]
        public List<string> TrustedIps { get; set; }

        /// <summary>
        /// A list names of enabled SQL Modes for MySQL engine types only.
        /// </summary>
        /// <value>A list names of enabled SQL Modes for MySQL engine types only.</value>
        [DataMember(Name = "mysql_sql_modes", EmitDefaultValue = false)]
        public List<string> MysqlSqlModes { get; set; }

        /// <summary>
        /// Configuration value for requiring table primary keys for MySQL engine types only.
        /// </summary>
        /// <value>Configuration value for requiring table primary keys for MySQL engine types only.</value>
        [DataMember(Name = "mysql_require_primary_key", EmitDefaultValue = true)]
        public bool MysqlRequirePrimaryKey { get; set; }

        /// <summary>
        /// Configuration value for slow query logging on the Managed Database for MySQL engine types only.
        /// </summary>
        /// <value>Configuration value for slow query logging on the Managed Database for MySQL engine types only.</value>
        [DataMember(Name = "mysql_slow_query_log", EmitDefaultValue = true)]
        public bool MysqlSlowQueryLog { get; set; }

        /// <summary>
        /// The number of seconds to denote a slow query when logging is enabled for MySQL engine types only.
        /// </summary>
        /// <value>The number of seconds to denote a slow query when logging is enabled for MySQL engine types only.</value>
        [DataMember(Name = "mysql_long_query_time", EmitDefaultValue = false)]
        public int MysqlLongQueryTime { get; set; }

        /// <summary>
        /// A list of objects containing names and versions (when applicable) of available extensions for PostgreSQL engine types only.
        /// </summary>
        /// <value>A list of objects containing names and versions (when applicable) of available extensions for PostgreSQL engine types only.</value>
        [DataMember(Name = "pg_available_extensions", EmitDefaultValue = false)]
        public List<Object> PgAvailableExtensions { get; set; }

        /// <summary>
        /// The current configured data eviction policy for Redis engine types only.
        /// </summary>
        /// <value>The current configured data eviction policy for Redis engine types only.</value>
        [DataMember(Name = "redis_eviction_policy", EmitDefaultValue = false)]
        public string RedisEvictionPolicy { get; set; }

        /// <summary>
        /// The configured time zone of the Managed Database in TZ database format.
        /// </summary>
        /// <value>The configured time zone of the Managed Database in TZ database format.</value>
        [DataMember(Name = "cluster_time_zone", EmitDefaultValue = false)]
        public string ClusterTimeZone { get; set; }

        /// <summary>
        /// A list of database objects containing details for all attached read-only replica nodes.
        /// </summary>
        /// <value>A list of database objects containing details for all attached read-only replica nodes.</value>
        [DataMember(Name = "read_replicas", EmitDefaultValue = false)]
        public List<Object> ReadReplicas { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Database {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  DateCreated: ").Append(DateCreated).Append("\n");
            sb.Append("  Plan: ").Append(Plan).Append("\n");
            sb.Append("  PlanDisk: ").Append(PlanDisk).Append("\n");
            sb.Append("  PlanRam: ").Append(PlanRam).Append("\n");
            sb.Append("  PlanVcpus: ").Append(PlanVcpus).Append("\n");
            sb.Append("  PlanReplicas: ").Append(PlanReplicas).Append("\n");
            sb.Append("  Region: ").Append(Region).Append("\n");
            sb.Append("  DatabaseEngine: ").Append(DatabaseEngine).Append("\n");
            sb.Append("  DatabaseEngineVersion: ").Append(DatabaseEngineVersion).Append("\n");
            sb.Append("  VpcId: ").Append(VpcId).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  Dbname: ").Append(Dbname).Append("\n");
            sb.Append("  FerretdbCredentials: ").Append(FerretdbCredentials).Append("\n");
            sb.Append("  Host: ").Append(Host).Append("\n");
            sb.Append("  PublicHost: ").Append(PublicHost).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  Port: ").Append(Port).Append("\n");
            sb.Append("  MaintenanceDow: ").Append(MaintenanceDow).Append("\n");
            sb.Append("  MaintenanceTime: ").Append(MaintenanceTime).Append("\n");
            sb.Append("  LatestBackup: ").Append(LatestBackup).Append("\n");
            sb.Append("  TrustedIps: ").Append(TrustedIps).Append("\n");
            sb.Append("  MysqlSqlModes: ").Append(MysqlSqlModes).Append("\n");
            sb.Append("  MysqlRequirePrimaryKey: ").Append(MysqlRequirePrimaryKey).Append("\n");
            sb.Append("  MysqlSlowQueryLog: ").Append(MysqlSlowQueryLog).Append("\n");
            sb.Append("  MysqlLongQueryTime: ").Append(MysqlLongQueryTime).Append("\n");
            sb.Append("  PgAvailableExtensions: ").Append(PgAvailableExtensions).Append("\n");
            sb.Append("  RedisEvictionPolicy: ").Append(RedisEvictionPolicy).Append("\n");
            sb.Append("  ClusterTimeZone: ").Append(ClusterTimeZone).Append("\n");
            sb.Append("  ReadReplicas: ").Append(ReadReplicas).Append("\n");
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
