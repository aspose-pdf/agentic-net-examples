using System;
using Aspose.Pdf.Facades;

public static class PdfImageRemover
{
    /// <summary>
    /// Removes all images from the specified PDF file and saves the result to a new file.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the image‑free PDF will be saved.</param>
    public static void RemoveAllImages(string inputPdfPath, string outputPdfPath)
    {
        // Validate input arguments.
        if (string.IsNullOrWhiteSpace(inputPdfPath))
            throw new ArgumentException("Input PDF path must be provided.", nameof(inputPdfPath));

        if (string.IsNullOrWhiteSpace(outputPdfPath))
            throw new ArgumentException("Output PDF path must be provided.", nameof(outputPdfPath));

        // Load the PDF, delete all images, and save the result.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);   // Load the PDF.
        editor.DeleteImage();           // Delete all images from every page.
        editor.Save(outputPdfPath);     // Persist the modified document.
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console‑style build.
    /// Accepts two arguments: input PDF path and output PDF path.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PdfImageRemover <inputPdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            PdfImageRemover.RemoveAllImages(args[0], args[1]);
            Console.WriteLine("All images removed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
