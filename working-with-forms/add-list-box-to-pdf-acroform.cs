using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // (left, bottom, right, top) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle listBoxRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, listBoxRect)
            {
                // Optional: set a name for the field
                Name = "CountryListBox"
            };

            // Add country options to the list box
            listBox.AddOption("United States");
            listBox.AddOption("Canada");
            listBox.AddOption("United Kingdom");
            listBox.AddOption("Australia");
            listBox.AddOption("Germany");
            listBox.AddOption("France");
            listBox.AddOption("Japan");
            listBox.AddOption("China");
            listBox.AddOption("India");
            listBox.AddOption("Brazil");

            // Add the field to the document's AcroForm
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save("ListBoxForm.pdf");
        }

        Console.WriteLine("PDF with ListBox created successfully.");
    }
}