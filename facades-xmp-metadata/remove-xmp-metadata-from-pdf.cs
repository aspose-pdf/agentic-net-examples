using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfMetadataCleaner
{
    // Removes the entire XMP metadata block from a PDF file.
    public static void RemoveXmp(string inputPdfPath, string outputPdfPath)
    {
        // Ensure the source file exists.
        if (!File.Exists(inputPdfPath))
            throw new FileNotFoundException($"Input file not found: {inputPdfPath}");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Bind the XMP metadata facade to the loaded document.
            // Using the constructor that accepts a Document ensures the facade works on this instance.
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Clear all XMP elements.
            xmp.Clear();

            // Save the modified document. No SaveOptions are required for PDF output.
            doc.Save(outputPdfPath);
        }
    }

    // Example entry point.
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_xmp.pdf";

        try
        {
            RemoveXmp(inputPath, outputPath);
            Console.WriteLine($"XMP metadata removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}