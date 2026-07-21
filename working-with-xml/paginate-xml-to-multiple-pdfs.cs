using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML file containing the large dataset
        const string xmlPath = "largeData.xml";

        // Directory where the paginated PDF files will be saved
        const string outputDir = "PaginatedPdfs";

        // Number of pages per output PDF file
        const int pagesPerFile = 20;

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the XML and convert it to a PDF document
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(xmlPath, new Aspose.Pdf.XmlLoadOptions()))
        {
            int totalPages = sourceDoc.Pages.Count;
            int currentPage = 1;      // 1‑based index of the page in the source document
            int fileIndex   = 1;      // Sequential PDF file number

            while (currentPage <= totalPages)
            {
                // Create a new empty PDF document for the current chunk
                using (Aspose.Pdf.Document chunkDoc = new Aspose.Pdf.Document())
                {
                    // Determine the last page to include in this chunk
                    int lastPage = Math.Min(currentPage + pagesPerFile - 1, totalPages);

                    // Copy pages from the source document to the chunk document
                    for (int i = currentPage; i <= lastPage; i++)
                    {
                        // Add a copy of the source page to the new document
                        chunkDoc.Pages.Add(sourceDoc.Pages[i]);

                        // Add a page number stamp with the global sequential number
                        PageNumberStamp pageNumberStamp = new PageNumberStamp
                        {
                            // The number that will appear on this page
                            StartingNumber = currentPage,

                            // Use Arabic numerals (1,2,3,…)
                            NumberingStyle = NumberingStyle.NumeralsArabic,

                            // Position the number at the bottom centre of the page
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment   = VerticalAlignment.Bottom
                        };

                        // Configure visual styling via the read‑only TextState object
                        pageNumberStamp.TextState.FontSize = 12;
                        pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                        pageNumberStamp.TextState.ForegroundColor = Color.Black;

                        // The page we just added is the last page in chunkDoc
                        Page addedPage = chunkDoc.Pages[chunkDoc.Pages.Count];
                        addedPage.AddStamp(pageNumberStamp);

                        // Increment the global page counter for the next page
                        currentPage++;
                    }

                    // Save the chunk PDF with a sequential file name
                    string outputPath = Path.Combine(outputDir, $"Part_{fileIndex}.pdf");
                    chunkDoc.Save(outputPath);
                    Console.WriteLine($"Saved: {outputPath}");

                    fileIndex++;
                }
            }
        }

        Console.WriteLine("Pagination complete.");
    }
}
