using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_fields.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination file names
        // This binds the source PDF and prepares the output file.
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Add a text field on page 1 (coordinates are in points)
        // FieldType.Text, field name, page number, lower‑left x, lower‑left y, upper‑right x, upper‑right y
        formEditor.AddField(FieldType.Text, "AddedTextField", 1, 50f, 700f, 250f, 730f);

        // Add a checkbox field on page 1
        formEditor.AddField(FieldType.CheckBox, "AgreementCheck", 1, 300f, 700f, 320f, 720f);

        // Add a radio button field on page 2
        formEditor.AddField(FieldType.Radio, "ChoiceRadio", 2, 100f, 600f, 120f, 620f);
        // Optional: set the visual size of each radio button item
        formEditor.RadioButtonItemSize = 15f;

        // Persist the changes to the output PDF
        formEditor.Save();

        Console.WriteLine($"Form fields added and saved to '{outputPdf}'.");
    }
}