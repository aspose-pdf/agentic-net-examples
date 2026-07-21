using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF to which the attachment will be added
        const string attachment = "attachment.pdf";   // File to attach
        const string outputPdf = "output_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // FileSpecification constructor takes the file path and a description.
            // The MIME type cannot be set directly – Aspose.PDF determines it from the file content.
            FileSpecification fileSpec = new FileSpecification(attachment, "Attached PDF document");
            fileSpec.Name = Path.GetFileName(attachment); // optional display name
            fileSpec.Description = "Attached PDF document"; // can be set again if needed

            // Define the rectangle that will represent the annotation icon on the page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the file attachment annotation on the first page (1‑based indexing)
            Page page = doc.Pages[1];
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                Title = "PDF Attachment",   // Title shown in the annotation popup
                Contents = "Click the icon to open the attached PDF.",
                Icon = FileIcon.Paperclip   // Choose an appropriate icon
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnnot);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdf}'.");
    }
}
