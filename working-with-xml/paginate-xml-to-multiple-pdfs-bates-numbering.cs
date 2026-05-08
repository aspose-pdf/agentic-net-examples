using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file containing the data to be converted.
        const string xmlInputPath = "large_dataset.xml";

        // Directory where the paginated PDF files will be saved.
        const string outputDir = "PaginatedPdfs";

        // Maximum number of pages per PDF file.
        const int pagesPerFile = 100;

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlInputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the XML and convert it to a PDF document.
        using (Document sourceDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            int totalPages = sourceDoc.Pages.Count;
            int fileIndex = 1;
            int nextStartNumber = 1; // For sequential Bates numbering across files.

            // Iterate over the source pages in chunks.
            for (int page = 1; page <= totalPages; page += pagesPerFile)
            {
                // Determine the range for the current chunk.
                int endPage = Math.Min(page + pagesPerFile - 1, totalPages);
                int pageCountInChunk = endPage - page + 1;

                // Create a new PDF document for this chunk.
                using (Document chunkDoc = new Document())
                {
                    // Add the selected pages from the source document.
                    for (int i = page; i <= endPage; i++)
                    {
                        // Pages collection is 1‑based.
                        chunkDoc.Pages.Add(sourceDoc.Pages[i]);
                    }

                    // Add sequential Bates numbering to each page in the chunk.
                    // The numbering will continue from the previous file.
                    chunkDoc.Pages.AddBatesNumbering(bates =>
                    {
                        bates.Prefix = "Doc";
                        bates.StartNumber = nextStartNumber;
                        bates.NumberOfDigits = 5; // e.g., 00001, 00002, ...
                    });

                    // Update the start number for the next chunk.
                    nextStartNumber += pageCountInChunk;

                    // Save the chunk as a PDF file.
                    string chunkPath = Path.Combine(outputDir, $"Part_{fileIndex:D3}.pdf");
                    chunkDoc.Save(chunkPath);
                    Console.WriteLine($"Saved {chunkPath} ({pageCountInChunk} pages).");
                }

                fileIndex++;
            }
        }

        Console.WriteLine("Pagination and splitting completed.");
    }
}