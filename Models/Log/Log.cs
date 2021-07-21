using System;
using System.IO;
using System.Xml;
using NLog;
using NLog.Config;

namespace FileSystemCopy
{
    public class Log
    {
        public static Logger Instance { get; private set; }
        static Log()
        {
#if DEBUG
            string writeTo = "cur_dir";
#else
            string writeTo = "app_data";
#endif
            XmlReader xr = XmlReader.Create(new StringReader(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<nlog xmlns=""http://www.nlog-project.org/schemas/NLog.xsd""
      xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
      autoReload=""true""
      throwExceptions=""false"">
  <extensions>
    <add prefix=""custom"" assembly=""FileSystemCopy""/>
  </extensions>
  <variable name=""companyName"" value=""PauliusUrmonas"" />
  <variable name=""appName"" value=""FileSystemCopy"" />
  <variable name=""fileName"" value=""log"" />
  
  <targets async=""true"">
    <target xsi:type=""File""
            name=""cur_dir""
            layout=""${longdate} - ${level:uppercase=true}(${callsite:className=true:includeSourcePath=false:methodName=true}): ${message}${onexception:${newline}EXCEPTION\: ${exception:format=ToString}}""
            fileName=""${basedir}\${fileName}.log""
            keepFileOpen=""false""
            archiveFileName=""${basedir}\${fileName}_${shortdate}.{##}.log""
            archiveNumbering=""Sequence""
            archiveEvery=""Month""
            maxArchiveFiles=""36""
            />
    <target xsi:type=""File""
            name=""app_data""
            layout=""${longdate} - ${level:uppercase=true}(${callsite:className=true:includeSourcePath=false:methodName=true}): ${message}${onexception:${newline}EXCEPTION\: ${exception:format=ToString}}""
            fileName=""${specialfolder:folder=ApplicationData}\${companyName}\${appName}\${fileName}.log""
            keepFileOpen=""false""
            archiveFileName=""${specialfolder:folder=ApplicationData}\${companyName}\${appName}\${fileName}_${shortdate}.{##}.log""
            archiveNumbering=""Sequence""
            archiveEvery=""Month""
            maxArchiveFiles=""36""
            />
    
    <target xsi:type=""EventLog""
            name=""eventlog""
            source=""${appName}""
            layout=""${message}${newline}${exception:format=ToString}""
            />
  </targets>
  <rules>
    <logger name=""*"" writeTo=""" + writeTo + @""" minlevel=""Trace"" />
    <logger name=""*"" writeTo=""eventlog"" minlevel=""Fatal"" />
  </rules>
</nlog>"));
            XmlLoggingConfiguration config = new XmlLoggingConfiguration(xr, null);
            LogManager.Configuration = config;
            Instance = LogManager.GetCurrentClassLogger();
        }
    }
}
