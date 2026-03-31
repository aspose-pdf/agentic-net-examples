using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for JavascriptAction

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the PhoneNumber field (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a NumberField (text field that accepts only digits) attached to the page
            NumberField phoneField = new NumberField(page, fieldRect);
            phoneField.Name = "PhoneNumber";
            phoneField.MaxLen = 14; // Length of "(###) ###‑####"
            phoneField.AllowedChars = "0123456789"; // Only digits can be entered

            // Add JavaScript to format the entered digits as a phone number mask
            JavascriptAction formatJs = new JavascriptAction(
                "if (event.value.length == 10) { " +
                "event.value = '(' + event.value.substr(0,3) + ') ' + " +
                "event.value.substr(3,3) + '-' + event.value.substr(6,4); }"
            );
            phoneField.ExecuteFieldJavaScript(formatJs);

            // Register the field with the document's form collection (NOT directly to page.Annotations)
            doc.Form.Add(phoneField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with PhoneNumber field saved to '{outputPath}'.");
    }
}
