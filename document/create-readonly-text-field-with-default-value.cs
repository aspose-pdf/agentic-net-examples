using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box form field
            TextBoxField textField = new TextBoxField(page, fieldRect);
            textField.PartialName = "MyTextField";   // field identifier
            textField.Value = "Default text";        // default value displayed in the field
            textField.ReadOnly = true;               // make the field read‑only by default

            // Add the field to the document's form collection (page index is 1‑based)
            doc.Form.Add(textField, 1);

            // Save the PDF with the read‑only text field
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with read‑only text field saved to '{outputPath}'.");
    }
}