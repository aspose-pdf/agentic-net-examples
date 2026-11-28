using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "countries.txt";
        const string fieldName = "Country";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Find the form field with the given name (FullName or Name)
            var field = doc.Form?.Fields?.FirstOrDefault(f =>
                string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase));

            if (field == null)
            {
                Console.WriteLine($"Field '{fieldName}' not found in the PDF.");
                return;
            }

            // Retrieve the list of items depending on the concrete field type
            string[] items = null;
            if (field is ComboBoxField comboBox)
            {
                // Options is a collection of Option objects; extract the display name (Name)
                items = comboBox.Options?.Select(o => o.Name).ToArray();
            }
            else if (field is ListBoxField listBox)
            {
                // Same extraction for list box fields
                items = listBox.Options?.Select(o => o.Name).ToArray();
            }

            if (items == null || items.Length == 0)
            {
                Console.WriteLine($"No items found for field '{fieldName}'.");
                return;
            }

            // Write each item on a separate line
            File.WriteAllLines(outputPath, items);
            Console.WriteLine($"Extracted {items.Length} items to '{outputPath}'.");
        }
    }
}
