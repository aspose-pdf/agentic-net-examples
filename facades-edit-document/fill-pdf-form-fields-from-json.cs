using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF, JSON data and the output PDF
        const string inputPdf = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPdf = "filled.pdf";

        // Validate input files
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

        // Read the JSON content that maps full field names to their values
        string jsonContent = File.ReadAllText(jsonPath, Encoding.UTF8);

        // Convert the JSON string into a stream for ImportJson
        using (MemoryStream jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent)))
        {
            // Initialize the Form facade with the source PDF (lifecycle handled by using)
            using (Form form = new Form(inputPdf))
            {
                // Import all field values from the JSON stream; field names must match the PDF's full names
                form.ImportJson(jsonStream);

                // Save the updated PDF with filled fields
                form.Save(outputPdf);
            }
        }

        Console.WriteLine($"Form fields have been filled and saved to '{outputPdf}'.");
    }
}