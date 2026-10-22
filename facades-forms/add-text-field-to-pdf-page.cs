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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define rectangle coordinates for the text field (replace with actual values)
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 550; // upper‑right Y

            // Create a rectangle that Aspose.Pdf expects (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(llx, lly, urx, ury);

            // Create a text box field (TextBoxField) on page 1
            TextBoxField txtField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "CustomerName",
                // Optional: set default value, font, etc.
                // Value = "",
                // TextState = new TextState { Font = FontRepository.FindFont("Helvetica"), FontSize = 12 }
            };

            // Add the field to the document form (page numbers are 1‑based)
            doc.Form.Add(txtField, 1);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}
