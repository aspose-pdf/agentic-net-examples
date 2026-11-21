using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // existing PDF or will be created if missing
        const string outputPath = "output_with_datepicker.pdf";

        // Ensure input file exists; if not, start with a new empty document
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            // Add a blank page to host the form field
            doc.Pages.Add();
        }

        using (doc)
        {
            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the date picker will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Aspose.Pdf does not expose a dedicated DateTimeField in older versions.
            // Use a TextBoxField and set its default value to the current system date.
            TextBoxField dateField = new TextBoxField(page, rect)
            {
                PartialName = "DatePicker",
                // Format the date as ISO (yyyy‑MM‑dd) – adjust as needed for your locale.
                Value = DateTime.Now.ToString("yyyy-MM-dd")
            };

            // Set appearance using the DefaultAppearance constructor
            dateField.DefaultAppearance = new DefaultAppearance(
                "Helvetica",
                12,
                System.Drawing.Color.Black);

            // Add the field to the document's form collection (also appears on the page)
            doc.Form.Add(dateField, page.Number);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}
