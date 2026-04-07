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

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page where the dropdown will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle for the ComboBox (left, bottom, right, top)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 650);

            // Create the ComboBox field on the specified page and rectangle
            ComboBoxField countryField = new ComboBoxField(page, rect);

            // Set the field name (used as the identifier in the PDF form)
            countryField.Name = "Country";
            countryField.PartialName = "Country";

            // Populate the dropdown with the country options
            foreach (string c in countries)
            {
                countryField.AddOption(c);
            }

            // Add the field to the document's form collection
            doc.Form.Add(countryField);

            // Save the resulting PDF
            doc.Save("CountryDropdown.pdf");
        }

        Console.WriteLine("PDF with 'Country' dropdown created successfully.");
    }
}