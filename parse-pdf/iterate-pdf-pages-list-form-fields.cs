using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
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
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Retrieve all form fields on the current page in tab order
                var fieldsOnPage = page.FieldsInTabOrder;

                foreach (Field field in fieldsOnPage)
                {
                    // Output field information; FullName provides the hierarchical name
                    Console.WriteLine($"Page {pageIndex}: Field \"{field.FullName}\"");
                }
            }

            // No modifications are made; simply save the document (optional)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing completed. Document saved as '{outputPath}'.");
    }
}