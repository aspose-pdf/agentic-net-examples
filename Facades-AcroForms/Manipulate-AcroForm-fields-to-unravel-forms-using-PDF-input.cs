using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            // Verify that the input file exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
                return;
            }

            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Check if the document contains an AcroForm
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any AcroForm fields.");
            }
            else
            {
                // Example manipulation: make every field read‑only and clear its value
                foreach (Field field in pdfDocument.Form)
                {
                    field.ReadOnly = true;   // Prevent further editing
                    field.Value = null;      // Remove existing data
                }

                // Disable automatic recalculation for better performance on large forms
                pdfDocument.Form.AutoRecalculate = false;

                // Flatten the form without custom settings (FormFlattenSettings is not required)
                pdfDocument.Form.Flatten();
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
