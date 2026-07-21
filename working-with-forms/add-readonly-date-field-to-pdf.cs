using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with a form (optional)
        const string outputPdf = "output.pdf";

        // Ensure the input file exists; if not, create a blank PDF.
        if (!File.Exists(inputPdf))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPdf);
            }
        }

        // Load the PDF, add a DateField, set its value, then make it read‑only.
        using (Document doc = new Document(inputPdf))
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear.
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the DateField on the page.
            DateField dateField = new DateField(page, rect);

            // Set an initial date value.
            dateField.Value = DateTime.Now;

            // Prevent the user from modifying the field after it is populated.
            dateField.ReadOnly = true;

            // Add the field to the document's form collection.
            doc.Form.Add(dateField);

            // Save the updated PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Date field added and set to read‑only. Saved as '{outputPdf}'.");
    }
}