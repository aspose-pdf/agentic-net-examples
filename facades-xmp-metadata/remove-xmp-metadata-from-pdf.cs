using System;
using System.IO;
using Aspose.Pdf.Facades;

public class XmpRemover
{
    /// <summary>
    /// Removes the entire XMP metadata block from a PDF file and saves the result.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the metadata‑free PDF will be saved.</param>
    public static void RemoveXmp(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

        // Initialize the XMP metadata facade
        PdfXmpMetadata xmp = new PdfXmpMetadata();

        // Load the PDF document into the facade
        xmp.BindPdf(inputPdfPath);

        // Clear all XMP elements
        xmp.Clear();

        // Save the cleaned PDF
        xmp.Save(outputPdfPath);

        // Release resources
        xmp.Close();
    }

    // Example usage
    public static void Main()
    {
        string source = "input.pdf";
        string destination = "output_no_xmp.pdf";

        try
        {
            RemoveXmp(source, destination);
            Console.WriteLine($"XMP metadata removed. Saved to '{destination}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}