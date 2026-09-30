using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchPdfProcessor
{
    static void Main()
    {
        // Define input PDF files and the pages to delete from each (1‑based indexing)
        var filesToProcess = new Dictionary<string, int[]>
        {
            // Example: delete pages 2 and 4 from "doc1.pdf"
            { "doc1.pdf", new int[] { 2, 4 } },

            // Example: delete page 1 from "doc2.pdf"
            { "doc2.pdf", new int[] { 1 } },

            // Add more entries as needed
            // { "path/to/other.pdf", new int[] { 3, 5, 7 } },
        };

        // Output path for the final concatenated PDF
        const string finalOutputPath = "merged_cleaned.pdf";

        // List to hold paths of the intermediate cleaned PDFs
        var cleanedPdfPaths = new List<string>();

        // Process each source PDF: delete specified pages and save to a temporary file
        foreach (var kvp in filesToProcess)
        {
            string sourcePath = kvp.Key;
            int[] pagesToDelete = kvp.Value;

            if (!File.Exists(sourcePath))
            {
                Console.Error.WriteLine($"Source file not found: {sourcePath}");
                continue;
            }

            // Create a temporary file for the cleaned PDF
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Load the PDF with Document (Aspose.Pdf) – PdfFileEditor has no DeletePages method in this version
            using (Document pdfDoc = new Document(sourcePath))
            {
                // Sort pages descending so removal does not affect subsequent indices
                Array.Sort(pagesToDelete);
                for (int i = pagesToDelete.Length - 1; i >= 0; i--)
                {
                    int pageNumber = pagesToDelete[i];
                    if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                    {
                        pdfDoc.Pages.Delete(pageNumber);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Page {pageNumber} is out of range for file {sourcePath} (1‑{pdfDoc.Pages.Count})");
                    }
                }

                // Save the cleaned PDF to the temporary location
                pdfDoc.Save(tempPath);
            }

            cleanedPdfPaths.Add(tempPath);
        }

        if (cleanedPdfPaths.Count == 0)
        {
            Console.Error.WriteLine("No cleaned PDFs were generated. Exiting.");
            return;
        }

        // Concatenate all cleaned PDFs into the final document using PdfFileEditor
        PdfFileEditor concatEditor = new PdfFileEditor();
        concatEditor.Concatenate(cleanedPdfPaths.ToArray(), finalOutputPath);

        Console.WriteLine($"Merged PDF created at: {finalOutputPath}");

        // Optional: clean up temporary files
        foreach (string tempFile in cleanedPdfPaths)
        {
            try
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to delete temporary file '{tempFile}': {ex.Message}");
            }
        }
    }
}
