using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFolder = "pdfs";
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            using (Document document = new Document(pdfPath))
            {
                string jsonPath = Path.ChangeExtension(pdfPath, ".json");
                using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    // Export form field metadata to JSON. Options are optional; null uses defaults.
                    document.Form.ExportToJson(jsonStream, null);
                }
                Console.WriteLine($"Metadata exported: {jsonPath}");
            }
        }
    }
}