using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form_v4.pdf";
        const string outputPath = "required_project.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Add a required text field named "ProjectName" on page 1.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            float llx = 100f; // lower‑left X
            float lly = 600f; // lower‑left Y
            float urx = 300f; // upper‑right X
            float ury = 620f; // upper‑right Y

            // Add the text field using the correct FormEditor API.
            editor.AddField(FieldType.Text, "ProjectName", 1, llx, lly, urx, ury);

            // Mark the field as required.
            editor.SetFieldAttribute("ProjectName", PropertyFlag.Required);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved required PDF to '{outputPath}'.");
    }
}
