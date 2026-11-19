using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Run each test and output result.
        RunTest(SimpleXsltConversion_ShouldCreatePdf, nameof(SimpleXsltConversion_ShouldCreatePdf));
        RunTest(XsltWithParameter_ShouldCreatePdf, nameof(XsltWithParameter_ShouldCreatePdf));
        RunTest(XsltWithExternalImage_ShouldCreatePdf, nameof(XsltWithExternalImage_ShouldCreatePdf));
    }

    private static void RunTest(Func<bool> testMethod, string testName)
    {
        try
        {
            bool result = testMethod();
            Console.WriteLine(result ? $"PASS {testName}" : $"FAIL {testName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL {testName}: {ex.GetType().Name} - {ex.Message}");
        }
    }

    // Helper to create a temporary file with the specified content.
    private static string CreateTempFile(string content, string extension)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
        File.WriteAllText(tempPath, content);
        return tempPath;
    }

    // Helper to delete temporary files safely.
    private static void DeleteIfExists(string path)
    {
        try { if (File.Exists(path)) File.Delete(path); } catch { /* ignore */ }
    }

    // Helper to transform XML + XSLT into XSL‑FO.
    private static string TransformToFo(string xmlPath, string xsltPath, XsltArgumentList args = null)
    {
        string foPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".fo");
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltPath);
        using (FileStream foStream = new FileStream(foPath, FileMode.Create, FileAccess.Write))
        using (XmlWriter writer = XmlWriter.Create(foStream, xslt.OutputSettings))
        {
            if (args != null)
                xslt.Transform(xmlPath, args, writer);
            else
                xslt.Transform(xmlPath, writer);
        }
        return foPath;
    }

    // Test 1: Simple XSLT conversion.
    private static bool SimpleXsltConversion_ShouldCreatePdf()
    {
        string xmlContent = "<?xml version=\"1.0\"?>\n<root>\n    <title>Hello World</title>\n</root>";
        string xsltContent = "<?xml version=\"1.0\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"\n                xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <fo:root>\n      <fo:layout-master-set>\n        <fo:simple-page-master master-name=\"simple\">\n          <fo:region-body/>\n        </fo:simple-page-master>\n      </fo:layout-master-set>\n      <fo:page-sequence master-reference=\"simple\">\n        <fo:flow flow-name=\"xsl-region-body\">\n          <fo:block font-size=\"14pt\"><xsl:value-of select=\"/root/title\"/></fo:block>\n        </fo:flow>\n      </fo:page-sequence>\n    </fo:root>\n  </xsl:template>\n</xsl:stylesheet>";

        string xmlPath = CreateTempFile(xmlContent, ".xml");
        string xsltPath = CreateTempFile(xsltContent, ".xslt");
        string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        string foPath = null;

        try
        {
            // Transform to XSL‑FO.
            foPath = TransformToFo(xmlPath, xsltPath);

            // Load the generated XSL‑FO and save as PDF.
            using (Document doc = new Document(foPath, new XslFoLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            return File.Exists(pdfPath) && new FileInfo(pdfPath).Length > 0;
        }
        finally
        {
            DeleteIfExists(xmlPath);
            DeleteIfExists(xsltPath);
            DeleteIfExists(foPath);
            DeleteIfExists(pdfPath);
        }
    }

    // Test 2: XSLT with a parameter.
    private static bool XsltWithParameter_ShouldCreatePdf()
    {
        string xmlContent = "<?xml version=\"1.0\"?>\n<root>\n    <title>Placeholder</title>\n</root>";
        string xsltContent = "<?xml version=\"1.0\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"\n                xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\n  <xsl:param name=\"docTitle\"/>\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <fo:root>\n      <fo:layout-master-set>\n        <fo:simple-page-master master-name=\"simple\">\n          <fo:region-body/>\n        </fo:simple-page-master>\n      </fo:layout-master-set>\n      <fo:page-sequence master-reference=\"simple\">\n        <fo:flow flow-name=\"xsl-region-body\">\n          <fo:block font-size=\"14pt\"><xsl:value-of select=\"$docTitle\"/></fo:block>\n        </fo:flow>\n      </fo:page-sequence>\n    </fo:root>\n  </xsl:template>\n</xsl:stylesheet>";

        string xmlPath = CreateTempFile(xmlContent, ".xml");
        string xsltPath = CreateTempFile(xsltContent, ".xslt");
        string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        string foPath = null;

        try
        {
            // Transform with a parameter into XSL‑FO.
            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("docTitle", "", "Parameterized Title");
            foPath = TransformToFo(xmlPath, xsltPath, args);

            // Load the generated XSL‑FO and save as PDF.
            using (Document doc = new Document(foPath, new XslFoLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            return File.Exists(pdfPath) && new FileInfo(pdfPath).Length > 0;
        }
        finally
        {
            DeleteIfExists(xmlPath);
            DeleteIfExists(xsltPath);
            DeleteIfExists(foPath);
            DeleteIfExists(pdfPath);
        }
    }

    // Test 3: XSLT that references an external image.
    private static bool XsltWithExternalImage_ShouldCreatePdf()
    {
        // 1x1 PNG image (base64).
        string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
        byte[] imageBytes = Convert.FromBase64String(base64Png);
        string imagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
        File.WriteAllBytes(imagePath, imageBytes);

        string xmlContent = "<?xml version=\"1.0\"?>\n<root/>";
        string xsltContent = $"<?xml version=\"1.0\"?>\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\"\n                xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\n  <xsl:output method=\"xml\"/>\n  <xsl:template match=\"/\">\n    <fo:root>\n      <fo:layout-master-set>\n        <fo:simple-page-master master-name=\"simple\">\n          <fo:region-body/>\n        </fo:simple-page-master>\n      </fo:layout-master-set>\n      <fo:page-sequence master-reference=\"simple\">\n        <fo:flow flow-name=\"xsl-region-body\">\n          <fo:block>\n            <fo:external-graphic src=\"url('{imagePath.Replace("\\", "/")}')\" content-width=\"50pt\"/>\n          </fo:block>\n        </fo:flow>\n      </fo:page-sequence>\n    </fo:root>\n  </xsl:template>\n</xsl:stylesheet>";

        string xmlPath = CreateTempFile(xmlContent, ".xml");
        string xsltPath = CreateTempFile(xsltContent, ".xslt");
        string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        string foPath = null;

        try
        {
            // Transform to XSL‑FO (no parameters needed).
            foPath = TransformToFo(xmlPath, xsltPath);

            // Load the generated XSL‑FO and save as PDF.
            using (Document doc = new Document(foPath, new XslFoLoadOptions()))
            {
                doc.Save(pdfPath);
            }

            return File.Exists(pdfPath) && new FileInfo(pdfPath).Length > 0;
        }
        finally
        {
            DeleteIfExists(xmlPath);
            DeleteIfExists(xsltPath);
            DeleteIfExists(foPath);
            DeleteIfExists(pdfPath);
            DeleteIfExists(imagePath);
        }
    }
}
