using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string printerName = @"\\PrintServer\MyPrinter"; // network printer UNC path
        const int startPage = 2; // first page to print (1‑based)
        const int endPage = 5;   // last page to print (inclusive)

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Validate page range
                int pageCount = doc.Pages.Count;
                int first = Math.Max(1, startPage);
                int last = Math.Min(endPage, pageCount);
                if (first > last)
                {
                    Console.Error.WriteLine("Invalid page range.");
                    return;
                }

                // Convert required pages to images (PNG) and keep them in memory
                List<System.Drawing.Image> pageImages = new List<System.Drawing.Image>();
                var resolution = new Resolution(300); // 300 DPI for good quality
                var pngDevice = new PngDevice(resolution);

                for (int i = first; i <= last; i++)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pngDevice.Process(doc.Pages[i], ms);
                        ms.Position = 0;
                        pageImages.Add(System.Drawing.Image.FromStream(ms));
                    }
                }

                // Prepare the PrintDocument
                using (PrintDocument printDoc = new PrintDocument())
                {
                    // Set printer name (network printer)
                    printDoc.PrinterSettings.PrinterName = printerName;

                    // Duplex mode – long‑edge binding (vertical duplex)
                    printDoc.PrinterSettings.Duplex = Duplex.Vertical;

                    // Paper size – A4 (210 mm × 297 mm). PrintDocument expects hundredths of an inch.
                    // 1 inch = 25.4 mm → 210 mm = 8.27 in → 827 hundredths, 297 mm = 11.69 in → 1169 hundredths.
                    printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

                    // Keep a copy of the images for the PrintPage handler
                    int currentPage = 0;
                    printDoc.PrintPage += (sender, e) =>
                    {
                        System.Drawing.Image img = pageImages[currentPage];
                        // Fit the image to the printable area while preserving aspect ratio
                        RectangleF printableArea = e.PageSettings.PrintableArea;
                        float scale = Math.Min(printableArea.Width / img.Width, printableArea.Height / img.Height);
                        float imgWidth = img.Width * scale;
                        float imgHeight = img.Height * scale;
                        float posX = printableArea.X + (printableArea.Width - imgWidth) / 2;
                        float posY = printableArea.Y + (printableArea.Height - imgHeight) / 2;
                        e.Graphics.DrawImage(img, posX, posY, imgWidth, imgHeight);

                        currentPage++;
                        e.HasMorePages = currentPage < pageImages.Count;
                    };

                    // Send the job to the printer
                    printDoc.Print();
                }

                // Dispose the images after printing
                foreach (var img in pageImages)
                {
                    img.Dispose();
                }
            }

            Console.WriteLine("Print job submitted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
