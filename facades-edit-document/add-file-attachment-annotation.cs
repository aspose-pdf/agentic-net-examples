using System;
using System.IO;
using System.Drawing;               // for Rectangle
using Aspose.Pdf.Facades;          // PdfContentEditor

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to modify
        const string attachment = "attachment_file.pdf"; // File to attach
        const string outputPdf  = "output.pdf";

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

        // Create a PdfContentEditor, bind the source PDF, add the file‑attachment annotation,
        // and save the result.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (position and size) on page 1.
            Rectangle rect = new Rectangle(100, 500, 100, 100); // x, y, width, height

            // Add the file attachment annotation.
            // Parameters: rectangle, annotation contents, path to file, page number, icon name.
            editor.CreateFileAttachment(rect, "Attached document", attachment, 1, "Graph");

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"File attachment annotation added. Output saved to '{outputPdf}'.");
    }
}