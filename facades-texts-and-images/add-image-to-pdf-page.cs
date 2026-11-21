using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public static class PdfHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF at the given coordinates.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to insert.</param>
    /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
    /// <param name="llx">Lower‑left X coordinate of the image rectangle.</param>
    /// <param name="lly">Lower‑left Y coordinate of the image rectangle.</param>
    /// <param name="urx">Upper‑right X coordinate of the image rectangle.</param>
    /// <param name="ury">Upper‑right Y coordinate of the image rectangle.</param>
    public static void AddImageToPage(
        string inputPdfPath,
        string outputPdfPath,
        string imagePath,
        int pageNumber,
        double llx,
        double lly,
        double urx,
        double ury)
    {
        // Validate input files
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"PDF not found: {inputPdfPath}");
        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image not found: {imagePath}");

        // Load the PDF document
        using (Document pdf = new Document(inputPdfPath))
        {
            // Validate page number against the loaded document
            if (pageNumber < 1 || pageNumber > pdf.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number is out of range.");

            // Create the image stamp with absolute positioning
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                Background = false, // place on top of existing content
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                XIndent = (float)llx,
                YIndent = (float)lly,
                Width = (float)(urx - llx),
                Height = (float)(ury - lly)
            };

            // Add the stamp to the requested page only
            Page targetPage = pdf.Pages[pageNumber];
            targetPage.AddStamp(imgStamp);

            // Save the modified PDF
            pdf.Save(outputPdfPath);
        }
    }
}

// Minimal entry point required for a console application.
class Program
{
    static void Main()
    {
        // Example usage – adjust paths as needed.
        string sourcePdf = "source.pdf";
        string resultPdf = "result.pdf";
        string logoImg = "logo.png";

        // Guard against missing demo files when the example is run in isolation.
        if (File.Exists(sourcePdf) && File.Exists(logoImg))
        {
            PdfHelper.AddImageToPage(sourcePdf, resultPdf, logoImg, 2, 100, 500, 300, 700);
            Console.WriteLine($"Image added to page 2 and saved as '{resultPdf}'.");
        }
        else
        {
            Console.WriteLine("Demo files not found – skip execution.");
        }
    }
}
