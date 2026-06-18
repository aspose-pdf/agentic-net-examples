using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to which the annotation will be added
        const string attachment = "attachment_file.pdf"; // File to be attached and opened on click
        const string outputPdf  = "output.pdf";         // Resulting PDF

        // Verify files exist
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

        // Create a PdfContentEditor facade, bind the source PDF, add the file‑attachment annotation,
        // and save the result. All disposable objects are wrapped in using blocks.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (position and size) using System.Drawing.Rectangle
            // (0,0) is the lower‑left corner of the page.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 200, 200); // x, y, width, height

            // Create the file attachment annotation.
            // Parameters: rectangle, annotation contents, path to the file to attach,
            // page number (1‑based), and icon name ("Graph", "PushPin", "Paperclip", "Tag").
            editor.CreateFileAttachment(rect, "Open attached file", attachment, 1, "Paperclip");

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"File attachment link added. Output saved to '{outputPdf}'.");
    }
}