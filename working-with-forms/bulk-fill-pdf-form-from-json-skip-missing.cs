using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonPath}");
            return;
        }

        // Open the JSON stream once and reuse it for each field import
        using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Reset stream position before each import attempt
                jsonStream.Position = 0;

                // ImportValueFromJson returns true if the field was found in the JSON,
                // false otherwise. We use the return value to decide whether to skip.
                bool imported = field.ImportValueFromJson(jsonStream);

                if (!imported)
                {
                    // Gracefully skip missing fields without throwing an exception
                    Console.WriteLine($"Field '{field.FullName}' not present in JSON – skipped.");
                }
            }

            // Save the filled PDF to the output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}