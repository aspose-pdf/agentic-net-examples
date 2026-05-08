using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class SetRadioButtonExportValue
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (or create a new one if needed)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page – radio button fields are placed on a page
            Page page = doc.Pages[1];

            // Create a new radio button field on the page
            RadioButtonField radio = new RadioButtonField(page);

            // Set a name for the field (used to reference it later)
            radio.PartialName = "MyRadioButton";

            // Define the field rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            radio.Rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Add options with specific export values.
            // The first argument is the export value (code used by downstream systems),
            // the second argument is the visible name shown in the PDF viewer.
            radio.AddOption("CODE_A", "Option A");
            radio.AddOption("CODE_B", "Option B");
            radio.AddOption("CODE_C", "Option C");

            // Optionally set a default selected option (1‑based index)
            radio.Selected = 1; // selects "Option A"

            // Add the radio button field to the document's form collection
            doc.Form.Add(radio);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Radio button with export values saved to '{outputPath}'.");
    }
}