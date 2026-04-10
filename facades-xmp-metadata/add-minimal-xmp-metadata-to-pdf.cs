using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_xmp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Bind the PDF to the XMP metadata facade
                using (PdfXmpMetadata xmp = new PdfXmpMetadata())
                {
                    xmp.BindPdf(doc);

                    // If no XMP metadata is present, add a minimal set
                    if (xmp.Count == 0)
                    {
                        // Add a few essential metadata fields
                        xmp.Add(DefaultMetadataProperties.CreatorTool, "Aspose.Pdf");
                        xmp.Add(DefaultMetadataProperties.CreateDate, DateTime.UtcNow);
                        xmp.Add(DefaultMetadataProperties.ModifyDate, DateTime.UtcNow);
                        xmp.Add(DefaultMetadataProperties.Nickname, "GeneratedMetadata");
                    }

                    // Save the PDF (with the XMP metadata) to the output file
                    xmp.Save(outputPdf);
                }
            }

            Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}