using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Try to get the field "DiscountCode" as a TextBoxField.
        // The indexer returns a generic Field; we cast to TextBoxField.
        // If the cast fails, the field either does not exist or is of a different type.
        if (doc.Form == null)
        {
            Console.Error.WriteLine("The document does not contain a form.");
            return;
        }

        TextBoxField discountField = doc.Form["DiscountCode"] as TextBoxField;
        if (discountField == null)
        {
            Console.Error.WriteLine("Form field 'DiscountCode' not found or is not a TextBoxField.");
            return;
        }

        // JavaScript that clears the field when it receives focus.
        // Using the 'event' object is the recommended way in Acrobat JavaScript.
        string focusClearJs = "event.target.value='';";

        // Attach the JavaScript to the field's OnEnter action (focus event).
        discountField.Actions.OnEnter = new JavascriptAction(focusClearJs);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
