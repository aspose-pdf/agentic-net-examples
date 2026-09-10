using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF (optional)
        const string outputPdf = "output.pdf";

        // Ensure the input file exists; if not, create a blank document.
        if (!File.Exists(inputPdf))
        {
            using (Document blank = new Document())
            {
                // Add a single empty page.
                blank.Pages.Add();
                blank.Save(inputPdf);
            }
        }

        // Load the PDF, add a DateField, set its value, then make it read‑only.
        using (Document doc = new Document(inputPdf))
        {
            // Choose the first page (1‑based indexing).
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear.
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the date field on the page.
            DateField dateField = new DateField(page, rect);

            // Add the field to the document's form collection.
            doc.Form.Add(dateField);

            // Populate the field with the current date and time.
            dateField.Value = DateTime.Now;

            // Prevent further user modification.
            dateField.ReadOnly = true;

            // Save the updated PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Date field added and set to read‑only. Saved as '{outputPdf}'.");
    }
}