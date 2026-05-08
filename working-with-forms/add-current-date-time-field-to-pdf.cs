using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a DateField on the first page (page indexing is 1‑based)
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format for the date and time
            dateField.DateFormat = "dd/MM/yyyy HH:mm:ss";

            // Set the field value to the current date and time
            dateField.Value = DateTime.Now;

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Ensure the field has a visible appearance on the page
            doc.Form.AddFieldAppearance(dateField, 1, rect);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date field saved to '{outputPath}'.");
    }
}