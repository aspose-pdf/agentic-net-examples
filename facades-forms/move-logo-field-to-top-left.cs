using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF containing the "Logo" field
        const string outputPdf = "output.pdf"; // PDF after moving the field
        const float fieldWidth = 150f;          // desired width of the field
        const float fieldHeight = 50f;          // desired height of the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the document to obtain page dimensions.
        Document doc = new Document(inputPdf);
        // PageInfo.Height returns double – cast to float for later calculations.
        float pageHeight = (float)doc.Pages[1].PageInfo.Height;

        // Calculate rectangle coordinates (lower‑left origin).
        float llx = 0f;                                 // left (X) coordinate
        float lly = pageHeight - fieldHeight;           // lower‑left Y coordinate (top of page minus height)
        float urx = llx + fieldWidth;                   // upper‑right X coordinate
        float ury = lly + fieldHeight;                  // upper‑right Y coordinate

        // Use the non‑obsolete FormEditor constructor that works with a Document instance.
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // Move the field named "Logo" to the calculated rectangle on page 1.
            bool moved = formEditor.MoveField("Logo", llx, lly, urx, ury);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the field 'Logo'.");
                return;
            }

            // Save the modified PDF to the desired output path.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'Logo' moved and saved to '{outputPdf}'.");
    }
}
