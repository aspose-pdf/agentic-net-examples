using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicListForm.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // Parameters: llx, lly, urx, ury
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 500);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                PartialName = "DynamicList",          // field name
                AlternateName = "Add items here",    // tooltip
                MultiSelect = false                  // single‑selection list
                // If you want an editable combo box, uncomment the line below
                // Combo = true;
            };

            // Add some initial items to the list
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");
            listBox.AddOption("Item 3");

            // Attach the field to the document's AcroForm (page number is 1‑based)
            doc.Form.Add(listBox, 1);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with dynamic list saved to '{outputPath}'.");
    }
}