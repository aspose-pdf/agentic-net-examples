using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade for editing AcroForm fields.
        // It implements IDisposable, so we wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF into the editor.
            editor.BindPdf(inputPath);

            // Add a new text field named "SampleField" on page 1.
            // Parameters: FieldType, field name, page number, lower‑left X, lower‑left Y,
            // upper‑right X, upper‑right Y (all in points).
            editor.AddField(FieldType.Text, "SampleField", 1, 100f, 500f, 300f, 520f);

            // Persist the changes to a new PDF file.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}