using System;
using System.IO;
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

        // Load the PDF with FormEditor (Facades API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Set a reasonable maximum length (e.g., 6 characters for values like 9999.99)
            // FormEditor uses SetFieldLimit to define the maximum number of characters.
            editor.SetFieldLimit("Discount", 6);

            // JavaScript validation: numeric value with up to two decimal places.
            // FormEditor applies a script to a field via SetFieldScript.
            string jsValidate = @"if (event.value.match(/^\\d+(\\.\\d{1,2})?$/) == null) {\n    app.alert('Please enter a numeric value with up to two decimal places.');\n    event.rc = false;\n}";
            editor.SetFieldScript("Discount", jsValidate);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with numeric validation on 'Discount' field: {outputPath}");
    }
}
