using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        const string pdfPath = "input.pdf";
        // Output XML file path
        const string xmlPath = "output.xml";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Save the document as XML. The .xml extension tells Aspose.Pdf to use the XML format.
            pdfDocument.Save(xmlPath);
            
            Console.WriteLine($"PDF successfully converted to XML and saved at '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}