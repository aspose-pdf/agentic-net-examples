using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMetadataCleaner
{
    /// <summary>
    /// Removes the entire XMP metadata block from the specified PDF file
    /// and saves the result to a new file.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
    public static void RemoveXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Ensure the input file exists before proceeding.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // PdfXmpMetadata is a facade that works with XMP data.
        // It implements IDisposable, so we wrap it in a using block for deterministic cleanup.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the facade to the source PDF.
            xmp.BindPdf(inputPdfPath);

            // Clear removes all XMP elements from the document.
            xmp.Clear();

            // Save writes the modified PDF (now without XMP) to the target path.
            xmp.Save(outputPdfPath);
        }
    }
}

// Entry point required for a console‑type project.
public class Program
{
    public static void Main(string[] args)
    {
        // Simple command‑line usage: <inputPdf> <outputPdf>
        if (args.Length == 2)
        {
            try
            {
                PdfMetadataCleaner.RemoveXmpMetadata(args[0], args[1]);
                Console.WriteLine("XMP metadata removed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Usage: PdfMetadataCleaner <inputPdfPath> <outputPdfPath>");
        }
    }
}
