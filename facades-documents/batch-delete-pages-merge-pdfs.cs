using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Define each source PDF and the pages (1‑based) that must be removed.
        // Adjust the array as needed for your batch.
        var inputs = new (string Path, int[] PagesToDelete)[]
        {
            ("doc1.pdf", new int[] { 2, 5 }),   // delete pages 2 and 5 from doc1.pdf
            ("doc2.pdf", new int[] { 1 }),      // delete page 1 from doc2.pdf
            ("doc3.pdf", new int[0])            // no pages to delete from doc3.pdf
        };

        // Temporary folder to store the cleaned PDFs before concatenation.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdfCleaned");
        Directory.CreateDirectory(tempDir);

        var cleanedFiles = new List<string>();

        // PdfFileEditor does NOT implement IDisposable – instantiate once and reuse.
        PdfFileEditor editor = new PdfFileEditor();

        foreach (var (sourcePath, pagesToDelete) in inputs)
        {
            if (!File.Exists(sourcePath))
            {
                Console.Error.WriteLine($"Input file not found: {sourcePath}");
                continue;
            }

            // Path for the intermediate cleaned PDF.
            string cleanedPath = Path.Combine(
                tempDir,
                Path.GetFileNameWithoutExtension(sourcePath) + "_cleaned.pdf");

            // Load the PDF with the Document API (PdfFileEditor has no DeletePages).
            Document doc = new Document(sourcePath);

            if (pagesToDelete != null && pagesToDelete.Length > 0)
            {
                // Delete pages in descending order to keep indices stable.
                foreach (int pageNum in pagesToDelete.OrderByDescending(p => p))
                {
                    if (pageNum >= 1 && pageNum <= doc.Pages.Count)
                    {
                        doc.Pages.Delete(pageNum);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Page {pageNum} is out of range for '{sourcePath}'.");
                    }
                }
            }
            // Save the cleaned version.
            doc.Save(cleanedPath);

            cleanedFiles.Add(cleanedPath);
        }

        // Final merged PDF that contains all cleaned documents.
        string outputPath = "merged_cleaned.pdf";

        if (cleanedFiles.Count > 0)
        {
            // Concatenate the cleaned PDFs into a single document.
            // Concatenate expects an array of source file names.
            editor.Concatenate(cleanedFiles.ToArray(), outputPath);
            Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("No cleaned files were generated; merging skipped.");
        }

        // Optional: delete temporary cleaned files and folder.
        try
        {
            foreach (var file in cleanedFiles)
                File.Delete(file);
            Directory.Delete(tempDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}
