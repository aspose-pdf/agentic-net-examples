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

        // Load the PDF document (using the prescribed lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Enumerate existing signature fields
            Console.WriteLine("Existing signature fields:");
            foreach (var field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    Console.WriteLine($"- Name: {sigField.Name}, Page: {sigField.PageIndex}, Rect: {sigField.Rect}");
                }
            }

            // Define a rectangle for the new signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a new empty signature field on the first page
            Page firstPage = doc.Pages[1];
            SignatureField newSignature = new SignatureField(firstPage, rect)
            {
                Name = "NewSignatureField"
                // Additional properties (e.g., ReadOnly, Required) can be set here if needed
            };

            // Add the new field to the form (using the Form.Add method)
            doc.Form.Add(newSignature);

            // Save the modified PDF (using the prescribed lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}