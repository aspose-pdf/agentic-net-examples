using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputFolder = "input_pdfs";
        const string outputFolder = "output_pdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            string fileName = Path.GetFileName(pdfFile);
            // Determine target page size based on file name (A4 or Letter)
            bool useA4 = fileName.IndexOf("A4", StringComparison.OrdinalIgnoreCase) >= 0;
            float targetWidth = useA4 ? 595f : 612f;   // points
            float targetHeight = useA4 ? 842f : 792f;  // points

            using (Document doc = new Document(pdfFile))
            {
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    page.PageInfo.Width = targetWidth;
                    page.PageInfo.Height = targetHeight;
                }

                string outputPath = Path.Combine(outputFolder, fileName);
                doc.Save(outputPath);
                Console.WriteLine($"Processed '{fileName}' -> '{outputPath}'");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
