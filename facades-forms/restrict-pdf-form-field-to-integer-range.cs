using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Limit the field to a maximum of 3 characters (enough for values 0‑100)
            formEditor.SetFieldLimit("Score", 3);

            // Add JavaScript to enforce integer range 0‑100
            string js = @"
                var val = parseInt(event.value, 10);
                if (isNaN(val) || val < 0 || val > 100) {
                    app.alert('Score must be an integer between 0 and 100.');
                    event.rc = false;
                }";
            formEditor.AddFieldScript("Score", js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field 'Score' configured and saved to '{outputPath}'.");
    }
}