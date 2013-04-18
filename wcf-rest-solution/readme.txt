read me
-----------
The solution demonstrates Wcf Service accessing like rest service, having two services named

1. Echo Service
2. Product Service

These service are rest services and can consume via browser alone.
e.g
http://localhost:port/web/services/echoservice.svc/echo/helloSomeMessage
http://localhost:port/web/services/productservice.svc/products/100 ( returns json )
http://localhost:port/web/services/productservice.svc/product/15 ( returns json )
