using System;
using System.Collections.Generic;
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
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Path for the temporary blank page PDF
        const string blankPagePath = "blank.pdf";

        // Output file that will contain the concatenated PDFs with separators
        const string outputPath = "merged_with_separators.pdf";

        // Ensure all input files exist
        foreach (var f in inputFiles)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Input file not found: {f}");
                return;
            }
        }

        // Create a single‑page blank PDF if it does not already exist
        if (!File.Exists(blankPagePath))
        {
            using (Document blankDoc = new Document())
            {
                // Add an empty page
                blankDoc.Pages.Add();
                // Save the blank page PDF
                blankDoc.Save(blankPagePath);
            }
        }

        // Build a new file list inserting the blank page between each input PDF
        List<string> filesWithSeparators = new List<string>();
        for (int i = 0; i < inputFiles.Length; i++)
        {
            filesWithSeparators.Add(inputFiles[i]);

            // Add a separator after every file except the last one
            if (i < inputFiles.Length - 1)
                filesWithSeparators.Add(blankPagePath);
        }

        // Use PdfFileEditor to concatenate the files
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(filesWithSeparators.ToArray(), outputPath);

        if (success)
            Console.WriteLine($"Successfully created '{outputPath}' with blank separator pages.");
        else
            Console.Error.WriteLine("Concatenation failed.");
    }
}