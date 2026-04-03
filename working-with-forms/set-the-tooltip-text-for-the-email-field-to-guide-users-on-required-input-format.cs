using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF (or a blank PDF)
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the Email field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a TextBoxField (text input) on the document
            TextBoxField emailField = new TextBoxField(doc, fieldRect)
            {
                // The internal name of the field (used for form data)
                PartialName = "Email",

                // Tooltip shown in Adobe Acrobat – guides the user on the required format
                AlternateName = "Enter email in format user@example.com"
            };

            // Add the field to page 1 of the PDF
            doc.Form.Add(emailField, 1);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with Email field tooltip saved to '{outputPdf}'.");
    }
}