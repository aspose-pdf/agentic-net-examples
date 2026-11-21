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
        const string outputPath = "output_no_signature.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Collect the full names of all signature fields
            List<string> fieldsToRemove = new List<string>();
            foreach (Field field in doc.Form)
            {
                // Signature fields are represented by SignatureField class
                if (field is SignatureField)
                {
                    fieldsToRemove.Add(field.FullName);
                }
            }

            // Remove the collected signature fields from the form collection
            foreach (string name in fieldsToRemove)
            {
                // The Form class provides a Delete method for removing fields
                doc.Form.Delete(name);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}
