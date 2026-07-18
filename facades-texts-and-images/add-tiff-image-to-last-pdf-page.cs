using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string outputPdfPath = "output.pdf";  // PDF with added image
        const string tiffImagePath = "image.tiff"; // TIFF to embed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(tiffImagePath))
        {
            Console.Error.WriteLine($"TIFF image not found: {tiffImagePath}");
            return;
        }

        try
        {
            // Initialize the PdfFileMend facade
            PdfFileMend mend = new PdfFileMend();

            // Bind the source PDF file
            mend.BindPdf(inputPdfPath);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = mend.Document.Pages.Count;

            // Define image placement rectangle (example coordinates)
            // lower‑left corner (x=50, y=50), upper‑right corner (x=300, y=300)
            float lowerLeftX  = 50f;
            float lowerLeftY  = 50f;
            float upperRightX = 300f;
            float upperRightY = 300f;

            // Add the TIFF image to the last page without affecting existing content
            mend.AddImage(tiffImagePath, lastPageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified PDF
            mend.Save(outputPdfPath);

            // Release resources
            mend.Close();

            Console.WriteLine($"TIFF image added to page {lastPageNumber}. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}