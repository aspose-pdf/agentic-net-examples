using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multiselect_listbox.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multi‑selection
                MultiSelect = true,
                // Optional: give the field a name
                Name = "Choices"
            };

            // Add options to the list box
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Pre‑select some items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 2, 4 }; // selects "Option B" and "Option D"

            // Add the field to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}