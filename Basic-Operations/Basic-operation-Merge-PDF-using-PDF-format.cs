using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfMergeExample
{
    static void Main()
    {
        // Define the source PDF files to be merged.
        // Ensure that these files exist in the specified location.
        string[] sourceFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            // Add more file names as needed.
        };

        // Define the output file path.
        string outputFile = "merged_output.pdf";

        // Verify that all source files exist before attempting the merge.
        foreach (string filePath in sourceFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Source file not found - {filePath}");
                return;
            }
        }

        // Create an instance of PdfFileEditor.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Optional: configure behavior for corrupted files.
        // pdfEditor.CorruptedFileAction = PdfFileEditor.ConcatenateCorruptedFileAction.ConcatenateIgnoringCorrupted;

        // Perform the concatenation using the overload that accepts an array of file paths.
        // This method handles opening and closing of the files internally.
        bool success = pdfEditor.Concatenate(sourceFiles, outputFile);

        if (success)
        {
            Console.WriteLine($"PDF files merged successfully into '{outputFile}'.");
        }
        else
        {
            // If the operation failed, retrieve the last exception for diagnostics.
            Console.WriteLine("PDF merge failed.");
            if (pdfEditor.LastException != null)
            {
                Console.WriteLine($"Exception: {pdfEditor.LastException.Message}");
            }
        }
    }
}