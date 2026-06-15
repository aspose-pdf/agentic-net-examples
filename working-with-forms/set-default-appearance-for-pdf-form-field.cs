using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Define the rectangle where the form field will be placed
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the page
            TextBoxField textBox = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextBox"
            };

            // Set the default appearance (DA) for the field:
            // Font: Helvetica, Size: 12 points, Color: Black
            // NOTE: DefaultAppearance expects a System.Drawing.Color, not Aspose.Pdf.Color
            DefaultAppearance defaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Assign the DefaultAppearance to the field
            textBox.DefaultAppearance = defaultAppearance;

            // Add the field to the document's form collection
            doc.Form.Add(textBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated field appearance to '{outputPath}'.");
    }
}
