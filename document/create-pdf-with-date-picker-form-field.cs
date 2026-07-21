using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                Name = "AppointmentDate",          // Full field name
                PartialName = "AppointmentDate",   // Partial name (used in form hierarchy)
                DateFormat = "MM/dd/yyyy",         // Desired display format
                // Set default appearance (font, size, color) using the correct constructor
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the field (required for proper rendering in some viewers)
            dateField.Init(page);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}