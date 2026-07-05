using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated.pdf";

        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use the Facade API to add a text annotation.
        // The annotation contains Arabic text (right‑to‑left) to verify Unicode rendering.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (x, y, width, height) in points.
            Rectangle rect = new Rectangle(100, 500, 200, 100);

            // Arabic phrase "Hello World" – right‑to‑left script.
            string arabicText = "مرحبا بالعالم";

            // Create the text annotation on page 1.
            // Parameters: rectangle, title, contents, open flag, icon name, page number.
            editor.CreateText(rect, "RTL Test", arabicText, true, "Note", 1);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Text annotation added and saved to '{outputPdf}'.");
    }
}