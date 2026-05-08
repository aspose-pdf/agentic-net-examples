using System;
using System.IO;
using Aspose.Pdf;

class ExportFormDataToJson
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string jsonTextPath = "formdata.json";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export form fields to JSON using a memory stream
            using (MemoryStream jsonStream = new MemoryStream())
            {
                // ExportToJson writes JSON to the provided stream
                pdfDoc.Form.ExportToJson(jsonStream);

                // Convert the stream contents to a string
                jsonStream.Position = 0; // rewind
                string jsonString = new StreamReader(jsonStream).ReadToEnd();

                // Save the JSON string to a text file
                File.WriteAllText(jsonTextPath, jsonString);
            }
        }

        Console.WriteLine($"Form data exported to JSON file: {jsonTextPath}");
    }
}