using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // PDF with AcroForm fields
        const string jsonDataPath  = "data.json";   // JSON containing field values
        const string outputPdfPath = "output.pdf";  // Resulting PDF

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonDataPath}");
            return;
        }

        try
        {
            // Initialize the Form facade on the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Open the JSON file as a stream and import its values into the form fields
                using (FileStream jsonStream = File.OpenRead(jsonDataPath))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the updated PDF to a new file
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields imported successfully. Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}