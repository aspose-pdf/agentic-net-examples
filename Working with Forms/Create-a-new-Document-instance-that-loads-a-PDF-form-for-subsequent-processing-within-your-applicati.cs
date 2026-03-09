using System;
using System.IO;
using System.Linq;                     // Added for LINQ Count()
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // Optional, if further processing requires Facades APIs

class Program
{
    static void Main()
    {
        const string formPath = "input_form.pdf";

        if (!File.Exists(formPath))
        {
            Console.Error.WriteLine($"File not found: {formPath}");
            return;
        }

        // Load the PDF form directly using Aspose.Pdf.Document.
        // The Document implements IDisposable, so we wrap it in a using block for deterministic disposal.
        using (Document pdfForm = new Document(formPath))
        {
            // At this point the PDF form is loaded and ready for further processing.
            // Example: access the AcroForm (if needed)
            var acroForm = pdfForm.Form;

            // Use explicit null‑checks instead of the null‑conditional operator to avoid CS8978.
            int fieldCount = 0;
            if (acroForm != null && acroForm.Fields != null)
            {
                // Corrected: invoke the LINQ Count() extension method.
                fieldCount = acroForm.Fields.Count();
            }
            Console.WriteLine($"Form fields count: {fieldCount}");

            // Additional processing can be performed here...

            // No explicit save is required unless modifications are made.
            // If you modify and want to persist changes, uncomment the line below:
            // pdfForm.Save("processed_form.pdf");
        }
    }
}
