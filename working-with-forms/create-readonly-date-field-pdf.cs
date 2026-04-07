using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_readonly.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the date field on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Populate the field with the current date/time
            dateField.Value = DateTime.Now;

            // Optional: set a display format for the date
            dateField.DateFormat = "dd/MM/yyyy";

            // Make the field read‑only to prevent user edits after this point
            dateField.ReadOnly = true;

            // Add the field to the document's interactive form collection
            doc.Form.Add(dateField);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}