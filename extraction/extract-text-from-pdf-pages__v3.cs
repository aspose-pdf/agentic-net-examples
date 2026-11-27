using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "PagesText";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document pdfDoc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                // Extract text from the current page
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Save the extracted text to a file named by page number
                string txtPath = Path.Combine(outputDir, $"Page_{i}.txt");
                File.WriteAllText(txtPath, pageText);
            }
        }

        Console.WriteLine("Text extraction completed.");
    }
}