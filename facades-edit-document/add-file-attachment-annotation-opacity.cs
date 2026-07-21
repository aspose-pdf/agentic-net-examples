using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facade API for annotation handling

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentPath = "attachment.txt";   // File to attach

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the attachment file exists (create a simple placeholder if missing)
        if (!File.Exists(attachmentPath))
        {
            File.WriteAllText(attachmentPath, "Sample attachment content");
        }

        // Use PdfContentEditor (facade) to add a file‑attachment annotation with opacity 0.6 on page 3
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle (x, y, width, height) in points
            // System.Drawing.Rectangle expects (x, y, width, height)
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 100, 100);

            // Create the file‑attachment annotation:
            //   rect      – annotation rectangle (System.Drawing.Rectangle)
            //   contents  – tooltip text
            //   filePath  – file to attach
            //   page      – target page (3)
            //   name      – icon name (e.g., "Graph")
            //   opacity   – 0.6 (semi‑transparent)
            editor.CreateFileAttachment(annotRect, "Sample attachment", attachmentPath, 3, "Graph", 0.6);

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotation with opacity 0.6 added on page 3. Saved to '{outputPdfPath}'.");
    }
}
