using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for text-related types (if needed)

class Program
{
    static void Main()
    {
        // Paths to the input XML definition and the output PDF.
        const string xmlPath   = "table-definition.xml";
        const string pdfPath   = "rendered-table.pdf";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML file not found: {xmlPath}");
            return;
        }

        // Load the XML representation of a PDF (which includes the Table definition)
        // using XmlLoadOptions. This reconstructs the entire PDF structure,
        // including the Table object, in memory.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // The document now contains the table as defined in the XML.
            // Save the reconstructed PDF to the desired output file.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Table rendered and saved to '{pdfPath}'.");
    }
}