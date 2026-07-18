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

        // Load the existing PDF (lifecycle rule: using block)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add(); // page indexing is 1‑based

            // -------------------------------------------------
            // Create a TextBox field on the new page
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField txtField = new TextBoxField(newPage, txtRect)
            {
                PartialName = "CustomerName", // field name
                Value = "Enter name"          // default display text
            };
            // Add the field to the form on the specific page (page number is 1‑based)
            doc.Form.Add(txtField, newPage.Number);

            // -------------------------------------------------
            // Create a CheckBox field on the same page
            Aspose.Pdf.Rectangle chkRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            CheckboxField chkField = new CheckboxField(newPage, chkRect)
            {
                PartialName = "Subscribe",
                Value = "Off" // unchecked by default
            };
            doc.Form.Add(chkField, newPage.Number);

            // -------------------------------------------------
            // Save the updated PDF (lifecycle rule: Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}