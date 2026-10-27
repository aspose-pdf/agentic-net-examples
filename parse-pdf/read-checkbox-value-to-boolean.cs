using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "myCheckBox";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox field from the form collection and cast it
            CheckboxField checkBox = doc.Form[fieldName] as CheckboxField;

            if (checkBox == null)
            {
                Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
                return;
            }

            // The Value property is a string (e.g., "On" when checked, "Off" otherwise)
            string rawValue = checkBox.Value;

            // Convert the string value to a Boolean
            bool isChecked = string.Equals(rawValue, "On", StringComparison.OrdinalIgnoreCase);

            // Example usage of the Boolean variable
            Console.WriteLine($"Checkbox '{fieldName}' is checked: {isChecked}");
        }
    }
}