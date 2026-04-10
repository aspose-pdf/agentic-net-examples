using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to which the attachment will be added
        const string attachment = "attachment_file.pdf"; // File to attach
        const string outputPdf  = "output.pdf";         // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(attachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachment}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (position and size) in points.
            // System.Drawing.Rectangle is required by the facade API.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a file attachment annotation on page 1.
            // Parameters: rectangle, annotation contents, file path, page number, icon name.
            editor.CreateFileAttachment(rect, "Attached file", attachment, 1, "Paperclip");

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"File attachment annotation added. Output saved to '{outputPdf}'.");
    }
}