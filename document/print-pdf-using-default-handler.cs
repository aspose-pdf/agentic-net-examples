using System;
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
        // Optional: specify a printer name; leave empty to use the default printer
        const string printerName = "";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // Create a PrintDocument that knows how to render Aspose.Pdf pages
                using (PdfPrintDocument printDoc = new PdfPrintDocument(doc))
                {
                    if (!string.IsNullOrWhiteSpace(printerName))
                    {
                        printDoc.PrinterSettings.PrinterName = printerName;
                    }

                    // Submit the print job
                    printDoc.Print();
                }
            }

            Console.WriteLine("Print job submitted successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error printing PDF: {ex.Message}");
        }
    }
}

/// <summary>
/// A PrintDocument implementation that renders each PDF page to a bitmap using Aspose.Pdf's
/// image‑conversion devices and prints the bitmap via the standard .NET printing infrastructure.
/// </summary>
public class PdfPrintDocument : PrintDocument
{
    private readonly Document _pdfDocument;
    private int _currentPageIndex = 1; // Aspose.Pdf pages are 1‑based

    public PdfPrintDocument(Document pdfDocument)
    {
        _pdfDocument = pdfDocument ?? throw new ArgumentNullException(nameof(pdfDocument));
        // Set default page settings based on the first page size
        var firstPage = _pdfDocument.Pages[1];
        this.DefaultPageSettings.PaperSize = new PaperSize(
            "Custom",
            (int)Math.Ceiling(firstPage.PageInfo.Width),
            (int)Math.Ceiling(firstPage.PageInfo.Height));
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        // Render the current PDF page to a bitmap (PNG format)
        using (var imageStream = new MemoryStream())
        {
            var pngDevice = new PngDevice();
            pngDevice.Process(_pdfDocument.Pages[_currentPageIndex], imageStream);
            imageStream.Position = 0;
            using (var pageImage = System.Drawing.Image.FromStream(imageStream))
            {
                // Fit the image to the printable area while preserving aspect ratio
                RectangleF printableArea = e.PageSettings.PrintableArea;
                float scale = Math.Min(printableArea.Width / pageImage.Width, printableArea.Height / pageImage.Height);
                float imgWidth = pageImage.Width * scale;
                float imgHeight = pageImage.Height * scale;
                float posX = printableArea.X + (printableArea.Width - imgWidth) / 2;
                float posY = printableArea.Y + (printableArea.Height - imgHeight) / 2;

                e.Graphics.DrawImage(pageImage, posX, posY, imgWidth, imgHeight);
            }
        }

        // Prepare for next page
        _currentPageIndex++;
        e.HasMorePages = _currentPageIndex <= _pdfDocument.Pages.Count;
        base.OnPrintPage(e);
    }
}