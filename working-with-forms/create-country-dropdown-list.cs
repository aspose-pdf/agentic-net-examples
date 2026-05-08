using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF file
        const string outputPath = "country_dropdown.pdf";

        // Predefined list of country names
        string[] countries = new[]
        {
            "United States",
            "Canada",
            "United Kingdom",
            "Australia",
            "Germany"
        };

        // Create a new PDF document (disposed automatically)
        using (Document doc = new Document())
        {
            // Add a page to place the dropdown
            Page page = doc.Pages.Add();

            // Define the rectangle for the ComboBox (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a ComboBox field attached to the page (not the document)
            ComboBoxField countryField = new ComboBoxField(page, rect);
            countryField.Name = "Country";                 // field identifier
            countryField.AlternateName = "Select Country"; // tooltip text

            // Populate the dropdown with country options
            foreach (string country in countries)
            {
                countryField.AddOption(country);
            }

            // Add the field to the document's form collection
            doc.Form.Add(countryField);

            // Save the PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 'Country' dropdown saved to '{outputPath}'.");
    }
}
