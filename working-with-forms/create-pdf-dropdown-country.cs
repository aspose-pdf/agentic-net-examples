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
            "United States",
            "Canada",
            "Mexico",
            "United Kingdom",
            "Germany",
            "France",
            "Italy",
            "Spain",
            "Australia",
            "Japan"
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a ComboBox (dropdown) field on the page
            ComboBoxField countryField = new ComboBoxField(page, rect);
            countryField.Name = "Country";          // field identifier
            countryField.PartialName = "Country";   // optional, same as Name

            // Populate the dropdown with country options
            foreach (string country in countries)
            {
                countryField.AddOption(country);
            }

            // Add the field to the document's form
            doc.Form.Add(countryField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}