using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Register the standard XMP namespace (required for the prefixed properties)
            doc.Metadata.RegisterNamespaceUri("xmp", "http://ns.adobe.com/xap/1.0/");

            // PDF/A requires a document identifier – store it as a UUID prefixed with "uuid:"
            string documentId = "uuid:" + Guid.NewGuid().ToString();
            doc.Metadata["xmp:DocumentID"] = documentId;

            // Creator tool information
            doc.Metadata["xmp:CreatorTool"] = "My PDF/A Generator";

            // Creation and modification dates in ISO‑8601 UTC format
            string nowIso = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            doc.Metadata["xmp:CreateDate"] = nowIso;
            doc.Metadata["xmp:ModifyDate"] = nowIso;

            // Save the PDF – the XMP packet is automatically embedded
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with PDF/A XMP metadata to '{outputPath}'.");
    }
}
