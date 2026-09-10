using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";

        // ------------------------------------------------------------
        // Ensure a PDF template with a list box named "CountryList" exists.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a minimal PDF document.
            using (var doc = new Document())
            {
                // Add a page – required for form fields.
                var page = doc.Pages.Add();

                // Define the rectangle for the list box (left, bottom, right, top).
                var rect = new Rectangle(100, 600, 300, 800);

                // Create the list box field and give it the required partial name.
                var listBox = new ListBoxField(page, rect)
                {
                    PartialName = "CountryList"
                    // The ListBoxField class in recent Aspose.PDF versions does not expose
                    // IsMultiline or IsReadOnly properties, so they are omitted.
                };

                // Add the field to the document's form collection.
                doc.Form.Add(listBox);

                // Save the template.
                doc.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // Generate 195 country names (Country1 … Country195).
        // ------------------------------------------------------------
        string[] countries = Enumerable.Range(1, 195)
                                        .Select(i => $"Country{i}")
                                        .ToArray();

        // ------------------------------------------------------------
        // Use FormEditor (new API) – bind the existing PDF.
        // ------------------------------------------------------------
        var formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // Add each country to the list box named "CountryList".
        foreach (string country in countries)
        {
            formEditor.AddListItem("CountryList", country);
        }

        // Save the updated PDF.
        formEditor.Save(outputPdf);
    }
}
