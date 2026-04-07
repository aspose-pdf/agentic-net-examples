using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Resolve the PDF path – allow a command‑line argument or fall back to a default file in the executable folder.
        string pdfPath = args.Length > 0 ? args[0] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invoice.pdf");
        string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "validation.log");

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{pdfPath}'. Please provide a valid path to a ZUGFeRD invoice PDF.");
            return;
        }

        try
        {
            using (Document doc = new Document(pdfPath))
            {
                // 1. Check PDF/A compliance (PDF/A‑3B is required for ZUGFeRD).
                bool isPdfa = doc.IsPdfaCompliant;
                Console.WriteLine($"PDF/A compliant: {isPdfa}");

                // 2. Validate against PDF/A‑3B and write a detailed log.
                bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_3B);
                Console.WriteLine($"PDF/A‑3B validation result: {validationResult}");
                Console.WriteLine($"Validation log written to: {logPath}");

                // 3. Look for an embedded XML file (the ZUGFeRD payload).
                EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;
                int found = 0;
                // The collection is 1‑based; iterate safely up to the count or a small limit.
                for (int i = 1; i <= embeddedFiles.Count && i <= 4; i++)
                {
                    FileSpecification fileSpec = embeddedFiles[i];
                    string name = fileSpec.Name; // Name property holds the original file name.
                    if (!string.IsNullOrEmpty(name) && name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Found embedded XML: {name}");
                        found++;
                    }
                }

                if (found == 0)
                {
                    Console.WriteLine("No embedded XML file found – this PDF is not a ZUGFeRD invoice.");
                }
                else
                {
                    Console.WriteLine($"ZUGFeRD payload detected ({found} XML file(s)).");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}
