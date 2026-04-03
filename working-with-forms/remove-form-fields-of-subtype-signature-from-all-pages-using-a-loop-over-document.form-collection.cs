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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Gather all signature fields (subtype 'Signature')
                List<Field> signatures = new List<Field>();
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is SignatureField)
                    {
                        signatures.Add(field);
                    }
                }

                // Remove each signature field from the form
                foreach (Field sig in signatures)
                {
                    doc.Form.Delete(sig);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
