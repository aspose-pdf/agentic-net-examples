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
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document and bind it to a FormEditor facade
        using (Document doc = new Document(inputPdf))
        using (FormEditor formEditor = new FormEditor(doc))
        {
            // 1. Make the "TaxRate" field read‑only
            formEditor.SetFieldAttribute("TaxRate", PropertyFlag.ReadOnly);

            // 2. Add JavaScript to calculate TaxRate = Subtotal * 0.1
            // Retrieve the field object from the document pages
            NumberField taxField = null;
            foreach (Page page in doc.Pages)
            {
                foreach (WidgetAnnotation widget in page.Annotations)
                {
                    if (widget is NumberField nf && nf.FullName == "TaxRate")
                    {
                        taxField = nf;
                        break;
                    }
                }
                if (taxField != null) break;
            }

            if (taxField != null)
            {
                // JavaScript uses the Acrobat JS API: event.value is the field's value,
                // this.getField('Subtotal').value retrieves the Subtotal field's value.
                string js = "event.value = this.getField('Subtotal').value * 0.1;";
                taxField.Actions.OnCalculate = new JavascriptAction(js);
            }
            else
            {
                Console.Error.WriteLine("TaxRate field not found.");
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}