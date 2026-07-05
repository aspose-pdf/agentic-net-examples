using System;
using System.IO;
using System.Drawing;               // System.Drawing.Color is required for FormFieldFacade
using Aspose.Pdf.Facades;          // Facade classes for form editing

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the form
        const string outputPdf = "output.pdf";         // PDF to be saved after modification
        const string fieldName = "Status";             // name of the field to modify

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor works with an input and an output file path.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Create a new facade to define visual attributes.
            editor.Facade = new FormFieldFacade();

            // Set the background color to light green.
            editor.Facade.BackgroundColor = Color.LightGreen;

            // Apply the visual changes to the specific field.
            editor.DecorateField(fieldName);

            // Persist the changes.
            editor.Save();
        }

        Console.WriteLine($"Field \"{fieldName}\" background set to light green and saved to '{outputPdf}'.");
    }
}