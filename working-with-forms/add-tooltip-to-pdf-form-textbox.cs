using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with a form page
        const string outputPdf = "output_with_tooltip.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the text box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field on the document
            TextBoxField txtField = new TextBoxField(doc, rect);

            // Set the tooltip (alternate name) that shows in Adobe Acrobat
            txtField.AlternateName = "Enter date as MM/DD/YYYY";

            // Optionally set a visible name for the field (used in the form hierarchy)
            txtField.Name = "DateField";

            // Add the field to the form
            doc.Form.Add(txtField);

            // Save the modified PDF (output format is PDF because no SaveOptions are supplied)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with tooltip: '{outputPdf}'");
    }
}