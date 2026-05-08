using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfMetadataHelper
{
    /// <summary>
    /// Clears all XMP metadata entries from the specified PDF, preserving only the mandatory PDF schema header.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
    public static void ClearXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // PdfXmpMetadata is a SaveableFacade; use a using block for deterministic disposal.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF document.
            xmp.BindPdf(inputPdfPath);

            // Remove all XMP entries. The required PDF schema header remains intact.
            xmp.Clear();

            // Save the modified PDF to the output location.
            xmp.Save(outputPdfPath);
        }
    }
}

// Minimal entry point required for a console‑style project.
public class Program
{
    public static void Main(string[] args)
    {
        // If two arguments are supplied, treat them as input and output PDF paths.
        if (args.Length == 2)
        {
            try
            {
                PdfMetadataHelper.ClearXmpMetadata(args[0], args[1]);
                Console.WriteLine("XMP metadata cleared successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Usage: <inputPdfPath> <outputPdfPath>");
        }
    }
}