using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_form_v5.pdf";
        const string outputPath = "required_date.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works directly with PDF files; no Document object is needed.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPath);

            // Add a text field named "EffectiveDate" on page 1.
            // Coordinates are given in points (1/72 inch). Use float literals as required by the API.
            float llx = 100f; // lower‑left X
            float lly = 500f; // lower‑left Y
            float urx = 200f; // upper‑right X
            float ury = 520f; // upper‑right Y

            // Add a plain text field (FieldType.Text).
            formEditor.AddField(FieldType.Text, "EffectiveDate", 1, llx, lly, urx, ury);

            // Mark the field as required using the PropertyFlag enum.
            formEditor.SetFieldAttribute("EffectiveDate", PropertyFlag.Required);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}
