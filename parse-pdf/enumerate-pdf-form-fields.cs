using System;
using System.IO;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                Console.WriteLine($"Page {pageIndex}:");

                // Enumerate form fields on the current page in tab order
                foreach (Field field in page.FieldsInTabOrder)
                {
                    // Field name (fallback to FullName or PartialName if Name is null)
                    string fieldName = field.Name ?? field.FullName ?? field.PartialName ?? "(unnamed)";

                    // Runtime type of the field (e.g., TextBoxField, CheckBoxField, etc.)
                    string fieldType = field.GetType().Name;

                    // Field value (convert to string safely)
                    string fieldValue = field.Value?.ToString() ?? "(null)";

                    Console.WriteLine($"  Name: {fieldName}, Type: {fieldType}, Value: {fieldValue}");
                }
            }
        }
    }
}