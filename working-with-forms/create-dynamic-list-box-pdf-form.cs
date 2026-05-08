using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicListForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            Rectangle listRect = new Rectangle(100, 500, 300, 650);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, listRect)
            {
                PartialName = "DynamicList",          // field name
                AlternateName = "Dynamic List",       // tooltip
                Color = Color.LightGray,               // background color
                MultiSelect = false                    // single‑selection list
            };

            // Set the border – Border requires the parent annotation in its constructor
            listBox.Border = new Border(listBox) { Width = 1 };

            // Add initial items to the list
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");
            listBox.AddOption("Item 3");

            // Demonstrate dynamic expansion by adding more items programmatically
            // (In a real form this could be driven by JavaScript at runtime)
            listBox.AddOption("Item 4");
            listBox.AddOption("Item 5");

            // Register the field with the document's form collection (page numbers are 1‑based)
            doc.Form.Add(listBox, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic list field saved to '{outputPath}'.");
    }
}
