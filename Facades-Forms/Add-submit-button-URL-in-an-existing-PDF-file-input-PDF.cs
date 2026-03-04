using System;
using System.IO;
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

        // Use FormEditor facade to edit the form
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF
            formEditor.BindPdf(inputPath);

            // Add a submit button:
            // fieldName: "submitBtn"
            // page: 1 (first page, 1‑based indexing)
            // label: "Submit"
            // url: target URL
            // llx, lly, urx, ury: button rectangle coordinates
            formEditor.AddSubmitBtn(
                "submitBtn",
                1,
                "Submit",
                "https://www.example.com",
                100f, 500f, 200f, 550f);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Submit button added and saved to '{outputPath}'.");
    }
}