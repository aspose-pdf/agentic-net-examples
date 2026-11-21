using System;
using System.IO;
using Aspose.Pdf;

class LargePdfSaveInterrupt
{
    static void Main()
    {
        const string inputPath = "large_input.pdf";
        const string outputPath = "large_output.pdf";
        const int batchSize = 100; // number of pages to process before releasing resources

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF once
        using (Document srcDoc = new Document(inputPath))
        {
            int totalPages = srcDoc.Pages.Count;
            bool firstBatch = true;
            Document batchDoc = null;

            try
            {
                // Start with an empty document for the first batch
                batchDoc = new Document();

                // Iterate using 1‑based page indexing (Aspose.Pdf requirement)
                for (int pageIndex = 1; pageIndex <= totalPages; pageIndex++)
                {
                    // Add the current page from source to the batch document
                    batchDoc.Pages.Add(srcDoc.Pages[pageIndex]);

                    // When the batch size is reached or it's the last page, save incrementally
                    if (pageIndex % batchSize == 0 || pageIndex == totalPages)
                    {
                        if (firstBatch)
                        {
                            // First save creates the file
                            batchDoc.Save(outputPath);
                        }
                        else
                        {
                            // Subsequent saves perform an incremental update
                            using (var fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
                            {
                                batchDoc.Save(fs); // parameterless Save on a stream triggers incremental update
                            }
                        }

                        // Release resources held by the batch document
                        batchDoc.Dispose();

                        // If there are more pages to process, reload the partially saved file
                        if (pageIndex != totalPages)
                        {
                            batchDoc = new Document(outputPath);
                        }

                        firstBatch = false;
                    }
                }
            }
            finally
            {
                // Ensure the last batch document is disposed even if an exception occurs
                batchDoc?.Dispose();
            }
        }

        Console.WriteLine($"Large PDF saved incrementally to '{outputPath}'.");
    }
}
