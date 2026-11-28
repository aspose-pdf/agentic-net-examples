using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "FormA.pdf";
        const string outputPath = "Question1Options.txt";
        const string fieldName = "Question1";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the form field by name from the document's form fields collection
            var field = doc.Form?.Fields?.FirstOrDefault(f =>
                string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                string.Equals(f.FullName, fieldName, StringComparison.Ordinal));

            string[] options = Array.Empty<string>();

            if (field is ComboBoxField comboBox)
            {
                // ComboBoxField.Options holds the list of selectable values (Option objects)
                options = comboBox.Options?
                    .Cast<Option>()
                    .Select(o => o.Name) // use the display text; use o.Value if export value is needed
                    .ToArray() ?? Array.Empty<string>();
            }
            else if (field is ListBoxField listBox)
            {
                // ListBoxField.Options holds the list of selectable values (Option objects)
                options = listBox.Options?
                    .Cast<Option>()
                    .Select(o => o.Name)
                    .ToArray() ?? Array.Empty<string>();
            }

            if (options.Length == 0)
            {
                Console.WriteLine($"No options found for field '{fieldName}'.");
            }
            else
            {
                // Write each option on a separate line
                File.WriteAllLines(outputPath, options);
                Console.WriteLine($"Options written to '{outputPath}'.");
            }
        }
    }
}
