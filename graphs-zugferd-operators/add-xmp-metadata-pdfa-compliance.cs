using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "pdfa_compliant.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF (using the standard load constructor)
        using (Document doc = new Document(inputPath))
        {
            // Basic document info (optional but useful)
            doc.Info.Title = "PDF/A Compliant Document";
            doc.Info.Creator = "MyApp 1.0";
            doc.Info.Producer = "Aspose.Pdf";

            // -----------------------------------------------------------------
            // Add required XMP metadata entries for PDF/A compliance
            // -----------------------------------------------------------------
            // The Metadata property gives direct access to the native XMP dictionary.
            // Add standard XMP properties.
            doc.Metadata.Add("xmp:CreateDate", DateTime.UtcNow);
            doc.Metadata.Add("xmp:ModifyDate", DateTime.UtcNow);
            doc.Metadata.Add("xmp:MetadataDate", DateTime.UtcNow);
            doc.Metadata.Add("xmp:CreatorTool", "MyApp 1.0");
            doc.Metadata.Add("xmp:DocumentID", "uuid:" + Guid.NewGuid().ToString());

            // -----------------------------------------------------------------
            // Add PDF/A extension schema entries (pdfaid) required for PDF/A-1a
            // -----------------------------------------------------------------
            // Directly add the pdfaid properties – Aspose.Pdf embeds them in the
            // appropriate PDF/A extension schema when they are present.
            doc.Metadata.Add("pdfaid:part", "1");          // PDF/A‑1
            doc.Metadata.Add("pdfaid:conformance", "A"); // PDF/A‑1a

            // -----------------------------------------------------------------
            // Save the PDF. The XMP metadata added above will be embedded.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A compliant file saved to '{outputPath}'.");
    }
}
