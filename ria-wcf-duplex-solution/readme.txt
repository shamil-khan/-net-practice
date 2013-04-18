read me
-----------
The solution demonstrates Wcf Service, Wcf Duplex Service and Ria Service consumption on silverlight end as well as at console end.

The solution was developed using VS 2010 and just migrated to VS 2012(need refactoring).

The solution contains following 4 projects i.e.

MyWork.Service -> It contains the core classes WcfService, RiaService and WcfDuplexService

MyWork.SampleApplication.Web -> It hosts all three services, as well as, also host Silverlight app.

MyWork.SilverlightClient -> It consumes all three services at silverlight end.

MyWork.Console.Client -> It consumes all three services at console end.


Note: Before executing the project, you need to set which project is the startup project b/w console or web(silverlight)