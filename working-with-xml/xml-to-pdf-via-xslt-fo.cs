using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to source XML, XSLT (which may contain multiple namespaces) and output PDF.
        const string xmlPath = "input.xml";
        const string xsltPath = "transform.xslt";
        const string pdfPath = "output.pdf";

        // Verify files exist.
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

        // Load the XSLT. The XSLT can declare any number of namespaces;
        // the XslCompiledTransform engine resolves them correctly.
        XslCompiledTransform xslt = new XslCompiledTransform();
        XsltSettings xsltSettings = new XsltSettings(enableDocumentFunction: true, enableScript: false);
        XmlUrlResolver resolver = new XmlUrlResolver(); // resolves any includes/imports.
        xslt.Load(xsltPath, xsltSettings, resolver);

        // Transform the XML to XSL‑FO (the format Aspose.Pdf expects for PDF creation).
        // The result is written to a memory stream to avoid intermediate files.
        using (MemoryStream foStream = new MemoryStream())
        {
            // The XSLT may need parameters; they can be supplied via XsltArgumentList if required.
            XsltArgumentList args = new XsltArgumentList();
            // Example of adding a parameter (optional):
            // args.AddParam("exampleParam", "", "value");

            // Perform the transformation.
            xslt.Transform(xmlPath, args, foStream);
            foStream.Position = 0; // Reset stream for reading.

            // Load the generated XSL‑FO into a PDF document.
            // XslFoLoadOptions is used because the source is XSL‑FO.
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();

            using (Document pdfDoc = new Document(foStream, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfPath);
            }
        }

        Console.WriteLine($"PDF successfully created at '{pdfPath}'.");
    }
}