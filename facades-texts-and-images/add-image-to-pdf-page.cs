using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF document at the given rectangle coordinates.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="outputPdfPath">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to be added.</param>
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
        // Validate input arguments
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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // PdfFileMend works on the loaded Document instance.
            using (PdfFileMend mend = new PdfFileMend(doc))
            {
                // Add the image to the specified page and rectangle.
                // The AddImage overload that accepts a file path is used.
                mend.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Save the modified document to the desired output location.
            doc.Save(outputPdfPath);
        }
    }
}

// A minimal entry point is required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the class library can be used from other code.
        // This stub satisfies the compiler's requirement for a static Main method.
    }
}
