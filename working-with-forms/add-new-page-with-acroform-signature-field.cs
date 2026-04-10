using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            Page newPage = doc.Pages.Add(); // PageCollection.Add() returns the added page

            // Define the rectangle where the field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 200, 50);

            // Create a fresh AcroForm field (SignatureField as an example) on the new page
            SignatureField sigField = new SignatureField(newPage, fieldRect)
            {
                PartialName = "Signature1",          // field name
                Color       = Aspose.Pdf.Color.Blue, // visual border color
                Required    = true                    // make the field required
            };

            // Add the field to the form, specifying the page number (1‑based indexing)
            doc.Form.Add(sigField, doc.Pages.Count);

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with new page and form field saved to '{outputPath}'.");
    }
}