using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "Options.pdf";
        const string outputPath = "OptionsUpdated.pdf";
        const string fieldName = "Options";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is used for editing/adding fields. It does NOT expose GetField –
        // reading field values is done via Aspose.Pdf.Facades.Form. To add a new list item
        // we use FormEditor.AddListItem(name, value). Decoration is applied through the
        // Facade property followed by DecorateField(name).
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Prepare visual appearance for the field we are going to modify
            editor.Facade = new FormFieldFacade
            {
                BorderColor = Color.LightGray,
                // Convert the 0‑1 float RGB values to 0‑255 integer range
                BackgroundColor = Color.FromArgb(
                    (int)(0.9 * 255), // Red
                    (int)(0.9 * 255), // Green
                    (int)(0.95 * 255) // Blue
                )
            };

            // Add the new list item "Extra" to the existing list/combobox field
            editor.AddListItem(fieldName, "Extra");

            // Apply the visual decoration to the field
            editor.DecorateField(fieldName);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
