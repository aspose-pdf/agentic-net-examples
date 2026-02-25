using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including XmlSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output XML file path
        const string xmlPath = "output.xml";

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Load the PDF document and ensure deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize XmlSaveOptions (required for non‑PDF output)
            XmlSaveOptions saveOptions = new XmlSaveOptions();

            // Save the document as XML using the options
            pdfDocument.Save(xmlPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{xmlPath}'.");
    }
}