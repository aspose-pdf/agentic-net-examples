using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF
        const string xmlAttachmentPath = "attachment.xml"; // XML file to attach
        const string outputPdfPath = "output_pdfa.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlAttachmentPath))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachmentPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Convert to PDF/A‑1b (PDF/A-1b is represented by PdfFormat.PDF_A_1B)
                // Errors during conversion are deleted (ConvertErrorAction.Delete)
                doc.Convert("conversion_log.txt", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Create a FileSpecification using the constructor (filePath, description)
                FileSpecification fileSpec = new FileSpecification(xmlAttachmentPath, "XML attachment");
                fileSpec.Description = "External XML attachment"; // optional description

                // Define a rectangle for the attachment annotation (position on the page)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

                // Create the FileAttachment annotation on the first page
                Page firstPage = doc.Pages[1];
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(firstPage, rect, fileSpec)
                {
                    // Optional visual settings
                    Icon = FileIcon.Graph,               // use FileIcon enum (Graph, Paperclip, PushPin, Tag)
                    Color = Color.Blue,
                    Contents = "External XML attachment"
                };

                // Add the annotation to the page
                firstPage.Annotations.Add(attachment);

                // Save the resulting PDF/A‑1b document with the attachment
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF/A‑1b document with XML attachment saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
