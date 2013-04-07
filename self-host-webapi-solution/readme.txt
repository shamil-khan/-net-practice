read me
--------

Self Host webapi solution contains two projects i.e.

1. SelfHost
It will open a port as defined in app.config to host the service. For opening the port it need administrative priviliges. After executing the program you can conusme the service in any browser as it is rest service.

2. SelfClient
It will consume the service, which is hosted by SelfHost application, so Self Host should executed first before running this application. Also th service address is in app.config. If you change the host-address, you also change the service address as well to the same value.
