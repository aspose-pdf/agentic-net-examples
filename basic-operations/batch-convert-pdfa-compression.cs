using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create sample PDFs
        for (int i = 1; i <= 3; i++)
        {
            string inputFile = "input" + i + ".pdf";
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Sample PDF " + i);
                page.Paragraphs.Add(tf);
                doc.Save(inputFile);
            }
        }

        // Batch conversion to PDF/A-1b with compression
        string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        string[] outputFiles = new string[3];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];
            string outputPath = "output" + (i + 1) + "_pdfa.pdf";
            outputFiles[i] = outputPath;

            using (Document doc = new Document(inputPath))
            {
                // Optimize resources to reduce size (high compression)
                doc.OptimizeResources();

                using (MemoryStream logStream = new MemoryStream())
                {
                    doc.Convert(logStream, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                }

                doc.Save(outputPath);
            }
        }

        // Summary report
        Console.WriteLine("Batch conversion completed. Summary:");
        for (int i = 0; i < inputFiles.Length; i++)
        {
            Console.WriteLine($"Input: {inputFiles[i]} => Output: {outputFiles[i]}");
        }
    }
}
