using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "form_template.pdf";
        const string outputPath = "multi_items.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works on the PDF structure (adding fields, items, etc.)
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Name of the ListBox field in the PDF form
            const string listBoxFieldName = "ListBox1";

            // Verify whether the field already exists; if not, create it.
            bool fieldExists;
            using (Form form = new Form())
            {
                form.BindPdf(inputPath);
                fieldExists = Array.Exists(form.FieldNames, n => n == listBoxFieldName);
            }

            if (!fieldExists)
            {
                // Add a new ListBox field on page 1. Adjust the rectangle as needed.
                // Parameters: FieldType, name, pageNumber, llx, lly, urx, ury
                editor.AddField(FieldType.ListBox, listBoxFieldName, 1, 100, 500, 200, 550);
            }

            // Items to populate the ListBox
            string[] listItems = new[] { "Option A", "Option B", "Option C", "Option D" };

            // Populate the ListBox with the items
            foreach (string item in listItems)
            {
                editor.AddListItem(listBoxFieldName, item);
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with list items to '{outputPath}'.");
    }
}
