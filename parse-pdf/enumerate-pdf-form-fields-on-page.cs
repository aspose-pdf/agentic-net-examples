using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // page to inspect (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate page number
            if (pageNumber < 1 || pageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page
            Page page = doc.Pages[pageNumber];

            // Retrieve all form fields on this page in tab order
            var fields = page.FieldsInTabOrder;

            Console.WriteLine($"Form fields on page {pageNumber}:");

            foreach (Field field in fields)
            {
                // Field name
                string name = field.Name ?? "(unnamed)";

                // Field type (class name)
                string type = field.GetType().Name;

                // Field value (may be null)
                string value = field.Value?.ToString() ?? "(null)";

                Console.WriteLine($"- Name: {name}, Type: {type}, Value: {value}");
            }
        }
    }
}