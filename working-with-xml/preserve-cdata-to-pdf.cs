using System;
using System.IO;
using Aspose.Pdf;

class PreserveCDataToPdf
{
    static void Main()
    {
        // Paths for input XML and output PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Verify the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML file using XmlLoadOptions.
        // This preserves CDATA sections as they appear in the source XML.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Document constructor with (string, LoadOptions) follows the required lifecycle rule.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF. No additional SaveOptions are needed because the
            // target format is PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with CDATA preserved: {pdfPath}");
    }
}