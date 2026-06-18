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

        // Load the existing PDF (Document creation is handled by the using block)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the checkbox will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a new checkbox field on the document
            CheckboxField checkbox = new CheckboxField(doc, rect)
            {
                Name = "AgreeTerms",          // Field name
                PartialName = "AgreeTerms",   // Optional, ensures the same name is used internally
                ExportValue = "Yes",          // Value when checked (optional)
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