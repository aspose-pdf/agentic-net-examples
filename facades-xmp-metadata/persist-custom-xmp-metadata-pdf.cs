using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

[TestFixture]
public class PdfMetadataTests
{
    private const string TestFolder = "TestOutput";

    [SetUp]
    public void SetUp()
    {
        if (!Directory.Exists(TestFolder))
            Directory.CreateDirectory(TestFolder);
    }

    [Test]
    public void BaseUrl_CreatorTool_Nickname_ShouldBePersisted()
    {
        // Arrange: create a minimal PDF document
        string sourcePath = Path.Combine(TestFolder, "source.pdf");
        string resultPath = Path.Combine(TestFolder, "result.pdf");

        using (Document doc = new Document())
        {
            doc.Pages.Add(); // add a blank page
            doc.Save(sourcePath); // save the source PDF
        }

        // Act: set metadata using PdfFileInfo and save to a new file
        using (PdfFileInfo info = new PdfFileInfo(sourcePath))
        {
            // Standard property
            info.Creator = "MyCreatorTool";

            // Custom metadata entries
            info.SetMetaInfo("BaseUrl", "https://example.com");
            info.SetMetaInfo("Nickname", "TestDocument");

            // Persist changes to a new PDF file
            info.SaveNewInfo(resultPath);
        }

        // Assert: read back the metadata and verify values
        using (PdfFileInfo readInfo = new PdfFileInfo(resultPath))
        {
            string creator = readInfo.Creator;
            string baseUrl = readInfo.GetMetaInfo("BaseUrl");
            string nickname = readInfo.GetMetaInfo("Nickname");

            Assert.AreEqual("MyCreatorTool", creator, "Creator property mismatch.");
            Assert.AreEqual("https://example.com", baseUrl, "BaseUrl metadata mismatch.");
            Assert.AreEqual("TestDocument", nickname, "Nickname metadata mismatch.");
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the real work is performed by the NUnit tests.
    }
}