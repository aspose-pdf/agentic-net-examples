using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "IpAddressForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the IP address field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box field for the IP address
            TextBoxField ipField = new TextBoxField(page, fieldRect);
            ipField.PartialName = "IPField";
            ipField.Contents = "Enter IPv4 address";
            ipField.Color = Aspose.Pdf.Color.LightGray;

            // NOTE: The AllowedChars property is not available in the current Aspose.Pdf version.
            // If character restriction is required, it can be enforced via JavaScript validation.

            // Set a JavaScript validation action that runs when the field loses focus
            // The script uses a regular expression to validate IPv4 format
            string js = @"
                var ip = this.getField('IPField').value;
                var regex = /^(\\d{1,3}\\.){3}\\d{1,3}$/;
                if (!regex.test(ip)) {
                    app.alert('Invalid IPv4 address. Please enter a value like 192.168.0.1');
                    // Optionally clear the field
                    this.getField('IPField').value = '';
                }
            ";
            ipField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(ipField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with IP address validation saved to '{outputPath}'.");
    }
}
