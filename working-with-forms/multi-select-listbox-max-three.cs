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

            // Define the rectangle where the list box will appear
            // Parameters: left, bottom, right, top (points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 700);

            // Create the ListBoxField on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multiple selection
                MultiSelect = true,
                // Assign a field name (used when accessing the field later)
                Name = "SampleListBox"
            };

            // Populate the list box with options
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Pre‑select three items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the list box to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}