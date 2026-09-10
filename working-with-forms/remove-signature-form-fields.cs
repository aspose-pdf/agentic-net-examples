using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_signatures.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the form object
            Form form = doc.Form;

            // Collect all signature fields (SignatureField derives from WidgetAnnotation)
            var signatureFields = form.OfType<SignatureField>().ToList();

            // Delete each signature field from the form
            foreach (SignatureField sigField in signatureFields)
            {
                form.Delete(sigField);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}