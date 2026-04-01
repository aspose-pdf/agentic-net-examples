using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the XMP metadata facade to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Determine whether any XMP metadata is present
            bool hasXmp = xmp.Count > 0;
            Console.WriteLine(hasXmp ? "XMP metadata found." : "No XMP metadata present.");

            if (!hasXmp)
            {
                // Add a simple custom XMP field
                xmp.Add("custom:example", "Sample");
                // Save the PDF with the new XMP metadata
                xmp.Save(outputPath);
                Console.WriteLine($"Added XMP metadata and saved to {outputPath}");
            }
            else
            {
                // No changes needed; just save the original PDF
                doc.Save(outputPath);
                Console.WriteLine($"Saved original PDF to {outputPath}");
            }
        }
    }
}