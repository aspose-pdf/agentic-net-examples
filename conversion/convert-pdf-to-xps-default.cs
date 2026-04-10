using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXpsPath = "output.xps";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create XpsSaveOptions with default settings
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the save options
            pdfDocument.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXpsPath}");
    }
}