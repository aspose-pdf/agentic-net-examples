using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Define the rectangle where the date picker will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set a partial name (field identifier) and an optional tooltip
            dateField.PartialName = "DateOfBirth";
            dateField.AlternateName = "Select your birth date";

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the field to generate its appearance correctly
            dateField.Init(page);

            // Save the PDF containing the date picker field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}