using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the interactive form
            Form form = doc.Form;

            // Retrieve the first field and cast it to CheckboxField
            CheckboxField checkbox = form.Fields[0] as CheckboxField;
            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found at index 0.");
                return;
            }

            // Read the Value property (string representation, e.g., "On" or "Off")
            string rawValue = checkbox.Value;

            // Convert the string value to a Boolean.
            // In Aspose.Pdf a checked box typically has a value other than "Off".
            bool isChecked = !string.Equals(rawValue, "Off", StringComparison.OrdinalIgnoreCase);

            // Output the results
            Console.WriteLine($"Checkbox Value (string): '{rawValue}'");
            Console.WriteLine($"Checkbox Checked (bool): {isChecked}");
        }
    }
}