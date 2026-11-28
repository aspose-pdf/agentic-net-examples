using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "NewField.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to a FormEditor instance
        using (var formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Coordinates for the new text field (example values)
            // llx,lly = lower‑left corner; urx,ury = upper‑right corner
            // Aspose.Pdf.Facades.FormEditor.AddField expects *float* values, not double.
            float llx = 100f; // X‑position of left edge
            float lly = 150f; // Y‑position of bottom edge (below existing fields)
            float urx = 300f; // X‑position of right edge
            float ury = 170f; // Y‑position of top edge

            // Add a new empty text field named "NewField" on page 1
            formEditor.AddField(FieldType.Text, "NewField", 1, llx, lly, urx, ury);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with new text field saved to '{outputPath}'.");
    }
}
