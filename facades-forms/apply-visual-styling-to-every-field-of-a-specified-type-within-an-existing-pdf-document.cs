using System;
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes for form editing
using Aspose.Pdf;                 // FieldType enum

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the styled output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "styled_output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use a using block to ensure the FormEditor is disposed correctly
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Create a FormFieldFacade instance to define visual attributes
            // (Only non‑color attributes are set to respect the global rule about System.Drawing.Color)
            formEditor.Facade = new FormFieldFacade();

            // Example visual styling: set alignment for the fields
            // AlignRight, AlignCenter, AlignLeft are valid enum values
            formEditor.Facade.Alignment = FormFieldFacade.AlignRight;

            // Apply the visual styling to all fields of the specified type.
            // Change FieldType.Text to any other type as needed (e.g., FieldType.CheckBox, FieldType.Radio, etc.).
            formEditor.DecorateField(FieldType.Text);

            // Save the modified PDF. The Save method writes to the output path provided in the constructor.
            formEditor.Save();
        }

        Console.WriteLine($"Styled PDF saved to '{outputPdf}'.");
    }
}