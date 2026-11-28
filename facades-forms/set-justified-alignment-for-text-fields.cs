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
        const string outputPath = "Justified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to FormEditor for modifications
        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Define the visual appearance that will be applied to each processed field
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.LightGray,
                BorderWidth = 1,
                // 0.95 * 255 ≈ 242 → a light‑gray background
                BackgroundColor = System.Drawing.Color.FromArgb(242, 242, 242)
            };

            // Use the Form facade to enumerate existing form fields
            using (var form = new Form())
            {
                form.BindPdf(inputPath);

                foreach (string fieldName in form.FieldNames)
                {
                    // Process only text fields (single‑line or multi‑line)
                    FieldType type = form.GetFieldType(fieldName);
                    if (type == FieldType.Text || type == FieldType.MultiLineText)
                    {
                        // Set justified alignment – the API expects an int value
                        editor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Justify);

                        // Apply the visual decoration defined in the facade
                        editor.DecorateField(fieldName);
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved justified PDF to '{outputPath}'.");
    }
}
