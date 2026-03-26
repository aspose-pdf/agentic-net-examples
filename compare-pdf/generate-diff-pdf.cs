using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

public class Program
{
    public static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputFolder = "DiffResults";
        const string outputFileName = "diff.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);
        Directory.SetCurrentDirectory(outputFolder);

        try
        {
            using (Document document1 = new Document(firstPdfPath))
            using (Document document2 = new Document(secondPdfPath))
            {
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(document1, document2, outputFileName);
            }

            Console.WriteLine($"Diff PDF saved as '{outputFileName}' in folder '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
