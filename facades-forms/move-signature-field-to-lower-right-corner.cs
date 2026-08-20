using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Define target rectangle for the "Signature" field on page 5
        // Example: lower‑right corner with width=150pt, height=50pt, 20pt margin
        float pageWidth = 595f; // typical A4 width in points
        float margin = 20f;
        float fieldWidth = 150f;
        float fieldHeight = 50f;

        float llx = pageWidth - margin - fieldWidth; // lower‑left X
        float lly = margin;                         // lower‑left Y
        float urx = pageWidth - margin;              // upper‑right X
        float ury = margin + fieldHeight;            // upper‑right Y

        // Move the field named "Signature" to the new rectangle
        bool moved = formEditor.MoveField("Signature", llx, lly, urx, ury);
        if (!moved)
        {
            Console.Error.WriteLine("Failed to move the field 'Signature'.");
        }

        // Save the modified PDF
        formEditor.Save();
        Console.WriteLine($"Field moved and saved to '{outputPdf}'.");
    }
}