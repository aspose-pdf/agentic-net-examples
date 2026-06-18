using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfImageHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF document at the given rectangle coordinates.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
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
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path is required.", nameof(inputPdfPath));
        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path is required.", nameof(outputPdfPath));
        if (string.IsNullOrWhiteSpace(imagePath))
            throw new ArgumentException("Image path is required.", nameof(imagePath));
        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be 1 or greater.");

        // Use the non‑obsolete constructor and bind the source PDF explicitly.
        PdfFileMend mender = new PdfFileMend();
        mender.BindPdf(inputPdfPath);

        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // AddImage adds the image to the given page. Coordinates are in points (1/72 inch).
            bool success = mender.AddImage(
                imgStream,
                pageNumber,
                lowerLeftX,
                lowerLeftY,
                upperRightX,
                upperRightY);

            if (!success)
                throw new InvalidOperationException("Failed to add the image to the PDF.");
        }

        // Save the modified PDF to the destination path and close the facade.
        mender.Save(outputPdfPath);
        mender.Close();
    }
}

// Adding a minimal entry point so the project compiles as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The helper can be called from here or from any other consumer.
        // Example (commented out to avoid runtime errors when paths are missing):
        // PdfImageHelper.AddImageToPage(
        //     inputPdfPath: "source.pdf",
        //     outputPdfPath: "result.pdf",
        //     imagePath: "logo.png",
        //     pageNumber: 2,
        //     lowerLeftX: 50f,
        //     lowerLeftY: 100f,
        //     upperRightX: 200f,
        //     upperRightY: 300f);
    }
}