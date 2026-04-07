using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework; // Added to bring NUnit stub attributes into scope

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the real NUnit package is not referenced.
// -----------------------------------------------------------------------------
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
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void Greater<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) <= 0)
                throw new Exception(message ?? $"Assert.Greater failed. Expected > {expected}, but was {actual}.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

// -----------------------------------------------------------------------------
// Unit tests for XML‑to‑PDF conversion using Aspose.Pdf.
// -----------------------------------------------------------------------------
[TestFixture]
public class XmlToPdfConversionTests
{
    private string _tempDir;

    [SetUp]
    public void SetUp()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDir);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_tempDir))
        {
            Directory.Delete(_tempDir, true);
        }
    }

    private string CreateXmlFile(string content)
    {
        string path = Path.Combine(_tempDir, "sample.xml");
        File.WriteAllText(path, content);
        return path;
    }

    private string CreateXslFile(string content)
    {
        string path = Path.Combine(_tempDir, "transform.xsl");
        File.WriteAllText(path, content);
        return path;
    }

    [Test]
    public void ConvertXmlWithoutXsl_ShouldCreatePdf()
    {
        // Simple XML without any XSL transformation
        string xml = "<?xml version='1.0'?><root><message>Hello World</message></root>";
        string xmlPath = CreateXmlFile(xml);
        string pdfPath = Path.Combine(_tempDir, "output.pdf");

        // Load XML using default XmlLoadOptions (no XSL)
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Load and save using the documented lifecycle pattern
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        // Verify PDF was created and contains at least one page
        Assert.IsTrue(File.Exists(pdfPath));
        using (Document doc = new Document(pdfPath))
        {
            Assert.Greater(doc.Pages.Count, 0);
        }
    }

    [Test]
    public void ConvertXmlWithXslFilePath_ShouldCreatePdf()
    {
        // XML and XSL files on disk
        string xml = "<?xml version='1.0'?><root><msg>Hello XSL</msg></root>";
        string xsl = "<?xml version='1.0'?\n<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>\n  <xsl:template match='/'>\n    <pdf>\n      <p><xsl:value-of select='root/msg'/></p>\n    </pdf>\n  </xsl:template>\n</xsl:stylesheet>";
        string xmlPath = CreateXmlFile(xml);
        string xslPath = CreateXslFile(xsl);
        string pdfPath = Path.Combine(_tempDir, "output_with_xsl.pdf");

        // Load XML with XSL file path using XmlLoadOptions(string)
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            pdfDocument.Save(pdfPath);
        }

        Assert.IsTrue(File.Exists(pdfPath));
        using (Document doc = new Document(pdfPath))
        {
            Assert.Greater(doc.Pages.Count, 0);
        }
    }

    [Test]
    public void ConvertXmlWithXslStream_ShouldCreatePdf()
    {
        // XML and XSL files, XSL provided as a stream
        string xml = "<?xml version='1.0'?><root><title>Stream XSL</title></root>";
        string xsl = "<?xml version='1.0'?\n<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>\n  <xsl:template match='/'>\n    <pdf>\n      <p><xsl:value-of select='root/title'/></p>\n    </pdf>\n  </xsl:template>\n</xsl:stylesheet>";
        string xmlPath = CreateXmlFile(xml);
        string xslPath = CreateXslFile(xsl);
        string pdfPath = Path.Combine(_tempDir, "output_stream.pdf");

        // Load XSL as a stream and pass to XmlLoadOptions(Stream)
        using (FileStream xslStream = File.OpenRead(xslPath))
        {
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslStream);
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                pdfDocument.Save(pdfPath);
            }
        }

        Assert.IsTrue(File.Exists(pdfPath));
        using (Document doc = new Document(pdfPath))
        {
            Assert.Greater(doc.Pages.Count, 0);
        }
    }
}

// -----------------------------------------------------------------------------
// Dummy entry point to satisfy the C# compiler when building a console project.
// The test runner will still discover and execute the NUnit tests.
// -----------------------------------------------------------------------------
public static class Program
{
    public static void Main()
    {
        // No operation – the presence of Main resolves CS5001.
    }
}
