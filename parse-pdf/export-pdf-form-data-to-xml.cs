using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            var form = pdfDoc.Form;

            // Export the form fields to JSON directly into a MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                // ExportToJson writes the JSON representation of the form fields into the stream
                form.ExportToJson(ms);

                // Reset the stream position to the beginning for reading
                ms.Position = 0;

                // Example: read and display the exported JSON
                using (StreamReader reader = new StreamReader(ms))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine("Form fields exported as JSON:");
                    Console.WriteLine(json);
                }
            }
        }
    }
}