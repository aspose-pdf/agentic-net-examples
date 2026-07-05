using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "dropdown.pdf";

        // Predefined list of country names
        string[] countries = new string[]
        {
            "USA",
            "Canada",
            "Mexico",
            "United Kingdom",
            "Germany",
            "France"
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the dropdown
            Page page = doc.Pages.Add();

            // Define the rectangle where the combo box will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a ComboBox (dropdown) field on the page
            ComboBoxField combo = new ComboBoxField(page, rect);

            // Set the field name (used in form data) and tooltip
            combo.PartialName = "Country";
            combo.AlternateName = "Select Country";

            // Populate the dropdown with the country options
            foreach (string country in countries)
            {
                combo.AddOption(country);
            }

            // Add the field to the document's form
            doc.Form.Add(combo);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}