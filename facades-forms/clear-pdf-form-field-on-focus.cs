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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field by its fully qualified name and cast to a text box field
            TextBoxField discountField = doc.Form["DiscountCode"] as TextBoxField;
            if (discountField == null)
            {
                Console.Error.WriteLine("Field 'DiscountCode' not found or is not a text box field.");
                return;
            }

            // Attach JavaScript that clears the field when it receives focus.
            // The correct action property for focus is OnReceiveFocus.
            discountField.Actions.OnReceiveFocus = new JavascriptAction("event.target.value='';");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPath}'.");
    }
}
