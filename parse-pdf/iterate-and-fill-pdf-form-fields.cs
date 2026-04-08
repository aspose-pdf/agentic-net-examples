using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field in the document
            // The Form object exposes a Fields collection of type Field[]
            foreach (Field field in doc.Form.Fields)
            {
                // Use FullName if available, otherwise fall back to Name
                string fieldName = field.FullName ?? field.Name ?? "<unnamed>";
                string fieldValue = field.Value?.ToString() ?? "<null>";

                // Example processing: output field name and current value
                Console.WriteLine($"Field: {fieldName}, Value: {fieldValue}");

                // Example: set a default value if the field is empty
                if (field.Value == null || string.IsNullOrEmpty(fieldValue))
                {
                    field.Value = "Default";
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
