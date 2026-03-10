using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // CryptoAlgorithm, Permissions are in root; this using is optional but allowed

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF containing AcroForm
        const string validationLogPath = "validation.log"; // Where validation report will be written

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Inspect AcroForm fields
            // -----------------------------------------------------------------
            Form acroForm = doc.Form;
            if (acroForm == null || acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
            }
            else
            {
                Console.WriteLine($"AcroForm contains {acroForm.Count} field(s):");
                foreach (Field field in acroForm)
                {
                    // Basic field information
                    string name = field.Name ?? "(unnamed)";
                    string partialName = field.PartialName ?? "(no partial name)";
                    string fullName = field.FullName ?? "(no full name)";
                    bool isReadOnly = field.ReadOnly;
                    bool isRequired = field.Required;
                    string value = field.Value?.ToString() ?? "(null)";

                    Console.WriteLine($"- Field Name: {name}");
                    Console.WriteLine($"  PartialName: {partialName}");
                    Console.WriteLine($"  FullName: {fullName}");
                    Console.WriteLine($"  ReadOnly: {isReadOnly}");
                    Console.WriteLine($"  Required: {isRequired}");
                    Console.WriteLine($"  Value: {value}");

                    // Simple validation: required fields must have a non‑empty value
                    if (isRequired && string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine("  >> Validation Issue: Required field is empty.");
                    }
                }
            }

            // -----------------------------------------------------------------
            // 2. Validate the PDF document structure (e.g., PDF/A‑1B compliance)
            // -----------------------------------------------------------------
            // The Validate method returns true if validation succeeded.
            // The log file will contain detailed messages.
            bool validationResult = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Document validation result: {(validationResult ? "Success" : "Failure")}");
            Console.WriteLine($"Validation log written to: {validationLogPath}");
        }
    }
}