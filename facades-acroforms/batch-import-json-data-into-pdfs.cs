using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the PDFs and their matching JSON files
        const string inputDirectory = @"C:\InputFiles";
        // Directory where the filled PDFs will be written
        const string outputDirectory = @"C:\OutputFiles";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Gather all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF/JSON pair in parallel
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
                string jsonPath = Path.Combine(inputDirectory, pdfFileName + ".json");

                if (!File.Exists(jsonPath))
                {
                    Console.Error.WriteLine($"JSON file not found for PDF '{pdfFileName}'. Skipping.");
                    return;
                }

                // Output file name (original name with suffix)
                string outputPdfPath = Path.Combine(outputDirectory, pdfFileName + "_filled.pdf");

                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Create a Form facade bound to the loaded document
                    using (Form form = new Form(doc))
                    {
                        // Open the JSON stream and import data into the form
                        using (FileStream jsonStream = File.OpenRead(jsonPath))
                        {
                            form.ImportJson(jsonStream);
                        }

                        // Save the updated document
                        doc.Save(outputPdfPath);
                    }
                }

                Console.WriteLine($"Processed: {pdfFileName} -> {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{pdfPath}': {ex.Message}");
            }
        });

        Console.WriteLine("Batch import completed.");
    }
}