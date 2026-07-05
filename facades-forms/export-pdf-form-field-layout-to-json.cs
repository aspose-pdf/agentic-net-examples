using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputJson = "fields.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPdf))
        {
            // Create a file stream for the JSON output
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                // Export all form fields (including their layout rectangles) to JSON.
                // 'indented: true' makes the JSON human‑readable.
                form.ExportJson(jsonStream, indented: true);
            }
        }

        Console.WriteLine($"Form field layout exported to JSON: {outputJson}");
    }
}