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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document must contain at least two pages.");
                return;
            }

            // Get page 2 (1‑based indexing)
            Page page = doc.Pages[2];

            // Define rectangle for the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the checkbox field on page 2
            CheckboxField checkbox = new CheckboxField(page, rect);
            checkbox.Name = "AgreeTerms";   // field name
            checkbox.Checked = false;        // default unchecked state

            // Add the field to the document form
            doc.Form.Add(checkbox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added to page 2. Saved as '{outputPath}'.");
    }
}