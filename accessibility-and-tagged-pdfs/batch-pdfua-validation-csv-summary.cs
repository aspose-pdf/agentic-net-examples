using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input directory containing PDF files.
        // If a command‑line argument is provided, use it; otherwise use the current directory.
        string inputDirectory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Path for the summary CSV file.
        string summaryCsvPath = Path.Combine(inputDirectory, "validation_summary.csv");

        try
        {
            using (var csvWriter = new StreamWriter(summaryCsvPath, false))
            {
                // Write CSV header.
                csvWriter.WriteLine("FileName,IsCompliant,LogFilePath");

                // Enumerate all *.pdf files (case‑insensitive) in the directory.
                foreach (string pdfFilePath in Directory.EnumerateFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly))
                {
                    string fileName = Path.GetFileName(pdfFilePath);
                    string logFilePath = Path.ChangeExtension(pdfFilePath, ".xml");

                    try
                    {
                        // Open the PDF document inside a using block for deterministic disposal.
                        using (var document = new Aspose.Pdf.Document(pdfFilePath))
                        {
                            // Validate against PDF/UA 1.0. The method returns true if the document complies.
                            bool isCompliant = document.Validate(logFilePath, Aspose.Pdf.PdfFormat.PDF_UA_1);

                            // Write the result to the CSV.
                            csvWriter.WriteLine($"{fileName},{isCompliant},{logFilePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // If validation fails (e.g., corrupted PDF), record the error.
                        csvWriter.WriteLine($"{fileName},Error,\"{ex.Message}\"");
                    }
                }
            }

            Console.WriteLine($"Validation completed. Summary CSV saved to: {summaryCsvPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create summary CSV: {ex.Message}");
        }
    }
}