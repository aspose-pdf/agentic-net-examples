using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, JSON data and the output PDF
        const string pdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Create a Form facade and bind it to the source PDF
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Open the JSON file as a stream and import the form data
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the modified PDF to the specified output path
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the import process
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}