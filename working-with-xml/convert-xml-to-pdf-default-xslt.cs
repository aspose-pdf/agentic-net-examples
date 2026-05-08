using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the input XML file and the output PDF file.
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Initialize load options for XML without providing an XSL file.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Load the XML and convert it to a PDF document.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the generated PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully at '{pdfPath}'.");
    }
}