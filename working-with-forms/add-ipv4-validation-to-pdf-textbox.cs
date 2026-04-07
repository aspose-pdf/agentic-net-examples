using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "IpAddressForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the IP address field will be placed
            // (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box field for the IP address
            TextBoxField ipField = new TextBoxField(page, fieldRect)
            {
                PartialName = "IpAddress",          // Field name
                Contents = "Enter IPv4 address",    // Tooltip / default text
                Color = Aspose.Pdf.Color.LightGray // Background color (optional)
            };

            // Add the field to the document's form collection
            doc.Form.Add(ipField, 1); // 1 = page number (1‑based)

            // JavaScript that validates the field value against an IPv4 regex
            string jsValidate = @"
var ip = event.value;
var regex = /^(?:(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(?:2[0-4]\d|25[0-5]|[01]?\d\d?)$/;
if (!regex.test(ip)) {
    app.alert('Invalid IPv4 address format.');
    event.rc = false; // Reject the value
} else {
    event.rc = true;  // Accept the value
}";
            // Assign the JavaScript validation action to the field
            ipField.Actions.OnValidate = new JavascriptAction(jsValidate);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with IP address validation saved to '{outputPath}'.");
    }
}