using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including PptxSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Path to the directory containing the source PDF
        string dataDir = "YOUR_DATA_DIRECTORY";

        // Input PDF file
        string pdfPath = Path.Combine(dataDir, "input.pdf");

        // Desired output PPTX file
        string pptxPath = Path.Combine(dataDir, "output.pptx");

        // Verify the input file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create an instance of PptxSaveOptions (default constructor)
            PptxSaveOptions saveOptions = new PptxSaveOptions();

            // Save the PDF as a PPTX file using the specified save options
            pdfDocument.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{pptxPath}'");
    }
}