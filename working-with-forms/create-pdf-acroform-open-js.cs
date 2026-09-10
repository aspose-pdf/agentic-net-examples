using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJS.pdf";

        // Create a new PDF document (lifecycle rule: use using)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field and set its appearance
            TextBoxField textField = new TextBoxField(page, fieldRect);
            textField.PartialName = "MyTextField"; // set the field name
            // DefaultAppearance expects System.Drawing.Color, not Aspose.Pdf.Color
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            // Optional initial value
            textField.Value = "Enter text here";

            // Add the field to the document's AcroForm
            doc.Form.Add(textField);

            // Set a document‑level JavaScript action that runs when the PDF is opened
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF (lifecycle rule: use Document.Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm and open‑action JavaScript saved to '{outputPath}'.");
    }
}
