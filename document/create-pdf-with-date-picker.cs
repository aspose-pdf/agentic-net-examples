using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for Border class

class Program
{
    static void Main()
    {
        const string outputPath = "date_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date picker will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect)
            {
                Name = "DateField1",               // Full field name
                PartialName = "DateField1",        // Partial name (used in form hierarchy)
                AlternateName = "Select a date",  // Tooltip shown in PDF viewers
                DateFormat = "MM/dd/yyyy"          // Desired display format
            };

            // Optional visual styling: blue border with 1‑point width
            // Border requires the parent annotation (the field) in its constructor
            dateField.Border = new Border(dateField) { Width = 1 };
            // Border colour is set via the field's own Color property
            dateField.Color = Aspose.Pdf.Color.Blue;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}
