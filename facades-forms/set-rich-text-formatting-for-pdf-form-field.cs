using System;
using System.IO;
using System.Drawing; // needed for FormFieldFacade color properties
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Comments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Create a FormFieldFacade to define appearance settings
            formEditor.Facade = new FormFieldFacade
            {
                // Appearance settings – using System.Drawing.Color as required by FormFieldFacade
                BackgroundColor = System.Drawing.Color.LightGray,
                TextColor       = System.Drawing.Color.Black,
                BorderColor     = System.Drawing.Color.DarkGray,
                Alignment       = FormFieldFacade.AlignLeft
            };

            // Apply the appearance settings to the "Comments" field (rich‑text enabled)
            formEditor.DecorateField(fieldName);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich‑text field \"{fieldName}\" updated and saved to '{outputPath}'.");
    }
}
