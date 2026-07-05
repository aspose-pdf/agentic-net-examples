using System;
using System.IO;
using System.Collections.Generic;
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
            // Gather all signature fields (subtype 'Signature')
            List<SignatureField> signatureFields = new List<SignatureField>();
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField sigField)
                {
                    signatureFields.Add(sigField);
                }
            }

            // Remove each signature field from the form
            foreach (SignatureField sigField in signatureFields)
            {
                doc.Form.Delete(sigField);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}
