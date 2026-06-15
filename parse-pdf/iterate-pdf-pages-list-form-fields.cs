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

                // Retrieve the list of form fields on the current page in tab order
                var fieldsOnPage = page.FieldsInTabOrder;

                // If there are no fields, continue to next page
                if (fieldsOnPage == null || fieldsOnPage.Count == 0)
                    continue;

                // Output information about each field found on this page
                foreach (Field field in fieldsOnPage)
                {
                    // FullName provides the hierarchical name of the field
                    Console.WriteLine($"Page {pageIndex}: Field = {field.FullName}");
                }
            }
        }
    }
}