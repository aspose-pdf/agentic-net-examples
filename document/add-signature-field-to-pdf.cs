using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used in DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_signed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the signature field will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create the signature field and configure its appearance
            SignatureField sigField = new SignatureField(page, rect)
            {
                // Internal name used by the PDF form
                PartialName = "UserSignature",
                // Light gray border to match typical document styling
                Color = Aspose.Pdf.Color.LightGray,
                // Placeholder text appearance (e.g., "Sign Here")
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.DarkGray)
            };

            // Add the signature field to the document's AcroForm collection
            doc.Form.Add(sigField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}
