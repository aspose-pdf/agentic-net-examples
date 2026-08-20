using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the requested page exists (pages are 1‑based)
            if (doc.Pages.Count < pageNumber)
            {
                Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                return;
            }

            // Get the specific page
            Page page = doc.Pages[pageNumber];

            // Retrieve all form fields on this page in tab order
            IList<Field> fields = page.FieldsInTabOrder;

            Console.WriteLine($"Page {pageNumber} contains {fields.Count} form field(s).");

            // Enumerate each field and log its name, type, and current value
            foreach (Field field in fields)
            {
                string name = field.Name ?? "(unnamed)";
                string type = field.GetType().Name;
                string value = field.Value?.ToString() ?? "(null)";

                Console.WriteLine($"Name: {name}, Type: {type}, Value: {value}");
            }
        }
    }
}