using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";
        const string xmlAttachmentPath = "metadata.xml";
        const string logPath        = "conversion_log.xml";
        const string outputPdfPath  = "output_pdfa1b.pdf";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(xmlAttachmentPath))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachmentPath}");
            return;
        }

        // Load the source PDF and convert it to PDF/A‑1b
        using (Document doc = new Document(sourcePdfPath))
        {
            // Convert to PDF/A‑1b; errors are logged to logPath and problematic objects are removed
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Add the external XML file as an embedded file (attachment)
            var fileSpec = new FileSpecification(Path.GetFileName(xmlAttachmentPath))
            {
                // The file content must be supplied as a stream
                Contents = new MemoryStream(File.ReadAllBytes(xmlAttachmentPath)),
                // Optional but helpful metadata
                MIMEType = "application/xml"
            };
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the resulting PDF/A‑1b document
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑1b document with XML attachment saved to '{outputPdfPath}'.");
    }
}
