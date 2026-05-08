using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the "Signature" field
        const string outputPdf = "output.pdf";  // PDF after moving the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Typical A4 page size in points: 595 (width) x 842 (height)
        // Desired field size: 150 x 50 points
        // Place it in the lower‑right corner with a 20‑point margin
        float pageWidth = 595f;
        float margin    = 20f;
        float fieldWidth  = 150f;
        float fieldHeight = 50f;

        float llx = pageWidth - fieldWidth - margin; // lower‑left X
        float lly = margin;                         // lower‑left Y
        float urx = pageWidth - margin;             // upper‑right X
        float ury = margin + fieldHeight;           // upper‑right Y

        // FormEditor works with AcroForm fields. It does not implement IDisposable,
        // so we just instantiate it, perform the move, and save the result.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);
        try
        {
            // Move the field named "Signature" on page 5 to the calculated rectangle.
            // Page numbers are 1‑based.
            bool moved = formEditor.MoveField("Signature", llx, lly, urx, ury);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the field. Verify that the field name exists.");
            }

            // Persist changes to the output file.
            formEditor.Save();
            Console.WriteLine($"Field \"Signature\" moved and saved to '{outputPdf}'.");
        }
        finally
        {
            // FormEditor does not require explicit disposal, but we close it to release resources.
            formEditor.Close();
        }
    }
}