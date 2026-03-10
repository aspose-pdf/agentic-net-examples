using System;
using System.IO;
using Aspose.Pdf;

class XmlToPdfConverter
{
    static void Main()
    {
        // Paths to the source XML, XSLT stylesheet, and the output PDF.
        const string xmlFilePath = "input.xml";
        const string xslFilePath = "transform.xsl";
        const string pdfFilePath = "output.pdf";

        // Verify that the input files exist.
        if (!File.Exists(xmlFilePath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        if (!File.Exists(xslFilePath))
        {
            Console.Error.WriteLine($"XSLT file not found: {xslFilePath}");
            return;
        }

        // Initialize XmlLoadOptions with the XSLT file.
        // This tells Aspose.Pdf to apply the XSLT transformation while loading the XML.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslFilePath);

        // Load the XML (with XSLT) into a PDF document.
        using (Document pdfDocument = new Document(xmlFilePath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFilePath);
        }

        Console.WriteLine($"PDF generated successfully at: {pdfFilePath}");
    }
}