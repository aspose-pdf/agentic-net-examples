using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the "Score" field
        const string outputPdf = "output.pdf";  // PDF after applying restrictions

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with two files: source and destination.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Limit the field to a maximum of 3 characters (enough for 0‑100).
            // This also ensures only digits can be entered because the field is a NumberField.
            formEditor.SetFieldLimit("Score", 3);

            // Add JavaScript validation to enforce the numeric range 0‑100.
            // The script runs when the field loses focus.
            string js = @"
                if (event.value < 0 || event.value > 100) {
                    app.alert('Score must be between 0 and 100');
                    event.rc = false; // reject the entered value
                }
            ";
            formEditor.AddFieldScript("Score", js);

            // Persist changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Field \"Score\" configured and saved to '{outputPdf}'.");
    }
}