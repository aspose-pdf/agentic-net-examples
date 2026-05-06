using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing the "Discount" field
        const string outputPath = "output.pdf";  // Resulting PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in a using block for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages to locate the field named "Discount"
            foreach (Page page in doc.Pages)
            {
                // Annotations collection holds form fields as widget annotations
                foreach (Annotation ann in page.Annotations)
                {
                    // Check for the field name and ensure it is a NumberField
                    if (ann.Name == "Discount" && ann is NumberField numberField)
                    {
                        // Allow only digits and a single decimal point
                        numberField.AllowedChars = "0123456789.";

                        // Optional: limit the maximum length (e.g., 10 characters)
                        // This helps restrict the total number of digits entered.
                        numberField.MaxLen = 10;

                        // Optional: set the field to be a single-line comb with enough slots
                        // for typical monetary values (e.g., "99999.99").
                        // numberField.ForceCombs = true;
                        // numberField.CombNumber = 8; // not a direct property; use FormEditor if needed

                        // Field configured – exit loops
                        break;
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}