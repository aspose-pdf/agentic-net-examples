using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the image to attach, and the output PDF
        const string inputPdfPath = "input.pdf";
        const string imageFilePath = "stamp.png";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPdfPath);

        // ------------------------------------------------------------
        // 1) Add the image as an embedded file attachment.
        // ------------------------------------------------------------
        // Create a FileSpecification using the (filePath, description) ctor.
        // No MimeType property is required – Aspose.Pdf infers it from the file.
        FileSpecification attachment = new FileSpecification(imageFilePath, "Stamp Image Attachment");
        // Optional: set modification date if needed (available via Params.ModDate).
        attachment.Params.ModDate = DateTime.UtcNow;
        doc.EmbeddedFiles.Add(attachment);

        // ------------------------------------------------------------
        // 2) Add a stamp annotation that references the same image.
        // ------------------------------------------------------------
        // Use ImageStamp – positioning is done via alignment + X/Y indents.
        ImageStamp imgStamp = new ImageStamp(imageFilePath)
        {
            // Define size of the stamp (optional – if omitted the image's original size is used)
            Width = 100f,
            Height = 100f,
            // Position the stamp 100 points from the left and 100 points from the bottom.
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            XIndent = 100f,
            YIndent = 100f,
            // Ensure the stamp is placed on top of page content.
            Background = false
        };

        // Add the stamp to the first page (pages are 1‑based).
        doc.Pages[1].AddStamp(imgStamp);

        // Save the modified PDF to a new file.
        doc.Save(outputPdfPath);

        Console.WriteLine($"PDF saved with attachment and stamp: {outputPdfPath}");
    }
}
