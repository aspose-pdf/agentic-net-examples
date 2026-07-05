using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // System.Drawing.Color is required by FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Set visual attributes via FormFieldFacade
                editor.Facade = new FormFieldFacade();
                // FormFieldFacade expects System.Drawing.Color, not Aspose.Pdf.Color
                editor.Facade.BackgroundColor = System.Drawing.Color.LightGray; // uniform background
                editor.Facade.BorderColor     = System.Drawing.Color.Black;   // uniform border color
                editor.Facade.BorderStyle     = FormFieldFacade.BorderStyleSolid;
                editor.Facade.BorderWidth     = FormFieldFacade.BorderWidthThin;

                // Apply the appearance to all list-like fields
                editor.DecorateField(FieldType.ListBox);   // list box fields
                editor.DecorateField(FieldType.ComboBox); // combo box fields (also list‑like)

                // Save the modified PDF (lifecycle rule: use provided Save method)
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
    }
}
