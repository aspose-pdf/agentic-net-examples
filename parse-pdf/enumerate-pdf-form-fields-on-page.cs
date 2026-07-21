using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // page to inspect (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Document must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate page number (Aspose.Pdf uses 1‑based indexing)
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document contains {doc.Pages.Count} pages.");
                return;
            }

            Page page = doc.Pages[pageNumber];

            // Retrieve all form fields on the specified page in tab order
            var fields = page.FieldsInTabOrder;

            Console.WriteLine($"Form fields on page {pageNumber}:");
            foreach (Field field in fields)
            {
                // Field name
                string name = field.Name;

                // Concrete field type (e.g., TextBoxField, CheckBoxField, etc.)
                string type = field.GetType().Name;

                // Current value; handle null safely
                string value = field.Value?.ToString() ?? "null";

                Console.WriteLine($"- Name: {name}, Type: {type}, Value: {value}");
            }
        }
    }
}