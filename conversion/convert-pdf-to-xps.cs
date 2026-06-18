using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize XpsSaveOptions with default settings
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the specified options
            pdfDoc.Save(outputPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputPath}");
    }
}