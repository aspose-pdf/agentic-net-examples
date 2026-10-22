using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing PDFs and matching JSON files
        const string inputFolder = @"C:\InputFiles";
        // Folder where the updated PDFs will be saved
        const string outputFolder = @"C:\OutputFiles";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Build a list of PDF/JSON file pairs (same base name)
        var filePairs = Directory.GetFiles(inputFolder, "*.pdf")
            .Select(pdfPath =>
            {
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string jsonPath = Path.Combine(inputFolder, baseName + ".json");
                return File.Exists(jsonPath)
                    ? new { PdfPath = pdfPath, JsonPath = jsonPath, OutputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath)) }
                    : null;
            })
            .Where(pair => pair != null)
            .ToList();

        if (!filePairs.Any())
        {
            Console.WriteLine("No matching PDF/JSON pairs found.");
            return;
        }

        // Process each pair in parallel
        Parallel.ForEach(filePairs, pair =>
        {
            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pair.PdfPath))
                {
                    // Bind the document to a Form facade
                    using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
                    {
                        pdfForm.BindPdf(pdfDoc);
                        // Import data from the JSON file into the form fields (stream overload)
                        using (FileStream jsonStream = File.OpenRead(pair.JsonPath))
                        {
                            pdfForm.ImportJson(jsonStream);
                        }
                    }

                    // Save the updated PDF
                    pdfDoc.Save(pair.OutputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pair.PdfPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pair.PdfPath}: {ex.Message}");
            }
        });

        Console.WriteLine("Batch import completed.");
    }
}
