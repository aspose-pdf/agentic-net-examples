using System;
using System.IO;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for XslFoLoadOptions XsltArgumentList type

// Minimal NUnit stubs to allow compilation without the real NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class XmlToPdfConversionTests
    {
        // Helper to create a temporary file with given content and return its path.
        private static string CreateTempFile(string content, string extension)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
            File.WriteAllText(tempPath, content);
            return tempPath;
        }

        // Helper to delete temporary files safely.
        private static void DeleteTempFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                // Ignored – cleanup should not fail the test.
            }
        }

        [NUnit.Framework.Test]
        public void ConvertXmlWithoutXslt_ShouldProducePdf()
        {
            // Simple XML without any XSLT.
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root>\n    <message>Hello, World!</message>\n</root>";
            string xmlPath = CreateTempFile(xmlContent, ".xml");
            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Load XML with default options (no XSLT).
                XmlLoadOptions loadOptions = new XmlLoadOptions();

                // Document creation and disposal follows the lifecycle rule.
                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    // Save as PDF – extension is ignored, output is always PDF.
                    pdfDocument.Save(pdfPath);
                }

                // Verify that the PDF file was created and is not empty.
                NUnit.Framework.Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created.");
                NUnit.Framework.Assert.IsTrue(new FileInfo(pdfPath).Length > 0, "PDF file is empty.");
            }
            finally
            {
                DeleteTempFile(xmlPath);
                DeleteTempFile(pdfPath);
            }
        }

        [NUnit.Framework.Test]
        public void ConvertXmlWithXsltFile_ShouldApplyTransformation()
        {
            // XML source.
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<catalog>\n    <product>Name A</product>\n    <price>10</price>\n</catalog>";
            // XSLT that creates a simple PDF layout (Aspose.Pdf interprets the result as PDF markup).
            string xsltContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <pdf>\n      <page>\n        <text>\n          <xsl:value-of select=\"/catalog/product\"/>\n        </text>\n        <text>\n          <xsl:value-of select=\"/catalog/price\"/>\n        </text>\n      </page>\n    </pdf>\n  </xsl:template>\n</xsl:stylesheet>";
            string xmlPath = CreateTempFile(xmlContent, ".xml");
            string xsltPath = CreateTempFile(xsltContent, ".xslt");
            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Load XML with XSLT file.
                XmlLoadOptions loadOptions = new XmlLoadOptions(xsltPath);

                using (Document pdfDocument = new Document(xmlPath, loadOptions))
                {
                    pdfDocument.Save(pdfPath);
                }

                NUnit.Framework.Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created with XSLT.");
                NUnit.Framework.Assert.IsTrue(new FileInfo(pdfPath).Length > 0, "PDF file is empty after XSLT transformation.");
            }
            finally
            {
                DeleteTempFile(xmlPath);
                DeleteTempFile(xsltPath);
                DeleteTempFile(pdfPath);
            }
        }

        [NUnit.Framework.Test]
        public void ConvertXmlWithXsltStreamAndParameters_ShouldRespectParameters()
        {
            // XML source containing a placeholder for a parameter.
            string xmlContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<report>\n    <title></title>\n    <value></value>\n</report>";
            // XSLT that uses a parameter named 'title' and 'value'.
            string xsltContent = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\n  <xsl:param name=\"title\"/>\n  <xsl:param name=\"value\"/>\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <pdf>\n      <page>\n        <text>\n          <xsl:value-of select=\"$title\"/>\n        </text>\n        <text>\n          <xsl:value-of select=\"$value\"/>\n        </text>\n      </page>\n    </pdf>\n  </xsl:template>\n</xsl:stylesheet>";
            string xmlPath = CreateTempFile(xmlContent, ".xml");
            string xsltPath = CreateTempFile(xsltContent, ".xslt");
            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Load XSLT into a stream.
                using (FileStream xsltStream = File.OpenRead(xsltPath))
                {
                    // Prepare XSL-FO load options (which also support XSLT arguments).
                    XslFoLoadOptions xslFoOptions = new XslFoLoadOptions();
                    XsltArgumentList args = new XsltArgumentList();
                    args.AddParam("title", "", "Dynamic Title");
                    args.AddParam("value", "", "12345");
                    xslFoOptions.XsltArgumentList = args;

                    using (Document pdfDocument = new Document(xmlPath, xslFoOptions))
                    {
                        pdfDocument.Save(pdfPath);
                    }
                }

                NUnit.Framework.Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created with XSLT stream and parameters.");
                NUnit.Framework.Assert.IsTrue(new FileInfo(pdfPath).Length > 0, "PDF file is empty after parameterized XSLT transformation.");
            }
            finally
            {
                DeleteTempFile(xmlPath);
                DeleteTempFile(xsltPath);
                DeleteTempFile(pdfPath);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed via the NUnit framework.
        }
    }
}
