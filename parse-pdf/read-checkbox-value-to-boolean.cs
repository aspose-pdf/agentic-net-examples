using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fieldName = "myCheckBox";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the checkbox field by name
            CheckboxField checkBox = doc.Form[fieldName] as CheckboxField;
            if (checkBox == null)
            {
                Console.Error.WriteLine($"Checkbox field '{fieldName}' not found.");
                return;
            }

            // The Value property is a string (e.g., "On" when checked, "Off" otherwise)
            string rawValue = checkBox.Value;

            // Convert to Boolean: true if value equals "On" (case‑insensitive)
            bool isChecked = string.Equals(rawValue, "On", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine($"Checkbox '{fieldName}' is checked: {isChecked}");
        }
    }
}