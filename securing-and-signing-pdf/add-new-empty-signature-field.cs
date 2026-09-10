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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // List existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"- Name: {sigField.Name}, Page: {sigField.PageIndex}, Rect: {sigField.Rect}");
                }
            }

            // Define rectangle for the new signature field (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a new empty signature field on the first page
            SignatureField newSignature = new SignatureField(doc.Pages[1], rect);
            newSignature.Name = "NewSignature"; // optional field name

            // Add the new signature field to the form (page number is 1‑based)
            doc.Form.Add(newSignature, 1);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}