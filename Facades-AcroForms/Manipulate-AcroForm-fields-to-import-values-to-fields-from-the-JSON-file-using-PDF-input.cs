using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string jsonPath  = "data.json";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with input and output PDF paths
            using (Form form = new Form(inputPdf, outputPdf))
            {
                // Open the JSON file containing field values
                using (FileStream jsonStream = File.OpenRead(jsonPath))
                {
                    // Import all field data from the JSON stream
                    form.ImportJson(jsonStream);
                }

                // Persist the changes to the output PDF
                form.Save();
            }

            Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}