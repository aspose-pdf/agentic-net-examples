using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for WidgetAnnotation.ReadOnly

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";   // existing PDF (can be empty)
        const string outputPath = "readonly_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the date field, set its initial value, and make it read‑only
            DateField dateField = new DateField(page, rect);
            dateField.Value = DateTime.Now;   // initial population
            dateField.ReadOnly = true;        // prevent further user edits

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only date field: {outputPath}");
    }
}