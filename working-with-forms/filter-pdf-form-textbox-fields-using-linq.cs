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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve all form fields
            Field[] allFields = doc.Form.Fields;

            // LINQ query: select only TextBoxField instances
            var textBoxFields = allFields.OfType<TextBoxField>();

            // Example: output the full names of the text box fields
            foreach (var tb in textBoxFields)
            {
                Console.WriteLine($"TextBox field: {tb.FullName}");
            }

            // Additional example: filter text boxes whose name starts with "Customer"
            var filtered = allFields
                .Where(f => f is TextBoxField && f.Name.StartsWith("Customer"))
                .Cast<TextBoxField>();

            foreach (var tb in filtered)
            {
                Console.WriteLine($"Filtered TextBox: {tb.Name}");
            }
        }
    }
}