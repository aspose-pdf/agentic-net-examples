using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor can be constructed with source and destination file names.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a new text field named "NewTextField" on page 1.
            // Rectangle coordinates: lower‑left (100, 500), upper‑right (300, 530).
            bool fieldAdded = formEditor.AddField(FieldType.Text, "NewTextField", 1, 100, 500, 300, 530);
            if (!fieldAdded)
            {
                Console.Error.WriteLine("Failed to add the text field.");
                return;
            }

            // Mark the field as required.
            formEditor.SetFieldAttribute("NewTextField", PropertyFlag.Required);

            // Set visual appearance for the field using FormFieldFacade.
            formEditor.Facade = new FormFieldFacade();
            formEditor.Facade.Alignment = FormFieldFacade.AlignCenter; // center the text

            // Apply the visual attributes to the specific field.
            formEditor.DecorateField("NewTextField");

            // Save the modified PDF to the output path.
            formEditor.Save();
        }

        Console.WriteLine($"Form editing completed. Output saved to '{outputPdf}'.");
    }
}