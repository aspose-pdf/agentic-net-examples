using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML, the XSL stylesheet (which may reference external CSS),
        // and the desired PDF output.
        const string xmlPath   = "input.xml";
        const string xslPath   = "style.xsl";   // XSL that applies CSS styling
        const string pdfPath   = "styled_output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"XSL file not found: {xslPath}");
            return;
        }

        // Create XmlLoadOptions with the XSL file. The XSL can contain
        // references to external CSS files; Aspose.Pdf will apply those styles
        // during the XML‑to‑PDF conversion.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML document using the options and convert it to PDF.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF. No SaveOptions are needed because the
            // target format is PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}