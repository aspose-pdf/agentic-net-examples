using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Access the form collection
            Form form = doc.Form;

            // Retrieve the first field and cast it to CheckboxField
            // (adjust the index if the checkbox is not the first field)
            CheckboxField checkbox = form.Fields[0] as CheckboxField;
            if (checkbox == null)
            {
                Console.Error.WriteLine("No checkbox field found at index 0.");
                return;
            }

            // Read the Value property (string) of the checkbox
            string rawValue = checkbox.Value; // e.g., "Off", "On", or a custom export value

            // Convert the string value to a Boolean.
            // In Aspose.Pdf a checkbox is considered checked if its value is NOT "Off".
            bool isChecked = !string.Equals(rawValue, "Off", StringComparison.OrdinalIgnoreCase);

            // Output the result
            Console.WriteLine($"Checkbox Value: \"{rawValue}\"");
            Console.WriteLine($"Converted to Boolean: {isChecked}");
        }
    }
}