using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multiselect_listbox.pdf";

        // Document lifecycle: wrap in using for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (first page is index 1)
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 700);

            // Create the ListBoxField on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multi‑selection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");

            // Optionally set default selected items (indices start at 1)
            listBox.SelectedItems = new int[] { 2, 4 };

            // Add the list box to the document's form
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}