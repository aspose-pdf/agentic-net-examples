using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "listbox.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ListBoxField on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                PartialName = "MyListBox", // field name
                MultiSelect = true         // enable multi‑selection
            };

            // Add items to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Set default selected items (indices start at 1)
            // This pre‑selects three items; the list box allows up to three selections.
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the list box field to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}