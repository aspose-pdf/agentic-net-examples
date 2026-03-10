using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Output PDF file path
        const string outputPath = "output.pdf";

        // Submit button parameters
        const string fieldName = "submitBtn";          // Unique name for the button
        const string label = "Submit";                 // Caption displayed on the button
        const string url = "https://www.example.com"; // Destination URL
        const int pageNumber = 1;                      // 1‑based page index
        const float llx = 100f; // lower‑left X coordinate
        const float lly = 200f; // lower‑left Y coordinate
        const float urx = 200f; // upper‑right X coordinate
        const float ury = 250f; // upper‑right Y coordinate

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use FormEditor (a facade) to edit the PDF form
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF document
            formEditor.BindPdf(inputPath);

            // Add a submit button with the specified URL
            formEditor.AddSubmitBtn(fieldName, pageNumber, label, url, llx, lly, urx, ury);

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}