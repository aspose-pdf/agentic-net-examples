using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePath = "large_input.pdf";
        const string outputPath = "large_output.pdf";
        const int batchSize = 100; // number of pages to process before releasing resources

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Remove any previous output file
        if (File.Exists(outputPath))
            File.Delete(outputPath);

        // Load the source PDF
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(sourcePath))
        {
            int totalPages = sourceDoc.Pages.Count;
            int processedPages = 0;

            Aspose.Pdf.Document targetDoc = null;

            try
            {
                while (processedPages < totalPages)
                {
                    // Create a new target document for the current batch
                    targetDoc = new Aspose.Pdf.Document();

                    // Add a batch of pages from the source to the target
                    for (int i = 0; i < batchSize && processedPages < totalPages; i++)
                    {
                        // Aspose.Pdf uses 1‑based page indexing
                        Aspose.Pdf.Page srcPage = sourceDoc.Pages[processedPages + 1];
                        targetDoc.Pages.Add(srcPage);
                        processedPages++;
                    }

                    // Save the batch
                    if (processedPages == batchSize)
                    {
                        // First batch creates the file
                        targetDoc.Save(outputPath);
                    }
                    else
                    {
                        // Subsequent batches append to the existing file.
                        // Incremental saving is achieved by reopening the file and adding more pages.
                        targetDoc.Save(outputPath, new Aspose.Pdf.PdfSaveOptions());
                    }

                    // Dispose the current target document to free memory/resources
                    targetDoc.Dispose();
                    targetDoc = null;
                }
            }
            finally
            {
                // Ensure any remaining target document is disposed
                if (targetDoc != null)
                    targetDoc.Dispose();
            }
        }

        Console.WriteLine($"Completed saving. Output file: {outputPath}");
    }
}