using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF document.
    /// </summary>
    /// <param name="sourcePdf">Path to the source PDF.</param>
    /// <param name="destinationPdf">Path where the modified PDF will be saved.</param>
    /// <param name="imagePath">Path to the image file to insert.</param>
    /// <param name="pageNumber">1‑based page number where the image will be placed.</param>
    /// <param name="lowerLeftX">X coordinate of the lower‑left corner of the image rectangle.</param>
    /// <param name="lowerLeftY">Y coordinate of the lower‑left corner of the image rectangle.</param>
    /// <param name="upperRightX">X coordinate of the upper‑right corner of the image rectangle.</param>
    /// <param name="upperRightY">Y coordinate of the upper‑right corner of the image rectangle.</param>
    public static void AddImage(string sourcePdf, string destinationPdf, string imagePath,
        int pageNumber, float lowerLeftX, float lowerLeftY, float upperRightX, float upperRightY)
    {
        if (!File.Exists(sourcePdf))
            throw new FileNotFoundException($"Source PDF not found: {sourcePdf}");

        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found: {imagePath}");

        // PdfFileMend implements IDisposable, so we use a using block to ensure resources are released.
        using (PdfFileMend mend = new PdfFileMend(sourcePdf, destinationPdf))
        {
            bool success = mend.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!success)
                throw new InvalidOperationException("Failed to add image to the PDF.");
        }
    }
}

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "output.pdf";
        string imagePath = "image.jpg";
        int pageNumber = 1;
        float lowerLeftX = 50f;
        float lowerLeftY = 50f;
        float upperRightX = 200f;
        float upperRightY = 200f;

        try
        {
            PdfHelper.AddImage(inputPdf, outputPdf, imagePath, pageNumber,
                lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            Console.WriteLine("Image added successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }
}