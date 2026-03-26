using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";
        const int startPage = 2;
        const int endPage = 4;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            Aspose.Pdf.Text.TextAbsorber absorber = new Aspose.Pdf.Text.TextAbsorber();

            int lastPage = doc.Pages.Count;
            int actualEnd = endPage < lastPage ? endPage : lastPage;

            for (int pageNumber = startPage; pageNumber <= actualEnd; pageNumber++)
            {
                absorber.Visit(doc.Pages[pageNumber]);
            }

            string extractedText = absorber.Text;
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}
