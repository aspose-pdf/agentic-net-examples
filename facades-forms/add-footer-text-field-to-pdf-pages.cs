using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document to obtain the number of pages.
        Document doc = new Document(inputPath);
        int pageCount = doc.Pages.Count;

        // Create a FormEditor and bind the existing PDF.
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Add a footer text field to each page.
        for (int page = 1; page <= pageCount; page++)
        {
            // Rectangle coordinates for the footer field (left, bottom, right, top).
            // AddField expects **float** values, not double.
            float llx = 50f;   // lower‑left X
            float lly = 20f;   // lower‑left Y
            float urx = 545f;  // upper‑right X
            float ury = 45f;   // upper‑right Y

            string fieldName = $"Footer_{page}";
            string defaultValue = string.Empty; // empty default value

            // Add a single‑line text field.
            editor.AddField(FieldType.Text, fieldName, defaultValue, page, llx, lly, urx, ury);

            // Set the field's text alignment to justified.
            editor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Justify);
        }

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Footer PDF saved to '{outputPath}'.");
    }
}
