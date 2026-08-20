using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "DiscountCode";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by its fully qualified name. The Form indexer returns a WidgetAnnotation,
            // so we cast it to the base Field type.
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found.");
                return;
            }

            // Attach JavaScript that clears the field when it receives focus.
            // The correct action property for focus is OnReceiveFocus.
            field.Actions.OnReceiveFocus = new JavascriptAction("event.target.value='';");

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}
