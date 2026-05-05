using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to which the annotation will be added
        const string attachment = "attachment_file.pdf"; // File to be attached
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

        // Create the facade, bind the existing PDF and add a file‑attachment annotation.
        // The annotation acts as a clickable link that opens the attached file.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdf);

        // Rectangle defines the clickable area (left, top, width, height) in points.
        // Here we place a 100×100 rectangle at (100,500) on page 1.
        Rectangle rect = new Rectangle(100, 500, 100, 100);

        // CreateFileAttachment adds the file and the visual annotation in one step.
        // Parameters: rectangle, tooltip text, path to file, page number (1‑based), icon name.
        editor.CreateFileAttachment(rect, "Open attached file", attachment, 1, "Paperclip");

        // Save the modified document.
        editor.Save(outputPdf);
        editor.Close();

        Console.WriteLine($"File attachment link added and saved to '{outputPdf}'.");
    }
}