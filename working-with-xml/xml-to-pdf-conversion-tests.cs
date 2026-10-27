using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit.Framework stubs – added to allow compilation when the NUnit
// package is not referenced. Only the attributes and Assert members used in
// the tests are implemented.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    // Additional NUnit attributes that might be useful in the future.
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

namespace AsposePdfXmlToPdfTests
{
    [TestFixture]
    public class XmlToPdfConversionTests
    {
        // Helper to create a temporary file with given content and return its path.
        private string CreateTempFile(string content, string extension)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
            File.WriteAllText(tempPath, content);
            return tempPath;
        }

        // Helper to delete temporary files safely.
        private void Cleanup(params string[] paths)
        {
            foreach (var p in paths)
            {
                try { if (File.Exists(p)) File.Delete(p); } catch { /* ignore */ }
            }
        }

        [Test]
        public void ConvertXmlToPdf_Simple_ShouldCreatePdf()
        {
            // Simple XML source.
            string xml = @"<?xml version='1.0' encoding='UTF-8'?>
<document>
    <title>Simple Test</title>
    <body>This is a simple paragraph.</body>
</document>";

            // XSLT that transforms the XML into XSL‑FO for PDF generation.
            string xslt = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <xsl:output method='xml' indent='yes'/>
  <xsl:template match='/'>
    <fo:root>
      <fo:layout-master-set>
        <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'/>
      </fo:layout-master-set>
      <fo:page-sequence master-reference='A4'>
        <fo:flow flow-name='xsl-region-body'>
          <fo:block font-size='14pt' font-weight='bold'>
            <xsl:value-of select='document/title'/>
          </fo:block>
          <fo:block margin-top='10pt'>
            <xsl:value-of select='document/body'/>
          </fo:block>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>
  </xsl:template>
</xsl:stylesheet>";

            string? xmlPath = null;
            string? xsltPath = null;
            string? pdfPath = null;
            try
            {
                xmlPath = CreateTempFile(xml, ".xml");
                xsltPath = CreateTempFile(xslt, ".xslt");
                pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                // Use a using block for deterministic disposal of the Document.
                using (Document doc = new Document())
                {
                    // Bind the XML and XSLT; Aspose.Pdf will apply the transformation internally.
                    doc.BindXml(xmlPath, xsltPath);
                    // Save the result as PDF.
                    doc.Save(pdfPath);
                }

                // Verify that the PDF file was created and is not empty.
                Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created.");
                Assert.IsTrue(new FileInfo(pdfPath).Length > 0, "PDF file is empty.");
            }
            finally
            {
                Cleanup(xmlPath, xsltPath, pdfPath);
            }
        }

        [Test]
        public void ConvertXmlToPdf_MultiplePages_ShouldCreatePdfWithTwoPages()
        {
            // XML with two items that will be rendered on separate pages.
            string xml = @"<?xml version='1.0' encoding='UTF-8'?>
<report>
    <section>
        <title>First Page</title>
        <content>Content of the first page.</content>
    </section>
    <section>
        <title>Second Page</title>
        <content>Content of the second page.</content>
    </section>
</report>";

            // XSLT that creates two <fo:page-sequence> elements, one per section.
            string xslt = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0'
    xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
    xmlns:fo='http://www.w3.org/1999/XSL/Format'>
  <xsl:output method='xml' indent='yes'/>
  <xsl:template match='/'>
    <fo:root>
      <fo:layout-master-set>
        <fo:simple-page-master master-name='A4' page-height='29.7cm' page-width='21cm' margin='2cm'/>
      </fo:layout-master-set>

      <xsl:for-each select='report/section'>
        <fo:page-sequence master-reference='A4'>
          <fo:flow flow-name='xsl-region-body'>
            <fo:block font-size='16pt' font-weight='bold'>
              <xsl:value-of select='title'/>
            </fo:block>
            <fo:block margin-top='10pt'>
              <xsl:value-of select='content'/>
            </fo:block>
          </fo:flow>
        </fo:page-sequence>
      </xsl:for-each>

    </fo:root>
  </xsl:template>
</xsl:stylesheet>";

            string? xmlPath = null;
            string? xsltPath = null;
            string? pdfPath = null;
            try
            {
                xmlPath = CreateTempFile(xml, ".xml");
                xsltPath = CreateTempFile(xslt, ".xslt");
                pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                using (Document doc = new Document())
                {
                    doc.BindXml(xmlPath, xsltPath);
                    doc.Save(pdfPath);
                }

                Assert.IsTrue(File.Exists(pdfPath), "PDF file was not created.");
                // Simple validation: page count should be 2.
                using (Document result = new Document(pdfPath))
                {
                    Assert.AreEqual(2, result.Pages.Count, "PDF should contain two pages.");
                }
            }
            finally
            {
                Cleanup(xmlPath, xsltPath, pdfPath);
            }
        }

        [Test]
        public void ConvertXmlToPdf_InvalidXslt_ShouldThrowException()
        {
            // Minimal XML.
            string xml = @"<?xml version='1.0' encoding='UTF-8'?><root/>";

            // Malformed XSLT (missing closing tag).
            string xslt = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:template match='/'><fo:root/></xsl:template>";

            string? xmlPath = null;
            string? xsltPath = null;
            try
            {
                xmlPath = CreateTempFile(xml, ".xml");
                xsltPath = CreateTempFile(xslt, ".xslt");

                using (Document doc = new Document())
                {
                    // Expect an exception when binding malformed XSLT.
                    Assert.Throws<Exception>(() => doc.BindXml(xmlPath, xsltPath));
                }
            }
            finally
            {
                Cleanup(xmlPath, xsltPath);
            }
        }

        [Test]
        public void ConvertXmlToPdf_MissingXmlFile_ShouldThrowFileNotFoundException()
        {
            // XSLT that would be valid if XML existed.
            string xslt = @"<?xml version='1.0' encoding='UTF-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform'>
  <xsl:template match='/'><fo:root/></xsl:template>
</xsl:stylesheet>";

            string? xsltPath = null;
            try
            {
                xsltPath = CreateTempFile(xslt, ".xslt");
                string nonExistentXml = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xml");

                using (Document doc = new Document())
                {
                    // Expect FileNotFoundException because the XML file does not exist.
                    Assert.Throws<FileNotFoundException>(() => doc.BindXml(nonExistentXml, xsltPath));
                }
            }
            finally
            {
                Cleanup(xsltPath);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an
    // executable. Test runners will ignore this method.
    public static class Program
    {
        public static void Main() { /* No-op */ }
    }
}
