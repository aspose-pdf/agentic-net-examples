using System;
using System.IO;
using System.Drawing;                 // needed for Rectangle (GDI+)
using Aspose.Pdf;                    // core PDF classes
using Aspose.Pdf.Facades;            // facades for annotation editing

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "annotated_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a PdfContentEditor facade, bind the source PDF, add a right‑to‑left text annotation,
        // and save the result. All resources are disposed via using blocks.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Define the annotation rectangle (System.Drawing.Rectangle is required by the API).
            // Position: lower‑left corner (50, 700), size 200x100.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(50, 700, 200, 100);

            // Arabic text (right‑to‑left) to verify Unicode rendering.
            string rtlText = "مرحبا بالعالم"; // "Hello World" in Arabic

            // Create a text annotation:
            //   rect   – location on the page
            //   title  – annotation title (shown in the popup)
            //   contents – the actual annotation text (Arabic)
            //   open   – show the annotation open by default
            //   icon   – built‑in icon name (e.g., "Note")
            //   page   – 1‑based page number
            editor.CreateText(rect, "RTL Test", rtlText, true, "Note", 1);

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Text annotation added. Output saved to '{outputPdf}'.");
    }
}