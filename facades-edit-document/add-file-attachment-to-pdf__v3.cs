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
        const string attachmentPath = "Terms.pdf";
        const string attachmentDescription = "Contract Terms";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define the rectangle where the attachment icon will appear on the first page.
        // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        var rect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);

        // Create a FileSpecification for the attachment.
        // The constructor accepts the file path and an optional description.
        FileSpecification fileSpec = new FileSpecification(attachmentPath, attachmentDescription);

        // Create the file attachment annotation, passing the page, rectangle, and FileSpecification.
        FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(pdfDocument.Pages[1], rect, fileSpec)
        {
            // The name that appears in the attachment list.
            Name = Path.GetFileName(attachmentPath)
        };

        // Add the annotation to the first page.
        pdfDocument.Pages[1].Annotations.Add(fileAttachment);

        // Save the updated PDF.
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Attachment added and saved to '{outputPdf}'.");
    }
}
