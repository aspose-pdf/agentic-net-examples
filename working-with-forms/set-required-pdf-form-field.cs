using System;
using System.IO;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";
        const string validationLog = "validation.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Access the AcroForm object
                Aspose.Pdf.Forms.Form form = doc.Form;

                if (form == null || form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the document.");
                }
                else
                {
                    // Name of the field to be marked as required
                    const string fieldName = "CustomerName";

                    // Retrieve the field (the indexer returns a WidgetAnnotation, cast to Field)
                    Aspose.Pdf.Forms.Field field = doc.Form[fieldName] as Aspose.Pdf.Forms.Field;

                    if (field != null)
                    {
                        // Mark the field as required; PDF viewers will draw a red border when the field is empty
                        field.Required = true;

                        // Ensure the field is empty to trigger the visual cue after form submission
                        field.Value = string.Empty;
                    }
                    else
                    {
                        Console.WriteLine($"Field '{fieldName}' not found.");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");

                // Validate the PDF (generates a log file; using PDF/A‑1B as an example format)
                bool isValid = doc.Validate(validationLog, Aspose.Pdf.PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log: {validationLog})");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}