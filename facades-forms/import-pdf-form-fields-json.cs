using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string jsonPath = "fields.json";
        const string outputPath = "output.pdf";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        using (Document doc = new Document())
        {
            // Add a blank page to host the form fields (optional)
            doc.Pages.Add();

            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                // Import form field definitions from the JSON file
                doc.Form.ImportFromJson(jsonStream);
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }
}