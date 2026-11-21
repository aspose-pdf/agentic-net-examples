using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "country_dropdown.pdf";

        // Predefined list of country names
        string[] countries = new string[]
        {
            "United States", "Canada", "United Kingdom", "Germany", "France",
            "Australia", "Japan", "China", "India", "Brazil"
        };

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the dropdown field
            Page page = doc.Pages.Add();

            // Define the rectangle for the field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a ComboBox (dropdown) form field
            ComboBoxField combo = new ComboBoxField(page, rect);
            combo.PartialName = "Country";          // field name
            combo.Value = countries[0];               // default selected value

            // Populate the dropdown with the country names using AddOption
            foreach (string country in countries)
            {
                combo.AddOption(country);
            }

            // Add the field to the document's form collection
            doc.Form.Add(combo);

            // Save the PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}
