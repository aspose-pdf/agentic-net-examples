using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing; // for Rectangle (fully qualified usage below)

class Program
{
    static void Main()
    {
        const string outputPath = "CountryDropdown.pdf";

        // Predefined list of country names
        string[] countries = new[]
        {
            "United States",
            "Canada",
            "Mexico",
            "United Kingdom",
            "Germany",
            "France",
            "Japan",
            "Australia"
        };

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown will appear (llx, lly, urx, ury)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create the combo box (dropdown) field
            ComboBoxField countryField = new ComboBoxField(page, rect)
            {
                PartialName = "Country" // field name
            };

            // Populate the dropdown with the predefined country list using AddOption
            foreach (string country in countries)
            {
                countryField.AddOption(country);
            }

            // Add the field to the document's form collection
            doc.Form.Add(countryField);

            // Save the PDF (output format is PDF by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown field saved to '{outputPath}'.");
    }
}
