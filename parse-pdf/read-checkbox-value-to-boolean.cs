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
            // Access the form fields collection
            Form form = doc.Form;

            // Assume the first field is a checkbox; cast safely
            CheckboxField checkbox = form.Fields[0] as CheckboxField;
            if (checkbox == null)
            {
                Console.WriteLine("The first field is not a checkbox.");
                return;
            }

            // Read the string value of the checkbox
            string rawValue = checkbox.Value; // e.g., "Off" or an allowed state name

            // Convert the string value to a Boolean.
            // In Aspose.Pdf a checkbox is considered checked when its value is NOT "Off".
            bool isChecked = !string.Equals(rawValue, "Off", StringComparison.OrdinalIgnoreCase);

            // Output the result
            Console.WriteLine($"Checkbox value string: \"{rawValue}\"");
            Console.WriteLine($"Converted to Boolean: {isChecked}");
        }
    }
}