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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the page where the new field will be placed (1‑based index)
            int targetPageNumber = 1;

            // Define the rectangle (llx, lly, urx, ury) for the field on the target page
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new text box form field
            TextBoxField textField = new TextBoxField(doc.Pages[targetPageNumber], fieldRect)
            {
                PartialName = "MyTextField",   // field name
                Value       = "Default text"   // initial value
            };

            // Add the field to the form at the specified page.
            // This respects the ordered layout of form fields on that page.
            doc.Form.Add(textField, targetPageNumber);

            // Save the modified document (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field inserted and document saved to '{outputPath}'.");
    }
}