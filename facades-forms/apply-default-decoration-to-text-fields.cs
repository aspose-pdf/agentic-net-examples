using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Use FormEditor to work with FormFieldFacade
                using (FormEditor editor = new FormEditor())
                {
                    // Bind the loaded document to the editor
                    editor.BindPdf(doc);

                    // Configure default decoration via the facade (fully‑qualified enum and color types)
                    editor.Facade.Font = Aspose.Pdf.Facades.FontStyle.TimesItalic;          // Font style
                    editor.Facade.FontSize = 12F;                                         // Font size (float)
                    editor.Facade.TextColor = System.Drawing.Color.Black;                // System.Drawing.Color
                    editor.Facade.BorderColor = System.Drawing.Color.Blue;                // System.Drawing.Color
                    editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin;          // float constant (≈1 pt)
                    editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;         // int constant
                    editor.Facade.Alignment = FormFieldFacade.AlignLeft;                  // int constant
                    // Optional background colour
                    // editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;

                    // Apply the decoration to every text field in the document
                    editor.DecorateField(FieldType.Text);

                    // Save the decorated PDF
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Decorated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
