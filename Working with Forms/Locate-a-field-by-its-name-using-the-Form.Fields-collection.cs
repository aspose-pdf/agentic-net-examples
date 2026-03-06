using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form associated with the document
            Form form = doc.Form;

            // Locate the field by its name using the Fields collection
            Field targetField = null;
            foreach (Field f in form.Fields)
            {
                // Compare both PartialName and FullName (case‑insensitive)
                if (string.Equals(f.PartialName, fieldName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    targetField = f;
                    break;
                }
            }

            if (targetField != null)
            {
                Console.WriteLine($"Field found: PartialName='{targetField.PartialName}', FullName='{targetField.FullName}'");
                // Example: read the current value of the field
                Console.WriteLine($"Current value: {targetField.Value}");
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found.");
            }
        }
    }
}