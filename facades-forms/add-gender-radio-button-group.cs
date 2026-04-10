using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // destination PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor handles loading, editing, and saving the PDF.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Define the radio button options.
            formEditor.Items = new[] { "Male", "Female", "Other" };

            // Optional: arrange radios horizontally (default is true).
            formEditor.RadioHoriz = true;

            // Optional: set the visual size of each radio button.
            formEditor.RadioButtonItemSize = 15;

            // Add the radio button group named "Gender" with "Male" pre‑selected.
            // Coordinates are in points; adjust as needed.
            int    pageNumber = 1;
            float  llx = 100f, lly = 700f, urx = 200f, ury = 720f;
            formEditor.AddField(FieldType.Radio, "Gender", "Male", pageNumber, llx, lly, urx, ury);

            // Persist the changes.
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}