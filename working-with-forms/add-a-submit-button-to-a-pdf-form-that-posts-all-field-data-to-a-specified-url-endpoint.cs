using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_form.pdf";
        const string buttonName = "SubmitAll";
        const string buttonLabel = "Submit";
        const string submitUrl = "https://example.com/submit";

        // Button rectangle coordinates (lower‑left and upper‑right)
        const float llx = 100f;
        const float lly = 100f;
        const float urx = 200f;
        const float ury = 130f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        // FormEditor works with an input PDF and writes to the specified output PDF.
        using (FormEditor editor = new FormEditor(inputPath, outputPath))
        {
            // Add a submit button on page 1.
            editor.AddSubmitBtn(buttonName, 1, buttonLabel, submitUrl, llx, lly, urx, ury);
            // Persist changes.
            editor.Save();
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}