using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facade namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // Source PDF
        const string xmpFilePath   = "metadata.xmp"; // External XMP file
        const string outputPdfPath = "output.pdf";    // Result PDF

        // Validate input files
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
            // Load the PDF document (lifecycle: using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the external XMP file as a stream
                using (FileStream xmpStream = File.OpenRead(xmpFilePath))
                {
                    // Replace the existing XMP block with the new metadata
                    pdfDoc.SetXmpMetadata(xmpStream);
                }

                // Save the updated PDF (lifecycle: using ensures the document stays alive until Save completes)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Successfully replaced XMP metadata and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}