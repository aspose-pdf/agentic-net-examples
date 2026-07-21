using System;
using System.IO;
using Aspose.Pdf;                 // Core API for Document
using Aspose.Pdf.Facades;        // Facades namespace (required by task)

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // Source PDF
        const string xmpFilePath   = "metadata.xmp"; // External XMP file
        const string outputPdfPath = "output.pdf";  // Result PDF

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpFilePath))
        {
            Console.Error.WriteLine($"XMP file not found: {xmpFilePath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Open the external XMP file as a stream
                using (FileStream xmpStream = File.OpenRead(xmpFilePath))
                {
                    // Replace the existing XMP metadata with the new stream
                    pdfDoc.SetXmpMetadata(xmpStream);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"XMP metadata replaced successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}