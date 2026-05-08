using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Get all form fields on the current page in tab order
                var fields = page.FieldsInTabOrder;

                Console.WriteLine($"Page {pageIndex} contains {fields.Count} form field(s).");

                foreach (Field field in fields)
                {
                    // Example: display field name and its concrete type
                    Console.WriteLine($"  Field Name: {field.FullName}, Type: {field.GetType().Name}");
                }
            }

            // Save the (unchanged) document – required lifecycle step
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}