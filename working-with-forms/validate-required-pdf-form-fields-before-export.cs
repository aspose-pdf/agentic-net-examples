using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputFdf = "form_data.fdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            bool allValid = true;

            // Iterate over all form fields
            foreach (Field field in doc.Form?.Fields ?? Array.Empty<Field>())
            {
                // Only check fields that are marked as required (use the Required property)
                if (field.Required)
                {
                    // Convert the field value to string for validation
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Treat unchecked checkboxes/radio buttons ("Off") as empty
                    if (string.IsNullOrWhiteSpace(value) ||
                        value.Equals("Off", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Error.WriteLine($"Required field '{field.FullName}' is empty.");
                        allValid = false;
                    }
                }
            }

            // Abort export if any required field is missing data
            if (!allValid)
            {
                Console.Error.WriteLine("Form validation failed – export aborted.");
                return;
            }

            // All required fields contain data – export the form data to FDF.
            // The overload without SaveFormat infers the format from the file extension.
            doc.Save(outputFdf);
            Console.WriteLine($"Form data exported to '{outputFdf}'.");
        }
    }
}
