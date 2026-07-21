using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string xmlAttachment  = "data.xml";           // external XML file to attach
        const string outputPdfPath  = "output_pdfa1b.pdf";  // resulting PDF/A‑1b file
        const string conversionLog  = "conversion.log";    // optional log file for conversion

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(xmlAttachment))
        {
            Console.Error.WriteLine($"XML attachment not found: {xmlAttachment}");
            return;
        }

        // Load the source PDF, convert it to PDF/A‑1b, add the XML attachment, and save.
        using (Document doc = new Document(inputPdfPath))
        {
            // Convert the document to PDF/A‑1b. Errors (if any) are written to conversionLog.
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Choose a page to host the attachment annotation (first page in this example).
            Page page = doc.Pages[1];

            // Define the rectangle that represents the annotation's clickable area.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            var rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);

            // Create a FileSpecification that points to the external XML file.
            // The file will be embedded in the PDF as an attachment.
            var fileSpec = new FileSpecification(xmlAttachment);

            // Create the FileAttachment annotation using the page, rectangle, and file specification.
            var attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // The Icon property expects a FileIcon enum value.
                Icon = FileIcon.Paperclip,
                Contents = "External XML data attached to the PDF/A‑1b document."
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(attachment);

            // Save the final PDF/A‑1b document with the attachment.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑1b document with XML attachment saved to '{outputPdfPath}'.");
    }
}
