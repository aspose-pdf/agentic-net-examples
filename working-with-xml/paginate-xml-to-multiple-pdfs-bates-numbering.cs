using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string xmlInputPath = "large_dataset.xml";
        const string outputFolder = "PaginatedPdfs";
        const int pagesPerPdf = 100; // adjust as needed
        const string batesPrefix = "DOC";
        const string batesSeparator = "-";
        const int batesDigits = 5;

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input XML not found: {xmlInputPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the XML and convert it to a PDF document
        using (Document sourceDoc = new Document(xmlInputPath, new XmlLoadOptions()))
        {
            int totalPages = sourceDoc.Pages.Count;
            int fileIndex = 1;
            int batesStartNumber = 1; // sequential number across all PDFs

            for (int startPage = 1; startPage <= totalPages; startPage += pagesPerPdf)
            {
                int endPage = Math.Min(startPage + pagesPerPdf - 1, totalPages);
                int pagesInChunk = endPage - startPage + 1;

                // Create a new PDF for the current chunk
                using (Document chunkDoc = new Document())
                {
                    // Copy the required pages from the source document
                    for (int p = startPage; p <= endPage; p++)
                    {
                        // AddPage clones the page into the target document
                        chunkDoc.Pages.Add(sourceDoc.Pages[p]);
                    }

                    // ---- Manual Bates numbering (core API only) ----
                    int currentBates = batesStartNumber;
                    foreach (Page page in chunkDoc.Pages)
                    {
                        string batesText = $"{batesPrefix}{batesSeparator}{currentBates.ToString().PadLeft(batesDigits, '0')}";
                        TextFragment tf = new TextFragment(batesText);
                        tf.Position = new Position(0, 0); // bottom‑left corner; adjust as needed
                        tf.TextState.FontSize = 10;
                        tf.TextState.Font = FontRepository.FindFont("Arial");
                        tf.TextState.ForegroundColor = Color.Black;
                        page.Paragraphs.Add(tf);
                        currentBates++;
                    }
                    // ------------------------------------------------

                    // Save the chunk PDF
                    string outputPath = System.IO.Path.Combine(outputFolder, $"Part_{fileIndex}.pdf");
                    chunkDoc.Save(outputPath);
                    Console.WriteLine($"Saved: {outputPath}");

                    // Update counters for next chunk
                    batesStartNumber += pagesInChunk;
                    fileIndex++;
                }
            }
        }

        Console.WriteLine("Pagination complete.");
    }
}
