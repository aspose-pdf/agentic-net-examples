using System;
using System.Drawing; // needed for System.Drawing.Rectangle
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPdf = "output.pdf";

        // Create a simple PDF in memory to serve as the input document.
        using (MemoryStream inputStream = new MemoryStream())
        {
            // Build a one‑page PDF.
            Document doc = new Document();
            doc.Pages.Add(); // adds a blank page.
            doc.Save(inputStream);
            inputStream.Position = 0; // reset for reading.

            // Define the annotation rectangle (x, y, width, height).
            // Fully qualified to avoid ambiguity.
            System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            const string title = "User Comment";   // Title shown in the annotation's title bar
            const string contents = "This is a user comment."; // Text displayed inside the sticky note

            // Use the PdfContentEditor facade to bind, create, and save.
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the in‑memory PDF.
                editor.BindPdf(inputStream);

                // Create a sticky‑note (text) annotation on page 1.
                // Icon "Note" produces the classic sticky‑note appearance.
                // The 'open' flag set to true makes the note visible when the PDF opens.
                editor.CreateText(annotRect, title, contents, true, "Note", 1);

                // Persist the changes to an output file.
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Sticky note annotation added and saved to '{outputPdf}'.");
    }
}
