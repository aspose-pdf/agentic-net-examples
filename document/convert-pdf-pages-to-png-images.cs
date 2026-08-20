using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "PageImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Convert the current page to a PNG image stored in a memory stream
                    using (MemoryStream pngStream = pdfDoc.ConvertPageToPNGMemoryStream(page))
                    {
                        // Build the output file name (e.g., Page_1.png)
                        string outputPath = Path.Combine(outputFolder, $"Page_{pageNum}.png");

                        // Write the PNG bytes to disk
                        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            pngStream.Position = 0;
                            pngStream.CopyTo(fileStream);
                        }

                        Console.WriteLine($"Saved page {pageNum} as image: {outputPath}");
                    }
                }
            }

            Console.WriteLine("All pages have been converted to images successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}