using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document and ensure proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize XpsSaveOptions with default settings
            XpsSaveOptions saveOptions = new XpsSaveOptions();

            // Save the document as XPS using the save options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: '{outputPath}'");
    }
}