using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor (Facades) to modify the form field
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // JavaScript that validates the field "Score" to be an integer between 0 and 100
            string validationJs = @"
if (event.value) {
    var val = parseInt(event.value);
    if (isNaN(val) || val < 0 || val > 100) {
        app.alert('Score must be an integer between 0 and 100.');
        event.rc = false; // reject the entered value
    } else {
        event.value = val.toString(); // ensure stored as integer string
    }
}";

            // Attach the validation script to the field's Validate action using SetFieldScript
            formEditor.SetFieldScript("Score", validationJs);

            // Limit the field length to three characters (max 100) using SetFieldLimit
            formEditor.SetFieldLimit("Score", 3);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated 'Score' field saved to '{outputPath}'.");
    }
}
