using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for XmlLoadOptions (inherits LoadOptions)

class Program
{
    static void Main()
    {
        // Path to the large XML file to be converted.
        const string xmlPath = "large_dataset.xml";

        // Directory where the split PDF files will be saved.
        const string outputDir = "PaginatedPdfs";

        // Maximum number of pages per PDF file.
        const int pagesPerFile = 100;

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the XML and convert it to a PDF document.
        // XmlLoadOptions is the correct way to load XML into Aspose.Pdf.
        using (Document sourceDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            int totalPages = sourceDoc.Pages.Count; // 1‑based indexing
            int partNumber = 1;

            // Iterate over the source pages in chunks of 'pagesPerFile'.
            for (int start = 1; start <= totalPages; start += pagesPerFile, partNumber++)
            {
                // Create a new empty PDF document for the current chunk.
                using (Document chunkDoc = new Document())
                {
                    // Determine the last page index for this chunk.
                    int end = Math.Min(start + pagesPerFile - 1, totalPages);

                    // Copy pages from the source document to the chunk document.
                    for (int pageIndex = start; pageIndex <= end; pageIndex++)
                    {
                        // Add copies of the pages; this does not modify the source document.
                        chunkDoc.Pages.Add(sourceDoc.Pages[pageIndex]);
                    }

                    // Build the output file name with sequential numbering.
                    string outputPath = Path.Combine(outputDir, $"Part_{partNumber:D3}.pdf");

                    // Save the chunk as a PDF file.
                    chunkDoc.Save(outputPath);

                    Console.WriteLine($"Saved {outputPath} (pages {start}-{end})");
                }
            }
        }

        Console.WriteLine("Pagination complete.");
    }
}