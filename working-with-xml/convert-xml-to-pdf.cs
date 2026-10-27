using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source XML and the resulting PDF
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML using the core XmlLoadOptions.
        // No XSLT is specified, so the default transformation (if any) is applied.
        Aspose.Pdf.XmlLoadOptions loadOptions = new Aspose.Pdf.XmlLoadOptions();

        // Load the XML into a Document. The Document constructor accepts the file path and load options.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            doc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}