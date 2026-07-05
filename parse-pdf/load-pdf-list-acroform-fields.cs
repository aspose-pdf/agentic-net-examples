using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // For accessing form fields

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
        using (Document doc = new Document(pdfPath))
        {
            // Retrieve the AcroForm object
            Form acroForm = doc.Form;

            // Check if the document contains any form fields
            if (acroForm == null || acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
            }
            else
            {
                Console.WriteLine($"AcroForm contains {acroForm.Count} field(s):");

                // Iterate over each field and display basic information
                foreach (Field field in acroForm)
                {
                    // Field.FullName gives the hierarchical name of the field
                    // field.Value returns the current value (may be null for some field types)
                    Console.WriteLine($"- Name: {field.FullName}, Type: {field.GetType().Name}, Value: {field.Value}");
                }
            }
        }
    }
}