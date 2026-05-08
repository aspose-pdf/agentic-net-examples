using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "signed_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Define the rectangle where the signature field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            SignatureField signatureField = new SignatureField(firstPage, rect);

            // Set a name and tooltip for the field (optional but recommended)
            signatureField.Name = "Signature1";
            signatureField.AlternateName = "Sign Here";

            // Add the field to the document's form collection
            doc.Form.Add(signatureField);

            // Save the modified PDF (still PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field saved to '{outputPath}'.");
    }
}