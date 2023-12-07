# Org.OpenAPITools.Model.Database
Managed Database information.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | A unique ID for the Managed Database. | [optional] 
**DateCreated** | **string** | The date this database was created. | [optional] 
**Plan** | **string** | The name of the Managed Database plan. | [optional] 
**PlanDisk** | **int** | The size of the disk in GB (excluded for Redis engine types). | [optional] 
**PlanRam** | **int** | The amount of RAM in MB. | [optional] 
**PlanVcpus** | **int** | Number of vCPUs. | [optional] 
**PlanReplicas** | **int** | Number of replica nodes. | [optional] 
**Region** | **string** | The [Region id](#operation/list-regions) where the Managed Database is located. | [optional] 
**DatabaseEngine** | **string** | The database engine type (MySQL, PostgreSQL, FerretDB + PostgreSQL, Redis). | [optional] 
**DatabaseEngineVersion** | **string** | The version number of the database engine in use. | [optional] 
**VpcId** | **string** | The ID of the [VPC Network](#operation/get-vpc) attached to the Managed Database. | [optional] 
**Status** | **string** | The current status.  * Rebuilding * Rebalancing * Running | [optional] 
**Label** | **string** | The user-supplied label for this managed database. | [optional] 
**Tag** | **string** | The user-supplied tag for this managed database. | [optional] 
**Dbname** | **string** | The default database name. | [optional] 
**FerretdbCredentials** | [**DatabaseFerretdbCredentials**](DatabaseFerretdbCredentials.md) |  | [optional] 
**Host** | **string** | The public hostname for database connections, or private hostname if this managed database is attached to a VPC network. | [optional] 
**PublicHost** | **string** | The public hostname for database connections. Only visible when the managed database is attached to a VPC network. | [optional] 
**User** | **string** | The default user configured on creation. | [optional] 
**Password** | **string** | The default user&#39;s secure password generated on creation. | [optional] 
**Port** | **string** | The assigned port for connecting to the Managed Database. | [optional] 
**MaintenanceDow** | **string** | The chosen date of week for routine maintenance updates. | [optional] 
**MaintenanceTime** | **string** | The chosen hour for routine maintenance updates. | [optional] 
**LatestBackup** | **string** | The date for the latest backup stored on the Managed Database. | [optional] 
**TrustedIps** | **List&lt;string&gt;** | A list of trusted IP addresses for connecting to this Managed Database (in CIDR notation). | [optional] 
**MysqlSqlModes** | **List&lt;string&gt;** | A list names of enabled SQL Modes for MySQL engine types only. | [optional] 
**MysqlRequirePrimaryKey** | **bool** | Configuration value for requiring table primary keys for MySQL engine types only. | [optional] 
**MysqlSlowQueryLog** | **bool** | Configuration value for slow query logging on the Managed Database for MySQL engine types only. | [optional] 
**MysqlLongQueryTime** | **int** | The number of seconds to denote a slow query when logging is enabled for MySQL engine types only. | [optional] 
**PgAvailableExtensions** | **List&lt;Object&gt;** | A list of objects containing names and versions (when applicable) of available extensions for PostgreSQL engine types only. | [optional] 
**RedisEvictionPolicy** | **string** | The current configured data eviction policy for Redis engine types only. | [optional] 
**ClusterTimeZone** | **string** | The configured time zone of the Managed Database in TZ database format. | [optional] 
**ReadReplicas** | **List&lt;Object&gt;** | A list of database objects containing details for all attached read-only replica nodes. | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

