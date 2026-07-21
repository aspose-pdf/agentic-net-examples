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
        const string emailFieldName = "email"; // name of the email field in the PDF form

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the field by its name – the Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field.
            Field? field = form[emailFieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field \"{emailFieldName}\" not found or is not a form field in the document.");
                return;
            }

            // The field value is stored in the Value property (object)
            string fieldValue = field.Value?.ToString() ?? string.Empty;

            // Simple validation: ensure the value contains an '@' character
            if (!fieldValue.Contains("@"))
            {
                Console.Error.WriteLine($"Validation failed: the \"{emailFieldName}\" field must contain an '@' character.");
                return;
            }

            // Validation passed – save the document
            doc.Save(outputPath);
            Console.WriteLine($"Document saved successfully to '{outputPath}'.");
        }
    }
}
