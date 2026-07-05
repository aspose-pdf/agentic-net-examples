using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
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

        // Use PdfContentEditor (a Facades class) to load the PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Access the underlying Document object
        Document doc = editor.Document;

        // Locate the "Quantity" field and assign a Calculate JavaScript action
        // Fully qualify the Form type to avoid ambiguity with Aspose.Pdf.Facades.Form
        Aspose.Pdf.Forms.Form form = doc.Form;
        foreach (WidgetAnnotation field in form)
        {
            // Full field name may include hierarchy; check for ending with "Quantity"
            if (field.FullName != null && field.FullName.EndsWith("Quantity"))
            {
                // JavaScript: Total = Quantity * UnitPrice
                string js = "var qty = this.getField('Quantity').value;" +
                            "var price = this.getField('UnitPrice').value;" +
                            "this.getField('Total').value = qty * price;";

                // Set the OnCalculate action for the field
                field.Actions.OnCalculate = new JavascriptAction(js);
                break;
            }
        }

        // Save the modified PDF using the same Facades instance
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with JavaScript action to '{outputPath}'.");
    }
}
