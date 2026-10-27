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
        const string outputPath = "output_no_signature.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect all signature fields first (cannot modify collection while iterating)
            List<Field> signatureFields = new List<Field>();
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField)
                {
                    signatureFields.Add(field);
                }
            }

            // Remove each signature field from the form
            foreach (Field sigField in signatureFields)
            {
                // Form.Delete correctly removes a field from the AcroForm collection.
                // The overload that accepts a Field instance is used here.
                doc.Form.Delete(sigField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}
