using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            string extractedText = await ExtractTextAsync(inputPath, CancellationToken.None);
            await File.WriteAllTextAsync(outputPath, extractedText, Encoding.UTF8);
            Console.WriteLine($"Text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static Task<string> ExtractTextAsync(string pdfPath, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetText(ms);
                    ms.Position = 0;
                    using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }, cancellationToken);
    }
}