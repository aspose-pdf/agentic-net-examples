using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multiselect_listbox.pdf";

        // Document lifecycle must be managed with using (see document-disposal-with-using rule)
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
            listBox.AddOption("Apple");
            listBox.AddOption("Banana");
            listBox.AddOption("Cherry");
            listBox.AddOption("Date");
            listBox.AddOption("Elderberry");

            // Optionally set default selected items (indices are 1‑based)
            // listBox.SelectedItems = new int[] { 1, 3 }; // selects "Apple" and "Cherry"

            // Add the field to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}