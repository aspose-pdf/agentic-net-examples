using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMetadataHelper
{
    /// <summary>
    /// Removes the entire XMP metadata block from a PDF file and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the metadata‑free PDF will be saved.</param>
    public static void RemoveXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Validate input file existence.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Use the PdfXmpMetadata facade to manipulate XMP data.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the facade to the source PDF.
            xmp.BindPdf(inputPdfPath);

            // Clear all XMP elements – this removes the entire XMP block.
            xmp.Clear();

            // Save the modified PDF to the specified output path.
            xmp.Save(outputPdfPath);
        }
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point to make the project compile as an executable.
    /// Accepts two arguments: input PDF path and output PDF path.
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            PdfMetadataHelper.RemoveXmpMetadata(args[0], args[1]);
            Console.WriteLine($"Metadata removed successfully. Output saved to '{args[1]}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
