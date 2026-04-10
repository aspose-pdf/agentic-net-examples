using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // PDF with attachment
        const string attachFile = "document.pdf";       // file to attach

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachFile}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the annotation icon (Aspose.Pdf.Rectangle, not System.Drawing.Rectangle)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a FileSpecification describing the attached file (description is optional)
            FileSpecification fileSpec = new FileSpecification(attachFile, "Attached PDF document");
            // NOTE: Aspose.Pdf.FileSpecification does not expose a MimeType property. The MIME type is inferred from the file extension.

            // Create the file attachment annotation
            FileAttachmentAnnotation fileAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                Title    = "Attachment",               // title shown in the popup
                Contents = "Click the icon to open the attached PDF.", // tooltip text
                Icon     = FileIcon.Paperclip          // use the correct enum for the icon
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnnot);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"File attachment added and saved to '{outputPdf}'.");
    }
}
