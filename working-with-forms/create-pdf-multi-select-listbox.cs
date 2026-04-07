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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ListBoxField on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multi‑selection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");

            // Set default selected items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 3 }; // selects "Option 1" and "Option 3"

            // Add the list box to the document's form
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}