using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF, add required XMP metadata, and save as PDF/A compliant document
        using (Document doc = new Document(inputPath))
        {
            // Standard document information (optional but useful)
            doc.Info.Title    = "PDF/A Document";
            doc.Info.Creator  = "MyApp";
            doc.Info.Producer = "Aspose.Pdf";

            // Add XMP metadata entries required for PDF/A compliance
            // Creator tool (who created the PDF)
            doc.Metadata.Add("xmp:CreatorTool", "MyApp 1.0");

            // Creation date (UTC)
            doc.Metadata.Add("xmp:CreateDate", DateTime.UtcNow);

            // Document identifier (must be a UUID prefixed with "uuid:")
            doc.Metadata.Add("pdf:DocumentID", "uuid:" + Guid.NewGuid().ToString());

            // Save the document (PDF/A compliance will be validated on save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A compliant file saved to '{outputPath}'.");
    }
}