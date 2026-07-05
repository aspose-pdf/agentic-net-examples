using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the IP address field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box field for the IP address
            TextBoxField ipField = new TextBoxField(page, fieldRect);
            ipField.PartialName = "IpAddress";          // Field name
            ipField.Value = "";                         // Default/placeholder value
            ipField.Color = Color.Black;                 // Border color (annotation's own color)
            ipField.Border = new Border(ipField)        // Add a visible border
            {
                Width = 1
            };

            // Add the field to the document's form collection (not directly to page annotations)
            doc.Form.Add(ipField);

            // JavaScript code that validates the field value against an IPv4 regex
            // The script runs when the field loses focus (OnValidate event)
            string js = @"
                var ip = this.getField('IpAddress').value;
                var pattern = /^(25[0-5]|2[0-4]\d|[01]?\d\d?)\.
                               (25[0-5]|2[0-4]\d|[01]?\d\d?)\.
                               (25[0-5]|2[0-4]\d|[01]?\d\d?)\.
                               (25[0-5]|2[0-4]\d|[01]?\d\d?)$/;
                if (!pattern.test(ip)) {
                    app.alert('Invalid IP address format. Please enter a valid IPv4 address.');
                }
            ";

            // Assign the JavaScript validation action to the field's OnValidate event
            ipField.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF to a file
            doc.Save("IpAddressValidation.pdf");
        }

        Console.WriteLine("PDF with IPv4 validation field created successfully.");
    }
}
