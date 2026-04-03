using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // Required for Field class

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
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure all required form fields have a non‑empty value
                if (!AllRequiredFieldsFilled(doc))
                {
                    Console.Error.WriteLine("Validation failed: one or more required fields are empty.");
                    return; // Prevent saving
                }

                // Optional structural validation (repairs if needed)
                doc.Check(true);

                // Save the validated document
                doc.Save(outputPath);
                Console.WriteLine($"Document saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Returns true if every required field contains a value
    static bool AllRequiredFieldsFilled(Document doc)
    {
        // If the document has no form, consider it valid
        if (doc?.Form == null) return true;

        // Iterate over the actual form fields (Field objects), not WidgetAnnotation
        foreach (Field field in doc.Form.Fields)
        {
            if (field.Required)
            {
                // Most field types store their content in the Value property
                string value = field.Value?.ToString();

                if (string.IsNullOrWhiteSpace(value))
                {
                    // Identify the problematic field by its partial name
                    Console.WriteLine($"Required field '{field.PartialName}' is empty.");
                    return false;
                }
            }
        }
        return true;
    }
}
