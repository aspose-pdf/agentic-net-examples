using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define the PDFs to process and the pages that should be removed from each file.
        // The key is the source PDF path, the value is a tuple containing the destination path
        // and an array of 1‑based page numbers to delete.
        var jobs = new Dictionary<string, (string outputPath, int[] pagesToDelete)>
        {
            { "doc1.pdf", ("doc1_clean.pdf", new[] { 2, 5 }) },
            { "doc2.pdf", ("doc2_clean.pdf", new[] { 1 }) },
            { "doc3.pdf", ("doc3_clean.pdf", new[] { 3, 4, 7 }) }
        };

        // Process all PDFs in parallel to improve throughput.
        Parallel.ForEach(jobs, job =>
        {
            string inputPath = job.Key;
            string outputPath = job.Value.outputPath;
            int[] pages = job.Value.pagesToDelete;

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the PDF using the Document class (PdfFileEditor does not provide DeletePages).
                Document pdfDoc = new Document(inputPath);

                // Delete pages in descending order to keep the remaining page numbers stable.
                foreach (int pageNum in pages.OrderByDescending(p => p))
                {
                    if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                    {
                        pdfDoc.Pages.Delete(pageNum);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Page {pageNum} out of range for '{inputPath}'.");
                    }
                }

                // Save the modified PDF to the destination path.
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Processed '{inputPath}' → '{outputPath}' (removed pages: {string.Join(",", pages)})");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        });

        Console.WriteLine("All PDF page‑deletion jobs completed.");
    }
}
