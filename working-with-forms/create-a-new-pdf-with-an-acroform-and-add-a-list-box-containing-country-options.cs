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
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // Rectangle(llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle listBoxRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the ListBoxField attached to the *page* (not the document)
            ListBoxField listBox = new ListBoxField(page, listBoxRect)
            {
                // Optional: set a name for the field (used for form data extraction)
                Name = "CountryListBox",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select a country"
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

            // Add the list box to the document's form
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save("ListBoxForm.pdf");
        }

        Console.WriteLine("PDF with ListBox created successfully.");
    }
}
