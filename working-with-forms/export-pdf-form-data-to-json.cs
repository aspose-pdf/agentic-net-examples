using System;
using System.IO;
using Aspose.Pdf;

class ExportFormDataToJson
{
    static void Main()
    {
        // Paths for the input PDF and the output text file that will contain the JSON string.
        const string pdfPath = "input.pdf";
        const string jsonTextPath = "formData.txt";

        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Export the form fields to JSON using a memory stream.
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // The ExportToJson method writes the JSON representation of the form fields to the provided stream.
                doc.Form.ExportToJson(jsonStream);

                // Convert the stream contents to a UTF‑8 string.
                string jsonString = System.Text.Encoding.UTF8.GetString(jsonStream.ToArray());

                // Write the JSON string to a plain text file.
                File.WriteAllText(jsonTextPath, jsonString);
            }
        }

        Console.WriteLine($"Form data exported to JSON and saved in '{jsonTextPath}'.");
    }
}