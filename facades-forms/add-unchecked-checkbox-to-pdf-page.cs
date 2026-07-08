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
        const string outputPath = "output_with_checkbox.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a checkbox field named "AgreeTerms" on page 2
                // Coordinates: lower‑left (100, 500), upper‑right (120, 520)
                // Adjust these values as needed for your layout
                bool added = formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2,
                                                100f, 500f, 120f, 520f);

                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the checkbox field.");
                    return;
                }
            }

            // Ensure the checkbox is unchecked (default is unchecked, but set explicitly)
            if (doc.Form["AgreeTerms"] is CheckboxField checkbox)
            {
                checkbox.Checked = false;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with checkbox field at '{outputPath}'.");
    }
}