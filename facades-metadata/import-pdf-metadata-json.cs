using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "metadata.json";
        const string outputPath = "output.pdf";

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

        // Load JSON content
        string jsonContent = File.ReadAllText(jsonPath);
        using (JsonDocument jsonDoc = JsonDocument.Parse(jsonContent))
        {
            JsonElement root = jsonDoc.RootElement;

            // Bind the PDF file
            using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
            {
                if (root.TryGetProperty("Title", out JsonElement title))
                    pdfInfo.Title = title.GetString();
                if (root.TryGetProperty("Author", out JsonElement author))
                    pdfInfo.Author = author.GetString();
                if (root.TryGetProperty("Subject", out JsonElement subject))
                    pdfInfo.Subject = subject.GetString();
                if (root.TryGetProperty("Keywords", out JsonElement keywords))
                    pdfInfo.Keywords = keywords.GetString();
                if (root.TryGetProperty("Creator", out JsonElement creator))
                    pdfInfo.Creator = creator.GetString();
                // Producer is read‑only in PdfFileInfo; it is set automatically by Aspose.Pdf.
                // if (root.TryGetProperty("Producer", out JsonElement producer))
                //     pdfInfo.Producer = producer.GetString(); // <-- removed

                if (root.TryGetProperty("CreationDate", out JsonElement creationDate))
                {
                    if (DateTime.TryParse(creationDate.GetString(), out DateTime dt))
                        // PdfFileInfo expects a string in PDF date format (e.g., "D:yyyyMMddHHmmss")
                        pdfInfo.CreationDate = dt.ToString("yyyyMMddHHmmss");
                }
                if (root.TryGetProperty("ModDate", out JsonElement modDate))
                {
                    if (DateTime.TryParse(modDate.GetString(), out DateTime dt))
                        pdfInfo.ModDate = dt.ToString("yyyyMMddHHmmss");
                }

                // Save the PDF with updated metadata
                pdfInfo.SaveNewInfo(outputPath);
            }
        }

        Console.WriteLine($"Metadata applied and saved to '{outputPath}'.");
    }
}
