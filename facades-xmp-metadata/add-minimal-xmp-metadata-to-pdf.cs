using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_xmp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the PDF to the XMP metadata facade (constructor with Document)
            using (PdfXmpMetadata xmp = new PdfXmpMetadata(doc))
            {
                // Retrieve existing XMP metadata (returns empty byte[] if none)
                byte[] existingMetadata = xmp.GetXmpMetadata();

                // If no metadata is present, add a minimal set
                if (existingMetadata == null || existingMetadata.Length == 0)
                {
                    // Add a title and creator – using the string/object overloads
                    xmp.Add("xmp:Title", "Untitled Document");
                    xmp.Add("xmp:CreatorTool", "Aspose.Pdf");
                }

                // Save the PDF with the (new or existing) XMP metadata
                // (SaveableFacade.Save(string) follows the provided save rule)
                xmp.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}