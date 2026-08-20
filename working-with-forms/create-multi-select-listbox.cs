using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "multiselect_listbox.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the ListBox field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multiselection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Select multiple items (indices are 1‑based)
            // Here we select "Option A", "Option C", and "Option D"
            listBox.SelectedItems = new int[] { 1, 3, 4 };

            // Add the field to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiselect ListBox saved to '{outputPath}'.");
    }
}