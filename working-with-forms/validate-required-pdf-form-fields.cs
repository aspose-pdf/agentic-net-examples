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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Validate that all required form fields have a value
            bool allRequiredFilled = true;

            // Iterate over the form fields collection (Aspose.Pdf.Forms)
            foreach (Field field in doc.Form.Fields)
            {
                // Check only fields marked as required
                if (field.Required)
                {
                    // The Value property holds the field's content; it may be null
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        allRequiredFilled = false;
                        Console.WriteLine($"Required field \"{field.PartialName}\" is empty.");
                    }
                }
            }

            if (!allRequiredFilled)
            {
                Console.WriteLine("Document contains empty required fields. Save operation aborted.");
                return; // Prevent saving
            }

            // All required fields are filled – proceed to save
            doc.Save(outputPath);
            Console.WriteLine($"Document saved successfully to '{outputPath}'.");
        }
    }
}
