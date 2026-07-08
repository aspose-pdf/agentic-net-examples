using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Desired field dimensions and position (lower‑right corner of page 5)
        // Adjust these values according to the actual page size if needed.
        float margin      = 20f;   // distance from page edges
        float fieldWidth  = 150f;  // width of the signature field
        float fieldHeight = 50f;   // height of the signature field
        float pageWidth   = 595f;  // typical A4 width in points (1 inch = 72 pt)

        // Calculate rectangle coordinates
        float llx = pageWidth - fieldWidth - margin; // lower‑left X
        float lly = margin;                         // lower‑left Y
        float urx = pageWidth - margin;             // upper‑right X
        float ury = margin + fieldHeight;           // upper‑right Y

        // Move the field named "Signature" on page 5
        bool success = formEditor.MoveField("Signature", llx, lly, urx, ury);
        if (!success)
        {
            Console.Error.WriteLine("Failed to move the 'Signature' field.");
        }

        // Save the modified PDF
        formEditor.Save();
        formEditor.Close();

        Console.WriteLine($"Signature field moved and saved to '{outputPath}'.");
    }
}