using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPdfPath  = "source.pdf";
        const string outputPdfPath = "result.pdf";

        // Example byte array representing the file to embed
        // Replace this with the actual data you want to attach
        byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes("Hello, embedded file!");

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the existing PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment will be placed (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Create a FileSpecification from the byte array using a MemoryStream
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                // The second argument is the name that will appear in the attachment list
                FileSpecification fileSpec = new FileSpecification(ms, "EmbeddedText.txt");

                // Define the rectangle that bounds the annotation icon
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Create the file attachment annotation (lifecycle: create)
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional: set the icon type (Paperclip, Graph, etc.)
                    Icon = FileIcon.Paperclip,
                    // Optional: set a tooltip that appears on hover
                    Contents = "Embedded text file"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF (lifecycle: save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with embedded file: {outputPdfPath}");
    }
}
