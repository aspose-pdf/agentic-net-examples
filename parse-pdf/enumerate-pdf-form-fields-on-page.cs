using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // 1‑based index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle pattern)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Retrieve the page (1‑based indexing)
            Page page = doc.Pages[pageNumber];

            // Enumerate all form fields on this page in tab order
            foreach (Field field in page.FieldsInTabOrder)
            {
                // Field name (the name assigned to the annotation)
                string name = field.Name ?? "(unnamed)";

                // Field type (e.g., TextBoxField, CheckBoxField, etc.)
                string type = field.GetType().Name;

                // Field value (may be null for empty fields)
                string value = field.Value?.ToString() ?? "(null)";

                Console.WriteLine($"Field Name: {name}, Type: {type}, Value: {value}");
            }
        }
    }
}