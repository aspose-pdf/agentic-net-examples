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
        const int pageNumber = 1; // 1‑based page index to inspect

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPath))
        {
            var form = doc.Form;
            if (form == null || form.Fields == null || !form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            Console.WriteLine($"Form fields on page {pageNumber}:");

            // Iterate over all fields and keep only those that belong to the requested page
            foreach (Field field in form.Fields)
            {
                // Field.PageIndex is zero‑based; convert to 1‑based for comparison with pageNumber
                if (field.PageIndex + 1 != pageNumber)
                    continue;

                // Use the logical name of the field; fall back to a placeholder if missing
                string name = field.Name ?? "(unnamed)";

                // Concrete class name gives a clear indication of the field type (e.g., TextBoxField)
                string type = field.GetType().Name;

                // Most fields expose their current content through the Value property
                string value = field.Value?.ToString() ?? "(no value)";

                Console.WriteLine($"  Name: {name}");
                Console.WriteLine($"  Type: {type}");
                Console.WriteLine($"  Value: {value}");
                Console.WriteLine();
            }
        }
    }
}
