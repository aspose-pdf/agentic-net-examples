using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output XPS file path
        const string outputXpsPath = "output.xps";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize XpsSaveOptions with default settings
            Aspose.Pdf.XpsSaveOptions xpsOptions = new Aspose.Pdf.XpsSaveOptions();

            // Save the document as XPS using the save options
            pdfDocument.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXpsPath}");
    }
}