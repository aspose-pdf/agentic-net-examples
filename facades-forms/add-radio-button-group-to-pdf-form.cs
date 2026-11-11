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

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Page where the radio button group will be placed (1‑based index)
        const int targetPage = 3;
        Page page = doc.Pages[targetPage];

        // Base rectangle for the first option (adjust as needed)
        // (llx, lly, urx, ury)
        float llx = 100f;
        float lly = 500f;
        float urx = 200f;
        float ury = 550f;
        // Vertical offset between the two options
        float offsetY = 30f;

        // Create the radio button group
        RadioButtonField paymentMethod = new RadioButtonField(page)
        {
            PartialName = "PaymentMethod"
        };

        // Add options with their visual rectangles
        paymentMethod.AddOption("Credit", new Rectangle(llx, lly, urx, ury));
        paymentMethod.AddOption("PayPal", new Rectangle(llx, lly - offsetY, urx, ury - offsetY));

        // Optionally set a default selected option (1‑based index). 0 means none selected.
        // paymentMethod.Selected = 1; // Uncomment to select "Credit" by default

        // Add the radio button group to the form
        doc.Form.Add(paymentMethod);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
