using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (or blank PDF)  
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the field will appear (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a text box field on the document
            TextBoxField txtField = new TextBoxField(doc, rect);

            // Set the tooltip (displayed as an alternate name in Acrobat)
            txtField.AlternateName = "Enter date in format MM/DD/YYYY";

            // Optionally set a visible name for the field (used in the form hierarchy)
            txtField.Name = "DateField";

            // Add the field to the form
            doc.Form.Add(txtField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}