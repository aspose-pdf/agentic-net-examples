using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        string inputDir = "InputPdfs";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Directory not found: {inputDir}");
            return;
        }

        // Get all PDF file paths in the directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);

        // Thread‑safe dictionary to hold extracted text keyed by file name
        var extractedTexts = new ConcurrentDictionary<string, string>();

        // Parallel extraction using Task Parallel Library
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load each PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Use TextAbsorber to extract all text from the document
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);
                    string text = absorber.Text;

                    // Store the result
                    extractedTexts[Path.GetFileName(pdfPath)] = text;
                }
            }
            catch (Exception ex)
            {
                // Record an empty string on error and log the issue
                extractedTexts[Path.GetFileName(pdfPath)] = string.Empty;
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });

        // Output the extracted text for each file
        foreach (var kvp in extractedTexts)
        {
            Console.WriteLine($"--- {kvp.Key} ---");
            Console.WriteLine(kvp.Value);
            Console.WriteLine();
        }
    }
}