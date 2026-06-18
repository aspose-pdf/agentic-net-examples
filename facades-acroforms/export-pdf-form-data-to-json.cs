using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formData.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // Export all form fields to a JSON file (indented for readability)
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
            {
                form.ExportJson(jsonStream, indented: true);
            }
        }

        // Verify that the exported JSON is well‑formed
        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            using JsonDocument doc = JsonDocument.Parse(jsonContent);
            Console.WriteLine("Exported JSON is valid. Root element:");
            Console.WriteLine(doc.RootElement.GetRawText());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"JSON verification failed: {ex.Message}");
        }
    }
}