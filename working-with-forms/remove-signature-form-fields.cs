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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Collect all signature fields (type Aspose.Pdf.Forms.SignatureField)
            var fieldsToRemove = new List<Field>();
            foreach (Field field in doc.Form)
            {
                if (field is SignatureField)
                {
                    fieldsToRemove.Add(field);
                }
            }

            // Delete the collected signature fields
            foreach (Field field in fieldsToRemove)
            {
                doc.Form.Delete(field);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}
