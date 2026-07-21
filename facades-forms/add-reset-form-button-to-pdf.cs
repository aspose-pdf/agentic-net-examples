using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor facade on the loaded document
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(doc);

                // Add a push button named "ResetForm" on page 1
                // Coordinates: lower‑left (50, 750), upper‑right (150, 800)
                formEditor.AddField(FieldType.PushButton, "ResetForm", 1, 50, 750, 150, 800);

                // Attach JavaScript to the button that clears all form fields
                formEditor.AddFieldScript("ResetForm", "this.resetForm();");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}