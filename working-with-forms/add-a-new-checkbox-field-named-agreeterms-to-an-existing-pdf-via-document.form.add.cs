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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the checkbox will appear
            // (left, bottom, right, top) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a new checkbox field on the specified page
            CheckboxField checkbox = new CheckboxField(page, rect)
            {
                Name = "AgreeTerms",          // Field name
                ExportValue = "Yes",          // Value exported when checked
                Checked = false               // Initial state (unchecked)
            };

            // Add the checkbox to the form
            doc.Form.Add(checkbox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added and saved to '{outputPath}'.");
    }
}