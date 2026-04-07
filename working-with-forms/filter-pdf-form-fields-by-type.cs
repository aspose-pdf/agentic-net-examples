using System;
using System.IO;
using System.Linq;
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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form fields from the document
            var allFields = doc.Form;

            // LINQ query to get only TextBoxField instances
            var textBoxFields = allFields.OfType<TextBoxField>().ToList();

            // Output the full names of the text box fields
            foreach (var field in textBoxFields)
            {
                Console.WriteLine($"TextBox field: {field.FullName}");
            }

            // Example: LINQ query to get only ListBoxField instances
            var listBoxFields = allFields.OfType<ListBoxField>().ToList();

            // Output the full names of the list box fields
            foreach (var field in listBoxFields)
            {
                Console.WriteLine($"ListBox field: {field.FullName}");
            }

            // Additional LINQ filters can be applied similarly for other field types,
            // e.g., ComboBoxField, CheckBoxField, etc.
        }
    }
}