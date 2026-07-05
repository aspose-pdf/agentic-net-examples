using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "validated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields present. Saving document.");
                    doc.Save(outputPath);
                    return;
                }

                // Verify that all required fields have a non‑empty value
                foreach (Field field in doc.Form)
                {
                    if (field.Required)
                    {
                        // Most field types expose their current value via the Value property.
                        // Treat null, empty string or whitespace as "not filled".
                        var value = field.Value?.ToString();
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            throw new InvalidOperationException(
                                $"Required field '{field.PartialName}' is not filled.");
                        }
                    }
                }

                // All required fields are filled – save the document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
