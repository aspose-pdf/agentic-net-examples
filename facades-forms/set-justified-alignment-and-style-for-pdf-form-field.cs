using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "RemarksJustified.pdf";
        const string fieldName = "Remarks";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – use a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Set the text alignment of the field to justified.
            // FormEditor.SetFieldAlignment expects an int; cast from HorizontalAlignment.
            editor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Justify);

            // Decorate the field: thin gray border and light‑yellow background.
            // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.LightGray,
                BorderWidth = 0.5f,
                BackgroundColor = System.Drawing.Color.FromArgb(255, 255, 230) // light yellow
            };
            // Apply the facade to the existing field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved justified form field to '{outputPath}'.");
    }
}
