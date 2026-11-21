using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath      = "input.xml";
        const string xsltPath     = "transform.xslt";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xsltPath}");
            return;
        }

        // Transform XML to XSL‑FO using XslCompiledTransform
        MemoryStream foStream = new MemoryStream();
        try
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using (XmlReader xmlReader = XmlReader.Create(xmlPath))
            using (XmlWriter foWriter = XmlWriter.Create(foStream, xslt.OutputSettings))
            {
                xslt.Transform(xmlReader, foWriter);
            }

            // Reset stream position before loading into Aspose.Pdf
            foStream.Position = 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"XSL‑FO transformation failed: {ex.Message}");
            return;
        }

        // Load the XSL‑FO stream into a PDF Document
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        try
        {
            using (Document pdfDoc = new Document(foStream, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"PDF creation failed: {ex.Message}");
        }
        finally
        {
            foStream.Dispose();
        }
    }
}