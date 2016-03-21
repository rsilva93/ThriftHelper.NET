using System;
using System.IO;
using ThriftHelper.NET.Enums;
using ThriftHelper.NET.Models;
using ThriftHelper.NET.Util;

namespace ThriftHelper.NET
{
    public class ThriftCmd
    {
        public DirectoryModel Execute(Language language, string thriftIDLCode)
        {
            try
            {
                string formattedLanguage = Formatter.FormatLanguage(language);
                string guid = Guid.NewGuid().ToString();
                string tempPath = Path.GetTempPath();
                string tempPathOut = Path.Combine(tempPath, guid, "Out");
                string tempPathMap = Path.Combine(tempPath, guid, "Map");
                string mapFilePath = Path.Combine(tempPathMap, "Code.thrift");
                string arguments = $"--gen \"{formattedLanguage}\" -out \"{tempPathOut}\" \"{mapFilePath}\"";

                Directory.CreateDirectory(tempPathMap);
                Directory.CreateDirectory(tempPathOut);
                File.WriteAllText(mapFilePath, thriftIDLCode);

                ThriftExe.Execute(arguments);

                var directoryModel = new DirectoryModel(guid);
                directoryModel.FillFromFileSystem(tempPathOut);

                return directoryModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
