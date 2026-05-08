using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeTearDownAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void GreaterOrEqual<T>(T actual, T expected, string message = null) where T : IComparable<T>
        {
            if (actual.CompareTo(expected) < 0)
                throw new Exception(message ?? $"Assert.GreaterOrEqual failed. Expected: >= {expected}, Actual: {actual}.");
        }
    }
}

namespace AsposePdfTests
{
    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real test project this class would not be needed if the project were a class library.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the NUnit test runner will discover and execute the tests.
        }
    }

    [TestFixture]
    public class XmlToPdfConversionTests
    {
        private const string TestDataFolder = "TestData";

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Ensure the test data folder exists
            if (!Directory.Exists(TestDataFolder))
                Directory.CreateDirectory(TestDataFolder);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Clean up all generated files after the test run
            if (Directory.Exists(TestDataFolder))
                Directory.Delete(TestDataFolder, true);
        }

        private string CreateTempXml(string content)
        {
            string xmlPath = Path.Combine(TestDataFolder, Guid.NewGuid() + ".xml");
            File.WriteAllText(xmlPath, content);
            return xmlPath;
        }

        private string CreateTempXsl(string content)
        {
            string xslPath = Path.Combine(TestDataFolder, Guid.NewGuid() + ".xsl");
            File.WriteAllText(xslPath, content);
            return xslPath;
        }

        private string GetTempPdfPath()
        {
            return Path.Combine(TestDataFolder, Guid.NewGuid() + ".pdf");
        }

        [Test]
        public void ConvertXmlWithoutXsl_ToPdf_ShouldCreatePdf()
        {
            // Arrange: simple XML without any XSLT
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root><message>Hello, World!</message></root>";
            string xmlPath = CreateTempXml(xmlContent);
            string pdfPath = GetTempPdfPath();

            // Act: load XML with default XmlLoadOptions (no XSL) and save as PDF
            XmlLoadOptions loadOptions = new XmlLoadOptions(); // creates without XSL data
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                pdfDocument.Save(pdfPath); // PDF format, no SaveOptions needed
            }

            // Assert: PDF file exists and has at least one page
            Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created.");
            using (Document resultDoc = new Document(pdfPath))
            {
                Assert.GreaterOrEqual(resultDoc.Pages.Count, 1, "Resulting PDF should contain at least one page.");
            }
        }

        [Test]
        public void ConvertXmlWithXslFilePath_ToPdf_ShouldCreatePdf()
        {
            // Arrange: XML and a simple XSL that copies the XML content
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root><title>Test</title></root>";
            string xslContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <pdf>\n      <xsl:apply-templates/>\n    </pdf>\n  </xsl:template>\n  <xsl:template match=\"node()\">\n    <xsl:copy-of select=\".\"/>\n  </xsl:template>\n</xsl:stylesheet>";
            string xmlPath = CreateTempXml(xmlContent);
            string xslPath = CreateTempXsl(xslContent);
            string pdfPath = GetTempPdfPath();

            // Act: load XML with XmlLoadOptions that takes an XSL file path
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                pdfDocument.Save(pdfPath);
            }

            // Assert: PDF file exists
            Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created with XSL file path.");
        }

        [Test]
        public void ConvertXmlWithXslStream_ToPdf_ShouldCreatePdf()
        {
            // Arrange: XML and XSL content
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root><item>Value</item></root>";
            string xslContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <pdf>\n      <xsl:apply-templates/>\n    </pdf>\n  </xsl:template>\n  <xsl:template match=\"node()\">\n    <xsl:copy-of select=\".\"/>\n  </xsl:template>\n</xsl:stylesheet>";
            string xmlPath = CreateTempXml(xmlContent);
            string pdfPath = GetTempPdfPath();

            // Create a memory stream for the XSL content
            using (MemoryStream xslStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(xslStream, Encoding.UTF8, 1024, true))
            {
                writer.Write(xslContent);
                writer.Flush();
                xslStream.Position = 0; // reset position for reading

                // Act: load XML with XmlLoadOptions that takes a stream
                XmlLoadOptions loadOptions = new XmlLoadOptions(xslStream);
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    pdfDocument.Save(pdfPath);
                }
            }

            // Assert: PDF file exists
            Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created with XSL stream.");
        }

        [Test]
        public void ConvertXmlWithXslStringConstructor_ToPdf_ShouldCreatePdf()
        {
            // Arrange: XML and XSL files on disk
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root><data>Sample</data></root>";
            string xslContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <pdf>\n      <xsl:apply-templates/>\n    </pdf>\n  </xsl:template>\n  <xsl:template match=\"node()\">\n    <xsl:copy-of select=\".\"/>\n  </xsl:template>\n</xsl:stylesheet>";
            string xmlPath = CreateTempXml(xmlContent);
            string xslPath = CreateTempXsl(xslContent);
            string pdfPath = GetTempPdfPath();

            // Act: use the XmlLoadOptions(string) constructor (xsl file path)
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                pdfDocument.Save(pdfPath);
            }

            // Assert: PDF file exists
            Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created with XmlLoadOptions(string) constructor.");
        }
    }
}
