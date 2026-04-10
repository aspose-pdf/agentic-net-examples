using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xmpFilePath   = "metadata.xmp";
        const string outputPdfPath = "output.pdf";

        // Verify that the input files exist
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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open the external XMP file as a stream
            using (FileStream xmpStream = File.OpenRead(xmpFilePath))
            {
                // Replace the existing XMP metadata block with the new one
                pdfDoc.SetXmpMetadata(xmpStream);
            }

            // Save the updated PDF to the specified output path
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"XMP metadata successfully replaced. Output saved to '{outputPdfPath}'.");
    }
}