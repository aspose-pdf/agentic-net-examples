using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_new_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // Page indexing in Aspose.Pdf is 1‑based, so the new page number is the current count.
            Page newPage = doc.Pages.Add();
            int newPageNumber = doc.Pages.Count; // 1‑based index of the newly added page.

            // Initialize FormEditor with the loaded document.
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a text field.
                // Parameters: field type, field name, page number, lower‑left X, lower‑left Y,
                // upper‑right X, upper‑right Y (all in points).
                formEditor.AddField(FieldType.Text, "NewTextField", newPageNumber, 100, 500, 300, 520);

                // Add a checkbox field.
                formEditor.AddField(FieldType.CheckBox, "NewCheckBox", newPageNumber, 100, 540, 120, 560);

                // Add a radio button group with two options.
                formEditor.AddField(FieldType.Radio, "NewRadioGroup", newPageNumber, 100, 580, 120, 600);
                // The RadioButtonItemSize property controls the size of each radio button.
                formEditor.RadioButtonItemSize = 20; // optional: set size in points
                // Add individual radio options (the same field name, different positions).
                formEditor.AddField(FieldType.Radio, "NewRadioGroup", newPageNumber, 150, 580, 170, 600);
                formEditor.AddField(FieldType.Radio, "NewRadioGroup", newPageNumber, 200, 580, 220, 600);

                // Add a combo box (drop‑down) field.
                formEditor.AddField(FieldType.ComboBox, "NewComboBox", newPageNumber, 100, 620, 250, 640);
                // Populate the combo box items.
                formEditor.Items = new string[] { "Option 1", "Option 2", "Option 3" };
                formEditor.AddField(FieldType.ComboBox, "NewComboBox", newPageNumber, 100, 620, 250, 640);

                // Save the modified PDF to the output path.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with new page and form fields saved to '{outputPath}'.");
    }
}