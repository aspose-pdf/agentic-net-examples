using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    // Holds the rendered page images
    private static List<System.Drawing.Image> _pageImages = new List<System.Drawing.Image>();
    // Current page index during printing
    private static int _currentPage = 0;

    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Render each page to a JPEG image and store in memory
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // JpegDevice renders a page to an image stream
                    JpegDevice jpegDevice = new JpegDevice(new Resolution(150));
                    jpegDevice.Process(pdfDoc.Pages[i], ms);
                    ms.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    _pageImages.Add(new Bitmap(img));
                }
            }
        }

        // Set up the PrintDocument
        PrintDocument printDoc = new PrintDocument();

        // Use the standard print controller to suppress the print dialog
        printDoc.PrintController = new StandardPrintController();

        // Optional: set printer name to default printer (null uses system default)
        // printDoc.PrinterSettings.PrinterName = "YourPrinterName";

        // Disable page scaling (print at actual size)
        printDoc.DefaultPageSettings.PrinterSettings.PrintRange = PrintRange.AllPages;
        printDoc.DefaultPageSettings.Landscape = false;

        // Attach the PrintPage event handler
        printDoc.PrintPage += PrintPageHandler;

        try
        {
            // Print silently
            printDoc.Print();
            Console.WriteLine("Printing completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Printing failed: {ex.Message}");
        }
        finally
        {
            // Clean up images
            foreach (var img in _pageImages)
                img.Dispose();
        }
    }

    // Event handler that prints each page image
    private static void PrintPageHandler(object sender, PrintPageEventArgs e)
    {
        if (_currentPage < _pageImages.Count)
        {
            System.Drawing.Image pageImage = _pageImages[_currentPage];

            // Fit the image to the printable area while preserving aspect ratio
            RectangleF printableArea = e.PageSettings.PrintableArea;
            float scale = Math.Min(printableArea.Width / pageImage.Width, printableArea.Height / pageImage.Height);
            float imgWidth = pageImage.Width * scale;
            float imgHeight = pageImage.Height * scale;
            float posX = printableArea.X + (printableArea.Width - imgWidth) / 2;
            float posY = printableArea.Y + (printableArea.Height - imgHeight) / 2;

            e.Graphics.DrawImage(pageImage, posX, posY, imgWidth, imgHeight);

            _currentPage++;
            e.HasMorePages = _currentPage < _pageImages.Count;
        }
        else
        {
            e.HasMorePages = false;
        }
    }
}
