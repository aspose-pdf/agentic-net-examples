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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Extract and list all existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"- Name: {sigField.Name}, Page: {sigField.PageIndex}, Rect: {sigField.Rect}");
                }
            }

            // Define the rectangle for the new signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a new empty signature field on the first page
            Page firstPage = doc.Pages[1]; // 1‑based indexing
            SignatureField newSignature = new SignatureField(firstPage, rect)
            {
                Name = "NewSignatureField" // optional identifier
            };

            // Add the new field to the form (no need to specify page number because we used the page constructor)
            doc.Form.Add(newSignature);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}