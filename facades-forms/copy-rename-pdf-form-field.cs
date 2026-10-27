using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Retrieve the existing field named "AddressBlock".
        // The Form indexer returns a WidgetAnnotation, so cast it to Field.
        Field addressField = doc.Form["AddressBlock"] as Field;
        if (addressField == null)
        {
            Console.Error.WriteLine("Field 'AddressBlock' not found or is not a form field.");
            return;
        }

        // Extract the rectangle (position and size) of the original field.
        double llx = addressField.Rect.LLX;
        double lly = addressField.Rect.LLY;
        double urx = addressField.Rect.URX;
        double ury = addressField.Rect.URY;

        // Create a new TextBox field "BillingAddress" on page 3 using the same rectangle
        // and the same value as the original field.
        TextBoxField billingField = new TextBoxField(doc.Pages[3], new Rectangle(llx, lly, urx, ury))
        {
            PartialName = "BillingAddress",
            Value = addressField.Value
        };

        // Add the new field to the form on page 3.
        doc.Form.Add(billingField, 3);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Field copied to page 3 and saved as '{outputPath}'.");
    }
}
