# Org.OpenAPITools - the C# library for the Vultr API

# Introduction

The Vultr API v2 is a set of HTTP endpoints that adhere to RESTful design principles and CRUD actions with predictable URIs. It uses standard HTTP response codes, authentication, and verbs. The API has consistent and well-formed JSON requests and responses with cursor-based pagination to simplify list handling. Error messages are descriptive and easy to understand. All functions of the Vultr customer portal are accessible via the API, enabling you to script complex unattended scenarios with any tool fluent in HTTP.

## Requests

Communicate with the API by making an HTTP request at the correct endpoint. The chosen method determines the action taken.

| Method | Usage |
| - -- -- - | - -- -- -- -- -- -- |
| DELETE | Use the DELETE method to destroy a resource in your account. If it is not found, the operation will return a 4xx error and an appropriate message. |
| GET | To retrieve information about a resource, use the GET method. The data is returned as a JSON object. GET methods are read-only and do not affect any resources. |
| PATCH | Some resources support partial modification with PATCH, which modifies specific attributes without updating the entire object representation. |
| POST | Issue a POST method to create a new object. Include all needed attributes in the request body encoded as JSON. |
| PUT | Use the PUT method to update information about a resource. PUT will set new values on the item without regard to their current values. |

**Rate Limit:** Vultr safeguards the API against bursts of incoming traffic based on the request's IP address to ensure stability for all users. If your application sends more than 30 requests per second, the API may return HTTP status code 429.

## Response Codes

We use standard HTTP response codes to show the success or failure of requests. Response codes in the 2xx range indicate success, while codes in the 4xx range indicate an error, such as an authorization failure or a malformed request. All 4xx errors will return a JSON response object with an `error` attribute explaining the error. Codes in the 5xx range indicate a server-side problem preventing Vultr from fulfilling your request.

| Response | Description |
| - -- -- - | - -- -- -- -- -- -- |
| 200 OK | The response contains your requested information. |
| 201 Created | Your request was accepted. The resource was created. |
| 202 Accepted | Your request was accepted. The resource was created or updated. |
| 204 No Content | Your request succeeded, there is no additional information returned. |
| 400 Bad Request | Your request was malformed. |
| 401 Unauthorized | You did not supply valid authentication credentials. |
| 403 Forbidden | You are not allowed to perform that action. |
| 404 Not Found | No results were found for your request. |
| 429 Too Many Requests | Your request exceeded the API rate limit. |
| 500 Internal Server Error | We were unable to perform the request due to server-side problems. |

## Meta and Pagination

Many API calls will return a `meta` object with paging information.

### Definitions

| Term | Description |
| - -- -- - | - -- -- -- -- -- -- |
| **List** | The items returned from the database for your request (not necessarily shown in a single response depending on the **cursor** size). |
| **Page** | A subset of the full **list** of items. Choose the size of a **page** with the `per_page` parameter. |
| **Total** | The `total` attribute indicates the number of items in the full **list**.|
| **Cursor** | Use the `cursor` query parameter to select a next or previous **page**. |
| **Next** & **Prev** | Use the `next` and `prev` attributes of the `links` meta object as `cursor` values. |

### How to use Paging

If your result **list** total exceeds the default **cursor** size (the default depends on the route, but is usually 100 records) or the value defined by the `per_page` query param (when present) the response will be returned to you paginated.

### Paging Example

> These examples have abbreviated attributes and sample values. Your actual `cursor` values will be encoded alphanumeric strings.

To return a **page** with the first two plans in the List:

    curl \"https://api.vultr.com/v2/plans?per_page=2\" \\
      -X GET \\
      -H \"Authorization: Bearer ${VULTR_API_KEY}\"

The API returns an object similar to this:

    {
        \"plans\": [
            {
                \"id\": \"vc2-1c-2gb\",
                \"vcpu_count\": 1,
                \"ram\": 2048,
                \"locations\": []
            },
            {
                \"id\": \"vc2-24c-97gb\",
                \"vcpu_count\": 24,
                \"ram\": 98304,
                \"locations\": []
            }
        ],
        \"meta\": {
            \"total\": 19,
            \"links\": {
                \"next\": \"WxYzExampleNext\",
                \"prev\": \"WxYzExamplePrev\"
            }
        }
    }

The object contains two plans. The `total` attribute indicates that 19 plans are available in the List. To navigate forward in the **list**, use the `next` value (`WxYzExampleNext` in this example) as your `cursor` query parameter.

    curl \"https://api.vultr.com/v2/plans?per_page=2&cursor=WxYzExampleNext\" \\
      -X GET
      -H \"Authorization: Bearer ${VULTR_API_KEY}\"

Likewise, you can use the example `prev` value `WxYzExamplePrev` to navigate backward.

## Parameters

You can pass information to the API with three different types of parameters.

### Path parameters

Some API calls require variable parameters as part of the endpoint path. For example, to retrieve information about a user, supply the `user-id` in the endpoint.

    curl \"https://api.vultr.com/v2/users/{user-id}\" \\
      -X GET \\
      -H \"Authorization: Bearer ${VULTR_API_KEY}\"

### Query parameters

Some API calls allow filtering with query parameters. For example, the `/v2/plans` endpoint supports a `type` query parameter. Setting `type=vhf` instructs the API only to return High Frequency Compute plans in the list. You'll find more specifics about valid filters in the endpoint documentation below.

    curl \"https://api.vultr.com/v2/plans?type=vhf\" \\
      -X GET \\
      -H \"Authorization: Bearer ${VULTR_API_KEY}\"

You can also combine filtering with paging. Use the `per_page` parameter to retreive a subset of vhf plans.

    curl \"https://api.vultr.com/v2/plans?type=vhf&per_page=2\" \\
      -X GET \\
      -H \"Authorization: Bearer ${VULTR_API_KEY}\"

### Request Body

PUT, POST, and PATCH methods may include an object in the request body with a content type of **application/json**. The documentation for each endpoint below has more information about the expected object.

## API Example Conventions

The examples in this documentation use `curl`, a command-line HTTP client, to demonstrate useage. Linux and macOS computers usually have curl installed by default, and it's [available for download](https://curl.se/download.html) on all popular platforms including Windows.

Each example is split across multiple lines with the `\\` character, which is compatible with a `bash` terminal. A typical example looks like this:

    curl \"https://api.vultr.com/v2/domains\" \\
      -X POST \\
      -H \"Authorization: Bearer ${VULTR_API_KEY}\" \\
      -H \"Content-Type: application/json\" \\
      - -data '{
        \"domain\" : \"example.com\",
        \"ip\" : \"192.0.2.123\"
      }'

* The `-X` parameter sets the request method. For consistency, we show the method on all examples, even though it's not explicitly required for GET methods.
* The `-H` lines set required HTTP headers. These examples are formatted to expand the VULTR\\_API\\_KEY environment variable for your convenience.
* Examples that require a JSON object in the request body pass the required data via the `- -data` parameter.

All values in this guide are examples. Do not rely on the OS or Plan IDs listed in this guide; use the appropriate endpoint to retreive values before creating resources.


This C# SDK is automatically generated by the [OpenAPI Generator](https://openapi-generator.tech) project:

- API version: 2.0
- SDK version: 1.0.0
- Build package: org.openapitools.codegen.languages.CSharpClientCodegen
    For more information, please visit [https://www.vultr.com](https://www.vultr.com)

<a id="frameworks-supported"></a>
## Frameworks supported

<a id="dependencies"></a>
## Dependencies

- [RestSharp](https://www.nuget.org/packages/RestSharp) - 106.13.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 13.0.2 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.8.0 or later
- [System.ComponentModel.Annotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) - 5.0.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
Install-Package System.ComponentModel.Annotations
```

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742).
NOTE: RestSharp for .Net Core creates a new socket for each api call, which can lead to a socket exhaustion problem. See [RestSharp#1406](https://github.com/restsharp/RestSharp/issues/1406).

<a id="installation"></a>
## Installation
Run the following command to generate the DLL
- [Mac/Linux] `/bin/sh build.sh`
- [Windows] `build.bat`

Then include the DLL (under the `bin` folder) in the C# project, and use the namespaces:
```csharp
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;
```
<a id="packaging"></a>
## Packaging

A `.nuspec` is included with the project. You can follow the Nuget quickstart to [create](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#create-the-package) and [publish](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#publish-the-package) packages.

This `.nuspec` uses placeholders from the `.csproj`, so build the `.csproj` directly:

```
nuget pack -Build -OutputDirectory out Org.OpenAPITools.csproj
```

Then, publish to a [local feed](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) or [other host](https://docs.microsoft.com/en-us/nuget/hosting-packages/overview) and consume the new package via Nuget as usual.

<a id="usage"></a>
## Usage

To use the API client with a HTTP proxy, setup a `System.Net.WebProxy`
```csharp
Configuration c = new Configuration();
System.Net.WebProxy webProxy = new System.Net.WebProxy("http://myProxyUrl:80/");
webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
c.Proxy = webProxy;
```

<a id="getting-started"></a>
## Getting Started

```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Client;
using Org.OpenAPITools.Model;

namespace Example
{
    public class Example
    {
        public static void Main()
        {

            Configuration config = new Configuration();
            config.BasePath = "https://api.vultr.com/v2";
            // Configure Bearer token for authorization: API Key
            config.AccessToken = "YOUR_BEARER_TOKEN";

            var apiInstance = new AccountApi(config);

            try
            {
                // Get Account Info
                GetAccount200Response result = apiInstance.GetAccount();
                Debug.WriteLine(result);
            }
            catch (ApiException e)
            {
                Debug.Print("Exception when calling AccountApi.GetAccount: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }

        }
    }
}
```

<a id="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *https://api.vultr.com/v2*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*AccountApi* | [**GetAccount**](docs/AccountApi.md#getaccount) | **GET** /account | Get Account Info
*ApplicationApi* | [**ListApplications**](docs/ApplicationApi.md#listapplications) | **GET** /applications | List Applications
*BackupApi* | [**GetBackup**](docs/BackupApi.md#getbackup) | **GET** /backups/{backup-id} | Get a Backup
*BackupApi* | [**ListBackups**](docs/BackupApi.md#listbackups) | **GET** /backups | List Backups
*BaremetalApi* | [**AttachBaremetalsVpc2**](docs/BaremetalApi.md#attachbaremetalsvpc2) | **POST** /bare-metals/{baremetal-id}/vpc2/attach | Attach VPC 2.0 Network to Bare Metal Instance
*BaremetalApi* | [**CreateBaremetal**](docs/BaremetalApi.md#createbaremetal) | **POST** /bare-metals | Create Bare Metal Instance
*BaremetalApi* | [**DeleteBaremetal**](docs/BaremetalApi.md#deletebaremetal) | **DELETE** /bare-metals/{baremetal-id} | Delete Bare Metal
*BaremetalApi* | [**DetachBaremetalVpc2**](docs/BaremetalApi.md#detachbaremetalvpc2) | **POST** /bare-metals/{baremetal-id}/vpc2/detach | Detach VPC 2.0 Network from Bare Metal Instance
*BaremetalApi* | [**GetBandwidthBaremetal**](docs/BaremetalApi.md#getbandwidthbaremetal) | **GET** /bare-metals/{baremetal-id}/bandwidth | Bare Metal Bandwidth
*BaremetalApi* | [**GetBareMetalUserdata**](docs/BaremetalApi.md#getbaremetaluserdata) | **GET** /bare-metals/{baremetal-id}/user-data | Get Bare Metal User Data
*BaremetalApi* | [**GetBareMetalVnc**](docs/BaremetalApi.md#getbaremetalvnc) | **GET** /bare-metals/{baremetal-id}/vnc | Get VNC URL for a Bare Metal
*BaremetalApi* | [**GetBareMetalsUpgrades**](docs/BaremetalApi.md#getbaremetalsupgrades) | **GET** /bare-metals/{baremetal-id}/upgrades | Get Available Bare Metal Upgrades
*BaremetalApi* | [**GetBaremetal**](docs/BaremetalApi.md#getbaremetal) | **GET** /bare-metals/{baremetal-id} | Get Bare Metal
*BaremetalApi* | [**GetIpv4Baremetal**](docs/BaremetalApi.md#getipv4baremetal) | **GET** /bare-metals/{baremetal-id}/ipv4 | Bare Metal IPv4 Addresses
*BaremetalApi* | [**GetIpv6Baremetal**](docs/BaremetalApi.md#getipv6baremetal) | **GET** /bare-metals/{baremetal-id}/ipv6 | Bare Metal IPv6 Addresses
*BaremetalApi* | [**HaltBaremetal**](docs/BaremetalApi.md#haltbaremetal) | **POST** /bare-metals/{baremetal-id}/halt | Halt Bare Metal
*BaremetalApi* | [**HaltBaremetals**](docs/BaremetalApi.md#haltbaremetals) | **POST** /bare-metals/halt | Halt Bare Metals
*BaremetalApi* | [**ListBaremetalVpc2**](docs/BaremetalApi.md#listbaremetalvpc2) | **GET** /bare-metals/{baremetal-id}/vpc2 | List Bare Metal Instance VPC 2.0 Networks
*BaremetalApi* | [**ListBaremetals**](docs/BaremetalApi.md#listbaremetals) | **GET** /bare-metals | List Bare Metal Instances
*BaremetalApi* | [**RebootBareMetals**](docs/BaremetalApi.md#rebootbaremetals) | **POST** /bare-metals/reboot | Reboot Bare Metals
*BaremetalApi* | [**RebootBaremetal**](docs/BaremetalApi.md#rebootbaremetal) | **POST** /bare-metals/{baremetal-id}/reboot | Reboot Bare Metal
*BaremetalApi* | [**ReinstallBaremetal**](docs/BaremetalApi.md#reinstallbaremetal) | **POST** /bare-metals/{baremetal-id}/reinstall | Reinstall Bare Metal
*BaremetalApi* | [**StartBareMetals**](docs/BaremetalApi.md#startbaremetals) | **POST** /bare-metals/start | Start Bare Metals
*BaremetalApi* | [**StartBaremetal**](docs/BaremetalApi.md#startbaremetal) | **POST** /bare-metals/{baremetal-id}/start | Start Bare Metal
*BaremetalApi* | [**UpdateBaremetal**](docs/BaremetalApi.md#updatebaremetal) | **PATCH** /bare-metals/{baremetal-id} | Update Bare Metal
*BillingApi* | [**GetInvoice**](docs/BillingApi.md#getinvoice) | **GET** /billing/invoices/{invoice-id} | Get Invoice
*BillingApi* | [**GetInvoiceItems**](docs/BillingApi.md#getinvoiceitems) | **GET** /billing/invoices/{invoice-id}/items | Get Invoice Items
*BillingApi* | [**ListBillingHistory**](docs/BillingApi.md#listbillinghistory) | **GET** /billing/history | List Billing History
*BillingApi* | [**ListInvoices**](docs/BillingApi.md#listinvoices) | **GET** /billing/invoices | List Invoices
*BlockApi* | [**AttachBlock**](docs/BlockApi.md#attachblock) | **POST** /blocks/{block-id}/attach | Attach Block Storage
*BlockApi* | [**CreateBlock**](docs/BlockApi.md#createblock) | **POST** /blocks | Create Block Storage
*BlockApi* | [**DeleteBlock**](docs/BlockApi.md#deleteblock) | **DELETE** /blocks/{block-id} | Delete Block Storage
*BlockApi* | [**DetachBlock**](docs/BlockApi.md#detachblock) | **POST** /blocks/{block-id}/detach | Detach Block Storage
*BlockApi* | [**GetBlock**](docs/BlockApi.md#getblock) | **GET** /blocks/{block-id} | Get Block Storage
*BlockApi* | [**ListBlocks**](docs/BlockApi.md#listblocks) | **GET** /blocks | List Block storages
*BlockApi* | [**UpdateBlock**](docs/BlockApi.md#updateblock) | **PATCH** /blocks/{block-id} | Update Block Storage
*ContainerRegistryApi* | [**CreateRegistry**](docs/ContainerRegistryApi.md#createregistry) | **POST** /registry | Create Container Registry
*ContainerRegistryApi* | [**CreateRegistryDockerCredentials**](docs/ContainerRegistryApi.md#createregistrydockercredentials) | **OPTIONS** /registry/{registry-id}/docker-credentials?expiry_seconds&#x3D;0&amp;read_write&#x3D;false | Create Docker Credentials
*ContainerRegistryApi* | [**CreateRegistryKubernetesDockerCredentials**](docs/ContainerRegistryApi.md#createregistrykubernetesdockercredentials) | **OPTIONS** /registry/{registry-id}/docker-credentials/kubernetes?expiry_seconds&#x3D;0&amp;read_write&#x3D;false&amp;base64_encode&#x3D;false | Create Docker Credentials for Kubernetes
*ContainerRegistryApi* | [**DeleteRegistry**](docs/ContainerRegistryApi.md#deleteregistry) | **DELETE** /registry/{registry-id} | Delete Container Registry
*ContainerRegistryApi* | [**DeleteRepository**](docs/ContainerRegistryApi.md#deleterepository) | **DELETE** /registry/{registry-id}/repository/{repository-image} | Delete Repository
*ContainerRegistryApi* | [**ListRegistries**](docs/ContainerRegistryApi.md#listregistries) | **GET** /registries | List Container Registries
*ContainerRegistryApi* | [**ListRegistryPlans**](docs/ContainerRegistryApi.md#listregistryplans) | **GET** /registry/plan/list | List Registry Plans
*ContainerRegistryApi* | [**ListRegistryRegions**](docs/ContainerRegistryApi.md#listregistryregions) | **GET** /registry/region/list | List Registry Regions
*ContainerRegistryApi* | [**ListRegistryRepositories**](docs/ContainerRegistryApi.md#listregistryrepositories) | **GET** /registry/{registry-id}/repositories | List Repositories
*ContainerRegistryApi* | [**ReadRegistry**](docs/ContainerRegistryApi.md#readregistry) | **GET** /registry/{registry-id} | Read Container Registry
*ContainerRegistryApi* | [**ReadRegistryRepository**](docs/ContainerRegistryApi.md#readregistryrepository) | **GET** /registry/{registry-id}/repository/{repository-image} | Read Repository
*ContainerRegistryApi* | [**UpdateRegistry**](docs/ContainerRegistryApi.md#updateregistry) | **PUT** /registry/{registry-id} | Update Container Registry
*ContainerRegistryApi* | [**UpdateRepository**](docs/ContainerRegistryApi.md#updaterepository) | **PUT** /registry/{registry-id}/repository/{repository-image} | Update Repository
*DnsApi* | [**CreateDnsDomain**](docs/DnsApi.md#creatednsdomain) | **POST** /domains | Create DNS Domain
*DnsApi* | [**CreateDnsDomainRecord**](docs/DnsApi.md#creatednsdomainrecord) | **POST** /domains/{dns-domain}/records | Create Record
*DnsApi* | [**DeleteDnsDomain**](docs/DnsApi.md#deletednsdomain) | **DELETE** /domains/{dns-domain} | Delete Domain
*DnsApi* | [**DeleteDnsDomainRecord**](docs/DnsApi.md#deletednsdomainrecord) | **DELETE** /domains/{dns-domain}/records/{record-id} | Delete Record
*DnsApi* | [**GetDnsDomain**](docs/DnsApi.md#getdnsdomain) | **GET** /domains/{dns-domain} | Get DNS Domain
*DnsApi* | [**GetDnsDomainDnssec**](docs/DnsApi.md#getdnsdomaindnssec) | **GET** /domains/{dns-domain}/dnssec | Get DNSSec Info
*DnsApi* | [**GetDnsDomainRecord**](docs/DnsApi.md#getdnsdomainrecord) | **GET** /domains/{dns-domain}/records/{record-id} | Get Record
*DnsApi* | [**GetDnsDomainSoa**](docs/DnsApi.md#getdnsdomainsoa) | **GET** /domains/{dns-domain}/soa | Get SOA information
*DnsApi* | [**ListDnsDomainRecords**](docs/DnsApi.md#listdnsdomainrecords) | **GET** /domains/{dns-domain}/records | List Records
*DnsApi* | [**ListDnsDomains**](docs/DnsApi.md#listdnsdomains) | **GET** /domains | List DNS Domains
*DnsApi* | [**UpdateDnsDomain**](docs/DnsApi.md#updatednsdomain) | **PUT** /domains/{dns-domain} | Update a DNS Domain
*DnsApi* | [**UpdateDnsDomainRecord**](docs/DnsApi.md#updatednsdomainrecord) | **PATCH** /domains/{dns-domain}/records/{record-id} | Update Record
*DnsApi* | [**UpdateDnsDomainSoa**](docs/DnsApi.md#updatednsdomainsoa) | **PATCH** /domains/{dns-domain}/soa | Update SOA information
*FirewallApi* | [**CreateFirewallGroup**](docs/FirewallApi.md#createfirewallgroup) | **POST** /firewalls | Create Firewall Group
*FirewallApi* | [**DeleteFirewallGroup**](docs/FirewallApi.md#deletefirewallgroup) | **DELETE** /firewalls/{firewall-group-id} | Delete Firewall Group
*FirewallApi* | [**DeleteFirewallGroupRule**](docs/FirewallApi.md#deletefirewallgrouprule) | **DELETE** /firewalls/{firewall-group-id}/rules/{firewall-rule-id} | Delete Firewall Rule
*FirewallApi* | [**GetFirewallGroup**](docs/FirewallApi.md#getfirewallgroup) | **GET** /firewalls/{firewall-group-id} | Get Firewall Group
*FirewallApi* | [**GetFirewallGroupRule**](docs/FirewallApi.md#getfirewallgrouprule) | **GET** /firewalls/{firewall-group-id}/rules/{firewall-rule-id} | Get Firewall Rule
*FirewallApi* | [**ListFirewallGroupRules**](docs/FirewallApi.md#listfirewallgrouprules) | **GET** /firewalls/{firewall-group-id}/rules | List Firewall Rules
*FirewallApi* | [**ListFirewallGroups**](docs/FirewallApi.md#listfirewallgroups) | **GET** /firewalls | List Firewall Groups
*FirewallApi* | [**PostFirewallsFirewallGroupIdRules**](docs/FirewallApi.md#postfirewallsfirewallgroupidrules) | **POST** /firewalls/{firewall-group-id}/rules | Create Firewall Rules
*FirewallApi* | [**UpdateFirewallGroup**](docs/FirewallApi.md#updatefirewallgroup) | **PUT** /firewalls/{firewall-group-id} | Update Firewall Group
*InstancesApi* | [**AttachInstanceIso**](docs/InstancesApi.md#attachinstanceiso) | **POST** /instances/{instance-id}/iso/attach | Attach ISO to Instance
*InstancesApi* | [**AttachInstanceNetwork**](docs/InstancesApi.md#attachinstancenetwork) | **POST** /instances/{instance-id}/private-networks/attach | Attach Private Network to Instance
*InstancesApi* | [**AttachInstanceVpc**](docs/InstancesApi.md#attachinstancevpc) | **POST** /instances/{instance-id}/vpcs/attach | Attach VPC to Instance
*InstancesApi* | [**AttachInstanceVpc2**](docs/InstancesApi.md#attachinstancevpc2) | **POST** /instances/{instance-id}/vpc2/attach | Attach VPC 2.0 Network to Instance
*InstancesApi* | [**CreateInstance**](docs/InstancesApi.md#createinstance) | **POST** /instances | Create Instance
*InstancesApi* | [**CreateInstanceBackupSchedule**](docs/InstancesApi.md#createinstancebackupschedule) | **POST** /instances/{instance-id}/backup-schedule | Set Instance Backup Schedule
*InstancesApi* | [**CreateInstanceIpv4**](docs/InstancesApi.md#createinstanceipv4) | **POST** /instances/{instance-id}/ipv4 | Create IPv4
*InstancesApi* | [**CreateInstanceReverseIpv4**](docs/InstancesApi.md#createinstancereverseipv4) | **POST** /instances/{instance-id}/ipv4/reverse | Create Instance Reverse IPv4
*InstancesApi* | [**CreateInstanceReverseIpv6**](docs/InstancesApi.md#createinstancereverseipv6) | **POST** /instances/{instance-id}/ipv6/reverse | Create Instance Reverse IPv6
*InstancesApi* | [**DeleteInstance**](docs/InstancesApi.md#deleteinstance) | **DELETE** /instances/{instance-id} | Delete Instance
*InstancesApi* | [**DeleteInstanceIpv4**](docs/InstancesApi.md#deleteinstanceipv4) | **DELETE** /instances/{instance-id}/ipv4/{ipv4} | Delete IPv4 Address
*InstancesApi* | [**DeleteInstanceReverseIpv6**](docs/InstancesApi.md#deleteinstancereverseipv6) | **DELETE** /instances/{instance-id}/ipv6/reverse/{ipv6} | Delete Instance Reverse IPv6
*InstancesApi* | [**DetachInstanceIso**](docs/InstancesApi.md#detachinstanceiso) | **POST** /instances/{instance-id}/iso/detach | Detach ISO from instance
*InstancesApi* | [**DetachInstanceNetwork**](docs/InstancesApi.md#detachinstancenetwork) | **POST** /instances/{instance-id}/private-networks/detach | Detach Private Network from Instance.
*InstancesApi* | [**DetachInstanceVpc**](docs/InstancesApi.md#detachinstancevpc) | **POST** /instances/{instance-id}/vpcs/detach | Detach VPC from Instance
*InstancesApi* | [**DetachInstanceVpc2**](docs/InstancesApi.md#detachinstancevpc2) | **POST** /instances/{instance-id}/vpc2/detach | Detach VPC 2.0 Network from Instance
*InstancesApi* | [**GetInstance**](docs/InstancesApi.md#getinstance) | **GET** /instances/{instance-id} | Get Instance
*InstancesApi* | [**GetInstanceBackupSchedule**](docs/InstancesApi.md#getinstancebackupschedule) | **GET** /instances/{instance-id}/backup-schedule | Get Instance Backup Schedule
*InstancesApi* | [**GetInstanceBandwidth**](docs/InstancesApi.md#getinstancebandwidth) | **GET** /instances/{instance-id}/bandwidth | Instance Bandwidth
*InstancesApi* | [**GetInstanceIpv4**](docs/InstancesApi.md#getinstanceipv4) | **GET** /instances/{instance-id}/ipv4 | List Instance IPv4 Information
*InstancesApi* | [**GetInstanceIpv6**](docs/InstancesApi.md#getinstanceipv6) | **GET** /instances/{instance-id}/ipv6 | Get Instance IPv6 Information
*InstancesApi* | [**GetInstanceIsoStatus**](docs/InstancesApi.md#getinstanceisostatus) | **GET** /instances/{instance-id}/iso | Get Instance ISO Status
*InstancesApi* | [**GetInstanceNeighbors**](docs/InstancesApi.md#getinstanceneighbors) | **GET** /instances/{instance-id}/neighbors | Get Instance neighbors
*InstancesApi* | [**GetInstanceUpgrades**](docs/InstancesApi.md#getinstanceupgrades) | **GET** /instances/{instance-id}/upgrades | Get Available Instance Upgrades
*InstancesApi* | [**GetInstanceUserdata**](docs/InstancesApi.md#getinstanceuserdata) | **GET** /instances/{instance-id}/user-data | Get Instance User Data
*InstancesApi* | [**HaltInstance**](docs/InstancesApi.md#haltinstance) | **POST** /instances/{instance-id}/halt | Halt Instance
*InstancesApi* | [**HaltInstances**](docs/InstancesApi.md#haltinstances) | **POST** /instances/halt | Halt Instances
*InstancesApi* | [**ListInstanceIpv6Reverse**](docs/InstancesApi.md#listinstanceipv6reverse) | **GET** /instances/{instance-id}/ipv6/reverse | List Instance IPv6 Reverse
*InstancesApi* | [**ListInstancePrivateNetworks**](docs/InstancesApi.md#listinstanceprivatenetworks) | **GET** /instances/{instance-id}/private-networks | List instance Private Networks
*InstancesApi* | [**ListInstanceVpc2**](docs/InstancesApi.md#listinstancevpc2) | **GET** /instances/{instance-id}/vpc2 | List Instance VPC 2.0 Networks
*InstancesApi* | [**ListInstanceVpcs**](docs/InstancesApi.md#listinstancevpcs) | **GET** /instances/{instance-id}/vpcs | List instance VPCs
*InstancesApi* | [**ListInstances**](docs/InstancesApi.md#listinstances) | **GET** /instances | List Instances
*InstancesApi* | [**PostInstancesInstanceIdIpv4ReverseDefault**](docs/InstancesApi.md#postinstancesinstanceidipv4reversedefault) | **POST** /instances/{instance-id}/ipv4/reverse/default | Set Default Reverse DNS Entry
*InstancesApi* | [**RebootInstance**](docs/InstancesApi.md#rebootinstance) | **POST** /instances/{instance-id}/reboot | Reboot Instance
*InstancesApi* | [**RebootInstances**](docs/InstancesApi.md#rebootinstances) | **POST** /instances/reboot | Reboot instances
*InstancesApi* | [**ReinstallInstance**](docs/InstancesApi.md#reinstallinstance) | **POST** /instances/{instance-id}/reinstall | Reinstall Instance
*InstancesApi* | [**RestoreInstance**](docs/InstancesApi.md#restoreinstance) | **POST** /instances/{instance-id}/restore | Restore Instance
*InstancesApi* | [**StartInstance**](docs/InstancesApi.md#startinstance) | **POST** /instances/{instance-id}/start | Start instance
*InstancesApi* | [**StartInstances**](docs/InstancesApi.md#startinstances) | **POST** /instances/start | Start instances
*InstancesApi* | [**UpdateInstance**](docs/InstancesApi.md#updateinstance) | **PATCH** /instances/{instance-id} | Update Instance
*IsoApi* | [**CreateIso**](docs/IsoApi.md#createiso) | **POST** /iso | Create ISO
*IsoApi* | [**DeleteIso**](docs/IsoApi.md#deleteiso) | **DELETE** /iso/{iso-id} | Delete ISO
*IsoApi* | [**IsoGet**](docs/IsoApi.md#isoget) | **GET** /iso/{iso-id} | Get ISO
*IsoApi* | [**ListIsos**](docs/IsoApi.md#listisos) | **GET** /iso | List ISOs
*IsoApi* | [**ListPublicIsos**](docs/IsoApi.md#listpublicisos) | **GET** /iso-public | List Public ISOs
*KubernetesApi* | [**CreateKubernetesCluster**](docs/KubernetesApi.md#createkubernetescluster) | **POST** /kubernetes/clusters | Create Kubernetes Cluster
*KubernetesApi* | [**CreateNodepools**](docs/KubernetesApi.md#createnodepools) | **POST** /kubernetes/clusters/{vke-id}/node-pools | Create NodePool
*KubernetesApi* | [**DeleteKubernetesCluster**](docs/KubernetesApi.md#deletekubernetescluster) | **DELETE** /kubernetes/clusters/{vke-id} | Delete Kubernetes Cluster
*KubernetesApi* | [**DeleteKubernetesClusterVkeIdDeleteWithLinkedResources**](docs/KubernetesApi.md#deletekubernetesclustervkeiddeletewithlinkedresources) | **DELETE** /kubernetes/clusters/{vke-id}/delete-with-linked-resources | Delete VKE Cluster and All Related Resources
*KubernetesApi* | [**DeleteNodepool**](docs/KubernetesApi.md#deletenodepool) | **DELETE** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Delete Nodepool
*KubernetesApi* | [**DeleteNodepoolInstance**](docs/KubernetesApi.md#deletenodepoolinstance) | **DELETE** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id} | Delete NodePool Instance
*KubernetesApi* | [**GetKubernetesAvailableUpgrades**](docs/KubernetesApi.md#getkubernetesavailableupgrades) | **GET** /kubernetes/clusters/{vke-id}/available-upgrades | Get Kubernetes Available Upgrades
*KubernetesApi* | [**GetKubernetesClusters**](docs/KubernetesApi.md#getkubernetesclusters) | **GET** /kubernetes/clusters/{vke-id} | Get Kubernetes Cluster
*KubernetesApi* | [**GetKubernetesClustersConfig**](docs/KubernetesApi.md#getkubernetesclustersconfig) | **GET** /kubernetes/clusters/{vke-id}/config | Get Kubernetes Cluster Kubeconfig
*KubernetesApi* | [**GetKubernetesResources**](docs/KubernetesApi.md#getkubernetesresources) | **GET** /kubernetes/clusters/{vke-id}/resources | Get Kubernetes Resources
*KubernetesApi* | [**GetKubernetesVersions**](docs/KubernetesApi.md#getkubernetesversions) | **GET** /kubernetes/versions | Get Kubernetes Versions
*KubernetesApi* | [**GetNodepool**](docs/KubernetesApi.md#getnodepool) | **GET** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Get NodePool
*KubernetesApi* | [**GetNodepools**](docs/KubernetesApi.md#getnodepools) | **GET** /kubernetes/clusters/{vke-id}/node-pools | List NodePools
*KubernetesApi* | [**ListKubernetesClusters**](docs/KubernetesApi.md#listkubernetesclusters) | **GET** /kubernetes/clusters | List all Kubernetes Clusters
*KubernetesApi* | [**RecycleNodepoolInstance**](docs/KubernetesApi.md#recyclenodepoolinstance) | **POST** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id}/nodes/{node-id}/recycle | Recycle a NodePool Instance
*KubernetesApi* | [**StartKubernetesClusterUpgrade**](docs/KubernetesApi.md#startkubernetesclusterupgrade) | **POST** /kubernetes/clusters/{vke-id}/upgrades | Start Kubernetes Cluster Upgrade
*KubernetesApi* | [**UpdateKubernetesCluster**](docs/KubernetesApi.md#updatekubernetescluster) | **PUT** /kubernetes/clusters/{vke-id} | Update Kubernetes Cluster
*KubernetesApi* | [**UpdateNodepool**](docs/KubernetesApi.md#updatenodepool) | **PATCH** /kubernetes/clusters/{vke-id}/node-pools/{nodepool-id} | Update Nodepool
*LoadBalancerApi* | [**CreateLoadBalancer**](docs/LoadBalancerApi.md#createloadbalancer) | **POST** /load-balancers | Create Load Balancer
*LoadBalancerApi* | [**CreateLoadBalancerForwardingRules**](docs/LoadBalancerApi.md#createloadbalancerforwardingrules) | **POST** /load-balancers/{load-balancer-id}/forwarding-rules | Create Forwarding Rule
*LoadBalancerApi* | [**DeleteLoadBalancer**](docs/LoadBalancerApi.md#deleteloadbalancer) | **DELETE** /load-balancers/{load-balancer-id} | Delete Load Balancer
*LoadBalancerApi* | [**DeleteLoadBalancerForwardingRule**](docs/LoadBalancerApi.md#deleteloadbalancerforwardingrule) | **DELETE** /load-balancers/{load-balancer-id}/forwarding-rules/{forwarding-rule-id} | Delete Forwarding Rule
*LoadBalancerApi* | [**DeleteLoadBalancerSsl**](docs/LoadBalancerApi.md#deleteloadbalancerssl) | **DELETE** /load-balancers/{load-balancer-id}/ssl | Delete Load Balancer SSL
*LoadBalancerApi* | [**GetLoadBalancer**](docs/LoadBalancerApi.md#getloadbalancer) | **GET** /load-balancers/{load-balancer-id} | Get Load Balancer
*LoadBalancerApi* | [**GetLoadBalancerForwardingRule**](docs/LoadBalancerApi.md#getloadbalancerforwardingrule) | **GET** /load-balancers/{load-balancer-id}/forwarding-rules/{forwarding-rule-id} | Get Forwarding Rule
*LoadBalancerApi* | [**GetLoadbalancerFirewallRule**](docs/LoadBalancerApi.md#getloadbalancerfirewallrule) | **GET** /load-balancers/{loadbalancer-id}/firewall-rules/{firewall-rule-id} | Get Firewall Rule
*LoadBalancerApi* | [**ListLoadBalancerForwardingRules**](docs/LoadBalancerApi.md#listloadbalancerforwardingrules) | **GET** /load-balancers/{load-balancer-id}/forwarding-rules | List Forwarding Rules
*LoadBalancerApi* | [**ListLoadBalancers**](docs/LoadBalancerApi.md#listloadbalancers) | **GET** /load-balancers | List Load Balancers
*LoadBalancerApi* | [**ListLoadbalancerFirewallRules**](docs/LoadBalancerApi.md#listloadbalancerfirewallrules) | **GET** /load-balancers/{loadbalancer-id}/firewall-rules | List Firewall Rules
*LoadBalancerApi* | [**UpdateLoadBalancer**](docs/LoadBalancerApi.md#updateloadbalancer) | **PATCH** /load-balancers/{load-balancer-id} | Update Load Balancer
*ManagedDatabasesApi* | [**CreateConnectionPool**](docs/ManagedDatabasesApi.md#createconnectionpool) | **POST** /databases/{database-id}/connection-pools | Create Connection Pool
*ManagedDatabasesApi* | [**CreateDatabase**](docs/ManagedDatabasesApi.md#createdatabase) | **POST** /databases | Create Managed Database
*ManagedDatabasesApi* | [**CreateDatabaseDb**](docs/ManagedDatabasesApi.md#createdatabasedb) | **POST** /databases/{database-id}/dbs | Create Logical Database
*ManagedDatabasesApi* | [**CreateDatabaseUser**](docs/ManagedDatabasesApi.md#createdatabaseuser) | **POST** /databases/{database-id}/users | Create Database User
*ManagedDatabasesApi* | [**DatabaseAddReadReplica**](docs/ManagedDatabasesApi.md#databaseaddreadreplica) | **POST** /databases/{database-id}/read-replica | Add Read-Only Replica
*ManagedDatabasesApi* | [**DatabaseDetachMigration**](docs/ManagedDatabasesApi.md#databasedetachmigration) | **DELETE** /databases/{database-id}/migration | Detach Migration
*ManagedDatabasesApi* | [**DatabaseFork**](docs/ManagedDatabasesApi.md#databasefork) | **POST** /databases/{database-id}/fork | Fork Managed Database
*ManagedDatabasesApi* | [**DatabasePromoteReadReplica**](docs/ManagedDatabasesApi.md#databasepromotereadreplica) | **POST** /databases/{database-id}/promote-read-replica | Promote Read-Only Replica
*ManagedDatabasesApi* | [**DatabaseRestoreFromBackup**](docs/ManagedDatabasesApi.md#databaserestorefrombackup) | **POST** /databases/{database-id}/restore | Restore from Backup
*ManagedDatabasesApi* | [**DatabaseStartMigration**](docs/ManagedDatabasesApi.md#databasestartmigration) | **POST** /databases/{database-id}/migration | Start Migration
*ManagedDatabasesApi* | [**DeleteConnectionPool**](docs/ManagedDatabasesApi.md#deleteconnectionpool) | **DELETE** /databases/{database-id}/connection-pools/{pool-name} | Delete Connection Pool
*ManagedDatabasesApi* | [**DeleteDatabase**](docs/ManagedDatabasesApi.md#deletedatabase) | **DELETE** /databases/{database-id} | Delete Managed Database
*ManagedDatabasesApi* | [**DeleteDatabaseDb**](docs/ManagedDatabasesApi.md#deletedatabasedb) | **DELETE** /databases/{database-id}/dbs/{db-name} | Delete Logical Database
*ManagedDatabasesApi* | [**DeleteDatabaseUser**](docs/ManagedDatabasesApi.md#deletedatabaseuser) | **DELETE** /databases/{database-id}/users/{username} | Delete Database User
*ManagedDatabasesApi* | [**GetBackupInformation**](docs/ManagedDatabasesApi.md#getbackupinformation) | **GET** /databases/{database-id}/backups | Get Backup Information
*ManagedDatabasesApi* | [**GetConnectionPool**](docs/ManagedDatabasesApi.md#getconnectionpool) | **GET** /databases/{database-id}/connection-pools/{pool-name} | Get Connection Pool
*ManagedDatabasesApi* | [**GetDatabase**](docs/ManagedDatabasesApi.md#getdatabase) | **GET** /databases/{database-id} | Get Managed Database
*ManagedDatabasesApi* | [**GetDatabaseDb**](docs/ManagedDatabasesApi.md#getdatabasedb) | **GET** /databases/{database-id}/dbs/{db-name} | Get Logical Database
*ManagedDatabasesApi* | [**GetDatabaseUsage**](docs/ManagedDatabasesApi.md#getdatabaseusage) | **GET** /databases/{database-id}/usage | Get Database Usage Information
*ManagedDatabasesApi* | [**GetDatabaseUser**](docs/ManagedDatabasesApi.md#getdatabaseuser) | **GET** /databases/{database-id}/users/{username} | Get Database User
*ManagedDatabasesApi* | [**ListAdvancedOptions**](docs/ManagedDatabasesApi.md#listadvancedoptions) | **GET** /databases/{database-id}/advanced-options | List Advanced Options
*ManagedDatabasesApi* | [**ListAvailableVersions**](docs/ManagedDatabasesApi.md#listavailableversions) | **GET** /databases/{database-id}/version-upgrade | List Available Versions
*ManagedDatabasesApi* | [**ListConnectionPools**](docs/ManagedDatabasesApi.md#listconnectionpools) | **GET** /databases/{database-id}/connection-pools | List Connection Pools
*ManagedDatabasesApi* | [**ListDatabaseDbs**](docs/ManagedDatabasesApi.md#listdatabasedbs) | **GET** /databases/{database-id}/dbs | List Logical Databases
*ManagedDatabasesApi* | [**ListDatabasePlans**](docs/ManagedDatabasesApi.md#listdatabaseplans) | **GET** /databases/plans | List Managed Database Plans
*ManagedDatabasesApi* | [**ListDatabaseUsers**](docs/ManagedDatabasesApi.md#listdatabaseusers) | **GET** /databases/{database-id}/users | List Database Users
*ManagedDatabasesApi* | [**ListDatabases**](docs/ManagedDatabasesApi.md#listdatabases) | **GET** /databases | List Managed Databases
*ManagedDatabasesApi* | [**ListMaintenanceUpdates**](docs/ManagedDatabasesApi.md#listmaintenanceupdates) | **GET** /databases/{database-id}/maintenance | List Maintenance Updates
*ManagedDatabasesApi* | [**ListServiceAlerts**](docs/ManagedDatabasesApi.md#listservicealerts) | **POST** /databases/{database-id}/alerts | List Service Alerts
*ManagedDatabasesApi* | [**SetDatabaseUserAcl**](docs/ManagedDatabasesApi.md#setdatabaseuseracl) | **PUT** /databases/{database-id}/users/{username}/access-control | Set Database User Access Control
*ManagedDatabasesApi* | [**StartMaintenanceUpdates**](docs/ManagedDatabasesApi.md#startmaintenanceupdates) | **POST** /databases/{database-id}/maintenance | Start Maintenance Updates
*ManagedDatabasesApi* | [**StartVersionUpgrade**](docs/ManagedDatabasesApi.md#startversionupgrade) | **POST** /databases/{database-id}/version-upgrade | Start Version Upgrade
*ManagedDatabasesApi* | [**UpdateAdvancedOptions**](docs/ManagedDatabasesApi.md#updateadvancedoptions) | **PUT** /databases/{database-id}/advanced-options | Update Advanced Options
*ManagedDatabasesApi* | [**UpdateConnectionPool**](docs/ManagedDatabasesApi.md#updateconnectionpool) | **PUT** /databases/{database-id}/connection-pools/{pool-name} | Update Connection Pool
*ManagedDatabasesApi* | [**UpdateDatabase**](docs/ManagedDatabasesApi.md#updatedatabase) | **PUT** /databases/{database-id} | Update Managed Database
*ManagedDatabasesApi* | [**UpdateDatabaseUser**](docs/ManagedDatabasesApi.md#updatedatabaseuser) | **PUT** /databases/{database-id}/users/{username} | Update Database User
*ManagedDatabasesApi* | [**ViewMigrationStatus**](docs/ManagedDatabasesApi.md#viewmigrationstatus) | **GET** /databases/{database-id}/migration | Get Migration Status
*OsApi* | [**ListOs**](docs/OsApi.md#listos) | **GET** /os | List OS
*PlansApi* | [**ListMetalPlans**](docs/PlansApi.md#listmetalplans) | **GET** /plans-metal | List Bare Metal Plans
*PlansApi* | [**ListPlans**](docs/PlansApi.md#listplans) | **GET** /plans | List Plans
*PrivateNetworksApi* | [**CreateNetwork**](docs/PrivateNetworksApi.md#createnetwork) | **POST** /private-networks | Create a Private Network
*PrivateNetworksApi* | [**DeleteNetwork**](docs/PrivateNetworksApi.md#deletenetwork) | **DELETE** /private-networks/{network-id} | Delete a private network
*PrivateNetworksApi* | [**GetNetwork**](docs/PrivateNetworksApi.md#getnetwork) | **GET** /private-networks/{network-id} | Get a private network
*PrivateNetworksApi* | [**ListNetworks**](docs/PrivateNetworksApi.md#listnetworks) | **GET** /private-networks | List Private Networks
*PrivateNetworksApi* | [**UpdateNetwork**](docs/PrivateNetworksApi.md#updatenetwork) | **PUT** /private-networks/{network-id} | Update a Private Network
*RegionApi* | [**ListAvailablePlansRegion**](docs/RegionApi.md#listavailableplansregion) | **GET** /regions/{region-id}/availability | List available plans in region
*RegionApi* | [**ListRegions**](docs/RegionApi.md#listregions) | **GET** /regions | List Regions
*ReservedIpApi* | [**AttachReservedIp**](docs/ReservedIpApi.md#attachreservedip) | **POST** /reserved-ips/{reserved-ip}/attach | Attach Reserved IP
*ReservedIpApi* | [**ConvertReservedIp**](docs/ReservedIpApi.md#convertreservedip) | **POST** /reserved-ips/convert | Convert Instance IP to Reserved IP
*ReservedIpApi* | [**CreateReservedIp**](docs/ReservedIpApi.md#createreservedip) | **POST** /reserved-ips | Create Reserved IP
*ReservedIpApi* | [**DeleteReservedIp**](docs/ReservedIpApi.md#deletereservedip) | **DELETE** /reserved-ips/{reserved-ip} | Delete Reserved IP
*ReservedIpApi* | [**DetachReservedIp**](docs/ReservedIpApi.md#detachreservedip) | **POST** /reserved-ips/{reserved-ip}/detach | Detach Reserved IP
*ReservedIpApi* | [**GetReservedIp**](docs/ReservedIpApi.md#getreservedip) | **GET** /reserved-ips/{reserved-ip} | Get Reserved IP
*ReservedIpApi* | [**ListReservedIps**](docs/ReservedIpApi.md#listreservedips) | **GET** /reserved-ips | List Reserved IPs
*ReservedIpApi* | [**PatchReservedIpsReservedIp**](docs/ReservedIpApi.md#patchreservedipsreservedip) | **PATCH** /reserved-ips/{reserved-ip} | Update Reserved IP
*S3Api* | [**CreateObjectStorage**](docs/S3Api.md#createobjectstorage) | **POST** /object-storage | Create Object Storage
*S3Api* | [**DeleteObjectStorage**](docs/S3Api.md#deleteobjectstorage) | **DELETE** /object-storage/{object-storage-id} | Delete Object Storage
*S3Api* | [**GetObjectStorage**](docs/S3Api.md#getobjectstorage) | **GET** /object-storage/{object-storage-id} | Get Object Storage
*S3Api* | [**ListObjectStorageClusters**](docs/S3Api.md#listobjectstorageclusters) | **GET** /object-storage/clusters | Get All Clusters
*S3Api* | [**ListObjectStorages**](docs/S3Api.md#listobjectstorages) | **GET** /object-storage | List Object Storages
*S3Api* | [**RegenerateObjectStorageKeys**](docs/S3Api.md#regenerateobjectstoragekeys) | **POST** /object-storage/{object-storage-id}/regenerate-keys | Regenerate Object Storage Keys
*S3Api* | [**UpdateObjectStorage**](docs/S3Api.md#updateobjectstorage) | **PUT** /object-storage/{object-storage-id} | Update Object Storage
*SnapshotApi* | [**CreateSnapshot**](docs/SnapshotApi.md#createsnapshot) | **POST** /snapshots | Create Snapshot
*SnapshotApi* | [**CreateSnapshotCreateFromUrl**](docs/SnapshotApi.md#createsnapshotcreatefromurl) | **POST** /snapshots/create-from-url | Create Snapshot from URL
*SnapshotApi* | [**DeleteSnapshot**](docs/SnapshotApi.md#deletesnapshot) | **DELETE** /snapshots/{snapshot-id} | Delete Snapshot
*SnapshotApi* | [**GetSnapshot**](docs/SnapshotApi.md#getsnapshot) | **GET** /snapshots/{snapshot-id} | Get Snapshot
*SnapshotApi* | [**ListSnapshots**](docs/SnapshotApi.md#listsnapshots) | **GET** /snapshots | List Snapshots
*SnapshotApi* | [**PutSnapshotsSnapshotId**](docs/SnapshotApi.md#putsnapshotssnapshotid) | **PUT** /snapshots/{snapshot-id} | Update Snapshot
*SshApi* | [**CreateSshKey**](docs/SshApi.md#createsshkey) | **POST** /ssh-keys | Create SSH key
*SshApi* | [**DeleteSshKey**](docs/SshApi.md#deletesshkey) | **DELETE** /ssh-keys/{ssh-key-id} | Delete SSH Key
*SshApi* | [**GetSshKey**](docs/SshApi.md#getsshkey) | **GET** /ssh-keys/{ssh-key-id} | Get SSH Key
*SshApi* | [**ListSshKeys**](docs/SshApi.md#listsshkeys) | **GET** /ssh-keys | List SSH Keys
*SshApi* | [**UpdateSshKey**](docs/SshApi.md#updatesshkey) | **PATCH** /ssh-keys/{ssh-key-id} | Update SSH Key
*StartupApi* | [**CreateStartupScript**](docs/StartupApi.md#createstartupscript) | **POST** /startup-scripts | Create Startup Script
*StartupApi* | [**DeleteStartupScript**](docs/StartupApi.md#deletestartupscript) | **DELETE** /startup-scripts/{startup-id} | Delete Startup Script
*StartupApi* | [**GetStartupScript**](docs/StartupApi.md#getstartupscript) | **GET** /startup-scripts/{startup-id} | Get Startup Script
*StartupApi* | [**ListStartupScripts**](docs/StartupApi.md#liststartupscripts) | **GET** /startup-scripts | List Startup Scripts
*StartupApi* | [**UpdateStartupScript**](docs/StartupApi.md#updatestartupscript) | **PATCH** /startup-scripts/{startup-id} | Update Startup Script
*SubaccountApi* | [**CreateSubaccount**](docs/SubaccountApi.md#createsubaccount) | **POST** /subaccounts | Create Sub-Account
*SubaccountApi* | [**ListSubaccounts**](docs/SubaccountApi.md#listsubaccounts) | **GET** /subaccounts | List Sub-Accounts
*UsersApi* | [**CreateUser**](docs/UsersApi.md#createuser) | **POST** /users | Create User
*UsersApi* | [**DeleteUser**](docs/UsersApi.md#deleteuser) | **DELETE** /users/{user-id} | Delete User
*UsersApi* | [**GetUser**](docs/UsersApi.md#getuser) | **GET** /users/{user-id} | Get User
*UsersApi* | [**ListUsers**](docs/UsersApi.md#listusers) | **GET** /users | Get Users
*UsersApi* | [**UpdateUser**](docs/UsersApi.md#updateuser) | **PATCH** /users/{user-id} | Update User
*VPC2Api* | [**AttachVpc2Nodes**](docs/VPC2Api.md#attachvpc2nodes) | **POST** /vpc2/{vpc-id}/nodes/attach | Attach nodes to a VPC 2.0 network
*VPC2Api* | [**CreateVpc2**](docs/VPC2Api.md#createvpc2) | **POST** /vpc2 | Create a VPC 2.0 network
*VPC2Api* | [**DeleteVpc2**](docs/VPC2Api.md#deletevpc2) | **DELETE** /vpc2/{vpc-id} | Delete a VPC 2.0 network
*VPC2Api* | [**DetachVpc2Nodes**](docs/VPC2Api.md#detachvpc2nodes) | **POST** /vpc2/{vpc-id}/nodes/detach | Remove nodes from a VPC 2.0 network
*VPC2Api* | [**GetVpc2**](docs/VPC2Api.md#getvpc2) | **GET** /vpc2/{vpc-id} | Get a VPC 2.0 network
*VPC2Api* | [**ListVpc2**](docs/VPC2Api.md#listvpc2) | **GET** /vpc2 | List VPC 2.0 networks
*VPC2Api* | [**ListVpc2Nodes**](docs/VPC2Api.md#listvpc2nodes) | **GET** /vpc2/{vpc-id}/nodes | Get a list of nodes attached to a VPC 2.0 network
*VPC2Api* | [**UpdateVpc2**](docs/VPC2Api.md#updatevpc2) | **PUT** /vpc2/{vpc-id} | Update a VPC 2.0 network
*VPCsApi* | [**CreateVpc**](docs/VPCsApi.md#createvpc) | **POST** /vpcs | Create a VPC
*VPCsApi* | [**DeleteVpc**](docs/VPCsApi.md#deletevpc) | **DELETE** /vpcs/{vpc-id} | Delete a VPC
*VPCsApi* | [**GetVpc**](docs/VPCsApi.md#getvpc) | **GET** /vpcs/{vpc-id} | Get a VPC
*VPCsApi* | [**ListVpcs**](docs/VPCsApi.md#listvpcs) | **GET** /vpcs | List VPCs
*VPCsApi* | [**UpdateVpc**](docs/VPCsApi.md#updatevpc) | **PUT** /vpcs/{vpc-id} | Update a VPC


<a id="documentation-for-models"></a>
## Documentation for Models

 - [Model.Account](docs/Account.md)
 - [Model.Application](docs/Application.md)
 - [Model.AttachBaremetalsVpc2Request](docs/AttachBaremetalsVpc2Request.md)
 - [Model.AttachBlockRequest](docs/AttachBlockRequest.md)
 - [Model.AttachInstanceIso202Response](docs/AttachInstanceIso202Response.md)
 - [Model.AttachInstanceIso202ResponseIsoStatus](docs/AttachInstanceIso202ResponseIsoStatus.md)
 - [Model.AttachInstanceIsoRequest](docs/AttachInstanceIsoRequest.md)
 - [Model.AttachInstanceNetworkRequest](docs/AttachInstanceNetworkRequest.md)
 - [Model.AttachInstanceVpc2Request](docs/AttachInstanceVpc2Request.md)
 - [Model.AttachInstanceVpcRequest](docs/AttachInstanceVpcRequest.md)
 - [Model.AttachReservedIpRequest](docs/AttachReservedIpRequest.md)
 - [Model.AttachVpc2NodesRequest](docs/AttachVpc2NodesRequest.md)
 - [Model.Backup](docs/Backup.md)
 - [Model.BackupSchedule](docs/BackupSchedule.md)
 - [Model.Bandwidth](docs/Bandwidth.md)
 - [Model.Baremetal](docs/Baremetal.md)
 - [Model.BaremetalIpv4](docs/BaremetalIpv4.md)
 - [Model.BaremetalIpv6](docs/BaremetalIpv6.md)
 - [Model.Billing](docs/Billing.md)
 - [Model.Blockstorage](docs/Blockstorage.md)
 - [Model.Clusters](docs/Clusters.md)
 - [Model.ConnectionPool](docs/ConnectionPool.md)
 - [Model.ConvertReservedIpRequest](docs/ConvertReservedIpRequest.md)
 - [Model.CreateBaremetal202Response](docs/CreateBaremetal202Response.md)
 - [Model.CreateBaremetalRequest](docs/CreateBaremetalRequest.md)
 - [Model.CreateBlock202Response](docs/CreateBlock202Response.md)
 - [Model.CreateBlockRequest](docs/CreateBlockRequest.md)
 - [Model.CreateConnectionPool202Response](docs/CreateConnectionPool202Response.md)
 - [Model.CreateConnectionPoolRequest](docs/CreateConnectionPoolRequest.md)
 - [Model.CreateDatabase202Response](docs/CreateDatabase202Response.md)
 - [Model.CreateDatabaseDb202Response](docs/CreateDatabaseDb202Response.md)
 - [Model.CreateDatabaseDbRequest](docs/CreateDatabaseDbRequest.md)
 - [Model.CreateDatabaseRequest](docs/CreateDatabaseRequest.md)
 - [Model.CreateDatabaseUser202Response](docs/CreateDatabaseUser202Response.md)
 - [Model.CreateDatabaseUserRequest](docs/CreateDatabaseUserRequest.md)
 - [Model.CreateDnsDomain200Response](docs/CreateDnsDomain200Response.md)
 - [Model.CreateDnsDomainRecord201Response](docs/CreateDnsDomainRecord201Response.md)
 - [Model.CreateDnsDomainRecordRequest](docs/CreateDnsDomainRecordRequest.md)
 - [Model.CreateDnsDomainRequest](docs/CreateDnsDomainRequest.md)
 - [Model.CreateFirewallGroup201Response](docs/CreateFirewallGroup201Response.md)
 - [Model.CreateFirewallGroupRequest](docs/CreateFirewallGroupRequest.md)
 - [Model.CreateInstance202Response](docs/CreateInstance202Response.md)
 - [Model.CreateInstanceBackupScheduleRequest](docs/CreateInstanceBackupScheduleRequest.md)
 - [Model.CreateInstanceIpv4Request](docs/CreateInstanceIpv4Request.md)
 - [Model.CreateInstanceRequest](docs/CreateInstanceRequest.md)
 - [Model.CreateInstanceReverseIpv4Request](docs/CreateInstanceReverseIpv4Request.md)
 - [Model.CreateInstanceReverseIpv6Request](docs/CreateInstanceReverseIpv6Request.md)
 - [Model.CreateIso201Response](docs/CreateIso201Response.md)
 - [Model.CreateIsoRequest](docs/CreateIsoRequest.md)
 - [Model.CreateKubernetesCluster201Response](docs/CreateKubernetesCluster201Response.md)
 - [Model.CreateKubernetesClusterRequest](docs/CreateKubernetesClusterRequest.md)
 - [Model.CreateKubernetesClusterRequestNodePoolsInner](docs/CreateKubernetesClusterRequestNodePoolsInner.md)
 - [Model.CreateLoadBalancer202Response](docs/CreateLoadBalancer202Response.md)
 - [Model.CreateLoadBalancerForwardingRulesRequest](docs/CreateLoadBalancerForwardingRulesRequest.md)
 - [Model.CreateLoadBalancerRequest](docs/CreateLoadBalancerRequest.md)
 - [Model.CreateLoadBalancerRequestFirewallRulesInner](docs/CreateLoadBalancerRequestFirewallRulesInner.md)
 - [Model.CreateLoadBalancerRequestForwardingRulesInner](docs/CreateLoadBalancerRequestForwardingRulesInner.md)
 - [Model.CreateLoadBalancerRequestHealthCheck](docs/CreateLoadBalancerRequestHealthCheck.md)
 - [Model.CreateLoadBalancerRequestSsl](docs/CreateLoadBalancerRequestSsl.md)
 - [Model.CreateLoadBalancerRequestStickySession](docs/CreateLoadBalancerRequestStickySession.md)
 - [Model.CreateNetworkRequest](docs/CreateNetworkRequest.md)
 - [Model.CreateNodepools201Response](docs/CreateNodepools201Response.md)
 - [Model.CreateNodepoolsRequest](docs/CreateNodepoolsRequest.md)
 - [Model.CreateObjectStorage202Response](docs/CreateObjectStorage202Response.md)
 - [Model.CreateObjectStorageRequest](docs/CreateObjectStorageRequest.md)
 - [Model.CreateRegistryRequest](docs/CreateRegistryRequest.md)
 - [Model.CreateReservedIpRequest](docs/CreateReservedIpRequest.md)
 - [Model.CreateSnapshotCreateFromUrlRequest](docs/CreateSnapshotCreateFromUrlRequest.md)
 - [Model.CreateSnapshotRequest](docs/CreateSnapshotRequest.md)
 - [Model.CreateSshKeyRequest](docs/CreateSshKeyRequest.md)
 - [Model.CreateStartupScriptRequest](docs/CreateStartupScriptRequest.md)
 - [Model.CreateSubaccount201Response](docs/CreateSubaccount201Response.md)
 - [Model.CreateSubaccountRequest](docs/CreateSubaccountRequest.md)
 - [Model.CreateUserRequest](docs/CreateUserRequest.md)
 - [Model.CreateVpc2Request](docs/CreateVpc2Request.md)
 - [Model.CreateVpcRequest](docs/CreateVpcRequest.md)
 - [Model.Database](docs/Database.md)
 - [Model.DatabaseAddReadReplicaRequest](docs/DatabaseAddReadReplicaRequest.md)
 - [Model.DatabaseConnections](docs/DatabaseConnections.md)
 - [Model.DatabaseDb](docs/DatabaseDb.md)
 - [Model.DatabaseFerretdbCredentials](docs/DatabaseFerretdbCredentials.md)
 - [Model.DatabaseForkRequest](docs/DatabaseForkRequest.md)
 - [Model.DatabaseLatestBackup](docs/DatabaseLatestBackup.md)
 - [Model.DatabaseOldestBackup](docs/DatabaseOldestBackup.md)
 - [Model.DatabaseRestoreFromBackupRequest](docs/DatabaseRestoreFromBackupRequest.md)
 - [Model.DatabaseStartMigrationRequest](docs/DatabaseStartMigrationRequest.md)
 - [Model.DatabaseUsage](docs/DatabaseUsage.md)
 - [Model.DatabaseUsageCpu](docs/DatabaseUsageCpu.md)
 - [Model.DatabaseUsageDisk](docs/DatabaseUsageDisk.md)
 - [Model.DatabaseUsageMemory](docs/DatabaseUsageMemory.md)
 - [Model.DatabaseUser](docs/DatabaseUser.md)
 - [Model.DatabaseUserAccessControl](docs/DatabaseUserAccessControl.md)
 - [Model.DbaasAlerts](docs/DbaasAlerts.md)
 - [Model.DbaasAvailableOptions](docs/DbaasAvailableOptions.md)
 - [Model.DbaasMeta](docs/DbaasMeta.md)
 - [Model.DbaasMigration](docs/DbaasMigration.md)
 - [Model.DbaasMigrationCredentials](docs/DbaasMigrationCredentials.md)
 - [Model.DbaasPlan](docs/DbaasPlan.md)
 - [Model.DetachBaremetalVpc2Request](docs/DetachBaremetalVpc2Request.md)
 - [Model.DetachBlockRequest](docs/DetachBlockRequest.md)
 - [Model.DetachInstanceIso202Response](docs/DetachInstanceIso202Response.md)
 - [Model.DetachInstanceIso202ResponseIsoStatus](docs/DetachInstanceIso202ResponseIsoStatus.md)
 - [Model.DetachInstanceNetworkRequest](docs/DetachInstanceNetworkRequest.md)
 - [Model.DetachInstanceVpc2Request](docs/DetachInstanceVpc2Request.md)
 - [Model.DetachInstanceVpcRequest](docs/DetachInstanceVpcRequest.md)
 - [Model.DetachVpc2NodesRequest](docs/DetachVpc2NodesRequest.md)
 - [Model.DnsRecord](docs/DnsRecord.md)
 - [Model.DnsSoa](docs/DnsSoa.md)
 - [Model.Domain](docs/Domain.md)
 - [Model.FirewallGroup](docs/FirewallGroup.md)
 - [Model.FirewallRule](docs/FirewallRule.md)
 - [Model.ForwardingRule](docs/ForwardingRule.md)
 - [Model.GetAccount200Response](docs/GetAccount200Response.md)
 - [Model.GetBackup200Response](docs/GetBackup200Response.md)
 - [Model.GetBackupInformation200Response](docs/GetBackupInformation200Response.md)
 - [Model.GetBandwidthBaremetal200Response](docs/GetBandwidthBaremetal200Response.md)
 - [Model.GetBandwidthBaremetal200ResponseBandwidth](docs/GetBandwidthBaremetal200ResponseBandwidth.md)
 - [Model.GetBareMetalUserdata200Response](docs/GetBareMetalUserdata200Response.md)
 - [Model.GetBareMetalUserdata200ResponseUserData](docs/GetBareMetalUserdata200ResponseUserData.md)
 - [Model.GetBareMetalVnc200Response](docs/GetBareMetalVnc200Response.md)
 - [Model.GetBareMetalVnc200ResponseVnc](docs/GetBareMetalVnc200ResponseVnc.md)
 - [Model.GetBareMetalsUpgrades200Response](docs/GetBareMetalsUpgrades200Response.md)
 - [Model.GetBareMetalsUpgrades200ResponseUpgrades](docs/GetBareMetalsUpgrades200ResponseUpgrades.md)
 - [Model.GetBaremetal200Response](docs/GetBaremetal200Response.md)
 - [Model.GetDatabaseUsage200Response](docs/GetDatabaseUsage200Response.md)
 - [Model.GetDnsDomainDnssec200Response](docs/GetDnsDomainDnssec200Response.md)
 - [Model.GetDnsDomainSoa200Response](docs/GetDnsDomainSoa200Response.md)
 - [Model.GetInstanceBackupSchedule200Response](docs/GetInstanceBackupSchedule200Response.md)
 - [Model.GetInstanceIsoStatus200Response](docs/GetInstanceIsoStatus200Response.md)
 - [Model.GetInstanceIsoStatus200ResponseIsoStatus](docs/GetInstanceIsoStatus200ResponseIsoStatus.md)
 - [Model.GetInstanceNeighbors200Response](docs/GetInstanceNeighbors200Response.md)
 - [Model.GetInstanceUpgrades200Response](docs/GetInstanceUpgrades200Response.md)
 - [Model.GetInstanceUpgrades200ResponseUpgrades](docs/GetInstanceUpgrades200ResponseUpgrades.md)
 - [Model.GetInstanceUserdata200Response](docs/GetInstanceUserdata200Response.md)
 - [Model.GetInstanceUserdata200ResponseUserData](docs/GetInstanceUserdata200ResponseUserData.md)
 - [Model.GetInvoice200Response](docs/GetInvoice200Response.md)
 - [Model.GetInvoiceItems200Response](docs/GetInvoiceItems200Response.md)
 - [Model.GetInvoiceItems200ResponseInvoiceItemsInner](docs/GetInvoiceItems200ResponseInvoiceItemsInner.md)
 - [Model.GetInvoiceItems200ResponseMeta](docs/GetInvoiceItems200ResponseMeta.md)
 - [Model.GetInvoiceItems200ResponseMetaLinks](docs/GetInvoiceItems200ResponseMetaLinks.md)
 - [Model.GetIpv4Baremetal200Response](docs/GetIpv4Baremetal200Response.md)
 - [Model.GetIpv6Baremetal200Response](docs/GetIpv6Baremetal200Response.md)
 - [Model.GetKubernetesAvailableUpgrades200Response](docs/GetKubernetesAvailableUpgrades200Response.md)
 - [Model.GetKubernetesClustersConfig200Response](docs/GetKubernetesClustersConfig200Response.md)
 - [Model.GetKubernetesResources200Response](docs/GetKubernetesResources200Response.md)
 - [Model.GetKubernetesResources200ResponseResources](docs/GetKubernetesResources200ResponseResources.md)
 - [Model.GetKubernetesResources200ResponseResourcesBlockStorageInner](docs/GetKubernetesResources200ResponseResourcesBlockStorageInner.md)
 - [Model.GetKubernetesResources200ResponseResourcesLoadBalancerInner](docs/GetKubernetesResources200ResponseResourcesLoadBalancerInner.md)
 - [Model.GetKubernetesVersions200Response](docs/GetKubernetesVersions200Response.md)
 - [Model.GetLoadBalancerForwardingRule200Response](docs/GetLoadBalancerForwardingRule200Response.md)
 - [Model.GetNetwork200Response](docs/GetNetwork200Response.md)
 - [Model.GetNodepools200Response](docs/GetNodepools200Response.md)
 - [Model.GetReservedIp200Response](docs/GetReservedIp200Response.md)
 - [Model.GetSnapshot200Response](docs/GetSnapshot200Response.md)
 - [Model.GetSshKey200Response](docs/GetSshKey200Response.md)
 - [Model.GetStartupScript200Response](docs/GetStartupScript200Response.md)
 - [Model.GetVpc200Response](docs/GetVpc200Response.md)
 - [Model.GetVpc2200Response](docs/GetVpc2200Response.md)
 - [Model.HaltBaremetalsRequest](docs/HaltBaremetalsRequest.md)
 - [Model.HaltInstancesRequest](docs/HaltInstancesRequest.md)
 - [Model.Instance](docs/Instance.md)
 - [Model.InstanceV6NetworksInner](docs/InstanceV6NetworksInner.md)
 - [Model.InstanceVpc2](docs/InstanceVpc2.md)
 - [Model.Invoice](docs/Invoice.md)
 - [Model.Iso](docs/Iso.md)
 - [Model.IsoPublic](docs/IsoPublic.md)
 - [Model.ListAdvancedOptions200Response](docs/ListAdvancedOptions200Response.md)
 - [Model.ListApplications200Response](docs/ListApplications200Response.md)
 - [Model.ListAvailablePlansRegion200Response](docs/ListAvailablePlansRegion200Response.md)
 - [Model.ListAvailableVersions200Response](docs/ListAvailableVersions200Response.md)
 - [Model.ListBackups200Response](docs/ListBackups200Response.md)
 - [Model.ListBaremetalVpc2200Response](docs/ListBaremetalVpc2200Response.md)
 - [Model.ListBaremetals200Response](docs/ListBaremetals200Response.md)
 - [Model.ListBillingHistory200Response](docs/ListBillingHistory200Response.md)
 - [Model.ListBlocks200Response](docs/ListBlocks200Response.md)
 - [Model.ListConnectionPools200Response](docs/ListConnectionPools200Response.md)
 - [Model.ListDatabaseDbs200Response](docs/ListDatabaseDbs200Response.md)
 - [Model.ListDatabasePlans200Response](docs/ListDatabasePlans200Response.md)
 - [Model.ListDatabaseUsers200Response](docs/ListDatabaseUsers200Response.md)
 - [Model.ListDatabases200Response](docs/ListDatabases200Response.md)
 - [Model.ListDnsDomainRecords200Response](docs/ListDnsDomainRecords200Response.md)
 - [Model.ListDnsDomains200Response](docs/ListDnsDomains200Response.md)
 - [Model.ListFirewallGroupRules200Response](docs/ListFirewallGroupRules200Response.md)
 - [Model.ListFirewallGroups200Response](docs/ListFirewallGroups200Response.md)
 - [Model.ListInstanceIpv6Reverse200Response](docs/ListInstanceIpv6Reverse200Response.md)
 - [Model.ListInstanceIpv6Reverse200ResponseReverseIpv6sInner](docs/ListInstanceIpv6Reverse200ResponseReverseIpv6sInner.md)
 - [Model.ListInstancePrivateNetworks200Response](docs/ListInstancePrivateNetworks200Response.md)
 - [Model.ListInstanceVpc2200Response](docs/ListInstanceVpc2200Response.md)
 - [Model.ListInstanceVpcs200Response](docs/ListInstanceVpcs200Response.md)
 - [Model.ListInstances200Response](docs/ListInstances200Response.md)
 - [Model.ListInvoices200Response](docs/ListInvoices200Response.md)
 - [Model.ListIsos200Response](docs/ListIsos200Response.md)
 - [Model.ListKubernetesClusters200Response](docs/ListKubernetesClusters200Response.md)
 - [Model.ListLoadBalancerForwardingRules200Response](docs/ListLoadBalancerForwardingRules200Response.md)
 - [Model.ListLoadBalancers200Response](docs/ListLoadBalancers200Response.md)
 - [Model.ListMaintenanceUpdates200Response](docs/ListMaintenanceUpdates200Response.md)
 - [Model.ListMetalPlans200Response](docs/ListMetalPlans200Response.md)
 - [Model.ListNetworks200Response](docs/ListNetworks200Response.md)
 - [Model.ListObjectStorageClusters200Response](docs/ListObjectStorageClusters200Response.md)
 - [Model.ListObjectStorages200Response](docs/ListObjectStorages200Response.md)
 - [Model.ListOs200Response](docs/ListOs200Response.md)
 - [Model.ListPlans200Response](docs/ListPlans200Response.md)
 - [Model.ListPublicIsos200Response](docs/ListPublicIsos200Response.md)
 - [Model.ListRegions200Response](docs/ListRegions200Response.md)
 - [Model.ListRegistries200Response](docs/ListRegistries200Response.md)
 - [Model.ListRegistryPlans200Response](docs/ListRegistryPlans200Response.md)
 - [Model.ListRegistryPlans200ResponsePlans](docs/ListRegistryPlans200ResponsePlans.md)
 - [Model.ListRegistryRegions200Response](docs/ListRegistryRegions200Response.md)
 - [Model.ListRegistryRepositories200Response](docs/ListRegistryRepositories200Response.md)
 - [Model.ListReservedIps200Response](docs/ListReservedIps200Response.md)
 - [Model.ListServiceAlerts200Response](docs/ListServiceAlerts200Response.md)
 - [Model.ListServiceAlertsRequest](docs/ListServiceAlertsRequest.md)
 - [Model.ListSnapshots200Response](docs/ListSnapshots200Response.md)
 - [Model.ListSshKeys200Response](docs/ListSshKeys200Response.md)
 - [Model.ListStartupScripts200Response](docs/ListStartupScripts200Response.md)
 - [Model.ListSubaccounts200Response](docs/ListSubaccounts200Response.md)
 - [Model.ListUsers200Response](docs/ListUsers200Response.md)
 - [Model.ListVpc2200Response](docs/ListVpc2200Response.md)
 - [Model.ListVpcs200Response](docs/ListVpcs200Response.md)
 - [Model.Loadbalancer](docs/Loadbalancer.md)
 - [Model.LoadbalancerFirewallRule](docs/LoadbalancerFirewallRule.md)
 - [Model.LoadbalancerFirewallRulesInner](docs/LoadbalancerFirewallRulesInner.md)
 - [Model.LoadbalancerForwardRulesInner](docs/LoadbalancerForwardRulesInner.md)
 - [Model.LoadbalancerGenericInfo](docs/LoadbalancerGenericInfo.md)
 - [Model.LoadbalancerGenericInfoStickySessions](docs/LoadbalancerGenericInfoStickySessions.md)
 - [Model.LoadbalancerHealthCheck](docs/LoadbalancerHealthCheck.md)
 - [Model.Meta](docs/Meta.md)
 - [Model.MetaLinks](docs/MetaLinks.md)
 - [Model.Network](docs/Network.md)
 - [Model.NodepoolInstances](docs/NodepoolInstances.md)
 - [Model.Nodepools](docs/Nodepools.md)
 - [Model.ObjectStorage](docs/ObjectStorage.md)
 - [Model.Os](docs/Os.md)
 - [Model.PatchReservedIpsReservedIpRequest](docs/PatchReservedIpsReservedIpRequest.md)
 - [Model.Plans](docs/Plans.md)
 - [Model.PlansMetal](docs/PlansMetal.md)
 - [Model.PostFirewallsFirewallGroupIdRules201Response](docs/PostFirewallsFirewallGroupIdRules201Response.md)
 - [Model.PostFirewallsFirewallGroupIdRulesRequest](docs/PostFirewallsFirewallGroupIdRulesRequest.md)
 - [Model.PostInstancesInstanceIdIpv4ReverseDefaultRequest](docs/PostInstancesInstanceIdIpv4ReverseDefaultRequest.md)
 - [Model.PrivateNetworks](docs/PrivateNetworks.md)
 - [Model.PutSnapshotsSnapshotIdRequest](docs/PutSnapshotsSnapshotIdRequest.md)
 - [Model.RebootInstancesRequest](docs/RebootInstancesRequest.md)
 - [Model.RegenerateObjectStorageKeys201Response](docs/RegenerateObjectStorageKeys201Response.md)
 - [Model.RegenerateObjectStorageKeys201ResponseS3Credentials](docs/RegenerateObjectStorageKeys201ResponseS3Credentials.md)
 - [Model.Region](docs/Region.md)
 - [Model.Registry](docs/Registry.md)
 - [Model.RegistryDockerCredentials](docs/RegistryDockerCredentials.md)
 - [Model.RegistryDockerCredentialsAuths](docs/RegistryDockerCredentialsAuths.md)
 - [Model.RegistryDockerCredentialsAuthsRegistryRegionNameVultrcrCom](docs/RegistryDockerCredentialsAuthsRegistryRegionNameVultrcrCom.md)
 - [Model.RegistryKubernetesDockerCredentials](docs/RegistryKubernetesDockerCredentials.md)
 - [Model.RegistryKubernetesDockerCredentialsData](docs/RegistryKubernetesDockerCredentialsData.md)
 - [Model.RegistryKubernetesDockerCredentialsMetadata](docs/RegistryKubernetesDockerCredentialsMetadata.md)
 - [Model.RegistryMetadata](docs/RegistryMetadata.md)
 - [Model.RegistryMetadataSubscription](docs/RegistryMetadataSubscription.md)
 - [Model.RegistryMetadataSubscriptionBilling](docs/RegistryMetadataSubscriptionBilling.md)
 - [Model.RegistryPlan](docs/RegistryPlan.md)
 - [Model.RegistryRegion](docs/RegistryRegion.md)
 - [Model.RegistryRepository](docs/RegistryRepository.md)
 - [Model.RegistryStorage](docs/RegistryStorage.md)
 - [Model.RegistryUser](docs/RegistryUser.md)
 - [Model.ReinstallBaremetalRequest](docs/ReinstallBaremetalRequest.md)
 - [Model.ReinstallInstanceRequest](docs/ReinstallInstanceRequest.md)
 - [Model.ReservedIp](docs/ReservedIp.md)
 - [Model.RestoreInstance202Response](docs/RestoreInstance202Response.md)
 - [Model.RestoreInstance202ResponseStatus](docs/RestoreInstance202ResponseStatus.md)
 - [Model.RestoreInstanceRequest](docs/RestoreInstanceRequest.md)
 - [Model.SetDatabaseUserAclRequest](docs/SetDatabaseUserAclRequest.md)
 - [Model.Snapshot](docs/Snapshot.md)
 - [Model.Ssh](docs/Ssh.md)
 - [Model.StartInstancesRequest](docs/StartInstancesRequest.md)
 - [Model.StartKubernetesClusterUpgradeRequest](docs/StartKubernetesClusterUpgradeRequest.md)
 - [Model.StartMaintenanceUpdates200Response](docs/StartMaintenanceUpdates200Response.md)
 - [Model.StartVersionUpgrade200Response](docs/StartVersionUpgrade200Response.md)
 - [Model.StartVersionUpgradeRequest](docs/StartVersionUpgradeRequest.md)
 - [Model.Startup](docs/Startup.md)
 - [Model.Subaccount](docs/Subaccount.md)
 - [Model.UpdateAdvancedOptionsRequest](docs/UpdateAdvancedOptionsRequest.md)
 - [Model.UpdateBaremetalRequest](docs/UpdateBaremetalRequest.md)
 - [Model.UpdateBlockRequest](docs/UpdateBlockRequest.md)
 - [Model.UpdateConnectionPoolRequest](docs/UpdateConnectionPoolRequest.md)
 - [Model.UpdateDatabaseRequest](docs/UpdateDatabaseRequest.md)
 - [Model.UpdateDatabaseUserRequest](docs/UpdateDatabaseUserRequest.md)
 - [Model.UpdateDnsDomainRecordRequest](docs/UpdateDnsDomainRecordRequest.md)
 - [Model.UpdateDnsDomainRequest](docs/UpdateDnsDomainRequest.md)
 - [Model.UpdateDnsDomainSoaRequest](docs/UpdateDnsDomainSoaRequest.md)
 - [Model.UpdateFirewallGroupRequest](docs/UpdateFirewallGroupRequest.md)
 - [Model.UpdateInstanceRequest](docs/UpdateInstanceRequest.md)
 - [Model.UpdateKubernetesClusterRequest](docs/UpdateKubernetesClusterRequest.md)
 - [Model.UpdateLoadBalancerRequest](docs/UpdateLoadBalancerRequest.md)
 - [Model.UpdateLoadBalancerRequestHealthCheck](docs/UpdateLoadBalancerRequestHealthCheck.md)
 - [Model.UpdateNetworkRequest](docs/UpdateNetworkRequest.md)
 - [Model.UpdateNodepoolRequest](docs/UpdateNodepoolRequest.md)
 - [Model.UpdateNodepoolRequest1](docs/UpdateNodepoolRequest1.md)
 - [Model.UpdateObjectStorageRequest](docs/UpdateObjectStorageRequest.md)
 - [Model.UpdateRegistryRequest](docs/UpdateRegistryRequest.md)
 - [Model.UpdateRepositoryRequest](docs/UpdateRepositoryRequest.md)
 - [Model.UpdateSshKeyRequest](docs/UpdateSshKeyRequest.md)
 - [Model.UpdateStartupScriptRequest](docs/UpdateStartupScriptRequest.md)
 - [Model.UpdateUserRequest](docs/UpdateUserRequest.md)
 - [Model.UpdateVpc2Request](docs/UpdateVpc2Request.md)
 - [Model.UpdateVpcRequest](docs/UpdateVpcRequest.md)
 - [Model.User](docs/User.md)
 - [Model.UserUser](docs/UserUser.md)
 - [Model.ViewMigrationStatus200Response](docs/ViewMigrationStatus200Response.md)
 - [Model.VkeCluster](docs/VkeCluster.md)
 - [Model.Vpc](docs/Vpc.md)
 - [Model.Vpc2](docs/Vpc2.md)


<a id="documentation-for-authorization"></a>
## Documentation for Authorization


Authentication schemes defined for the API:
<a id="API Key"></a>
### API Key

- **Type**: Bearer Authentication

