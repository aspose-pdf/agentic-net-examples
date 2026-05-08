using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF file at the given rectangle coordinates.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to be added.</param>
    /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
    /// <param name="lowerLeftX">Lower‑left X coordinate of the image rectangle.</param>
    /// <param name="lowerLeftY">Lower‑left Y coordinate of the image rectangle.</param>
    /// <param name="upperRightX">Upper‑right X coordinate of the image rectangle.</param>
    /// <param name="upperRightY">Upper‑right Y coordinate of the image rectangle.</param>
    public static void AddImageToPage(
        string inputPdfPath,
        string outputPdfPath,
        string imagePath,
        int pageNumber,
        float lowerLeftX,
        float lowerLeftY,
        float upperRightX,
        float upperRightY)
    {
        // Validate arguments (optional but helpful)
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException("Input PDF not found.", inputPdfPath);
        if (!File.Exists(imagePath))
            throw new FileNotFoundException("Image file not found.", imagePath);
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

        // Use the parameter‑less constructor and bind the source PDF.
        using (PdfFileMend mend = new PdfFileMend())
        {
            mend.BindPdf(inputPdfPath);
            mend.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            mend.Save(outputPdfPath);
        }
    }
}

// Minimal entry point so the project builds as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The helper can be invoked from other code or unit tests.
    }
}