using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf; // Use Document class for PDF manipulation

class PdfPageRemover
{
    // Removes the specified pages from each PDF file in parallel.
    // pdfPaths: full paths to source PDF files.
    // pagesToRemove: list of 1‑based page numbers to delete from each PDF.
    // outputFolder: folder where the processed PDFs will be saved.
    public static void RemovePagesFromMultiplePdfs(
        List<string> pdfPaths,
        List<int> pagesToRemove,
        string outputFolder)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));
        if (pagesToRemove == null) throw new ArgumentNullException(nameof(pagesToRemove));
        if (string.IsNullOrWhiteSpace(outputFolder)) throw new ArgumentException("Output folder must be specified.", nameof(outputFolder));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Prepare a descending‑sorted array of pages to delete – this prevents index shifting when removing multiple pages.
        int[] pagesArray = pagesToRemove.Distinct().OrderByDescending(p => p).ToArray();

        // Process each PDF file concurrently.
        Parallel.ForEach(pdfPaths, pdfPath =>
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                return;
            }

            // Build the output file name (e.g., "document_trimmed.pdf").
            string outputPath = Path.Combine(
                outputFolder,
                Path.GetFileNameWithoutExtension(pdfPath) + "_trimmed.pdf");

            try
            {
                // Load the PDF document.
                Document pdfDoc = new Document(pdfPath);

                // Remove the requested pages. The Pages collection is 1‑based.
                foreach (int pageNum in pagesArray)
                {
                    if (pageNum >= 1 && pageNum <= pdfDoc.Pages.Count)
                    {
                        pdfDoc.Pages.Delete(pageNum);
                    }
                }

                // Save the modified document.
                pdfDoc.Save(outputPath);

                Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }

    // Example entry point.
    static void Main()
    {
        // Example input: list of PDF files.
        var pdfFiles = new List<string>
        {
            @"C:\Docs\Report1.pdf",
            @"C:\Docs\Report2.pdf",
            @"C:\Docs\Report3.pdf"
        };

        // Pages to remove (1‑based indexing).
        var pagesToDelete = new List<int> { 2, 5, 7 };

        // Output directory for the trimmed PDFs.
        string outputDir = @"C:\Docs\Trimmed";

        RemovePagesFromMultiplePdfs(pdfFiles, pagesToDelete, outputDir);
    }
}
