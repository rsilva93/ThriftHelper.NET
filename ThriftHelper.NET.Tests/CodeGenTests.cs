using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using ThriftHelper.NET.Enums;
using System.Linq;

namespace ThriftHelper.NET.Tests
{
    [TestClass]
    public class CodeGenTests
    {
        private string GetIDLCode()
        {
            var builder = new StringBuilder();
            builder.AppendLine("namespace * Message");
            builder.AppendLine("struct MessageObject");
            builder.AppendLine("{");
            builder.AppendLine("   1:  i32 id,");
            builder.AppendLine("   2:  string content");
            builder.AppendLine("}");
            builder.AppendLine("service MessageService");
            builder.AppendLine("{");
            builder.AppendLine("    list<MessageObject> getMessages();");
            builder.AppendLine("    MessageObject getMessageById(i32 id);");
            builder.AppendLine("}");

            return builder.ToString();
        }

        [TestMethod]
        public void CanGenerateCsharp()
        {
            var cmd = new ThriftCmd();
            var virtualDirectory = cmd.Execute(Language.CSharp, GetIDLCode());

            Assert.IsNotNull(virtualDirectory);
            Assert.AreEqual(0, virtualDirectory.Files.Count);
            Assert.AreEqual(1, virtualDirectory.Directories.Count);
            Assert.IsNotNull(virtualDirectory.Directories.FirstOrDefault());
            Assert.AreEqual("Message", virtualDirectory.Directories.FirstOrDefault().Name);
            Assert.AreEqual(2, virtualDirectory.Directories.FirstOrDefault().Files.Count);
            Assert.AreEqual(0, virtualDirectory.Directories.FirstOrDefault().Directories.Count);
            Assert.IsNotNull(virtualDirectory.Directories.FirstOrDefault().Files.FirstOrDefault());
            Assert.AreEqual("MessageObject.cs", virtualDirectory.Directories.FirstOrDefault().Files.FirstOrDefault().Name);
        }
    }
}
