using System;
using System.IO;
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
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // (left, bottom, right, top) coordinates
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the ListBoxField attached to the page (not the document)
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Optional: set a name for the field
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

            // Add the list box field to the document's AcroForm
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ListBox created at '{outputPath}'.");
    }
}
