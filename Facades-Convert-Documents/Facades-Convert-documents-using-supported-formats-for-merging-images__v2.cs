using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files that contain images
        string[] inputFiles = { "image1.pdf", "image2.pdf", "image3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        try
        {
            // Use PdfFileEditor (Facades) to concatenate the PDFs
            PdfFileEditor editor = new PdfFileEditor();
            bool merged = editor.Concatenate(inputFiles, outputFile);

            if (merged)
                Console.WriteLine($"PDFs merged successfully into '{outputFile}'.");
            else
                Console.Error.WriteLine("Merging operation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merging: {ex.Message}");
        }
    }
}