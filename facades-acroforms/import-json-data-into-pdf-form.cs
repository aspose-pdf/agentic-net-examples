using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF with form fields
        const string outputPdfPath = "output.pdf";  // PDF after importing JSON data
        const string jsonFilePath  = "data.json";   // JSON file containing field values

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonFilePath))
        {
            Console.Error.WriteLine($"Error: JSON file not found – {jsonFilePath}");
            return;
        }

        try
        {
            // Initialize the Form facade and bind it to the source PDF
            using (Form form = new Form())
            {
                form.BindPdf(inputPdfPath);

                // Open the JSON stream and import the data into the PDF form fields
                using (FileStream jsonStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the modified PDF to the output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form data imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any unexpected errors (e.g., malformed JSON, I/O issues)
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}