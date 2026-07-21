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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve a checkbox field (adjust index or use field name as needed)
            CheckboxField checkbox = form.Fields[0] as CheckboxField;
            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found in the document.");
                return;
            }

            // Read the string value of the checkbox (e.g., "On", "Off", or a custom export value)
            string rawValue = checkbox.Value;
            Console.WriteLine($"Checkbox raw Value: {rawValue}");

            // Convert the string value to a Boolean.
            // Convention: any value other than "Off" (case‑insensitive) means the box is checked.
            bool isChecked = !string.Equals(rawValue, "Off", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Checkbox is checked: {isChecked}");
        }
    }
}