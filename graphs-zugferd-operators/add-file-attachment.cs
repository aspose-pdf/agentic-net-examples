using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF to work with
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Create a sample PDF file that will be attached
        using (Document attachment = new Document())
        {
            attachment.Pages.Add();
            attachment.Save("attachment.pdf");
        }

        // Open the sample PDF and add a file attachment annotation
        using (Document doc = new Document("input.pdf"))
        {
            // Create file specification for the attachment using the constructor
            FileSpecification fileSpec = new FileSpecification("attachment.pdf", "Sample PDF attachment");
            // Description can be updated if needed
            fileSpec.Description = "Sample PDF attachment";

            // Define rectangle for the annotation icon (Aspose.Pdf.Rectangle)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the file attachment annotation on the first page
            FileAttachmentAnnotation attachmentAnnotation = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec);
            attachmentAnnotation.Icon = FileIcon.Graph; // Use FileIcon enum instead of non‑existent IconGraph
            attachmentAnnotation.Subject = "Attached PDF Document";
            attachmentAnnotation.Contents = "This annotation embeds a PDF file.";

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(attachmentAnnotation);

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}