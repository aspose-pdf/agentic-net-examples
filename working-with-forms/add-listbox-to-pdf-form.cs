using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "listbox_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required before placing the field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle listBoxRect = new Aspose.Pdf.Rectangle(100, 500, 250, 700);

            // Create the ListBox field on the document using the rectangle and the page
            ListBoxField listBox = new ListBoxField(page, listBoxRect)
            {
                // Set a name for the field (used to reference it later)
                PartialName = "CountryListBox",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select a country",
                // Optional: make the field visible and editable
                ReadOnly = false,
                // Optional: allow multiple selection (false for single-select)
                MultiSelect = false
            };

            // Add country options to the list box
            listBox.AddOption("United States");
            listBox.AddOption("Canada");
            listBox.AddOption("United Kingdom");
            listBox.AddOption("Australia");
            listBox.AddOption("Germany");
            listBox.AddOption("France");
            listBox.AddOption("Japan");
            listBox.AddOption("India");
            listBox.AddOption("Brazil");
            listBox.AddOption("South Africa");

            // Add the list box field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ListBox created at '{outputPath}'.");
    }
}