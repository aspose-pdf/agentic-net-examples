using System;
using System.IO;
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
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 400);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multi‑selection
                MultiSelect = true,
                // Optional: give the field a name
                PartialName = "listBox1"
            };

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Set the default selected items (Aspose.Pdf uses 1‑based indices)
            // Selecting the first three items enforces the required maximum of three selections.
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the list box to the document's form
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}
