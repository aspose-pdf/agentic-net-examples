using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string attachment = "document_to_attach.pdf"; // file to attach
        const string outputPdf  = "output_with_attachment.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle that represents the annotation's border
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification describing the attached file
            // Use the constructor that accepts the file path and a description.
            FileSpecification fileSpec = new FileSpecification(attachment, "Sample PDF attachment");
            // Optional: set Description explicitly (the constructor already sets it)
            fileSpec.Description = "Sample PDF attachment";

            // Create the file attachment annotation
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual properties
                Icon = FileIcon.Paperclip,
                Title = "Attached Document",
                Subject = "PDF Attachment"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdf}'.");
    }
}
