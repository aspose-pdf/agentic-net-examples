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

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page where the dropdown will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle (llx, lly, urx, ury) for the ComboBox field
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a ComboBox field (dropdown list) on the page
            ComboBoxField countryField = new ComboBoxField(page, rect);

            // Set the field name (used to identify the field in the PDF)
            countryField.Name = "Country";

            // Populate the dropdown with the country options
            foreach (string c in countries)
            {
                countryField.AddOption(c);
            }

            // Add the field to the document's form collection
            doc.Form.Add(countryField);

            // Save the PDF to disk
            doc.Save("CountryDropdown.pdf");
        }

        Console.WriteLine("PDF with 'Country' dropdown created successfully.");
    }
}