using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Count form fields on the current page
                int fieldCount = page.FieldsInTabOrder?.Count ?? 0;

                // Log the count for monitoring purposes
                Console.WriteLine($"Page {i}: {fieldCount} form field(s) found.");

                // Example: extract field values (optional)
                // foreach (var field in page.FieldsInTabOrder)
                // {
                //     Console.WriteLine($"  Field Name: {field.FullName}, Value: {field.Value}");
                // }
            }

            // (Optional) Perform additional processing here, e.g., flatten fields
            // doc.Pages.Flatten();

            // Save the (potentially modified) document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Output saved to '{outputPath}'.");
    }
}