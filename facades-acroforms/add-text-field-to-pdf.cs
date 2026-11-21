using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Text field properties
        const string fieldName = "MyTextField";
        const string fieldValue = "Default text";

        // Coordinates for the field (left, bottom, right, top)
        const double llx = 100; // lower‑left X
        const double lly = 500; // lower‑left Y
        const double urx = 300; // upper‑right X
        const double ury = 520; // upper‑right Y

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor to add a text field.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Add a single‑line text field (FieldType.Text) on page 1.
            // Signature: AddField(FieldType, string fieldName, string value, int pageNumber,
            //                    float llx, float lly, float urx, float ury)
            editor.AddField(
                FieldType.Text,
                fieldName,
                fieldValue,
                1, // page number (1‑based)
                (float)llx,
                (float)lly,
                (float)urx,
                (float)ury);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}
