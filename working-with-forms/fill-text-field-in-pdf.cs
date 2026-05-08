using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, target field name and the value to set
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName  = "myTextField";
        const string fieldValue = "Hello World";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                // Check if the form contains the specified field
                if (doc.Form.HasField(fieldName))
                {
                    // Retrieve the field as a generic Field object and set its value
                    Field field = doc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        field.Value = fieldValue;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Field '{fieldName}' exists but is not a supported form field type.");
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in the PDF.");
                }

                // Save the modified document (lifecycle rule: save inside using block)
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
