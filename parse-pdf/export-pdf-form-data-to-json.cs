using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonOutputPath = "formData.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Export all form fields to a JSON file using ExportToJson (stream based)
            using (FileStream jsonStream = new FileStream(jsonOutputPath, FileMode.Create, FileAccess.Write))
            {
                doc.Form.ExportToJson(jsonStream);
            }

            Console.WriteLine($"Form data exported to '{jsonOutputPath}'.");
        }
    }
}