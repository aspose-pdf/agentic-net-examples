using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF
        const string outputPath = "output.pdf";  // result PDF
        const string fieldName  = "submitBtn";   // name of the new button
        const string label      = "Submit";      // button caption
        const string url        = "https://www.example.com/submit"; // target URL

        // Button rectangle (lower‑left and upper‑right coordinates)
        const float llx = 100f;
        const float lly = 200f;
        const float urx = 200f;
        const float ury = 250f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a facade that implements IDisposable.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPath);

            // Add a submit button on page 1.
            formEditor.AddSubmitBtn(fieldName, 1, label, url, llx, lly, urx, ury);

            // Persist the changes.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}