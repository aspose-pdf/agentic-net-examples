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
            "first.pdf",
            "second.pdf",
            "third.pdf"
        };

        // Output file that will contain the concatenated PDFs with blank separator pages
        const string outputFile = "merged_with_separators.pdf";

        // Validate input files existence
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // -----------------------------------------------------------------
        // Step 1: Create a single‑page blank PDF that will be used as a separator
        // -----------------------------------------------------------------
        string blankPagePath = Path.Combine(Path.GetTempPath(), "blank_separator.pdf");

        // Ensure any previous temporary file is removed
        if (File.Exists(blankPagePath))
            File.Delete(blankPagePath);

        // Create a new PDF document, add an empty page, and save it as the separator
        using (Document blankDoc = new Document())
        {
            // Add a blank page (default size A4)
            blankDoc.Pages.Add();

            // Save the blank PDF to the temporary location
            blankDoc.Save(blankPagePath);
        }

        // -----------------------------------------------------------------
        // Step 2: Build the sequence: file1, blank, file2, blank, ..., lastFile
        // -----------------------------------------------------------------
        var filesToMerge = new string[inputFiles.Length * 2 - 1];
        int idx = 0;
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesToMerge[idx++] = inputFiles[i];
            // Insert a blank separator after each file except the last one
            if (i < inputFiles.Length - 1)
                filesToMerge[idx++] = blankPagePath;
        }

        // -----------------------------------------------------------------
        // Step 3: Concatenate the prepared list using PdfFileEditor
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(filesToMerge, outputFile);

        if (success)
            Console.WriteLine($"Successfully created: {outputFile}");
        else
            Console.Error.WriteLine("Concatenation failed.");

        // Clean up the temporary blank page file
        try { File.Delete(blankPagePath); } catch { /* ignore cleanup errors */ }
    }
}