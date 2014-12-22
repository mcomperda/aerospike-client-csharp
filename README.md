Aerospike C# Client Package
===========================

Aerospike C# client.  This package contains full source code for two Visual Studio solutions:

* **Aerospike.sln**    
	C# library and demonstration programs with full functionality including "AerospikeClient.QueryAggregate()" (secondary index query with user-defined aggregation).  Aggregations require a Lua interpreter (written in C) on the client side.  This results in a dependency on an unmanaged DLL.  Supported compile targets are x64 (64-bit) and x86 (32-bit).  The projects are:
	
	* **AerospikeClient**    
		C# client library.
	* **AerospikeDemo**    
		C# demonstration program for examples and benchmarks.
	* **AerospikeAdmin**    
		Aerospike user administration.  This application is only valid for enterprise servers that are configured to require user authentication.

* **AerospikeLite.sln**    
	C# library and demonstration programs without "AerospikeClient.QueryAggregate()" (secondary index query with user-defined aggregation).  This solution is fully managed.  Supported compile targets are AnyCPU, x64 (64-bit) and x86 (32-bit).  The included projects are appended with the "Lite" suffix. 

	* **AerospikeClientLite**    
	* **AerospikeDemoLite**    
	* **AerospikeAdminLite**    

All solutions support the following configurations:

* Debug
* Release
* Debug IIS : Same as Debug, but store reusable buffers in HttpContext.Current.Items.
* Release IIS : Same as Release, but store reusable buffers in HttpContext.Current.Items.

**Prerequisites**

* Windows 7/Windows Server 2008 or greater, preferably 64 bit.
* Visual Studio 2010 or greater, preferably 64 bit.  Visual C# 2010 Express or greater will also work.
* .NET 4.0 or greater.  Visual Studio 2010 will install .NET 4.0 by default.

**Build Instructions**

* Double click on Aerospike.sln.  The solution will be opened in Visual Studio.
* Click menu Build -> BuildSolution.  The target platform is 64 bit by default.

**Demonstration Instructions**

* Ensure Aerospike server has been installed and is operational.
* Open "Aerospike.sln" in Visual Studio.
* Press F5 to run demonstration program.
* Enter server hostname/port of the Aerospike server.
* Enter server's namespace and set name if they are different from the default.
* Click on an example (i.e. ServerInfo).  The example source code will be displayed.
* Press "Start" button.  

The console window will display the log results of the example.
Long running examples can be stopped with "Stop" button.
