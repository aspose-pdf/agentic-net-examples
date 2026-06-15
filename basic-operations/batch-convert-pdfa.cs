using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create sample PDF files (self‑contained example)
        string[] sampleFiles = new string[3];
        for (int i = 0; i < 3; i++)
        {
            string fileName = $"sample{i + 1}.pdf";
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment($"This is sample PDF {i + 1}");
                page.Paragraphs.Add(tf);
                doc.Save(fileName);
            }
            sampleFiles[i] = fileName;
        }

        // Prepare CSV file with header
        string csvPath = "conversion_results.csv";
        File.WriteAllText(csvPath, "SourceFile,OutputFile,LogFile,Success\r\n");

        // Batch conversion to PDF/A‑1b
        foreach (string srcFile in sampleFiles)
        {
            string outputFile = Path.GetFileNameWithoutExtension(srcFile) + "_pdfa.pdf";
            string logFile = Path.GetFileNameWithoutExtension(srcFile) + "_log.txt";

            using (Document doc = new Document(srcFile))
            {
                bool success = doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                if (success)
                {
                    doc.Save(outputFile);
                }
                // Append conversion result to CSV
                string line = $"{srcFile},{outputFile},{logFile},{success}\r\n";
                File.AppendAllText(csvPath, line);
            }
        }

        Console.WriteLine("Batch conversion completed. Results saved to " + csvPath);
    }
}
