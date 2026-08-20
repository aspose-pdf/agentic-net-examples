using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfImageHelper
{
    /// <summary>
    /// Adds an image to a specific page of a PDF file at the given rectangle coordinates.
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
        // Validate arguments (optional but helpful)
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found: {imagePath}");

        // PdfFileMend is a facade for adding images/text to existing PDFs.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Bind the source PDF file.
            mender.BindPdf(inputPdfPath);

            // Add the image to the specified page and rectangle.
            // This uses the AddImage(string, int, float, float, float, float) overload.
            bool success = mender.AddImage(imagePath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            if (!success)
                throw new InvalidOperationException("Failed to add image to the PDF.");

            // Save the modified PDF to the output path.
            mender.Save(outputPdfPath);

            // Close the facade (optional; using will also call Dispose).
            mender.Close();
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console‑application build. Demonstrates the helper usage.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expected arguments:
        //   0: input PDF path
        //   1: output PDF path
        //   2: image file path
        //   3: page number (1‑based)
        //   4: lower‑left X
        //   5: lower‑left Y
        //   6: upper‑right X
        //   7: upper‑right Y
        if (args.Length < 8)
        {
            Console.WriteLine("Usage: <inputPdf> <outputPdf> <imagePath> <pageNumber> <llx> <lly> <urx> <ury>");
            return;
        }

        string inputPdf = args[0];
        string outputPdf = args[1];
        string imagePath = args[2];
        if (!int.TryParse(args[3], out int pageNumber))
        {
            Console.WriteLine("Invalid page number.");
            return;
        }
        if (!float.TryParse(args[4], out float llx) ||
            !float.TryParse(args[5], out float lly) ||
            !float.TryParse(args[6], out float urx) ||
            !float.TryParse(args[7], out float ury))
        {
            Console.WriteLine("Invalid coordinate values.");
            return;
        }

        try
        {
            PdfImageHelper.AddImageToPdf(inputPdf, outputPdf, imagePath, pageNumber, llx, lly, urx, ury);
            Console.WriteLine("Image added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
