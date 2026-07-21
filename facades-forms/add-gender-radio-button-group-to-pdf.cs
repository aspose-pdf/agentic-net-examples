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

        // Initialize FormEditor with source PDF and destination PDF
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Define the radio button options
            formEditor.Items = new[] { "Male", "Female", "Other" };

            // Optional: arrange horizontally and set gap between buttons
            formEditor.RadioHoriz = true;   // true = horizontal (default)
            formEditor.RadioGap   = 10;     // gap in pixels

            // Add the radio button group named "Gender"
            // Default selected option is "Male"
            // Page 1, lower‑left (100,500), upper‑right (200,530) defines the area for the first button;
            // subsequent buttons are placed automatically based on RadioGap and RadioHoriz.
            formEditor.AddField(FieldType.Radio, "Gender", "Male", 1, 100, 500, 200, 530);

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}