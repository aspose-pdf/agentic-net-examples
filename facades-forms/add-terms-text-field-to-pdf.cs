using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "TermsField.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least one page
        if (doc.Pages.Count == 0)
            doc.Pages.Add();

        // Define the rectangle for the new text field (lower‑left X,Y – upper‑right X,Y)
        // Here we place it on page 1 at (100,100) – (300,150)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

        // Create the text field (TextBoxField) and give it a partial name "Terms"
        TextBoxField termsField = new TextBoxField(doc.Pages[1], rect)
        {
            PartialName = "Terms"
        };

        // Set the field's font to Arial, size 12, black colour
        // DefaultAppearance expects a System.Drawing.Color, so fully qualify it
        termsField.DefaultAppearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

        // Optionally set a default value (placeholder text)
        termsField.Value = "Enter terms here...";

        // Add the field to the document's form on page 1
        doc.Form.Add(termsField, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with Terms field saved to '{outputPath}'.");
    }
}