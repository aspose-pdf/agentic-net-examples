using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName = "myTextField";
        const string fieldValue = "Hello, World!";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Check if the specified field exists
                if (doc.Form.HasField(fieldName))
                {
                    // Retrieve the field via the Form indexer and cast to Aspose.Pdf.Forms.Field
                    Field? field = doc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        field.Value = fieldValue;
                    }
                    else
                    {
                        // This should not happen because HasField returned true, but guard against unexpected types
                        Console.Error.WriteLine($"Field '{fieldName}' exists but is not a supported form field type.");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
