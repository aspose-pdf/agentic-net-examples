using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";        // result PDF
        const string attachmentFile = "datafile.dat"; // file to attach
        const string description = "Data attachment"; // description for the attachment

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Ensure the attachment file exists
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Use PdfContentEditor (Facade) to open, modify, and save the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Access the underlying Document object for low‑level modifications
            Document doc = editor.Document;

            // Create a FileSpecification describing the attachment (constructor sets file path and description)
            FileSpecification fileSpec = new FileSpecification(attachmentFile, description)
            {
                // Set the AFRelationship to Data for better document organization
                AFRelationship = AFRelationship.Data
            };

            // Choose the page where the attachment annotation will appear (first page)
            Page page = doc.Pages[1];

            // Define the rectangle for the annotation icon (position on the page)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the file attachment annotation using the specification
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Choose an icon style (Graph, PushPin, Paperclip, Tag)
                Icon = FileIcon.Graph
            };

            // Add the annotation to the page
            page.Annotations.Add(fileAnn);

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added with relationship 'Data' and saved to '{outputPdf}'.");
    }
}
