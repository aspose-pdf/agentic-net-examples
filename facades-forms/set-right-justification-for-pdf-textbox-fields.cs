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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (pdfDoc.Form != null && pdfDoc.Form.Fields != null)
            {
                // Iterate over all form fields
                foreach (var field in pdfDoc.Form.Fields)
                {
                    // Process only TextBox fields
                    if (field is TextBoxField textBox)
                    {
                        // Set right justification for the textbox
                        textBox.TextHorizontalAlignment = HorizontalAlignment.Right;
                    }
                }
            }

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
