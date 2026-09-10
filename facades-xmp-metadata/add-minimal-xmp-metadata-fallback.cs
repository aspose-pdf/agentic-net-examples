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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create the XMP metadata facade and bind it to the document
            using (PdfXmpMetadata xmp = new PdfXmpMetadata())
            {
                xmp.BindPdf(doc);

                // Retrieve existing XMP metadata (if any)
                byte[] existingMetadata = xmp.GetXmpMetadata();

                // If the PDF has no XMP metadata, add a minimal set
                if (existingMetadata == null || existingMetadata.Length == 0)
                {
                    // Minimal required metadata entries
                    xmp.Add("dc:title", "Untitled Document");
                    xmp.Add("dc:creator", "Aspose.Pdf");
                    xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                }

                // Save the PDF (with updated XMP metadata) to the output file
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}