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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            formEditor.AddSubmitBtn(
                "SubmitForm",   // field name
                1,               // page number (1‑based)
                "Submit",       // button label
                "https://api.example.com/submit", // target URL
                10f, 200f, 70f, 270f); // llx, lly, urx, ury
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}
