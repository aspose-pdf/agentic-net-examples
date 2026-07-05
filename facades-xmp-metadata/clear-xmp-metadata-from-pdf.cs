using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class XmpMetadataHelper
{
    // Clears all XMP metadata fields from a PDF, preserving only the required PDF schema header.
    public static void ClearXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // PdfXmpMetadata is a facade that implements IDisposable, so use a using block.
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Load the PDF document into the facade.
            xmp.BindPdf(inputPdfPath);

            // Remove all XMP entries. The minimal required schema header remains intact.
            xmp.Clear();

            // Save the cleaned PDF to the destination path.
            xmp.Save(outputPdfPath);
        }
    }
}

public class Program
{
    // Simple console entry point. Expects two arguments: input PDF path and output PDF path.
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: XmpMetadataHelper <inputPdfPath> <outputPdfPath>");
            return;
        }

        try
        {
            XmpMetadataHelper.ClearXmpMetadata(args[0], args[1]);
            Console.WriteLine("XMP metadata cleared successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}