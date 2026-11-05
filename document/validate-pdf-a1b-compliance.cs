using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1B compliance. The Validate method writes
                // detailed validation information to the supplied log file and
                // returns a boolean indicating overall compliance.
                bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                if (isValid)
                {
                    Console.WriteLine("Document is PDF/A‑1B compliant.");
                }
                else
                {
                    Console.WriteLine("Document is NOT PDF/A‑1B compliant. See log for details.");
                    // Output the validation errors that were written to the log file.
                    if (File.Exists(logPath))
                    {
                        foreach (string line in File.ReadLines(logPath))
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}
