using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "JsonTarget.pdf";
        const string jsonInputPath = "FieldsInput.json";
        const string outputPdfPath = "JsonUpdated.pdf";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(jsonInputPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonInputPath}");
            return;
        }

        try
        {
            // Form implements IDisposable – wrap in using for deterministic disposal
            using (Form form = new Form())
            {
                // Bind the existing PDF document
                form.BindPdf(sourcePdfPath);

                // Import field values from the JSON file (ImportJson expects a Stream)
                using (FileStream jsonStream = File.OpenRead(jsonInputPath))
                {
                    form.ImportJson(jsonStream);
                }

                // Save the updated PDF
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
