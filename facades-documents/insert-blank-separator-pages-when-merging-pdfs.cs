using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = new string[]
        {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Desired output file
        const string outputFile = "merged_with_separators.pdf";

        // Validate input files
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a temporary blank page PDF (single blank page)
        string blankPagePath = Path.GetTempFileName();
        using (Document blankDoc = new Document())
        {
            // Add an empty page
            blankDoc.Pages.Add();
            // Save the blank page PDF
            blankDoc.Save(blankPagePath);
        }

        // If there is only one input file, just copy it to the output
        if (inputFiles.Length == 1)
        {
            File.Copy(inputFiles[0], outputFile, overwrite: true);
            File.Delete(blankPagePath);
            Console.WriteLine($"Single file copied to '{outputFile}'.");
            return;
        }

        // Temporary files used for intermediate concatenation results
        string tempPrev = Path.GetTempFileName();
        string tempNext = Path.GetTempFileName();

        // First concatenation: combine first two PDFs with a blank separator
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(inputFiles[0], inputFiles[1], blankPagePath, tempPrev);
        if (!success)
        {
            Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
            CleanupTempFiles(blankPagePath, tempPrev, tempNext);
            return;
        }

        // Process remaining files, inserting a blank page between each
        for (int i = 2; i < inputFiles.Length; i++)
        {
            // Concatenate the current intermediate result with the next PDF, inserting a blank page
            success = editor.Concatenate(tempPrev, inputFiles[i], blankPagePath, tempNext);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to concatenate file '{inputFiles[i]}'.");
                CleanupTempFiles(blankPagePath, tempPrev, tempNext);
                return;
            }

            // Prepare for next iteration: swap temp files
            File.Delete(tempPrev);
            string swap = tempPrev;
            tempPrev = tempNext;
            tempNext = swap;
        }

        // Final result is in tempPrev; move it to the desired output location
        File.Move(tempPrev, outputFile, overwrite: true);

        // Clean up temporary resources
        CleanupTempFiles(blankPagePath, tempPrev, tempNext);

        Console.WriteLine($"Merged PDF with separators saved to '{outputFile}'.");
    }

    // Helper method to delete temporary files safely
    private static void CleanupTempFiles(params string[] paths)
    {
        foreach (var path in paths)
        {
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                // Ignored – best‑effort cleanup
            }
        }
    }
}