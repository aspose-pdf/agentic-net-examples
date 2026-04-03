using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the list box
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

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
            // Here we select "Option A" (index 1) and "Option C" (index 3)
            listBox.SelectedItems = new int[] { 1, 3 };

            // Add the list box to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save("MultiSelectListBox.pdf");
        }

        Console.WriteLine("PDF with multiselect list box created successfully.");
    }
}