using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with FormEditor (facade API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Add a push‑button field named "ResetBtn" on page 1.
            // Coordinates: lower‑left (50, 750), upper‑right (150, 800)
            editor.AddField(FieldType.PushButton, "ResetBtn", 1, 50f, 750f, 150f, 800f);

            // Attach JavaScript that resets the form to its default values.
            string resetScript = "this.resetForm();";
            editor.SetFieldScript("ResetBtn", resetScript);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}