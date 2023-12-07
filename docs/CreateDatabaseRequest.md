# Org.OpenAPITools.Model.CreateDatabaseRequest

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**DatabaseEngine** | **string** | The database engine type for the Managed Database. * &#x60;mysql&#x60; * &#x60;pg&#x60; * &#x60;ferretpg&#x60; * &#x60;redis&#x60; | 
**DatabaseEngineVersion** | **string** | The version of the chosen database engine type for the Managed Database. * MySQL: &#x60;8&#x60; * PostgreSQL: &#x60;12&#x60; - &#x60;15&#x60; * FerretDB + PostgreSQL: &#x60;12&#x60; - &#x60;15&#x60; * Redis: &#x60;7&#x60; | 
**Region** | **string** | The [Region id](#operation/list-regions) where the Managed Database is located. | 
**Plan** | **string** | The [Plan id](#operation/list-database-plans) to use when deploying this Managed Database. | 
**Label** | **string** | A user-supplied label for this Managed Database. | 
**Tag** | **string** | The user-supplied tag for this Managed Database. | [optional] 
**VpcId** | **string** | The [VPC id](#operation/list-vpcs) to use when deploying this Managed Database. It can also be set to &#x60;new&#x60; to configure a new VPC network with this deployment. | [optional] 
**MaintenanceDow** | **string** | The day of week for routine maintenance updates. * &#x60;monday&#x60; * &#x60;tuesday&#x60; * &#x60;wednesday&#x60; * &#x60;thursday&#x60; * &#x60;friday&#x60; * &#x60;saturday&#x60; * &#x60;sunday&#x60; | [optional] 
**MaintenanceTime** | **string** | The preferred time (UTC) for routine maintenance updates to occur in 24-hour HH:00 format (e.g. &#x60;01:00&#x60;, &#x60;13:00&#x60;, &#x60;23:00&#x60;, etc.). | [optional] 
**TrustedIps** | **List&lt;string&gt;** | A list of IP addresses allowed to access the Managed Database in CIDR notation (defaults to /32 if excluded). | [optional] 
**MysqlSqlModes** | **List&lt;string&gt;** | A list of SQL modes to enable on the Managed Database (MySQL engine type only). * &#x60;ALLOW_INVALID_DATES&#x60; * &#x60;ANSI&#x60; (Combination Mode) * &#x60;ANSI_QUOTES&#x60; * &#x60;ERROR_FOR_DIVISION_BY_ZERO&#x60; * &#x60;HIGH_NOT_PRECEDENCE&#x60; * &#x60;IGNORE_SPACE&#x60; * &#x60;NO_AUTO_VALUE_ON_ZERO&#x60; * &#x60;NO_DIR_IN_CREATE&#x60; * &#x60;NO_ENGINE_SUBSTITUTION&#x60; * &#x60;NO_UNSIGNED_SUBTRACTION&#x60; * &#x60;NO_ZERO_DATE&#x60; * &#x60;NO_ZERO_IN_DATE&#x60; * &#x60;ONLY_FULL_GROUP_BY&#x60; * &#x60;PIPES_AS_CONCAT&#x60; * &#x60;REAL_AS_FLOAT&#x60; * &#x60;STRICT_ALL_TABLES&#x60; * &#x60;STRICT_TRANS_TABLES&#x60; * &#x60;TIME_TRUNCATE_FRACTIONAL&#x60; * &#x60;TRADITIONAL&#x60; (Combination Mode) | [optional] 
**MysqlRequirePrimaryKey** | **bool** | Require a primary key for all tables on the Managed Database (MySQL engine type only). | [optional] 
**MysqlSlowQueryLog** | **bool** | Enable or disable slow query logging on the Managed Database (MySQL engine type only). | [optional] 
**MysqlLongQueryTime** | **int** | The number of seconds to denote a slow query when logging is enabled (MySQL engine type only). | [optional] 
**RedisEvictionPolicy** | **string** | Set the data eviction policy for the Managed Database (Redis engine type only) | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

