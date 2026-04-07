using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "FieldAppearances";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Use the field's partial name to create a unique file name
                string fieldName = field.PartialName ?? "UnnamedField";
                string jsonPath  = Path.Combine(outputDir, $"{fieldName}_appearance.json");

                // Export the field (including its appearance streams) to a JSON file
                // The ExportToJson method writes the field definition and appearance data
                using (FileStream fs = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    field.ExportToJson(fs);
                }

                Console.WriteLine($"Exported appearance of field '{fieldName}' to '{jsonPath}'.");
            }
        }

        Console.WriteLine("All field appearances have been exported.");
    }
}