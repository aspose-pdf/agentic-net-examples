using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "formdata.json";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Export form fields to JSON using a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Form.ExportToJson(ms); // writes JSON to the stream

                // Reset stream position to read the content
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();

                    // Save the JSON string to a text file
                    File.WriteAllText(jsonPath, json);
                }
            }
        }

        Console.WriteLine($"Form data exported to '{jsonPath}'.");
    }
}