using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor loads the source PDF and prepares the destination file.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Add a submit button named "SubmitForm" on page 1.
            // Button label: "Submit"
            // Submission URL: https://api.example.com/submit
            // Position: lower‑left (100, 500), upper‑right (200, 550)
            formEditor.AddSubmitBtn("SubmitForm", 1, "Submit", "https://api.example.com/submit", 100, 500, 200, 550);

            // Save the modified PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}