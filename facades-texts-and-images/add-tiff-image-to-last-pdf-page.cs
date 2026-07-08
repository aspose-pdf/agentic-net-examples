using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF manipulation

class Program
{
    static void Main()
    {
        // Input PDF and TIFF image paths
        const string inputPdfPath  = "input.pdf";
        const string tiffImagePath = "image.tif";
        const string outputPdfPath = "output.pdf";

        // Verify that the source files exist
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

            // Bind the existing PDF document
            mend.BindPdf(inputPdfPath);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            // Since PdfFileMend does not expose the document directly,
            // we obtain the page count via a temporary Document instance.
            int lastPageNumber;
            using (Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                lastPageNumber = tempDoc.Pages.Count;
            }

            // Define the rectangle where the image will be placed.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            float lowerLeftX  = 50f;   // X coordinate of lower‑left corner
            float lowerLeftY  = 50f;   // Y coordinate of lower‑left corner
            float upperRightX = 250f;  // X coordinate of upper‑right corner
            float upperRightY = 250f;  // Y coordinate of upper‑right corner

            // Add the TIFF image to the last page without affecting existing content.
            // The method returns true on success; we ignore the return value here.
            mend.AddImage(tiffImagePath, lastPageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified PDF to the output file.
            mend.Save(outputPdfPath);

            // Close the facade to release resources.
            mend.Close();

            Console.WriteLine($"TIFF image added to page {lastPageNumber}. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}