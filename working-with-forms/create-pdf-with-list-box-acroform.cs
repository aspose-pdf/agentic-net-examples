using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "AcroForm_ListBox.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY)
            Aspose.Pdf.Rectangle listBoxRect = new Aspose.Pdf.Rectangle(100, 500, 250, 650);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, listBoxRect)
            {
                // Set a name for the field (used to reference it later)
                PartialName = "CountryListBox",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select a country"
            };

            // Add country options to the list box
            string[] countries = new string[]
            {
                "United States",
                "Canada",
                "United Kingdom",
                "Germany",
                "France",
                "Australia",
                "Japan",
                "China",
                "India",
                "Brazil"
            };

            foreach (string country in countries)
            {
                listBox.AddOption(country);
            }

            // Add the list box field to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list box saved to '{outputPath}'.");
    }
}