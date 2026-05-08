using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentFile = "attachment.txt";

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification for the attachment
            FileSpecification fileSpec = new FileSpecification(attachmentFile);

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Select the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the file attachment annotation using the page, rectangle, and file specification
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional visual properties
                Contents = "Attached file",
                Color = Aspose.Pdf.Color.Blue
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(attachment);

            // Save the modified PDF to a new file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdf}'.");
    }
}