using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "DateStyled.pdf";
        const string fieldName = "Date"; // name of the date field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor works with the Form facade – it is the correct way to style fields.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                // Appearance settings are applied to the Facade, not to the field instance.
                editor.Facade.BorderWidth = 2f;                     // 2‑point border width
                editor.Facade.BorderColor = Color.Blue;            // border colour (System.Drawing.Color)
                editor.Facade.BackgroundColor = Color.LightGray;   // background colour

                // Apply the facade settings to the specific field.
                editor.DecorateField(fieldName);

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Styled PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
