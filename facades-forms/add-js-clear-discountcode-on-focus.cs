using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "DiscountCode"
            // The Form indexer returns a WidgetAnnotation, so cast it to Field first
            Field genericField = doc.Form["DiscountCode"] as Field;
            if (genericField == null)
            {
                Console.Error.WriteLine("Field 'DiscountCode' not found or is not a form field.");
                return;
            }

            // Most form fields are represented by TextBoxField (or other derived types).
            // Cast to TextBoxField to gain access to the Actions collection.
            if (!(genericField is TextBoxField discountField))
            {
                Console.Error.WriteLine("Field 'DiscountCode' is not a text box field.");
                return;
            }

            // Attach JavaScript that clears the field when it receives focus.
            // The correct action property for focus is OnReceiveFocus.
            discountField.Actions.OnReceiveFocus = new JavascriptAction("event.target.value='';");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript to '{outputPath}'.");
    }
}
