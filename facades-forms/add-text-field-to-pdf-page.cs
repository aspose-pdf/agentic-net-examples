using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // FormEditor, FieldType
using Aspose.Pdf.Forms;    // Not needed for this example but kept for completeness

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath  = "input.pdf";
        // Output PDF file with the new text field
        const string outputPath = "output_with_customername.pdf";

        // Rectangle coordinates for the new text field (lower‑left x/y, upper‑right x/y)
        // Adjust these values as needed.
        const float llx = 100f; // lower‑left X
        const float lly = 500f; // lower‑left Y
        const float urx = 300f; // upper‑right X
        const float ury = 520f; // upper‑right Y

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Add a text field named "CustomerName" on page 1
            bool added = formEditor.AddField(FieldType.Text, "CustomerName", 1, llx, lly, urx, ury);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the text field.");
                return;
            }

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Text field \"CustomerName\" added and saved to '{outputPath}'.");
    }
}