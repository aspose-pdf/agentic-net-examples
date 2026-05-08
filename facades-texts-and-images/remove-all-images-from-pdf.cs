using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfImageRemover
{
    /// <summary>
    /// Removes all images from the specified PDF file and saves the result to a new file.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF.</param>
    /// <param name="outputPath">Path where the image‑free PDF will be saved.</param>
    public static void RemoveAllImages(string inputPath, string outputPath)
    {
        // Validate input file existence.
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF to the editor.
            editor.BindPdf(inputPath);

            // Delete every image from the document.
            editor.DeleteImage();

            // Save the modified document to the desired output path.
            editor.Save(outputPath);
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console‑style build. Demonstrates the image‑removal utility.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: PdfImageRemover <inputPdfPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            PdfImageRemover.RemoveAllImages(inputPath, outputPath);
            Console.WriteLine($"All images removed. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}