using System;
using System.IO;
using Aspose.Pdf;
using System.Collections.Generic;

class PdfMetadataCleaner
{
    /// <summary>
    /// Removes the entire XMP metadata block from the specified PDF file.
    /// The resulting file will be free of XMP metadata.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF containing XMP metadata.</param>
    /// <param name="outputPdfPath">Path where the metadata‑free PDF will be saved.</param>
    public static void RemoveXmpMetadata(string inputPdfPath, string outputPdfPath)
    {
        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF using the Document class – this class exposes XMP handling via the Metadata dictionary.
            Document pdfDoc = new Document(inputPdfPath);

            // Aspose.PDF (v26.x) does not expose an XmpMetadata property.
            // The XMP packet is represented by the Metadata dictionary. Clearing this dictionary removes the XMP block.
            if (pdfDoc.Metadata != null && pdfDoc.Metadata.Count > 0)
            {
                pdfDoc.Metadata.Clear();
            }

            // Save the cleaned PDF to the desired location.
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"XMP metadata removed. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., file access issues, corrupted PDF)
            Console.Error.WriteLine($"Error removing XMP metadata: {ex.Message}");
        }
    }

    // Example usage
    static void Main()
    {
        const string inputPath  = "sample_with_xmp.pdf";
        const string outputPath = "sample_without_xmp.pdf";

        RemoveXmpMetadata(inputPath, outputPath);
    }
}
