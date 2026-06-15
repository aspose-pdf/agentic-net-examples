using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Predefined list of country names
        string[] countries = new string[]
        {
            "United States", "Canada", "United Kingdom", "Germany",
            "France", "Australia", "Japan", "China", "India", "Brazil"
        };

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown (ComboBox) will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the ComboBox field on the page
            ComboBoxField countryField = new ComboBoxField(page, rect);

            // Set the field name (this is the name used when extracting form data)
            countryField.Name = "Country";

            // Populate the dropdown with the country options
            foreach (string country in countries)
            {
                countryField.AddOption(country);
            }

            // Add the field to the document's form collection
            doc.Form.Add(countryField);

            // Save the PDF to disk
            doc.Save("CountryDropdown.pdf");
        }

        Console.WriteLine("PDF with 'Country' dropdown created successfully.");
    }
}