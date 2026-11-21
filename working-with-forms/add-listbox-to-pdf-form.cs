using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormListBox.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners
            var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 500);

            // Create the list box field and associate it with the page
            var countryListBox = new ListBoxField(page, rect)
            {
                // The field name used in the form data
                PartialName = "CountryList"
            };

            // Add country options to the list box using AddOption (Option class is not used directly)
            countryListBox.AddOption("United States");
            countryListBox.AddOption("Canada");
            countryListBox.AddOption("United Kingdom");
            countryListBox.AddOption("Germany");
            countryListBox.AddOption("France");
            countryListBox.AddOption("Australia");
            countryListBox.AddOption("Japan");
            countryListBox.AddOption("India");
            countryListBox.AddOption("Brazil");
            countryListBox.AddOption("South Africa");

            // Add the list box to the document's AcroForm
            doc.Form.Add(countryListBox);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm list box saved to '{outputPath}'.");
    }
}
