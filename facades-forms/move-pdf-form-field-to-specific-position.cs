using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize FormEditor facade with source and destination files
            FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

            // Move the field named "Signature" to the desired rectangle on page 5.
            // Replace the placeholder values with the exact coordinates (in points).
            // llx, lly = lower‑left corner; urx, ury = upper‑right corner.
            float llx = 400f;   // placeholder: X of lower‑left corner
            float lly = 50f;    // placeholder: Y of lower‑left corner
            float urx = 560f;   // placeholder: X of upper‑right corner
            float ury = 120f;   // placeholder: Y of upper‑right corner

            bool moved = formEditor.MoveField("Signature", llx, lly, urx, ury);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the 'Signature' field.");
            }

            // Persist changes to the output PDF
            formEditor.Save();
            Console.WriteLine($"Field moved and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}