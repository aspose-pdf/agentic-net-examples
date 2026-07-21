using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // WidgetAnnotation base type
using Aspose.Pdf.Forms;        // Form, SignatureField, Field

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
            // Collect all signature fields (SignatureField derives from Field -> WidgetAnnotation)
            List<WidgetAnnotation> signatures = new List<WidgetAnnotation>();
            foreach (WidgetAnnotation field in doc.Form)
            {
                if (field is SignatureField)
                {
                    signatures.Add(field);
                }
            }

            // Remove each collected signature field from the form
            foreach (WidgetAnnotation sig in signatures)
            {
                // Delete expects a Field; SignatureField is a Field, so cast is safe
                doc.Form.Delete((Field)sig);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature fields removed. Saved to '{outputPath}'.");
    }
}