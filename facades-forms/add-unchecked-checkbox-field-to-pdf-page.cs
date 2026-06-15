using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Define the rectangle for the checkbox (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                // Adjust these coordinates to position the checkbox as required
                float llx = 100f; // left
                float lly = 500f; // bottom
                float urx = 120f; // right
                float ury = 520f; // top

                // Add an unchecked checkbox named "AgreeTerms" on page 2
                formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2, llx, lly, urx, ury);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added to page 2 and saved as '{outputPath}'.");
    }
}