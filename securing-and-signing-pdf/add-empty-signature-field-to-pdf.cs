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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Prevent automatic removal of signature fields
            doc.EnableSignatureSanitization = false;

            // List all existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (var field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"- Name: {sigField.Name}, Page: {sigField.PageIndex}, Rect: {sigField.Rect}");
                }
            }

            // Define rectangle for the new signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new empty signature field on the document
            SignatureField newSignature = new SignatureField(doc, rect);
            newSignature.Name = "NewSignatureField";

            // Add the new field to the form
            doc.Form.Add(newSignature);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with new signature field: '{outputPath}'.");
    }
}