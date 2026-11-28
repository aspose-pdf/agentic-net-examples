using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string pdfPath = "input.pdf";          // source PDF
        const string bmpImagePath = "replacement.bmp"; // BMP image to insert
        const string outputPath = "output.pdf";
        const string tempPath = "temp_deleted.pdf"; // intermediate file after deletion

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(bmpImagePath))
        {
            Console.Error.WriteLine($"BMP image not found: {bmpImagePath}");
            return;
        }

        // ---------------------------------------------------------------------
        // Step 1: Delete the existing PNG image on page 39.
        // ---------------------------------------------------------------------
        const int pageNumber = 39;   // 1‑based page index
        const int imageIndex = 1;    // 1‑based image index on the page (assumed first image)

        var editor = new PdfContentEditor();
        editor.BindPdf(pdfPath);
        // DeleteImage expects the page number and an array of image indexes to remove.
        editor.DeleteImage(pageNumber, new[] { imageIndex });
        // Persist the change to a temporary file.
        editor.Save(tempPath);
        editor.Close();

        // ---------------------------------------------------------------------
        // Step 2: Add the BMP image at the required coordinates.
        // ---------------------------------------------------------------------
        // Desired placement rectangle (lower‑left X/Y and upper‑right X/Y) in points.
        // Use float literals as required by PdfFileMend.
        const float llx = 100f; // lower‑left X
        const float lly = 200f; // lower‑left Y
        const float urx = 300f; // upper‑right X
        const float ury = 400f; // upper‑right Y

        var mend = new PdfFileMend();
        mend.BindPdf(tempPath);
        // AddImage(page, llx, lly, urx, ury) places the image exactly in the rectangle.
        mend.AddImage(bmpImagePath, pageNumber, llx, lly, urx, ury);
        mend.Save(outputPath);
        mend.Close();

        // Clean up the intermediate file (optional).
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Image replaced and PDF saved to '{outputPath}'.");
    }
}
