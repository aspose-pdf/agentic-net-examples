using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box field
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multiselection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");

            // Select multiple items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 3, 4 };

            // Add the field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save("MultiSelectListBox.pdf");
        }

        Console.WriteLine("PDF with multi‑select list box created.");
    }
}