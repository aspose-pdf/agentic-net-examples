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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;
            bool allRequiredFieldsFilled = true;

            // Iterate over all form fields via the Form.Fields collection
            foreach (Field field in form.Fields)
            {
                // Check only fields marked as required
                if (field.Required)
                {
                    // Retrieve the field value; null or whitespace means not filled
                    string value = field.Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        allRequiredFieldsFilled = false;
                        Console.WriteLine($"Required field '{field.PartialName}' is empty.");
                    }
                }
            }

            if (!allRequiredFieldsFilled)
            {
                Console.WriteLine("Document contains unfilled required fields. Save aborted.");
                return; // Prevent saving the document
            }

            // Optional structural validation (repairs if needed)
            doc.Check(true);

            // Save the document after successful validation
            doc.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
    }
}
