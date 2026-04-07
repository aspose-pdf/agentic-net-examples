using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_signatures.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect all signature fields present in the form
            List<Field> signatureFields = new List<Field>();
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField)
                {
                    signatureFields.Add(field);
                }
            }

            // Remove each signature field from the form
            foreach (Field sig in signatureFields)
            {
                doc.Form.Delete(sig);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}
