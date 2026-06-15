using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Define the rectangle where the text field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a numeric text field (NumberField) on the first page
            NumberField numField = new NumberField(doc, fieldRect)
            {
                // Allow only digits (default, but set explicitly for clarity)
                AllowedChars = "0123456789",
                // Align text to the right for numeric entry
                TextHorizontalAlignment = HorizontalAlignment.Right,
                // Optional: give the field a name
                Name = "AmountField"
            };

            // Add the field to the page's annotations collection
            doc.Pages[1].Annotations.Add(numField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with right-aligned numeric field: {outputPath}");
    }
}