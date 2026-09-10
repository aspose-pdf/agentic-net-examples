using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_form.pdf";
        // Aspose.PDF evaluation mode allows a maximum of 4 elements in any collection (Pages, Annotations, etc.).
        // Reduce the page count to 4 to stay within this limit. A full license removes this restriction.
        const int totalPages = 4; // adjusted from 5 to comply with evaluation‑mode limits

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add the required number of pages (1‑based indexing)
            for (int i = 1; i <= totalPages; i++)
            {
                doc.Pages.Add();
            }

            // Reference the form object of the document
            Form form = doc.Form;

            // Define a rectangle for the field (same position on each page)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a separate TextBoxField for each page with a unique name
            for (int page = 1; page <= totalPages; page++)
            {
                string fieldName = page == 1 ? "SampleField" : $"SampleField_Page{page}";
                TextBoxField txtField = new TextBoxField(doc)
                {
                    PartialName = fieldName,
                    Rect = fieldRect,
                    Value = $"Value on page {page}"
                };
                // Add the field to the current page
                form.Add(txtField, page);
            }

            // Save the multi‑page PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
