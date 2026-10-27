using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm_ListBox.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page where the list box will be placed
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (lower‑left X/Y, upper‑right X/Y)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 500);

            // Create the list box field and assign a partial name (field identifier)
            ListBoxField countryListBox = new ListBoxField(page, rect)
            {
                PartialName = "CountryList",
                // Optional: set the default selected index (0‑based). -1 means no selection.
                Selected = -1
            };

            // Populate the list box with country options using AddOption (Options is read‑only)
            countryListBox.AddOption("United States");
            countryListBox.AddOption("Canada");
            countryListBox.AddOption("Mexico");
            countryListBox.AddOption("United Kingdom");
            countryListBox.AddOption("Germany");
            countryListBox.AddOption("France");
            countryListBox.AddOption("Japan");
            countryListBox.AddOption("Australia");
            countryListBox.AddOption("India");
            countryListBox.AddOption("Brazil");

            // Add the list box to the document's AcroForm (page index is 1‑based)
            doc.Form.Add(countryListBox, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm list box saved to '{outputPath}'.");
    }
}
