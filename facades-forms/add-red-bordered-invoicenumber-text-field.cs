using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output097.pdf";
        const string fieldName = "InvoiceNumber";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create FormEditor and bind the existing PDF.
            FormEditor editor = new FormEditor();
            editor.BindPdf(inputPath);

            // Configure the appearance that will be applied to fields added *after* this assignment.
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.Red,
                BorderWidth = 1,
                BorderStyle = FormFieldFacade.BorderStyleSolid // solid line
            };

            // Define the rectangle for the text field (lower‑left and upper‑right coordinates).
            float llx = 100f;
            float lly = 120f;
            float urx = 200f; // width ~100 points (enough for 12 chars)
            float ury = 140f; // height 20 points

            // Add a text field on page 1.
            editor.AddField(FieldType.Text, fieldName, 1, llx, lly, urx, ury);

            // Apply the previously defined facade to the newly added field.
            editor.DecorateField(fieldName);

            // Set the maximum length to 12 characters.
            editor.SetFieldLimit(fieldName, 12);

            // Save the modified PDF.
            editor.Save(outputPath);

            Console.WriteLine($"PDF saved with text field '{fieldName}' to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
