using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] inputFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Destination file for the concatenated result
        const string outputFile = "merged_resized.pdf";

        // Verify that all input files exist
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // List to keep paths of the intermediate resized PDFs
        List<string> resizedFiles = new List<string>();

        // Resize each PDF to 1024x768 points (default space units)
        foreach (var srcPath in inputFiles)
        {
            // Create a unique temporary file name for the resized PDF
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // PdfFileEditor provides a direct method to resize page contents
            PdfFileEditor editor = new PdfFileEditor();
            // Passing null for the pages array applies the operation to all pages
            editor.ResizeContents(srcPath, tempPath, null, 1024, 768);

            resizedFiles.Add(tempPath);
        }

        // Concatenate all resized PDFs into a single document
        PdfFileEditor concatEditor = new PdfFileEditor();
        concatEditor.Concatenate(resizedFiles.ToArray(), outputFile);

        // Clean up temporary files
        foreach (var temp in resizedFiles)
        {
            try
            {
                File.Delete(temp);
            }
            catch
            {
                // Ignored – best‑effort cleanup
            }
        }

        Console.WriteLine($"Resized and concatenated PDF saved to '{outputFile}'.");
    }
}