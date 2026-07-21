using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object (creates one if it doesn't exist)
            Form form = doc.Form;

            // Define the rectangle where the signature field will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the document (first page by default)
            SignatureField signatureField = new SignatureField(doc, rect)
            {
                PartialName = "Signature1",      // field name used in scripts or later signing
                AlternateName = "Sign Here"      // tooltip shown in PDF viewers
            };

            // Add the field to the form on page 1 (pages are 1‑based)
            form.Add(signatureField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}