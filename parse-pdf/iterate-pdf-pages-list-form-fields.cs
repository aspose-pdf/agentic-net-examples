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
            // Iterate through each page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get all form fields on the current page in tab order
                var fields = page.FieldsInTabOrder;

                foreach (Field field in fields)
                {
                    // Output basic information about each field
                    Console.WriteLine($"Page {i}: Field Name = {field.FullName}, Type = {field.GetType().Name}");
                }
            }

            // Save the (unchanged) document – required by lifecycle rule
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Document saved as '{outputPath}'.");
    }
}