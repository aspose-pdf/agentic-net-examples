using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the opened document
            FormEditor formEditor = new FormEditor(doc);

            // Define the rectangle for the checkbox (lower‑left and upper‑right coordinates)
            // Adjust these values as needed for the desired position and size
            float llx = 100f; // lower‑left X
            float lly = 500f; // lower‑left Y
            float urx = 120f; // upper‑right X
            float ury = 520f; // upper‑right Y

            // Add a checkbox field named "AgreeTerms" on page 2 (pages are 1‑based)
            bool added = formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2, llx, lly, urx, ury);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the checkbox field.");
                return;
            }

            // Ensure the checkbox is unchecked by default.
            // Access the field via the document's Form indexer (doc.Form["fieldName"]).
            var field = doc.Form["AgreeTerms"] as CheckboxField;
            if (field != null)
            {
                field.Checked = false; // explicit default unchecked state
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
    }
}
