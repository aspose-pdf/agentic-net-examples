using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string outputPath = "output_pdfa3a.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Enable auto‑tagging globally
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Embed the external file using the EmbeddedFiles collection
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                doc.EmbeddedFiles.Add(fileSpec);

                // Convert to PDF/A‑3a
                doc.Convert(logPath, PdfFormat.PDF_A_3A, ConvertErrorAction.Delete);

                // Save the PDF/A‑3a document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3a file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
