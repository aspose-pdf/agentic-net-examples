using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "source.pdf";      // original PDF
        const string xmlAttachment   = "metadata.xml";   // XML file to attach
        const string conversionLog  = "conversion_log.xml";
        const string outputPdfPath  = "output_pdfa1b.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlAttachment))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachment}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Convert to PDF/A‑1b (PDF/A-1b)
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Embed the external XML file as an attachment using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(xmlAttachment));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(xmlAttachment));
                // Optional: set MIME type for better identification (not required for PDF/A‑1b)
                fileSpec.MIMEType = "application/xml";
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the resulting PDF/A‑1b document (attachment is now embedded)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑1b file with XML attachment saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
