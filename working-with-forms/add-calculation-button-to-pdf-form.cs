using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the button field (assumed name "CalcButton")
                ButtonField button = doc.Form["CalcButton"] as ButtonField;
                if (button == null)
                {
                    Console.Error.WriteLine("Button field 'CalcButton' not found.");
                }
                else
                {
                    // JavaScript that reads Quantity and UnitPrice fields, calculates Total, and sets the Total field
                    string js = @"
var qty = this.getField('Quantity').value;
var price = this.getField('UnitPrice').value;
var total = qty * price;
this.getField('Total').value = total;
";

                    // Assign the JavaScript to the button's mouse‑up (release) action
                    button.Actions.OnReleaseMouseBtn = new JavascriptAction(js);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
