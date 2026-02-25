using System;
using System.IO;
using Aspose.Pdf; // MhtLoadOptions and Document are in this namespace

class Program
{
    static void Main()
    {
        // Path to the input MHT file
        const string inputMhtPath = "input.mht";

        // Path where the resulting PDF will be saved
        const string outputPdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputMhtPath))
        {
            Console.Error.WriteLine($"File not found: {inputMhtPath}");
            return;
        }

        // Initialize load options for MHT format
        MhtLoadOptions loadOptions = new MhtLoadOptions();

        // Load the MHT file into a PDF Document and ensure deterministic disposal
        using (Document pdfDocument = new Document(inputMhtPath, loadOptions))
        {
            // Save the document as PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"MHT file successfully converted to PDF: '{outputPdfPath}'");
    }
}