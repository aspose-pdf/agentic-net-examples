using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Output PDF file
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Error: Input file not found - {file}");
                return;
            }
        }

        try
        {
            // Create the PdfFileEditor facade
            PdfFileEditor pdfEditor = new PdfFileEditor();

            // Concatenate the input PDFs into the output PDF
            // This method binds the source files internally and writes the result.
            pdfEditor.Concatenate(inputFiles, outputFile);

            Console.WriteLine($"Successfully concatenated {inputFiles.Length} PDFs into '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during concatenation: {ex.Message}");
        }
    }
}