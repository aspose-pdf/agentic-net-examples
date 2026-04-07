using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // Source PDF with form fields
        const string txtPath = "formdata.txt";   // Destination text file for JSON

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Export form fields to a memory stream as JSON
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Form.ExportToJson(ms);               // Writes JSON to the stream
                ms.Position = 0;                         // Reset stream position for reading

                // Read the JSON content as a string
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();

                    // Save the JSON string to a plain text file
                    File.WriteAllText(txtPath, json);
                }
            }
        }

        Console.WriteLine($"Form data exported to '{txtPath}'.");
    }
}