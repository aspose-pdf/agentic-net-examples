using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string backgroundPng = "background.png";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundPng))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPng}");
            return;
        }

        // Create the PdfFileMend facade
        PdfFileMend mender = new PdfFileMend();

        // Load the source PDF
        mender.BindPdf(inputPdfPath);

        // Open the background image once and reuse the stream for each page
        using (FileStream imgStream = File.OpenRead(backgroundPng))
        {
            // Get total page count (Aspose.Pdf uses 1‑based indexing)
            int pageCount = mender.Document.Pages.Count;

            // Iterate over all pages and add the image as background
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Reset stream position for each AddImage call
                imgStream.Position = 0;

                // Retrieve page dimensions
                Page page = mender.Document.Pages[pageNum];
                float pageWidth  = (float)page.Rect.Width;
                float pageHeight = (float)page.Rect.Height;

                // Add the image covering the whole page
                // lowerLeftX = 0, lowerLeftY = 0, upperRightX = pageWidth, upperRightY = pageHeight
                mender.AddImage(imgStream, pageNum, 0, 0, pageWidth, pageHeight);
            }
        }

        // Save the modified PDF
        mender.Save(outputPdfPath);

        // Release resources
        mender.Close();
    }
}