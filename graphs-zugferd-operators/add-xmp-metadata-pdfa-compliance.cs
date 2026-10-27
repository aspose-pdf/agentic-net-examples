using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "pdfa_output.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page (PDF must contain at least one page)
            doc.Pages.Add();

            // ------------------------------------------------------------
            // Build XMP metadata required for PDF/A‑1b compliance using
            // the core Document.Metadata API (no XmpMetadata property).
            // ------------------------------------------------------------
            string documentId = Guid.NewGuid().ToString("N");

            // Register the namespaces used in the XMP packet
            doc.Metadata.RegisterNamespaceUri("pdfaid", "http://www.aiim.org/pdfa/id/");
            doc.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");

            // Populate the required XMP entries
            doc.Metadata["pdfaid:part"] = "1";                 // PDF/A part
            doc.Metadata["pdfaid:conformance"] = "B";          // Conformance level B
            doc.Metadata["pdfaid:documentID"] = documentId;    // Document identifier
            doc.Metadata["pdfaid:creationTool"] = "Aspose.Pdf for .NET"; // Creation tool
            doc.Metadata["dc:format"] = "application/pdf";   // MIME type (optional but common)

            // ------------------------------------------------------------
            // Populate standard document information (optional but recommended)
            // ------------------------------------------------------------
            doc.Info.Title = "Sample PDF/A Document";
            doc.Info.Author = "Aspose Example";
            doc.Info.Creator = "Aspose.Pdf for .NET"; // maps to PDF "Creator" entry
            // Aspose expects local time for these properties (not UTC)
            doc.Info.CreationDate = DateTime.Now;
            doc.Info.ModDate = DateTime.Now; // modification date
            // Store the same identifier in the document info dictionary for completeness
            doc.Info["DocumentID"] = documentId;

            // Convert the document to PDF/A‑1b using conversion options
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            doc.Convert(convOpts);

            // Save the PDF/A compliant file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}
