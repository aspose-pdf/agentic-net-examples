using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // source PDF with form fields
        const string csvPath = "output.csv"; // destination CSV file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Export form fields to JSON using an in‑memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                doc.Form.ExportToJson(jsonStream);
                jsonStream.Position = 0; // rewind for reading

                // Parse the JSON document
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonStream))
                {
                    JsonElement root = jsonDoc.RootElement; // expected to be an array

                    // Write CSV manually (Document.Save would always produce PDF)
                    using (StreamWriter writer = new StreamWriter(csvPath, false, Encoding.UTF8))
                    {
                        // CSV header
                        writer.WriteLine("Name,Value,PartialName,FullName,ExportValue");

                        foreach (JsonElement field in root.EnumerateArray())
                        {
                            // Extract known properties; use empty string if missing
                            string name        = field.GetProperty("Name").GetString() ?? "";
                            string value       = field.GetProperty("Value").GetString() ?? "";
                            string partialName = field.TryGetProperty("PartialName", out JsonElement pn) ? pn.GetString() ?? "" : "";
                            string fullName    = field.TryGetProperty("FullName", out JsonElement fn) ? fn.GetString() ?? "" : "";
                            string exportValue = field.TryGetProperty("ExportValue", out JsonElement ev) ? ev.GetString() ?? "" : "";

                            // Escape fields that contain commas, quotes or newlines
                            name        = EscapeCsv(name);
                            value       = EscapeCsv(value);
                            partialName = EscapeCsv(partialName);
                            fullName    = EscapeCsv(fullName);
                            exportValue = EscapeCsv(exportValue);

                            writer.WriteLine($"{name},{value},{partialName},{fullName},{exportValue}");
                        }
                    }
                }
            }
        }

        Console.WriteLine($"CSV saved to '{csvPath}'.");
    }

    // Helper to escape CSV fields according to RFC 4180
    static string EscapeCsv(string text)
    {
        if (text.Contains('"') || text.Contains(',') || text.Contains('\n') || text.Contains('\r'))
        {
            text = text.Replace("\"", "\"\"");
            return $"\"{text}\"";
        }
        return text;
    }
}