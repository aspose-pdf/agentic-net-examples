using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfConcatenator
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = {
            "doc1.pdf",
            "doc2.pdf",
            "doc3.pdf"
        };

        // Output file that will contain the concatenated result
        const string outputFile = "merged_with_blank_pages.pdf";

        // Verify that all input files exist
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Create a temporary PDF that contains a single blank page
        string blankPageFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        try
        {
            using (Document blankDoc = new Document())
            {
                // Add an empty page
                blankDoc.Pages.Add();
                // Save the blank page PDF
                blankDoc.Save(blankPageFile);
            }

            // Build a new array that interleaves the blank page between each input PDF
            // Example: [doc1, blank, doc2, blank, doc3]
            string[] filesWithBlanks = new string[inputFiles.Length * 2 - 1];
            for (int i = 0, j = 0; i < inputFiles.Length; i++)
            {
                filesWithBlanks[j++] = inputFiles[i];
                if (i < inputFiles.Length - 1)
                {
                    filesWithBlanks[j++] = blankPageFile;
                }
            }

            // Perform concatenation using PdfFileEditor
            PdfFileEditor editor = new PdfFileEditor();
            bool success = editor.Concatenate(filesWithBlanks, outputFile);

            if (success)
                Console.WriteLine($"Successfully concatenated PDFs with blank pages into '{outputFile}'.");
            else
                Console.Error.WriteLine("Concatenation failed.");
        }
        finally
        {
            // Clean up the temporary blank page file
            if (File.Exists(blankPageFile))
            {
                try { File.Delete(blankPageFile); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}