using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        string[] inputFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputFile = "merged_with_separators.pdf";

        // Validate input files
        foreach (string file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Path for the temporary blank separator page
        string blankPagePath = Path.Combine(Path.GetTempPath(), "blank_separator.pdf");

        try
        {
            // Create a single blank page PDF using Document (lifecycle rule)
            using (Document blankDoc = new Document())
            {
                // Add an empty page
                blankDoc.Pages.Add();
                // Save the blank PDF
                blankDoc.Save(blankPagePath);
            }

            // Build the list of files to concatenate, inserting the blank page between PDFs
            List<string> filesToConcat = new List<string>();
            for (int i = 0; i < inputFiles.Length; i++)
            {
                filesToConcat.Add(inputFiles[i]);
                // Add separator after each file except the last one
                if (i < inputFiles.Length - 1)
                {
                    filesToConcat.Add(blankPagePath);
                }
            }

            // Use PdfFileEditor (Facades) to concatenate the PDFs
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(filesToConcat.ToArray(), outputFile);

            Console.WriteLine($"Merged PDF with separators saved to '{outputFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary blank page file
            if (File.Exists(blankPagePath))
            {
                try { File.Delete(blankPagePath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}