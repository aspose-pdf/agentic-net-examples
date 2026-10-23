using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                PrintPdfSilently(doc);
            }

            Console.WriteLine("Print job sent to the default printer.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void PrintPdfSilently(Document doc)
    {
        // Index of the page currently being printed (Aspose.Pdf pages are 1‑based)
        int currentPage = 1;

        PrintDocument printer = new PrintDocument();
        printer.PrintPage += (sender, e) =>
        {
            // Render the current PDF page to a PNG image in memory
            using (MemoryStream imgStream = new MemoryStream())
            {
                // 300 DPI gives good quality for printing; adjust if needed
                var pngDevice = new PngDevice(300, 300);
                pngDevice.Process(doc.Pages[currentPage], imgStream);
                imgStream.Position = 0;

                // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
                using (System.Drawing.Image pageImage = System.Drawing.Image.FromStream(imgStream))
                {
                    // Fit the image into the printable area while preserving aspect ratio
                    RectangleF printableArea = e.MarginBounds;
                    float scale = Math.Min(printableArea.Width / pageImage.Width, printableArea.Height / pageImage.Height);
                    float imgWidth = pageImage.Width * scale;
                    float imgHeight = pageImage.Height * scale;
                    float posX = printableArea.X + (printableArea.Width - imgWidth) / 2;
                    float posY = printableArea.Y + (printableArea.Height - imgHeight) / 2;

                    e.Graphics.DrawImage(pageImage, posX, posY, imgWidth, imgHeight);
                }
            }

            // Move to the next page
            currentPage++;
            // Indicate whether there are more pages to print
            e.HasMorePages = currentPage <= doc.Pages.Count;
        };

        // Print silently to the default printer (no dialog shown)
        printer.Print();
    }
}
