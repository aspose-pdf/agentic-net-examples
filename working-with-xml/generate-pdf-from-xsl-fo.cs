using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, XslFoLoadOptions)

class XslFoToPdfConverter
{
    static void Main()
    {
        // Paths to the source XML, the XSL‑FO stylesheet, and the desired PDF output.
        const string xmlPath      = "source.xml";
        const string xsltPath     = "template.xsl";   // XSL‑FO stylesheet (produces FO)
        const string outputPdf    = "result.pdf";

        // Verify that input files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xsltPath))
        {
            Console.Error.WriteLine($"XSL‑FO stylesheet not found: {xsltPath}");
            return;
        }

        try
        {
            // Transform the XML document into an XSL‑FO stream using .NET's XslCompiledTransform.
            // The resulting FO is held in a memory stream to avoid temporary files.
            using (MemoryStream foStream = new MemoryStream())
            {
                // Prepare the XSLT processor.
                XslCompiledTransform xslt = new XslCompiledTransform();

                // Load the XSL‑FO stylesheet.
                xslt.Load(xsltPath);

                // Execute the transformation: XML -> XSL‑FO.
                using (XmlReader xmlReader = XmlReader.Create(xmlPath))
                using (XmlWriter foWriter = XmlWriter.Create(foStream, xslt.OutputSettings))
                {
                    xslt.Transform(xmlReader, foWriter);
                }

                // Reset the stream position so Aspose.Pdf can read from the beginning.
                foStream.Position = 0;

                // Load the XSL‑FO content into an Aspose.Pdf Document.
                // XslFoLoadOptions tells Aspose.Pdf that the input is XSL‑FO.
                using (Document pdfDoc = new Document(foStream, new XslFoLoadOptions()))
                {
                    // Save the document as PDF. No SaveOptions are needed because PDF is the default format.
                    pdfDoc.Save(outputPdf);
                }
            }

            Console.WriteLine($"PDF successfully created: {outputPdf}");
        }
        catch (Exception ex)
        {
            // Catch any errors (e.g., malformed XML/FO, file I/O issues) and report them.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}