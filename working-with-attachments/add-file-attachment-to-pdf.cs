using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the existing PDF (document disposal handled by using)
        using (Document doc = new Document(inputPdf))
        {
            // Use 1‑based page indexing to get the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

            // Create a FileSpecification for the file to be attached
            FileSpecification fileSpec = new FileSpecification(attachmentFile);

            // Create the file attachment annotation
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual settings
                Icon = FileIcon.Paperclip, // corrected enum reference
                Color = Aspose.Pdf.Color.Blue,
                Contents = "Attached file"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdf}'.");
    }
}
