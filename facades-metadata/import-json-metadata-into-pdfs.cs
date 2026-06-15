using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string jsonPath = "metadata.json";
        const string outputFolder = "UpdatedPdfs";

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string jsonContent = File.ReadAllText(jsonPath);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        JsonElement root = doc.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
        {
            Console.Error.WriteLine("Invalid JSON format: expected an array of metadata objects.");
            return;
        }

        foreach (JsonElement item in root.EnumerateArray())
        {
            if (!item.TryGetProperty("FilePath", out JsonElement filePathElem) ||
                filePathElem.ValueKind != JsonValueKind.String)
            {
                Console.Error.WriteLine("Skipping entry without a valid \"FilePath\" property.");
                continue;
            }

            string sourcePdfPath = filePathElem.GetString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(sourcePdfPath) || !File.Exists(sourcePdfPath))
            {
                Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
                continue;
            }

            string destPdfPath = Path.Combine(outputFolder, Path.GetFileName(sourcePdfPath));

            // PdfFileInfo implements IDisposable – use a using block
            using (PdfFileInfo pdfInfo = new PdfFileInfo(sourcePdfPath))
            {
                // Standard metadata
                if (item.TryGetProperty("Author", out JsonElement authorElem) && authorElem.ValueKind == JsonValueKind.String)
                    pdfInfo.Author = authorElem.GetString();

                if (item.TryGetProperty("Title", out JsonElement titleElem) && titleElem.ValueKind == JsonValueKind.String)
                    pdfInfo.Title = titleElem.GetString();

                if (item.TryGetProperty("Subject", out JsonElement subjectElem) && subjectElem.ValueKind == JsonValueKind.String)
                    pdfInfo.Subject = subjectElem.GetString();

                if (item.TryGetProperty("Keywords", out JsonElement keywordsElem) && keywordsElem.ValueKind == JsonValueKind.String)
                    pdfInfo.Keywords = keywordsElem.GetString();

                if (item.TryGetProperty("Creator", out JsonElement creatorElem) && creatorElem.ValueKind == JsonValueKind.String)
                    pdfInfo.Creator = creatorElem.GetString();

                // Dates – PdfFileInfo expects a PDF‑date formatted string
                if (item.TryGetProperty("CreationDate", out JsonElement creationDateElem) &&
                    creationDateElem.ValueKind == JsonValueKind.String &&
                    DateTime.TryParse(creationDateElem.GetString(), out DateTime creationDate))
                {
                    pdfInfo.CreationDate = creationDate.ToString("yyyyMMddHHmmss");
                }

                if (item.TryGetProperty("ModDate", out JsonElement modDateElem) &&
                    modDateElem.ValueKind == JsonValueKind.String &&
                    DateTime.TryParse(modDateElem.GetString(), out DateTime modDate))
                {
                    pdfInfo.ModDate = modDate.ToString("yyyyMMddHHmmss");
                }

                // Custom metadata (properties not handled above)
                foreach (JsonProperty prop in item.EnumerateObject())
                {
                    string name = prop.Name;
                    if (name == "FilePath" || name == "Author" || name == "Title" ||
                        name == "Subject" || name == "Keywords" || name == "Creator" ||
                        name == "CreationDate" || name == "ModDate")
                    {
                        continue;
                    }

                    if (prop.Value.ValueKind == JsonValueKind.String)
                        pdfInfo.SetMetaInfo(name, prop.Value.GetString());
                    else
                        pdfInfo.SetMetaInfo(name, prop.Value.GetRawText());
                }

                // Save the updated PDF to the destination path
                pdfInfo.SaveNewInfo(destPdfPath);
            }

            Console.WriteLine($"Updated PDF saved to: {destPdfPath}");
        }
    }
}
