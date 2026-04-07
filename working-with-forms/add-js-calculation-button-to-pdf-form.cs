using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "FormWithCalculation.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor and bind it to the document
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);

                // Add Quantity text field
                editor.AddField(FieldType.Text, "Quantity", 1,
                    100f, 700f, // llx, lly
                    200f, 720f); // urx, ury

                // Add UnitPrice text field
                editor.AddField(FieldType.Text, "UnitPrice", 1,
                    100f, 650f,
                    200f, 670f);

                // Add Total text field (read‑only)
                editor.AddField(FieldType.Text, "Total", 1,
                    100f, 600f,
                    200f, 620f);
                // Set the Total field to read‑only via JavaScript later (optional)

                // Add a PushButton field
                editor.AddField(FieldType.PushButton, "CalcButton", 1,
                    250f, 700f,
                    350f, 730f);
                editor.SetFieldScript("CalcButton",
                    @"var qty = this.getField('Quantity').value;
                      var price = this.getField('UnitPrice').value;
                      var total = parseFloat(qty) * parseFloat(price);
                      this.getField('Total').value = total;");

                // Save the modified PDF
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with calculation button saved to '{outputPdf}'.");
    }
}