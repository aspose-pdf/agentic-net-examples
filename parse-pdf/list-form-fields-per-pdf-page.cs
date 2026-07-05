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

        // Load the PDF document (Document implements IDisposable)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based indexed
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                // Retrieve the page
                Page page = doc.Pages[pageIndex];

                // Get all form fields on this page in tab order
                var fields = page.FieldsInTabOrder;

                if (fields.Count == 0)
                    continue; // No fields on this page

                Console.WriteLine($"Page {pageIndex} contains {fields.Count} form field(s):");

                // Iterate through each field and display its name and type
                foreach (Field field in fields)
                {
                    // FullName gives the hierarchical name of the field
                    Console.WriteLine($"  Name: {field.FullName}, Type: {field.GetType().Name}");
                }
            }
        }
    }
}