using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use FormEditor facade to modify the existing PDF
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Parameters for the required text field
            string fieldName   = "RequiredTextField";
            int    pageNumber  = 1;               // 1‑based page index
            float  llx         = 100f;            // lower‑left X (points)
            float  lly         = 700f;            // lower‑left Y (points)
            float  urx         = 300f;            // upper‑right X (points)
            float  ury         = 720f;            // upper‑right Y (points)
            string defaultText = string.Empty;    // initial value

            // Add a text field (FieldType.Text) with an optional initial value
            editor.AddField(FieldType.Text, fieldName, defaultText, pageNumber, llx, lly, urx, ury);

            // Mark the field as required using the PropertyFlag enum
            editor.SetFieldAttribute(fieldName, PropertyFlag.Required);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}
