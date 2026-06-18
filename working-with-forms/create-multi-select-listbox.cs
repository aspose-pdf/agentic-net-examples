using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle for the ListBox field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multiselection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Select multiple items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 3 }; // selects "Option A" and "Option C"

            // Add the field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save("multiselect_listbox.pdf");
        }

        Console.WriteLine("PDF with multiselect ListBox created successfully.");
    }
}