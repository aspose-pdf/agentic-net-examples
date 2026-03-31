using System;
using System.IO;
using Aspose.Pdf;

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

        using (Document document = new Document())
        {
            // Add a blank page (A4 size) to host the imported fields
            Aspose.Pdf.Page page = document.Pages.Add();
            page.PageInfo.Width = 595;   // points (A4 width)
            page.PageInfo.Height = 842;  // points (A4 height)

            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                // Import form fields (including their layout) from the JSON definition
                document.Form.ImportFromJson(jsonStream);
            }

            // Save the resulting PDF with the imported fields
            document.Save(outputPath);
        }

        Console.WriteLine($"PDF with imported fields saved to '{outputPath}'.");
    }
}
