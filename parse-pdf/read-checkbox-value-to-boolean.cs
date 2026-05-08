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

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Assume the first field is a checkbox; cast safely
            if (form.Fields[0] is CheckboxField checkbox)
            {
                // Read the Value property (string) of the checkbox
                string rawValue = checkbox.Value; // e.g., "On", "Off", or a custom state

                // Convert the string value to a Boolean
                // Convention: any value other than "Off" (case‑insensitive) is considered checked
                bool isChecked = !string.Equals(rawValue, "Off", StringComparison.OrdinalIgnoreCase);

                Console.WriteLine($"Checkbox raw Value: \"{rawValue}\"");
                Console.WriteLine($"Converted to Boolean: {isChecked}");
            }
            else
            {
                Console.WriteLine("The first form field is not a checkbox.");
            }
        }
    }
}