using System;
using Aspose.Pdf.Facades;

public static class PdfHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF document.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to insert.</param>
    /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
    /// <param name="lowerLeftX">X coordinate of the lower‑left corner of the image rectangle.</param>
    /// <param name="lowerLeftY">Y coordinate of the lower‑left corner of the image rectangle.</param>
    /// <param name="upperRightX">X coordinate of the upper‑right corner of the image rectangle.</param>
    /// <param name="upperRightY">Y coordinate of the upper‑right corner of the image rectangle.</param>
    public static void AddImageToPdf(
        string inputPdfPath,
        string outputPdfPath,
        string imagePath,
        int pageNumber,
        float lowerLeftX,
        float lowerLeftY,
        float upperRightX,
        float upperRightY)
    {
        // Validate arguments (optional, can be omitted for brevity)
        if (string.IsNullOrEmpty(inputPdfPath))
            throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
        if (string.IsNullOrEmpty(outputPdfPath))
            throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));
        if (string.IsNullOrEmpty(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

        // Use PdfFileMend facade to modify the existing PDF.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF file.
            mend.BindPdf(inputPdfPath);

            // Add the image to the specified page and rectangle.
            // This uses the overload that accepts a file path for the image.
            mend.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified document to the desired output location.
            mend.Save(outputPdfPath);
        }
    }
}

// Minimal entry point to satisfy the compiler when building an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The helper can be invoked from here if desired.
        // Example (commented out to avoid runtime errors when paths are not provided):
        // PdfHelper.AddImageToPdf("input.pdf", "output.pdf", "image.png", 1, 100, 100, 300, 300);
    }
}