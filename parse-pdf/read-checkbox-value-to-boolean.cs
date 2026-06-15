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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve the first checkbox field (adjust index as needed)
            CheckboxField checkbox = form.Fields[0] as CheckboxField;
            if (checkbox == null)
            {
                Console.WriteLine("No checkbox field found in the document.");
                return;
            }

            // Read the string value of the checkbox
            string value = checkbox.Value;

            // Convert to a Boolean: true if the value is not "Off"
            bool isChecked = !string.Equals(value, "Off", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Checkbox Value = \"{value}\"");
            Console.WriteLine($"Converted Boolean = {isChecked}");
        }
    }
}