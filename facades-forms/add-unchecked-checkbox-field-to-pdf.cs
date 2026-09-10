using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a checkbox field named "AgreeTerms" on page 2
            // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
            FormEditor formEditor = new FormEditor(doc);
            formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2, 100, 500, 120, 520);

            // Explicitly set the checkbox to unchecked (default is unchecked)
            // Access the field through Document.Form collection.
            if (doc.Form["AgreeTerms"] is CheckboxField checkBox)
            {
                // The CheckboxField class provides a Checked property (bool).
                checkBox.Checked = false; // ensures the box is unchecked
            }
            else
            {
                Console.Error.WriteLine("Checkbox field 'AgreeTerms' was not found after creation.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
    }
}
