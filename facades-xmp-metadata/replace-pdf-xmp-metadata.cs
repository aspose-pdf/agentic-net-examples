using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace is required by the task

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";
        const string xmpPath      = "metadata.xmp";
        const string outputPath   = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmpPath))
        {
            Console.Error.WriteLine($"XMP file not found: {xmpPath}");
            return;
        }

        // Read the external XMP metadata as a raw string
        string xmpContent = File.ReadAllText(xmpPath);

        try
        {
            // Load the PDF document (detect encryption via PdfException)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Replace (or add) the XMP block.
                // The XMP metadata is stored under the key "xmp" in the Metadata dictionary.
                pdfDoc.Metadata["xmp"] = xmpContent;

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"XMP metadata successfully replaced. Output saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Handles encrypted PDFs or other PDF‑specific errors
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}