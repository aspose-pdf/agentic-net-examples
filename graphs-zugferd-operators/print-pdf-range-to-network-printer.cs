using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PrintPdfRange
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Set the document‑level duplex option (used by the print dialog).
            doc.Duplex = PrintDuplex.DuplexFlipLongEdge; // long‑edge binding for double‑sided printing

            // Configure printer settings for a network printer.
            var printerSettings = new PrinterSettings
            {
                // Specify the network printer (use either PrinterName or PrinterUri).
                PrinterName = @"\\PrintServer\\NetworkPrinter",
                // Set duplex mode for the printer (vertical = long‑edge binding).
                Duplex = Duplex.Vertical,
                // Define the page range to print (pages 2 through 5, inclusive).
                PrintRange = PrintRange.SomePages,
                FromPage = 2,
                ToPage = 5
            };

            // Set the desired paper size (A4 in hundredths of an inch).
            printerSettings.DefaultPageSettings.PaperSize =
                new PaperSize("A4", 827, 1169); // 8.27 in × 11.69 in

            // Use System.Drawing.Printing.PrintDocument to send the pages to the printer.
            using (PrintDocument printDoc = new PrintDocument())
            {
                printDoc.PrinterSettings = printerSettings;
                // Ensure the PrintDocument uses the same paper size.
                printDoc.DefaultPageSettings.PaperSize = printerSettings.DefaultPageSettings.PaperSize;

                int currentPage = printerSettings.FromPage;
                int lastPage = printerSettings.ToPage;

                printDoc.PrintPage += (sender, e) =>
                {
                    if (currentPage > lastPage)
                    {
                        e.HasMorePages = false;
                        return;
                    }

                    // Render the current PDF page to a PNG image in memory.
                    using (var ms = new MemoryStream())
                    {
                        var pngDevice = new PngDevice();
                        pngDevice.Process(doc.Pages[currentPage], ms);
                        ms.Position = 0;
                        // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image.
                        using (var img = System.Drawing.Image.FromStream(ms))
                        {
                            // Scale the image to fit the printable area while preserving aspect ratio.
                            RectangleF printableArea = e.PageBounds;
                            float scale = Math.Min(printableArea.Width / img.Width, printableArea.Height / img.Height);
                            float imgWidth = img.Width * scale;
                            float imgHeight = img.Height * scale;
                            float posX = printableArea.X + (printableArea.Width - imgWidth) / 2f;
                            float posY = printableArea.Y + (printableArea.Height - imgHeight) / 2f;
                            e.Graphics.DrawImage(img, posX, posY, imgWidth, imgHeight);
                        }
                    }

                    currentPage++;
                    e.HasMorePages = currentPage <= lastPage;
                };

                // Submit the print job.
                printDoc.Print();
            }
        }

        Console.WriteLine("Print job submitted.");
    }
}
