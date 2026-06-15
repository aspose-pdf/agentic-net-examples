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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count the form fields present on the current page
                int fieldCount = page.FieldsInTabOrder?.Count ?? 0;
                Console.WriteLine($"Page {i}: {fieldCount} form field(s) found.");

                // Log each field's name and value (if any)
                foreach (Field field in page.FieldsInTabOrder)
                {
                    string name  = field.FullName;
                    string value = field.Value?.ToString() ?? string.Empty;
                    Console.WriteLine($"  Field '{name}' = '{value}'");
                }
            }

            // Optional: flatten the document to make fields non‑interactive
            doc.Flatten();

            // Save the processed document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}