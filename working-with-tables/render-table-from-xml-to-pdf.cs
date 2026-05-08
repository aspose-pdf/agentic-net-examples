using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document, XmlLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Paths to the source XML definition and the target PDF file
        const string xmlPath = "table-definition.xml";
        const string pdfPath = "rendered-table.pdf";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        // Load the XML definition (PDFXML format) using XmlLoadOptions.
        // XmlLoadOptions resides directly in the Aspose.Pdf namespace, so no extra using directive is required.
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // The Document constructor with XmlLoadOptions reconstructs the Table object defined in the XML.
        using (Document pdfDocument = new Document(xmlPath, loadOptions))
        {
            // Save the reconstructed document as a PDF.
            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"Table rendered successfully to '{pdfPath}'.");
    }
}