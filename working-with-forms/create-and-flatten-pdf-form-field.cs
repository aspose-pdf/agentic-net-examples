using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "xfa_flattened.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a single page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Define the rectangle where the XFA text box will appear
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field (XFA compatible)
            Aspose.Pdf.Forms.TextBoxField textBox = new Aspose.Pdf.Forms.TextBoxField(page, fieldRect);
            textBox.PartialName = "txtName";
            textBox.Value = "Enter name";

            // Add the field to the document's form (page index is 1‑based)
            doc.Form.Add(textBox, 1);

            // Save the PDF containing the XFA form field
            doc.Save("xfa_with_fields.pdf");

            // Flatten the form fields so they become static content
            doc.Form.Flatten();

            // Save the flattened PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}