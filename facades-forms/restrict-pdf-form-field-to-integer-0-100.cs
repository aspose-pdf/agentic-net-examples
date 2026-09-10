using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API for form editing
using Aspose.Pdf;          // Core PDF types (required for Document if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF that already contains a field named "Score"
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with two file names: source and destination.
        // It automatically loads the source PDF and prepares the destination for saving.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Limit the field to a maximum of 3 characters (enough for values 0‑100).
            // This also implicitly restricts the field to digits only because the default
            // AllowedChars for a text field is "0123456789".
            formEditor.SetFieldLimit("Score", 3);

            // Add a JavaScript validation script that enforces the numeric range 0‑100.
            // The script runs when the field loses focus (on blur) and rejects values
            // outside the allowed range.
            string js = @"
                if (event.value !== '' && (event.value < 0 || event.value > 100)) {
                    app.alert('Score must be an integer between 0 and 100.');
                    event.rc = false; // reject the entered value
                }
            ";
            formEditor.SetFieldScript("Score", js);

            // Persist the changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field 'Score' configured and saved to '{outputPdf}'.");
    }
}