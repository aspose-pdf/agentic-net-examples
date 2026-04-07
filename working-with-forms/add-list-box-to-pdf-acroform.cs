using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm_ListBox.pdf";

        // Create a new PDF document and add a blank page (1‑based indexing)
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor with the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Define field name and rectangle coordinates (points)
                string fieldName = "CountryListBox";
                int pageNumber = 1;
                float llx = 100f; // lower‑left X
                float lly = 500f; // lower‑left Y
                float urx = 250f; // upper‑right X
                float ury = 650f; // upper‑right Y

                // Add a ListBox field to the form
                formEditor.AddField(FieldType.ListBox, fieldName, pageNumber, llx, lly, urx, ury);

                // Add country options to the list box
                string[] countries = new string[]
                {
                    "United States",
                    "Canada",
                    "United Kingdom",
                    "Australia",
                    "Germany",
                    "France",
                    "Japan",
                    "China",
                    "India",
                    "Brazil"
                };

                foreach (string country in countries)
                {
                    formEditor.AddListItem(fieldName, country);
                }

                // Save the PDF with the AcroForm
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with list box saved to '{outputPath}'.");
    }
}