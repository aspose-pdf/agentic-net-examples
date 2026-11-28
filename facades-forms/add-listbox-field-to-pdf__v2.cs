using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "draft_form_v6.pdf";
        const string outputPath = "categories_box.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Instantiate the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPath);

            // Define the rectangle where the ListBox will appear (llx, lly, urx, ury)
            // Page coordinates are measured from the bottom‑left corner.
            Rectangle listBoxRect = new Rectangle(100, 500, 250, 550);

            // Items to display in the ListBox
            string[] items = new[] { "Category A", "Category B", "Category C" };

            // Set the items collection before adding a ListBox/ComboBox/Radio field
            formEditor.Items = items;

            // Add the ListBox field named "Categories" on page 3 (pages are 1‑based)
            formEditor.AddField(
                FieldType.ListBox,
                "Categories",
                3,
                (float)listBoxRect.LLX,
                (float)listBoxRect.LLY,
                (float)listBoxRect.URX,
                (float)listBoxRect.URY);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"ListBox added and saved to '{outputPath}'.");
    }
}
