using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Paths for input and output PDF files
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Check if the document contains an AcroForm
            if (pdfDocument.Form != null && pdfDocument.Form.Count > 0)
            {
                // Disable automatic recalculation for better performance while filling many fields
                pdfDocument.Form.AutoRecalculate = false;

                // Iterate over each form field and set a sample value
                foreach (Field field in pdfDocument.Form)
                {
                    // Set a generic value; specific field types can be handled with more detailed checks
                    field.Value = "Sample";

                    // Set an alternate name (tooltip) for the field
                    field.AlternateName = $"Field {field.FullName}";
                }

                // Flatten the form so that fields become regular page content
                pdfDocument.Form.Flatten();
            }

            // Save the modified PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}