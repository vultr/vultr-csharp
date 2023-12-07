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
    /// UpdateAdvancedOptionsRequest
    /// </summary>
    [DataContract(Name = "update_advanced_options_request")]
    public partial class UpdateAdvancedOptionsRequest : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAdvancedOptionsRequest" /> class.
        /// </summary>
        /// <param name="autovacuumAnalyzeScaleFactor">Accepted values: 0 - 1.</param>
        /// <param name="autovacuumAnalyzeThreshold">Accepted values: 0 - 2147483647.</param>
        /// <param name="autovacuumFreezeMaxAge">Accepted values: 200000000 - 1500000000.</param>
        /// <param name="autovacuumMaxWorkers">Accepted values: 1 - 20.</param>
        /// <param name="autovacuumNaptime">Accepted values: 1 - 86400.</param>
        /// <param name="autovacuumVacuumCostDelay">Accepted values: -1 - 100.</param>
        /// <param name="autovacuumVacuumCostLimit">Accepted values: -1 - 10000.</param>
        /// <param name="autovacuumVacuumScaleFactor">Accepted values: 0 - 1.</param>
        /// <param name="autovacuumVacuumThreshold">Accepted values: 0 - 2147483647.</param>
        /// <param name="bgwriterDelay">Accepted values: 10 - 10000.</param>
        /// <param name="bgwriterFlushAfter">Accepted values: 0 - 2048.</param>
        /// <param name="bgwriterLruMaxpages">Accepted values: 0 - 1073741823.</param>
        /// <param name="bgwriterLruMultiplier">Accepted values: 0 - 10.</param>
        /// <param name="deadlockTimeout">Accepted values: 500 - 1800000.</param>
        /// <param name="defaultToastCompression">Accepted values: * &#x60;lz4&#x60; * &#x60;pglz&#x60;.</param>
        /// <param name="idleInTransactionSessionTimeout">Accepted values: 0 - 604800000.</param>
        /// <param name="jit">Accepted values: * &#x60;true&#x60; * &#x60;false&#x60;.</param>
        /// <param name="logAutovacuumMinDuration">Accepted values: -1 - 2147483647.</param>
        /// <param name="logErrorVerbosity">Accepted values: * &#x60;TERSE&#x60; * &#x60;DEFAULT&#x60; * &#x60;VERBOSE&#x60;.</param>
        /// <param name="logLinePrefix">Accepted values: * &#x60;&#39;pid&#x3D;%p,user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%t [%p]: [%l-1] user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%m [%p] %q[user&#x3D;%u,db&#x3D;%d,app&#x3D;%a] &#39;&#x60;.</param>
        /// <param name="logMinDurationStatement">Accepted values: -1 - 86400000.</param>
        /// <param name="maxFilesPerProcess">Accepted values: 1000 - 4096.</param>
        /// <param name="maxLocksPerTransaction">Accepted values: 64 - 6400.</param>
        /// <param name="maxLogicalReplicationWorkers">Accepted values: 4 - 64.</param>
        /// <param name="maxParallelWorkers">Accepted values: 0 - 96.</param>
        /// <param name="maxParallelWorkersPerGather">Accepted values: 0 - 96.</param>
        /// <param name="maxPredLocksPerTransaction">Accepted values: 64 - 5120.</param>
        /// <param name="maxPreparedTransactions">Accepted values: 0 - 10000.</param>
        /// <param name="maxReplicationSlots">Accepted values: 8 - 64.</param>
        /// <param name="maxStackDepth">Accepted values: 2097152 - 6291456.</param>
        /// <param name="maxStandbyArchiveDelay">Accepted values: 1 - 43200000.</param>
        /// <param name="maxStandbyStreamingDelay">Accepted values: 1 - 43200000.</param>
        /// <param name="maxWalSenders">Accepted values: 20 - 64.</param>
        /// <param name="maxWorkerProcesses">Accepted values: 8 - 96.</param>
        /// <param name="pgPartmanBgwInterval">Accepted values: 3600 - 604800.</param>
        /// <param name="pgPartmanBgwRole">Maximum length: 64 characters.</param>
        /// <param name="pgStatStatementsTrack">Accepted values: * &#x60;all&#x60; * &#x60;top&#x60; * &#x60;none&#x60;.</param>
        /// <param name="tempFileLimit">Accepted values: -1 - 2147483647.</param>
        /// <param name="trackActivityQuerySize">Accepted values: 1024 - 10240.</param>
        /// <param name="trackCommitTimestamp">Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;.</param>
        /// <param name="trackFunctions">Accepted values: * &#x60;all&#x60; * &#x60;pl&#x60; * &#x60;none&#x60;.</param>
        /// <param name="trackIoTiming">Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;.</param>
        /// <param name="walSenderTimeout">Accepted values: 0, 5000 - 10800000.</param>
        /// <param name="walWriterDelay">Accepted values: 10 - 200.</param>
        public UpdateAdvancedOptionsRequest(float autovacuumAnalyzeScaleFactor = default(float), Int autovacuumAnalyzeThreshold = default(Int), Int autovacuumFreezeMaxAge = default(Int), Int autovacuumMaxWorkers = default(Int), Int autovacuumNaptime = default(Int), Int autovacuumVacuumCostDelay = default(Int), Int autovacuumVacuumCostLimit = default(Int), float autovacuumVacuumScaleFactor = default(float), Int autovacuumVacuumThreshold = default(Int), Int bgwriterDelay = default(Int), Int bgwriterFlushAfter = default(Int), Int bgwriterLruMaxpages = default(Int), float bgwriterLruMultiplier = default(float), Int deadlockTimeout = default(Int), Enum defaultToastCompression = default(Enum), Int idleInTransactionSessionTimeout = default(Int), bool jit = default(bool), Int logAutovacuumMinDuration = default(Int), Enum logErrorVerbosity = default(Enum), Enum logLinePrefix = default(Enum), Int logMinDurationStatement = default(Int), Int maxFilesPerProcess = default(Int), Int maxLocksPerTransaction = default(Int), Int maxLogicalReplicationWorkers = default(Int), Int maxParallelWorkers = default(Int), Int maxParallelWorkersPerGather = default(Int), Int maxPredLocksPerTransaction = default(Int), Int maxPreparedTransactions = default(Int), Int maxReplicationSlots = default(Int), Int maxStackDepth = default(Int), Int maxStandbyArchiveDelay = default(Int), Int maxStandbyStreamingDelay = default(Int), Int maxWalSenders = default(Int), Int maxWorkerProcesses = default(Int), Int pgPartmanBgwInterval = default(Int), string pgPartmanBgwRole = default(string), Enum pgStatStatementsTrack = default(Enum), Int tempFileLimit = default(Int), Int trackActivityQuerySize = default(Int), Enum trackCommitTimestamp = default(Enum), Enum trackFunctions = default(Enum), Enum trackIoTiming = default(Enum), Int walSenderTimeout = default(Int), Int walWriterDelay = default(Int))
        {
            this.AutovacuumAnalyzeScaleFactor = autovacuumAnalyzeScaleFactor;
            this.AutovacuumAnalyzeThreshold = autovacuumAnalyzeThreshold;
            this.AutovacuumFreezeMaxAge = autovacuumFreezeMaxAge;
            this.AutovacuumMaxWorkers = autovacuumMaxWorkers;
            this.AutovacuumNaptime = autovacuumNaptime;
            this.AutovacuumVacuumCostDelay = autovacuumVacuumCostDelay;
            this.AutovacuumVacuumCostLimit = autovacuumVacuumCostLimit;
            this.AutovacuumVacuumScaleFactor = autovacuumVacuumScaleFactor;
            this.AutovacuumVacuumThreshold = autovacuumVacuumThreshold;
            this.BgwriterDelay = bgwriterDelay;
            this.BgwriterFlushAfter = bgwriterFlushAfter;
            this.BgwriterLruMaxpages = bgwriterLruMaxpages;
            this.BgwriterLruMultiplier = bgwriterLruMultiplier;
            this.DeadlockTimeout = deadlockTimeout;
            this.DefaultToastCompression = defaultToastCompression;
            this.IdleInTransactionSessionTimeout = idleInTransactionSessionTimeout;
            this.Jit = jit;
            this.LogAutovacuumMinDuration = logAutovacuumMinDuration;
            this.LogErrorVerbosity = logErrorVerbosity;
            this.LogLinePrefix = logLinePrefix;
            this.LogMinDurationStatement = logMinDurationStatement;
            this.MaxFilesPerProcess = maxFilesPerProcess;
            this.MaxLocksPerTransaction = maxLocksPerTransaction;
            this.MaxLogicalReplicationWorkers = maxLogicalReplicationWorkers;
            this.MaxParallelWorkers = maxParallelWorkers;
            this.MaxParallelWorkersPerGather = maxParallelWorkersPerGather;
            this.MaxPredLocksPerTransaction = maxPredLocksPerTransaction;
            this.MaxPreparedTransactions = maxPreparedTransactions;
            this.MaxReplicationSlots = maxReplicationSlots;
            this.MaxStackDepth = maxStackDepth;
            this.MaxStandbyArchiveDelay = maxStandbyArchiveDelay;
            this.MaxStandbyStreamingDelay = maxStandbyStreamingDelay;
            this.MaxWalSenders = maxWalSenders;
            this.MaxWorkerProcesses = maxWorkerProcesses;
            this.PgPartmanBgwInterval = pgPartmanBgwInterval;
            this.PgPartmanBgwRole = pgPartmanBgwRole;
            this.PgStatStatementsTrack = pgStatStatementsTrack;
            this.TempFileLimit = tempFileLimit;
            this.TrackActivityQuerySize = trackActivityQuerySize;
            this.TrackCommitTimestamp = trackCommitTimestamp;
            this.TrackFunctions = trackFunctions;
            this.TrackIoTiming = trackIoTiming;
            this.WalSenderTimeout = walSenderTimeout;
            this.WalWriterDelay = walWriterDelay;
        }

        /// <summary>
        /// Accepted values: 0 - 1
        /// </summary>
        /// <value>Accepted values: 0 - 1</value>
        [DataMember(Name = "autovacuum_analyze_scale_factor", EmitDefaultValue = false)]
        public float AutovacuumAnalyzeScaleFactor { get; set; }

        /// <summary>
        /// Accepted values: 0 - 2147483647
        /// </summary>
        /// <value>Accepted values: 0 - 2147483647</value>
        [DataMember(Name = "autovacuum_analyze_threshold", EmitDefaultValue = false)]
        public Int AutovacuumAnalyzeThreshold { get; set; }

        /// <summary>
        /// Accepted values: 200000000 - 1500000000
        /// </summary>
        /// <value>Accepted values: 200000000 - 1500000000</value>
        [DataMember(Name = "autovacuum_freeze_max_age", EmitDefaultValue = false)]
        public Int AutovacuumFreezeMaxAge { get; set; }

        /// <summary>
        /// Accepted values: 1 - 20
        /// </summary>
        /// <value>Accepted values: 1 - 20</value>
        [DataMember(Name = "autovacuum_max_workers", EmitDefaultValue = false)]
        public Int AutovacuumMaxWorkers { get; set; }

        /// <summary>
        /// Accepted values: 1 - 86400
        /// </summary>
        /// <value>Accepted values: 1 - 86400</value>
        [DataMember(Name = "autovacuum_naptime", EmitDefaultValue = false)]
        public Int AutovacuumNaptime { get; set; }

        /// <summary>
        /// Accepted values: -1 - 100
        /// </summary>
        /// <value>Accepted values: -1 - 100</value>
        [DataMember(Name = "autovacuum_vacuum_cost_delay", EmitDefaultValue = false)]
        public Int AutovacuumVacuumCostDelay { get; set; }

        /// <summary>
        /// Accepted values: -1 - 10000
        /// </summary>
        /// <value>Accepted values: -1 - 10000</value>
        [DataMember(Name = "autovacuum_vacuum_cost_limit", EmitDefaultValue = false)]
        public Int AutovacuumVacuumCostLimit { get; set; }

        /// <summary>
        /// Accepted values: 0 - 1
        /// </summary>
        /// <value>Accepted values: 0 - 1</value>
        [DataMember(Name = "autovacuum_vacuum_scale_factor", EmitDefaultValue = false)]
        public float AutovacuumVacuumScaleFactor { get; set; }

        /// <summary>
        /// Accepted values: 0 - 2147483647
        /// </summary>
        /// <value>Accepted values: 0 - 2147483647</value>
        [DataMember(Name = "autovacuum_vacuum_threshold", EmitDefaultValue = false)]
        public Int AutovacuumVacuumThreshold { get; set; }

        /// <summary>
        /// Accepted values: 10 - 10000
        /// </summary>
        /// <value>Accepted values: 10 - 10000</value>
        [DataMember(Name = "bgwriter_delay", EmitDefaultValue = false)]
        public Int BgwriterDelay { get; set; }

        /// <summary>
        /// Accepted values: 0 - 2048
        /// </summary>
        /// <value>Accepted values: 0 - 2048</value>
        [DataMember(Name = "bgwriter_flush_after", EmitDefaultValue = false)]
        public Int BgwriterFlushAfter { get; set; }

        /// <summary>
        /// Accepted values: 0 - 1073741823
        /// </summary>
        /// <value>Accepted values: 0 - 1073741823</value>
        [DataMember(Name = "bgwriter_lru_maxpages", EmitDefaultValue = false)]
        public Int BgwriterLruMaxpages { get; set; }

        /// <summary>
        /// Accepted values: 0 - 10
        /// </summary>
        /// <value>Accepted values: 0 - 10</value>
        [DataMember(Name = "bgwriter_lru_multiplier", EmitDefaultValue = false)]
        public float BgwriterLruMultiplier { get; set; }

        /// <summary>
        /// Accepted values: 500 - 1800000
        /// </summary>
        /// <value>Accepted values: 500 - 1800000</value>
        [DataMember(Name = "deadlock_timeout", EmitDefaultValue = false)]
        public Int DeadlockTimeout { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;lz4&#x60; * &#x60;pglz&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;lz4&#x60; * &#x60;pglz&#x60;</value>
        [DataMember(Name = "default_toast_compression", EmitDefaultValue = false)]
        public Enum DefaultToastCompression { get; set; }

        /// <summary>
        /// Accepted values: 0 - 604800000
        /// </summary>
        /// <value>Accepted values: 0 - 604800000</value>
        [DataMember(Name = "idle_in_transaction_session_timeout", EmitDefaultValue = false)]
        public Int IdleInTransactionSessionTimeout { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;true&#x60; * &#x60;false&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;true&#x60; * &#x60;false&#x60;</value>
        [DataMember(Name = "jit", EmitDefaultValue = true)]
        public bool Jit { get; set; }

        /// <summary>
        /// Accepted values: -1 - 2147483647
        /// </summary>
        /// <value>Accepted values: -1 - 2147483647</value>
        [DataMember(Name = "log_autovacuum_min_duration", EmitDefaultValue = false)]
        public Int LogAutovacuumMinDuration { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;TERSE&#x60; * &#x60;DEFAULT&#x60; * &#x60;VERBOSE&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;TERSE&#x60; * &#x60;DEFAULT&#x60; * &#x60;VERBOSE&#x60;</value>
        [DataMember(Name = "log_error_verbosity", EmitDefaultValue = false)]
        public Enum LogErrorVerbosity { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;&#39;pid&#x3D;%p,user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%t [%p]: [%l-1] user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%m [%p] %q[user&#x3D;%u,db&#x3D;%d,app&#x3D;%a] &#39;&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;&#39;pid&#x3D;%p,user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%t [%p]: [%l-1] user&#x3D;%u,db&#x3D;%d,app&#x3D;%a,client&#x3D;%h &#39;&#x60; * &#x60;&#39;%m [%p] %q[user&#x3D;%u,db&#x3D;%d,app&#x3D;%a] &#39;&#x60;</value>
        [DataMember(Name = "log_line_prefix", EmitDefaultValue = false)]
        public Enum LogLinePrefix { get; set; }

        /// <summary>
        /// Accepted values: -1 - 86400000
        /// </summary>
        /// <value>Accepted values: -1 - 86400000</value>
        [DataMember(Name = "log_min_duration_statement", EmitDefaultValue = false)]
        public Int LogMinDurationStatement { get; set; }

        /// <summary>
        /// Accepted values: 1000 - 4096
        /// </summary>
        /// <value>Accepted values: 1000 - 4096</value>
        [DataMember(Name = "max_files_per_process", EmitDefaultValue = false)]
        public Int MaxFilesPerProcess { get; set; }

        /// <summary>
        /// Accepted values: 64 - 6400
        /// </summary>
        /// <value>Accepted values: 64 - 6400</value>
        [DataMember(Name = "max_locks_per_transaction", EmitDefaultValue = false)]
        public Int MaxLocksPerTransaction { get; set; }

        /// <summary>
        /// Accepted values: 4 - 64
        /// </summary>
        /// <value>Accepted values: 4 - 64</value>
        [DataMember(Name = "max_logical_replication_workers", EmitDefaultValue = false)]
        public Int MaxLogicalReplicationWorkers { get; set; }

        /// <summary>
        /// Accepted values: 0 - 96
        /// </summary>
        /// <value>Accepted values: 0 - 96</value>
        [DataMember(Name = "max_parallel_workers", EmitDefaultValue = false)]
        public Int MaxParallelWorkers { get; set; }

        /// <summary>
        /// Accepted values: 0 - 96
        /// </summary>
        /// <value>Accepted values: 0 - 96</value>
        [DataMember(Name = "max_parallel_workers_per_gather", EmitDefaultValue = false)]
        public Int MaxParallelWorkersPerGather { get; set; }

        /// <summary>
        /// Accepted values: 64 - 5120
        /// </summary>
        /// <value>Accepted values: 64 - 5120</value>
        [DataMember(Name = "max_pred_locks_per_transaction", EmitDefaultValue = false)]
        public Int MaxPredLocksPerTransaction { get; set; }

        /// <summary>
        /// Accepted values: 0 - 10000
        /// </summary>
        /// <value>Accepted values: 0 - 10000</value>
        [DataMember(Name = "max_prepared_transactions", EmitDefaultValue = false)]
        public Int MaxPreparedTransactions { get; set; }

        /// <summary>
        /// Accepted values: 8 - 64
        /// </summary>
        /// <value>Accepted values: 8 - 64</value>
        [DataMember(Name = "max_replication_slots", EmitDefaultValue = false)]
        public Int MaxReplicationSlots { get; set; }

        /// <summary>
        /// Accepted values: 2097152 - 6291456
        /// </summary>
        /// <value>Accepted values: 2097152 - 6291456</value>
        [DataMember(Name = "max_stack_depth", EmitDefaultValue = false)]
        public Int MaxStackDepth { get; set; }

        /// <summary>
        /// Accepted values: 1 - 43200000
        /// </summary>
        /// <value>Accepted values: 1 - 43200000</value>
        [DataMember(Name = "max_standby_archive_delay", EmitDefaultValue = false)]
        public Int MaxStandbyArchiveDelay { get; set; }

        /// <summary>
        /// Accepted values: 1 - 43200000
        /// </summary>
        /// <value>Accepted values: 1 - 43200000</value>
        [DataMember(Name = "max_standby_streaming_delay", EmitDefaultValue = false)]
        public Int MaxStandbyStreamingDelay { get; set; }

        /// <summary>
        /// Accepted values: 20 - 64
        /// </summary>
        /// <value>Accepted values: 20 - 64</value>
        [DataMember(Name = "max_wal_senders", EmitDefaultValue = false)]
        public Int MaxWalSenders { get; set; }

        /// <summary>
        /// Accepted values: 8 - 96
        /// </summary>
        /// <value>Accepted values: 8 - 96</value>
        [DataMember(Name = "max_worker_processes", EmitDefaultValue = false)]
        public Int MaxWorkerProcesses { get; set; }

        /// <summary>
        /// Accepted values: 3600 - 604800
        /// </summary>
        /// <value>Accepted values: 3600 - 604800</value>
        [DataMember(Name = "pg_partman_bgw.interval", EmitDefaultValue = false)]
        public Int PgPartmanBgwInterval { get; set; }

        /// <summary>
        /// Maximum length: 64 characters
        /// </summary>
        /// <value>Maximum length: 64 characters</value>
        [DataMember(Name = "pg_partman_bgw.role", EmitDefaultValue = false)]
        public string PgPartmanBgwRole { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;all&#x60; * &#x60;top&#x60; * &#x60;none&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;all&#x60; * &#x60;top&#x60; * &#x60;none&#x60;</value>
        [DataMember(Name = "pg_stat_statements.track", EmitDefaultValue = false)]
        public Enum PgStatStatementsTrack { get; set; }

        /// <summary>
        /// Accepted values: -1 - 2147483647
        /// </summary>
        /// <value>Accepted values: -1 - 2147483647</value>
        [DataMember(Name = "temp_file_limit", EmitDefaultValue = false)]
        public Int TempFileLimit { get; set; }

        /// <summary>
        /// Accepted values: 1024 - 10240
        /// </summary>
        /// <value>Accepted values: 1024 - 10240</value>
        [DataMember(Name = "track_activity_query_size", EmitDefaultValue = false)]
        public Int TrackActivityQuerySize { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;</value>
        [DataMember(Name = "track_commit_timestamp", EmitDefaultValue = false)]
        public Enum TrackCommitTimestamp { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;all&#x60; * &#x60;pl&#x60; * &#x60;none&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;all&#x60; * &#x60;pl&#x60; * &#x60;none&#x60;</value>
        [DataMember(Name = "track_functions", EmitDefaultValue = false)]
        public Enum TrackFunctions { get; set; }

        /// <summary>
        /// Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;
        /// </summary>
        /// <value>Accepted values: * &#x60;off&#x60; * &#x60;on&#x60;</value>
        [DataMember(Name = "track_io_timing", EmitDefaultValue = false)]
        public Enum TrackIoTiming { get; set; }

        /// <summary>
        /// Accepted values: 0, 5000 - 10800000
        /// </summary>
        /// <value>Accepted values: 0, 5000 - 10800000</value>
        [DataMember(Name = "wal_sender_timeout", EmitDefaultValue = false)]
        public Int WalSenderTimeout { get; set; }

        /// <summary>
        /// Accepted values: 10 - 200
        /// </summary>
        /// <value>Accepted values: 10 - 200</value>
        [DataMember(Name = "wal_writer_delay", EmitDefaultValue = false)]
        public Int WalWriterDelay { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class UpdateAdvancedOptionsRequest {\n");
            sb.Append("  AutovacuumAnalyzeScaleFactor: ").Append(AutovacuumAnalyzeScaleFactor).Append("\n");
            sb.Append("  AutovacuumAnalyzeThreshold: ").Append(AutovacuumAnalyzeThreshold).Append("\n");
            sb.Append("  AutovacuumFreezeMaxAge: ").Append(AutovacuumFreezeMaxAge).Append("\n");
            sb.Append("  AutovacuumMaxWorkers: ").Append(AutovacuumMaxWorkers).Append("\n");
            sb.Append("  AutovacuumNaptime: ").Append(AutovacuumNaptime).Append("\n");
            sb.Append("  AutovacuumVacuumCostDelay: ").Append(AutovacuumVacuumCostDelay).Append("\n");
            sb.Append("  AutovacuumVacuumCostLimit: ").Append(AutovacuumVacuumCostLimit).Append("\n");
            sb.Append("  AutovacuumVacuumScaleFactor: ").Append(AutovacuumVacuumScaleFactor).Append("\n");
            sb.Append("  AutovacuumVacuumThreshold: ").Append(AutovacuumVacuumThreshold).Append("\n");
            sb.Append("  BgwriterDelay: ").Append(BgwriterDelay).Append("\n");
            sb.Append("  BgwriterFlushAfter: ").Append(BgwriterFlushAfter).Append("\n");
            sb.Append("  BgwriterLruMaxpages: ").Append(BgwriterLruMaxpages).Append("\n");
            sb.Append("  BgwriterLruMultiplier: ").Append(BgwriterLruMultiplier).Append("\n");
            sb.Append("  DeadlockTimeout: ").Append(DeadlockTimeout).Append("\n");
            sb.Append("  DefaultToastCompression: ").Append(DefaultToastCompression).Append("\n");
            sb.Append("  IdleInTransactionSessionTimeout: ").Append(IdleInTransactionSessionTimeout).Append("\n");
            sb.Append("  Jit: ").Append(Jit).Append("\n");
            sb.Append("  LogAutovacuumMinDuration: ").Append(LogAutovacuumMinDuration).Append("\n");
            sb.Append("  LogErrorVerbosity: ").Append(LogErrorVerbosity).Append("\n");
            sb.Append("  LogLinePrefix: ").Append(LogLinePrefix).Append("\n");
            sb.Append("  LogMinDurationStatement: ").Append(LogMinDurationStatement).Append("\n");
            sb.Append("  MaxFilesPerProcess: ").Append(MaxFilesPerProcess).Append("\n");
            sb.Append("  MaxLocksPerTransaction: ").Append(MaxLocksPerTransaction).Append("\n");
            sb.Append("  MaxLogicalReplicationWorkers: ").Append(MaxLogicalReplicationWorkers).Append("\n");
            sb.Append("  MaxParallelWorkers: ").Append(MaxParallelWorkers).Append("\n");
            sb.Append("  MaxParallelWorkersPerGather: ").Append(MaxParallelWorkersPerGather).Append("\n");
            sb.Append("  MaxPredLocksPerTransaction: ").Append(MaxPredLocksPerTransaction).Append("\n");
            sb.Append("  MaxPreparedTransactions: ").Append(MaxPreparedTransactions).Append("\n");
            sb.Append("  MaxReplicationSlots: ").Append(MaxReplicationSlots).Append("\n");
            sb.Append("  MaxStackDepth: ").Append(MaxStackDepth).Append("\n");
            sb.Append("  MaxStandbyArchiveDelay: ").Append(MaxStandbyArchiveDelay).Append("\n");
            sb.Append("  MaxStandbyStreamingDelay: ").Append(MaxStandbyStreamingDelay).Append("\n");
            sb.Append("  MaxWalSenders: ").Append(MaxWalSenders).Append("\n");
            sb.Append("  MaxWorkerProcesses: ").Append(MaxWorkerProcesses).Append("\n");
            sb.Append("  PgPartmanBgwInterval: ").Append(PgPartmanBgwInterval).Append("\n");
            sb.Append("  PgPartmanBgwRole: ").Append(PgPartmanBgwRole).Append("\n");
            sb.Append("  PgStatStatementsTrack: ").Append(PgStatStatementsTrack).Append("\n");
            sb.Append("  TempFileLimit: ").Append(TempFileLimit).Append("\n");
            sb.Append("  TrackActivityQuerySize: ").Append(TrackActivityQuerySize).Append("\n");
            sb.Append("  TrackCommitTimestamp: ").Append(TrackCommitTimestamp).Append("\n");
            sb.Append("  TrackFunctions: ").Append(TrackFunctions).Append("\n");
            sb.Append("  TrackIoTiming: ").Append(TrackIoTiming).Append("\n");
            sb.Append("  WalSenderTimeout: ").Append(WalSenderTimeout).Append("\n");
            sb.Append("  WalWriterDelay: ").Append(WalWriterDelay).Append("\n");
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
