Readme
-----------------

First create a website in IIS. Just run iis.bat file (with admin rights), it will ask you a new site name and its port, than it will automatically create a application pool in IIS with runtime set to .net 4.0 and create a new website on mentioned name:port 


To make https enable, please follow the steps given below

1. Create a self signed certificate in IIS
2. Add certificate in application by edit bindings and add https in site bindings with appropriate port.
3. Last thing to make sure that https port is open and site is running

In wpf-client, you need to configure the service reference
e.g: http://localhost:4455/echo-service.svc

After that you can build the project.

Note: before running wpf-client you need to remove multiple endpoint bindings (from app.config) to only single one. 
