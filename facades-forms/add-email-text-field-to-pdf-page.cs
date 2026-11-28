using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output002.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPath);

            // Define the rectangle for the text field (lower‑left and upper‑right corners).
            // AddField expects float values, not double.
            float llx = 150f; // lower‑left X
            float lly = 250f; // lower‑left Y
            float urx = 350f; // upper‑right X (150 + 200)
            float ury = 300f; // upper‑right Y (250 + 50)

            // Add a text field named "EmailAddress" on page 2.
            // Use FormEditor.AddField with FieldType.Text.
            formEditor.AddField(FieldType.Text, "EmailAddress", 2, llx, lly, urx, ury);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}
