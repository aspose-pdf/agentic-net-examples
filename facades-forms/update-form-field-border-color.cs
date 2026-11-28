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
        const string outputPath = "Updated.pdf";
        const string fieldName = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create a FormEditor and bind the source PDF.
            var editor = new FormEditor();
            editor.BindPdf(inputPath);

            // Configure the appearance that will be applied to fields.
            editor.Facade.BorderColor = System.Drawing.Color.Blue;                 // border colour
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;        // background colour
            // Optional: fine‑tune other visual aspects.
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin;
            editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid;

            // Apply the decoration to the specific field.
            editor.DecorateField(fieldName);

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Field \"{fieldName}\" updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
