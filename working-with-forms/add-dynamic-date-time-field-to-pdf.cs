using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // existing PDF to augment
        const string outputPath = "output_with_date.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity (rectangle-disambiguation rule)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a DateField on page 1 (page-indexing-one-based rule)
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format (default is "dd/MM/yyyy")
            dateField.DateFormat = "dd/MM/yyyy HH:mm:ss";

            // Set the initial value to the current date and time
            dateField.Value = DateTime.Now;

            // Add the field to the form on page 1
            doc.Form.Add(dateField, 1);

            // Save the modified PDF (save-to-non-pdf-always-use-save-options rule not needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic date field: {outputPath}");
    }
}