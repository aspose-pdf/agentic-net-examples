using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "validated_output.pdf";
        const string emailFieldName = "email"; // name of the email form field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the form field by name – the Form indexer returns a WidgetAnnotation,
                // so we need to cast it to Aspose.Pdf.Forms.Field (or a derived type).
                Field? field = doc.Form[emailFieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field \"{emailFieldName}\" not found or is not a text field.");
                    return;
                }

                // Retrieve the current value of the field
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Validate that the value contains an '@' character
                if (!fieldValue.Contains("@"))
                {
                    Console.Error.WriteLine("Email validation failed: value does not contain '@'. PDF will not be saved.");
                    return;
                }

                // Validation passed – save the document
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
