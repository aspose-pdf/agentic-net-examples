using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";

        // If the source PDF does not exist, create a simple one-page document.
        if (!File.Exists(inputPath))
        {
            using (Document tempDoc = new Document())
            {
                tempDoc.Pages.Add();                     // 1‑based page indexing
                tempDoc.Save(inputPath);                 // Save as PDF
            }
        }

        // Open the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the first page (pages are 1‑based).
            Page page = doc.Pages[1];

            // Define the field rectangle. Fully qualify to avoid ambiguity.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field on the page.
            TextBoxField textField = new TextBoxField(page, rect);
            textField.Name = "UserNameField";

            // Set the tooltip (displayed as a hover hint in PDF viewers).
            textField.AlternateName = "Enter your full name (e.g., John Doe)";

            // Add the field to the document's form.
            doc.Form.Add(textField);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}