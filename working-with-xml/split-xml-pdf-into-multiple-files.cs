using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the large XML dataset
        const string xmlPath = "large_dataset.xml";

        // Directory where the split PDFs will be saved
        const string outputDir = "SplitPdfs";

        // Maximum number of PDF pages per output file (adjust as needed)
        const int maxPagesPerPdf = 100;

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the XML as a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document sourceDoc = new Document(xmlPath, loadOptions))
        {
            // Total pages generated from the XML
            int totalPages = sourceDoc.Pages.Count;

            // Sequential part number for output files
            int partNumber = 1;

            // Iterate over the source pages in chunks of maxPagesPerPdf
            for (int startPage = 1; startPage <= totalPages; startPage += maxPagesPerPdf)
            {
                // Create a new PDF document for the current chunk
                using (Document partDoc = new Document())
                {
                    // Determine the last page index for this chunk (1‑based indexing)
                    int endPage = Math.Min(startPage + maxPagesPerPdf - 1, totalPages);

                    // Copy pages from the source document to the part document
                    for (int i = startPage; i <= endPage; i++)
                    {
                        // Pages collection is 1‑based; Add copies the page into the target document
                        partDoc.Pages.Add(sourceDoc.Pages[i]);
                    }

                    // Build the output file name with zero‑padded sequential numbering
                    string outputPath = Path.Combine(outputDir, $"Part_{partNumber:D3}.pdf");

                    // Save the part document (PDF format)
                    partDoc.Save(outputPath);

                    Console.WriteLine($"Saved {outputPath} (pages {startPage}-{endPage})");
                }

                partNumber++;
            }
        }

        Console.WriteLine("Pagination complete.");
    }
}