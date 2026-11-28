using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output004.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        try
        {
            // FormEditor does NOT implement IDisposable, so we instantiate it directly.
            FormEditor editor = new FormEditor();

            // Bind the existing PDF document.
            editor.BindPdf(sourcePath);

            // Define the rectangle for the text field (lower‑left and upper‑right corners).
            int pageNumber = 1;
            // The AddField method expects float values, not double.
            float llx = 120f;   // lower‑left X
            float lly = 180f;   // lower‑left Y
            float urx = 220f;   // upper‑right X
            float ury = 200f;   // upper‑right Y

            // Add a text field named "PhoneNumber" using the correct FormEditor API.
            editor.AddField(FieldType.Text, "PhoneNumber", pageNumber, llx, lly, urx, ury);

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();

            Console.WriteLine($"Form field added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
