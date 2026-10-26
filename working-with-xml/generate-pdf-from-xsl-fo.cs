using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class XslFoToPdf
{
    static void Main()
    {
        // Paths to the source XML, XSL‑FO template (XSLT that produces FO), and the output PDF.
        const string xmlPath = "data.xml";
        const string xsltPath = "template.xsl";
        const string outputPdfPath = "result.pdf";

        // Verify that input files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSL‑FO template not found: {xsltPath}");
            return;
        }

        try
        {
            // Transform the XML into XSL‑FO using XslCompiledTransform.
            // The result is stored in a memory stream for further processing.
            using (MemoryStream foStream = new MemoryStream())
            {
                XslCompiledTransform transformer = new XslCompiledTransform();
                transformer.Load(xsltPath);

                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                using (XmlWriter foWriter = XmlWriter.Create(foStream, transformer.OutputSettings))
                {
                    transformer.Transform(xmlReader, foWriter);
                }

                // Reset stream position before feeding it to Aspose.Pdf.
                foStream.Position = 0;

                // Load the generated XSL‑FO into a Document using XslFoLoadOptions.
                // XslFoLoadOptions resides in the Aspose.Pdf namespace.
                using (Document pdfDoc = new Document(foStream, new XslFoLoadOptions()))
                {
                    // Save the resulting PDF. No SaveOptions are required for PDF output.
                    pdfDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}