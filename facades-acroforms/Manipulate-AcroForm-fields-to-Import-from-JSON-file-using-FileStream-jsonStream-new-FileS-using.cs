using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPdfPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"Error: JSON file not found at '{jsonPath}'.");
            return;
        }

        // Create a Form facade, bind the PDF, import JSON data, and save the result
        using (Form form = new Form())
        {
            // Load the PDF document into the facade
            form.BindPdf(inputPdfPath);

            // Open the JSON file and import its field values into the PDF form
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            // Persist the changes to a new PDF file
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Successfully imported form fields from JSON and saved to '{outputPdfPath}'.");
    }
}