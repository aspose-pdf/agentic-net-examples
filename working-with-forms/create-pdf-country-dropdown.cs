using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Define the output PDF path
        const string outputPath = "dropdown_country.pdf";

        // Predefined list of country names
        string[] countries = new string[]
        {
            "United States", "Canada", "United Kingdom", "Germany",
            "France", "Australia", "Japan", "China", "India", "Brazil"
        };

        // Create a new PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document())
        {
            // Add a page to host the dropdown field
            Page page = doc.Pages.Add();

            // Define the rectangle where the ComboBox will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a ComboBox (dropdown) field on the page
            ComboBoxField countryField = new ComboBoxField(page, rect);

            // Set the field name (partial name) – this is the identifier used in the form
            countryField.PartialName = "Country";

            // Populate the dropdown with the country options
            foreach (string c in countries)
            {
                countryField.AddOption(c);
            }

            // Optionally set a default selected value (e.g., first country)
            countryField.Value = countries[0];

            // Add the field to the document's form
            doc.Form.Add(countryField);

            // Save the PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown list saved to '{outputPath}'.");
    }
}