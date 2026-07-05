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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Retrieve existing XMP metadata (if any)
            byte[] existingMetadata;
            using (MemoryStream ms = new MemoryStream())
            {
                doc.GetXmpMetadata(ms);
                existingMetadata = ms.ToArray();
            }

            // If no XMP metadata is present, create minimal metadata
            if (existingMetadata == null || existingMetadata.Length == 0)
            {
                // Create a PdfXmpMetadata facade and bind it to the document
                using (PdfXmpMetadata xmp = new PdfXmpMetadata())
                {
                    xmp.BindPdf(doc);

                    // Add a few essential XMP entries
                    xmp.Add("dc:title", "Untitled Document");
                    xmp.Add("dc:creator", "Aspose.Pdf");
                    xmp.Add("xmp:CreateDate", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));

                    // Save the PDF with the newly added XMP metadata
                    xmp.Save(outputPath);
                }
            }
            else
            {
                // XMP metadata already exists; simply copy the PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}