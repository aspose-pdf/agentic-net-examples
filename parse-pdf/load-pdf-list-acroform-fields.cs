using System;
using System.IO;
using System.Linq; // Needed for Count() / Any()
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Provides access to AcroForm and its fields

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Access the AcroForm of the document
            Form acroForm = pdfDoc.Form;

            // If the document contains a form, enumerate its fields
            if (acroForm != null && acroForm.Fields != null && acroForm.Fields.Any())
            {
                Console.WriteLine("AcroForm fields:");
                foreach (var field in acroForm.Fields)
                {
                    // Field.FullName gives the hierarchical name, Field.Value holds the current value
                    Console.WriteLine($"- {field.FullName}: {field.Value}");
                }
            }
            else
            {
                Console.WriteLine("No AcroForm fields found in the document.");
            }
        }
    }
}
