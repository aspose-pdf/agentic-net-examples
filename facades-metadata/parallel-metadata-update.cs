using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input-pdfs";
        const string outputSuffix = "_updated.pdf";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
                string outputFileName = fileNameWithoutExt + outputSuffix; // simple filename, no directory

                using (Document doc = new Document(pdfPath))
                {
                    // Apply metadata changes
                    doc.Info.Title = "Updated Title";
                    doc.Info.Author = "John Doe";
                    doc.Info.Subject = "Metadata Update Example";
                    doc.Info.Keywords = "Aspose.Pdf;Metadata;Parallel";

                    // Save the updated PDF (simple filename, saved in the same folder as the input)
                    doc.Save(outputFileName);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(pdfPath)} -> {outputFileName}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}
