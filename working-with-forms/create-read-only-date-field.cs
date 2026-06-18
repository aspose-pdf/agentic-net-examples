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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the date field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a new DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Populate the field with the current date and time
            dateField.Value = DateTime.Now;

            // Set the field to read‑only to prevent further edits
            dateField.ReadOnly = true;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date field set to read‑only and saved to '{outputPath}'.");
    }
}