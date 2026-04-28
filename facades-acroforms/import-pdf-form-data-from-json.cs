using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string jsonFilePath  = "data.json";
        const string outputPdfPath = "output.pdf";

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonFilePath}");
            return;
        }

        try
        {
            // Bind the PDF document to the Form facade
            using (Form form = new Form(inputPdfPath))
            {
                // Import form values from JSON.
                // Fields that do not exist in the PDF are ignored automatically.
                using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the updated PDF to a new file
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}