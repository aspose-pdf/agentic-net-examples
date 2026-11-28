using System;
using System.IO;
using System.Drawing; // System.Drawing.Color, System.Drawing.FontStyle
using Aspose.Pdf.Facades; // FormEditor, FormFieldFacade, FontStyle

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "HeaderLarge.pdf";
        const string fieldName = "Header"; // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF with FormEditor.
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);

                // Configure appearance via the Facade (not the field itself).
                editor.Facade.FontSize = 20F; // increase font size
                editor.Facade.Font = Aspose.Pdf.Facades.FontStyle.TimesBold; // make text bold
                editor.Facade.TextColor = System.Drawing.Color.Black;
                editor.Facade.BorderColor = System.Drawing.Color.Black;
                editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant
                editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant
                editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;

                // Apply the configured facade to the specific field.
                editor.DecorateField(fieldName);

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
