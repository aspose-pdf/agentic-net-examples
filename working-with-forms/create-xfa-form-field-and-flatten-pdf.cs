using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "XfaFlattened.pdf";

        // Create a new PDF document
        Aspose.Pdf.Document doc = new Aspose.Pdf.Document();

        // Add a page to the document
        Aspose.Pdf.Page page = doc.Pages.Add();

        // Define the rectangle where the XFA text field will be placed
        Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

        // Create a text box field (compatible with XFA)
        Aspose.Pdf.Forms.TextBoxField textField = new Aspose.Pdf.Forms.TextBoxField(page, fieldRect);
        textField.PartialName = "CustomerName";
        textField.Value = "John Doe";

        // Add the field to the document's form
        doc.Form.Add(textField);

        // Flatten the form so that fields become static content
        doc.Form.Flatten();

        // Save the resulting PDF
        doc.Save(outputPath);
    }
}