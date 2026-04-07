using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction
using Aspose.Pdf.Forms;        // DateField
using Aspose.Pdf.Drawing;      // Rectangle (fully qualified below)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the field rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                Name       = "DatePicker",          // field name
                DateFormat = "mm/dd/yyyy"          // display format
            };

            // JavaScript to open the calendar picker when the field is activated
            // AFDate_Pick() is a built‑in Acrobat function that shows the date picker UI
            dateField.OnActivated = new JavascriptAction("AFDate_Pick();");

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Initialize the field (required for some actions)
            dateField.Init(page);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date picker field: {outputPath}");
    }
}