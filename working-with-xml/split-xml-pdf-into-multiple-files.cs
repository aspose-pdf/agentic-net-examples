using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input XML file and optional XSL (null means direct conversion)
        const string xmlPath = "large_dataset.xml";
        // Directory where split PDFs will be written
        const string outputDir = "SplitPdfs";

        // Maximum number of pages per split PDF (adjust as needed)
        const int maxPagesPerFile = 100;

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the XML into a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document sourceDoc = new Document(xmlPath, loadOptions))
        {
            int totalPages = sourceDoc.Pages.Count;
            int currentPage = 1;      // 1‑based index of the next page to copy
            int fileIndex = 1;        // Sequential output file number

            while (currentPage <= totalPages)
            {
                // Create a new document that will hold a chunk of pages
                using (Document partDoc = new Document())
                {
                    // Determine the last page for this chunk
                    int lastPage = Math.Min(currentPage + maxPagesPerFile - 1, totalPages);

                    // Copy pages from the source document into the part document
                    for (int i = currentPage; i <= lastPage; i++)
                    {
                        partDoc.Pages.Add(sourceDoc.Pages[i]); // Pages are 1‑based
                    }

                    // Add page numbers that continue across all parts
                    PageNumberStamp pageNumberStamp = new PageNumberStamp
                    {
                        // The first page of this part should be numbered as the global page number
                        StartingNumber = currentPage,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom
                    };
                    // Optional styling of the page number text
                    pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                    pageNumberStamp.TextState.FontSize = 12;
                    pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Apply the stamp to every page in the part document
                    foreach (Page page in partDoc.Pages)
                    {
                        page.AddStamp(pageNumberStamp);
                    }

                    // Save the part document
                    string outPath = Path.Combine(outputDir, $"Part_{fileIndex}.pdf");
                    partDoc.Save(outPath);
                    Console.WriteLine($"Saved {outPath} (pages {currentPage}-{lastPage})");

                    // Prepare for the next chunk
                    fileIndex++;
                    currentPage = lastPage + 1;
                }
            }
        }

        Console.WriteLine("XML pagination and splitting completed.");
    }
}