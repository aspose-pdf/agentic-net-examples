using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF into a Document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Access the form (AcroForm) of the document
            Form form = pdfDoc.Form;

            // Verify that the form contains fields
            if (form != null && form.Count > 0)
            {
                Console.WriteLine($"AcroForm contains {form.Count} fields:");
                foreach (Field field in form)
                {
                    // Output field name and its current value
                    Console.WriteLine($"- {field.Name}: {field.Value}");
                }
            }
            else
            {
                Console.WriteLine("No AcroForm fields found in the PDF.");
            }
        }
    }
}
