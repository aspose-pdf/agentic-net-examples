using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the form field
        const string inputPdf = "input.pdf";
        // Output PDF where the field will be repositioned
        const string outputPdf = "output_moved_field.pdf";
        // Fully qualified name of the field to move
        const string fieldName = "textField";

        // New rectangle coordinates for the field (lower‑left and upper‑right corners)
        float llx = 20.5f; // lower‑left X
        float lly = 20.3f; // lower‑left Y
        float urx = 120.6f; // upper‑right X
        float ury = 40.8f; // upper‑right Y

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Move the specified field to the new location
            bool success = formEditor.MoveField(fieldName, llx, lly, urx, ury);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
                return;
            }

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Field '{fieldName}' successfully moved and saved to '{outputPdf}'.");
    }
}