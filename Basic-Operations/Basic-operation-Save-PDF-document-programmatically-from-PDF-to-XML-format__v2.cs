using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses, including XmlSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXmlPath = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize XmlSaveOptions as required for non‑PDF output formats
            XmlSaveOptions xmlOptions = new XmlSaveOptions();

            // Save the document as XML using the explicit save options
            pdfDocument.Save(outputXmlPath, xmlOptions);
        }

        Console.WriteLine($"PDF successfully saved as XML to '{outputXmlPath}'.");
    }
}