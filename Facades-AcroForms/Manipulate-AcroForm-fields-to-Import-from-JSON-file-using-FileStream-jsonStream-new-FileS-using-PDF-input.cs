using System;
using System.IO;
using Aspose.Pdf.Facades;   // Form facade for AcroForm operations

class Program
{
    static void Main()
    {
        const string pdfInputPath  = "input.pdf";   // source PDF with AcroForm
        const string jsonInputPath = "data.json";   // JSON file containing field values
        const string pdfOutputPath = "output.pdf";  // result PDF after import

        // Verify that input files exist
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfInputPath}");
            return;
        }
        if (!File.Exists(jsonInputPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonInputPath}");
            return;
        }

        try
        {
            // Open the PDF document via the Form facade
            using (Form form = new Form(pdfInputPath))
            {
                // Open the JSON stream containing field data
                using (FileStream jsonStream = new FileStream(jsonInputPath, FileMode.Open, FileAccess.Read))
                {
                    // Import all field values from the JSON stream
                    form.ImportJson(jsonStream);
                }

                // Save the modified PDF to a new file
                form.Save(pdfOutputPath);
            }

            Console.WriteLine($"Form fields imported from JSON and saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}