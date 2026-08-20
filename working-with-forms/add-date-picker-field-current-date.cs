using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (page-indexing-one-based)
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity (rectangle-disambiguation)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);

            // Set the default value to the current system date
            dateField.Value = DateTime.Now;

            // Optional: specify the display format (default is dd/MM/yyyy)
            dateField.DateFormat = "dd/MM/yyyy";

            // Add the field to the form and initialize it on the page
            doc.Form.Add(dateField);
            dateField.Init(page);

            // Save the modified PDF (save-to-non-pdf-always-use-save-options not needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date picker field added and saved to '{outputPath}'.");
    }
}