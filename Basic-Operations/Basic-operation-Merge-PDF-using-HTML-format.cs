using System;
using System.IO;
using Aspose.Pdf.Facades;

class MergePdfExample
{
    static void Main()
    {
        // Define the full paths of the PDF files to be merged.
        // Use Path.Combine to build platform‑independent paths.
        string dataDir = "/Users/fahadadeelqazi/Projects/Aspose/agents/aspose-pdf-api/_worker_0";
        string[] inputFiles =
        {
            Path.Combine(dataDir, "C:\\Data\\file1.pdf"),
            Path.Combine(dataDir, "C:\\Data\\file2.pdf")
        };

        // Define the output PDF file path.
        string outputFile = Path.Combine(dataDir, "merged_output.pdf");

        // Ensure that the input files exist before attempting concatenation.
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a PdfFileEditor instance.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Optional: skip corrupted files instead of throwing an exception.
        pdfEditor.CorruptedFileAction = PdfFileEditor.ConcatenateCorruptedFileAction.ConcatenateIgnoringCorrupted;

        // Perform the concatenation.
        bool success = pdfEditor.Concatenate(inputFiles, outputFile);

        if (success)
        {
            Console.WriteLine($"PDF files merged successfully into: {outputFile}");
        }
        else
        {
            Console.WriteLine("PDF concatenation failed.");
        }

        // If any corrupted files were ignored, report them.
        if (pdfEditor.CorruptedItems != null && pdfEditor.CorruptedItems.Length > 0)
        {
            Console.WriteLine("The following input files were corrupted and ignored:");
            foreach (var item in pdfEditor.CorruptedItems)
            {
                Console.WriteLine($"Index: {item.Index}, Reason: {item.Exception.Message}");
            }
        }
    }
}