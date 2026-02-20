using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string dataPath = "data.json";
        const string outputPath = "output.pdf";

        // Verify that the source PDF and data files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        try
        {
            // Create a Form facade and bind it to the existing PDF
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Import form field values from a JSON stream
                using (FileStream dataStream = File.OpenRead(dataPath))
                {
                    form.ImportJson(dataStream);
                }

                // Save the modified PDF
                form.Save(outputPath);
            }

            Console.WriteLine($"Form data imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}