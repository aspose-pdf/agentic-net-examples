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
            // Export all form fields (including their layout rectangles) to JSON
            using (FileStream jsonStream = new FileStream(outputJson, FileMode.Create, FileAccess.Write))
            {
                // 'true' makes the JSON output indented for readability
                form.ExportJson(jsonStream, true);
            }
        }

        Console.WriteLine($"Form field layout exported to '{outputJson}'.");
    }
}