using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class XmpMetadataHelper
{
    /// <summary>
    /// Clears all XMP metadata fields from the input PDF, preserving only the mandatory PDF schema header,
    /// and saves the result to the specified output file.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF file.</param>
    /// <param name="outputPdfPath">Path where the cleaned PDF will be saved.</param>
    public static void ClearAllXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Validate input file existence
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create the XMP metadata facade and bind it to the loaded document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(doc);

                // Remove all XMP entries; the PDF schema header remains intact by design
                xmp.Clear();

                // Save the modified PDF (the Save method of PdfXmpMetadata writes the document)
                xmp.Save(outputPdfPath);
            }
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application. Demonstrates usage of ClearAllXmpMetadata.
    /// </summary>
    public static void Main(string[] args)
    {
        // Expect exactly two arguments: input PDF path and output PDF path.
        if (args.Length == 2)
        {
            try
            {
                XmpMetadataHelper.ClearAllXmpMetadata(args[0], args[1]);
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