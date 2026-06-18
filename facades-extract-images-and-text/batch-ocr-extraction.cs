using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class BatchOcrExtraction
{
    public static async Task Main(string[] args)
    {
        // Create sample PDFs (simulating files in Azure Blob container)
        string inputFolder = "input";
        Directory.CreateDirectory(inputFolder);
        for (int i = 1; i <= 2; i++)
        {
            string samplePath = Path.Combine(inputFolder, $"sample{i}.pdf");
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                // Add sample text to the first page (1‑based indexing)
                TextFragment fragment = new TextFragment($"This is sample PDF number {i}.");
                doc.Pages[1].Paragraphs.Add(fragment);
                doc.Save(samplePath);
            }
        }

        // Process up to four PDF files (evaluation mode limit)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        int maxFiles = Math.Min(pdfFiles.Length, 4);
        for (int index = 0; index < maxFiles; index++)
        {
            string pdfPath = pdfFiles[index];

            // Open PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Extract text using TextAbsorber
                TextAbsorber textAbsorber = new TextAbsorber();
                pdfDocument.Pages.Accept(textAbsorber);
                string extractedText = textAbsorber.Text;

                // Save extracted text to a .txt file
                string txtPath = Path.ChangeExtension(pdfPath, ".txt");
                File.WriteAllText(txtPath, extractedText);
                Console.WriteLine($"Extracted text saved to: {txtPath}");
            }
        }

        await Task.CompletedTask;
    }
}
