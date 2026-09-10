using System;
using System.Drawing; // needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF (created if missing)
        const string outputPath = "output.pdf";        // PDF with sticky note
        const string title = "User Comment";          // annotation title (author)
        const string contents = "This is a user comment added as a sticky note.";
        const bool open = false;                       // initially closed
        const string icon = "Note";                    // icon type (Note, Comment, etc.)

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!System.IO.File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // System.Drawing.Rectangle expects (x, y, width, height).
        // Convert the PDF‑style coordinates (left, top, right, bottom) to this form.
        System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 700, 100, 100);

        // Use the facade to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Create a sticky note (text annotation) on page 1
            editor.CreateText(annotRect, title, contents, open, icon, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Sticky note added and saved to '{outputPath}'.");
    }
}
