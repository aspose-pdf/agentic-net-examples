using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the PDF contains AcroForm fields
            bool allRequiredFieldsFilled = true;

            // Iterate over all form fields
            foreach (Field field in doc.Form.Fields)
            {
                // Only validate fields that are marked as required
                if (field.Required)
                {
                    // Field.Value may be null; treat null/empty as not filled
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.Error.WriteLine($"Required field '{field.FullName}' is empty.");
                        allRequiredFieldsFilled = false;
                    }
                }
            }

            if (!allRequiredFieldsFilled)
            {
                Console.Error.WriteLine("PDF not saved because one or more required fields are missing.");
                return;
            }

            // All required fields are present; save the PDF
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
    }
}