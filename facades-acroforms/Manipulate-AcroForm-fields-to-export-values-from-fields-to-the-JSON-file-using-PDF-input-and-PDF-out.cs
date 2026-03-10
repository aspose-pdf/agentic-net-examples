using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string jsonPath      = "fields.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF.
            using (Form form = new Form(inputPdfPath))
            {
                // Export all AcroForm field values to a JSON file.
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    // 'true' produces indented (readable) JSON.
                    form.ExportJson(jsonStream, true);
                }

                // Save the (unchanged) PDF to the desired output location.
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Exported form fields to '{jsonPath}'.");
            Console.WriteLine($"Saved PDF to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}