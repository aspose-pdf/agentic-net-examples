using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the text field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a numeric text field (NumberField) on the first page
            NumberField numField = new NumberField(doc.Pages[1], fieldRect)
            {
                // Set the field name (optional, useful for form data extraction)
                Name = "AmountField",

                // Align the entered text to the right for numeric values
                TextHorizontalAlignment = HorizontalAlignment.Right,

                // Optional: limit allowed characters to digits and decimal separator
                AllowedChars = "0123456789.,"
            };

            // Add the field to the page's annotations collection
            doc.Pages[1].Annotations.Add(numField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with right-aligned numeric field: {outputPdf}");
    }
}