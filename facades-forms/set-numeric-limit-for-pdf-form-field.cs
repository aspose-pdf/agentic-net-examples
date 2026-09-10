using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Quantity";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF form for editing
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Set maximum character count to 3 (enough for values up to 100)
            formEditor.SetFieldLimit(fieldName, 3);

            // Optional: add JavaScript validation to enforce numeric range 1‑100
            string validationScript = "if (event.value < 1 || event.value > 100) { app.alert('Enter a value between 1 and 100'); event.rc = false; }";
            formEditor.SetFieldScript(fieldName, validationScript);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPath}'.");
    }
}