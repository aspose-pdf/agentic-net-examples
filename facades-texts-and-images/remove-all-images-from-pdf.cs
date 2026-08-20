using System;
using System.IO;
using Aspose.Pdf.Facades;

class PdfImageRemover
{
    /// <summary>
    /// Removes all images from the specified PDF and saves the result to a new file.
    /// </summary>
    /// <param name="sourcePdf">Path to the input PDF file.</param>
    /// <param name="destinationPdf">Path where the output PDF without images will be saved.</param>
    public static void RemoveAllImages(string sourcePdf, string destinationPdf)
    {
        // Validate input file existence
        if (!File.Exists(sourcePdf))
            throw new FileNotFoundException($"Input PDF not found: {sourcePdf}");

        // PdfContentEditor implements IDisposable, so use a using block for deterministic cleanup
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(sourcePdf);

            // Delete every image from the document
            editor.DeleteImage();

            // Persist the modified document to the specified output path
            editor.Save(destinationPdf);
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_without_images.pdf";

        try
        {
            RemoveAllImages(inputPath, outputPath);
            Console.WriteLine($"All images removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}