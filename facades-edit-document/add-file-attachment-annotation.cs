using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // Source PDF
        const string outputPdf     = "output.pdf";         // Result PDF
        const string attachment    = "attachment_file.pdf";// File to attach

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

        // Initialize the PdfContentEditor facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Define the annotation rectangle (x, y, width, height)
        // Adjust coordinates as needed for your document layout
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 100, 100);

        // Create a file attachment annotation on page 1.
        // The annotation will display the specified icon ("Graph") and,
        // when clicked, open the attached file.
        editor.CreateFileAttachment(
            rect,                 // Annotation rectangle (System.Drawing.Rectangle)
            "Attached file",    // Tooltip / contents
            attachment,           // Path to the file to attach
            1,                    // Page number (1‑based)
            "Graph");            // Icon name (Graph, PushPin, Paperclip, Tag)

        // Save the modified PDF
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"PDF with file‑attachment link saved to '{outputPdf}'.");
    }
}
