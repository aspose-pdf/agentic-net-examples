using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            // Load the PDF and work with form fields using FormEditor
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                float llx = 100f;
                float lly = 500f;
                float urx = 200f;
                float ury = 530f;

                // Add a push‑button field named "calcButton" on page 1
                formEditor.AddField(FieldType.PushButton, "calcButton", 1, llx, lly, urx, ury);

                // JavaScript that reads "price" and "quantity" fields, multiplies them, and sets "total"
                string js = @"
var price = this.getField('price').value;
var qty = this.getField('quantity').value;
if (!isNaN(price) && !isNaN(qty)) {
    this.getField('total').value = price * qty;
}
";

                // Attach the JavaScript action to the button
                formEditor.AddFieldScript("calcButton", js);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }

            Console.WriteLine($"PDF with JavaScript button saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
