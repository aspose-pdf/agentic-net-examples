using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "listbox.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to host the list box
            Page page = doc.Pages.Add();

            // Define the rectangle (left, bottom, right, top) for the list box
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 300);

            // Create the ListBoxField on the page
            var listBox = new ListBoxField(page, rect)
            {
                // Enable multiselection
                MultiSelect = true
                // Note: Aspose.Pdf does not expose a MaxSelect property. The maximum
                // number of selections must be enforced by the consuming application
                // (e.g., via JavaScript or by limiting the initial selection).
            };

            // Add options to the list box (example options)
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Set the initially selected items (maximum three selections)
            // Indices are 1‑based as per Aspose.Pdf documentation
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the list box to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}
