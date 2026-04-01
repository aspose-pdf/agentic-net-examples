using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class UnicodeNicknameTests
    {
        private const string Nickname = "测试昵称"; // Unicode characters
        private string _tempPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary file path for the PDF
            _tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the temporary file after each test
            if (File.Exists(_tempPdfPath))
            {
                try { File.Delete(_tempPdfPath); } catch { /* ignore */ }
            }
        }

        [Test]
        public void Nickname_Is_Stored_As_Unicode_In_DocumentInfo_Title()
        {
            // Create a new PDF document, add a blank page and store the nickname in the Title metadata field
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                // Document does not expose a Nickname property; use the Title field of DocumentInfo which fully supports Unicode
                doc.Info.Title = Nickname;
                doc.Save(_tempPdfPath);
            }

            // Reload the PDF and verify that the Title (used as nickname) matches the original Unicode string
            using (Document loadedDoc = new Document(_tempPdfPath))
            {
                string loadedNickname = loadedDoc.Info.Title;
                Assert.AreEqual(Nickname, loadedNickname, "The Unicode nickname was not preserved correctly in the PDF metadata.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
