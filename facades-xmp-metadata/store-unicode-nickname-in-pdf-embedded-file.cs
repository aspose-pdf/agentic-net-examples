using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

// Stub NUnit framework if not referenced
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

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
    using NUnit.Framework;

    [TestFixture]
    public class NicknameUnicodeTests
    {
        [Test]
        public void StoreUnicodeNickname()
        {
            // Unicode nickname to test
            string unicodeNickname = "昵称😊";

            // Create a new PDF document in a using block (ensures disposal)
            using (var doc = new Document())
            {
                // Add a blank page (required for a valid PDF)
                doc.Pages.Add();

                // Create a dummy file attachment with some content
                using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes("dummy content")))
                {
                    // FileSpecification(string filePath, string description) constructor
                    var fileSpec = new FileSpecification("dummy.txt", "dummy description");
                    // Assign the stream to the Contents property
                    fileSpec.Contents = contentStream;
                    // Set the Unicode name (nickname) on the file specification
                    fileSpec.UnicodeName = unicodeNickname;
                    // Add the file specification to the document's EmbeddedFiles collection
                    doc.EmbeddedFiles.Add(fileSpec);
                }

                // Save the document to a memory stream
                using (var outStream = new MemoryStream())
                {
                    doc.Save(outStream);
                    outStream.Position = 0; // Reset for reading

                    // Load the document back from the memory stream
                    using (var loadedDoc = new Document(outStream))
                    {
                        // EmbeddedFiles collection is 1‑based indexed
                        var loadedSpec = loadedDoc.EmbeddedFiles[1];
                        // Verify that the UnicodeName was persisted correctly
                        Assert.AreEqual(unicodeNickname, loadedSpec.UnicodeName, "Unicode nickname was not stored correctly.");
                    }
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are discovered via the stubbed NUnit attributes.
    }
}
