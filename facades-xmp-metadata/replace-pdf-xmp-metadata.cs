using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API (required for SetXmpMetadata)
using Aspose.Pdf.Facades;            // Facades namespace (included as per task requirement)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Source PDF
        const string xmpFilePath   = "metadata.xmp"; // External XMP file (XML format)
        const string outputPdfPath = "output.pdf";  // PDF with replaced XMP block

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpFilePath))
        {
            Console.Error.WriteLine($"Error: XMP file not found – {xmpFilePath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the external XMP file as a stream
                using (FileStream xmpStream = File.OpenRead(xmpFilePath))
                {
                    // Replace the existing XMP metadata block with the new one
                    pdfDoc.SetXmpMetadata(xmpStream);
                }

                // Save the modified PDF (lifecycle rule: save inside using block)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully replaced XMP metadata. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}