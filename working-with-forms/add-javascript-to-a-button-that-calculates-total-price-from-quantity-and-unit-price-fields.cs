using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (must contain the fields: Quantity, UnitPrice, Total)
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_calc_button.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Define the rectangle for the push‑button (lower‑left X/Y, upper‑right X/Y)
                // Adjust the coordinates as needed for your layout
                float llx = 100f; // lower‑left X
                float lly = 500f; // lower‑left Y
                float urx = 200f; // upper‑right X
                float ury = 540f; // upper‑right Y

                // Add a push‑button field named "calcBtn" on page 1
                formEditor.AddField(FieldType.PushButton, "calcBtn", 1, llx, lly, urx, ury);

                // JavaScript that reads Quantity and UnitPrice fields, calculates Total, and writes it back
                string js = @"
                    var qty = this.getField('Quantity').value;
                    var price = this.getField('UnitPrice').value;
                    var total = qty * price;
                    this.getField('Total').value = total;
                ";

                // Attach the JavaScript to the button (adds it after any existing scripts)
                formEditor.AddFieldScript("calcBtn", js);

                // Save the modified PDF
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPdf}'.");
    }
}