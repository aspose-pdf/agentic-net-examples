using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // Required for System.Drawing.Color used by FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "Status";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor handles both loading and saving; use the constructor that takes input and output paths.
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Set visual attributes via the Facade.
            editor.Facade = new FormFieldFacade();
            editor.Facade.BackgroundColor = Color.LightGreen; // Light green background

            // Apply the appearance to the specific field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save();
        }

        Console.WriteLine($"Field \"{fieldName}\" background set to light green. Saved to '{outputPdf}'.");
    }
}