using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        string folderPath;
        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            folderPath = Directory.GetCurrentDirectory();
        }

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string pdfFile in pdfFiles)
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfFile);
                extractor.ExtractText(Encoding.Unicode);
                string txtFile = Path.ChangeExtension(pdfFile, ".txt");
                extractor.GetText(txtFile);
                extractor.Close();
                Console.WriteLine("Extracted text from '" + Path.GetFileName(pdfFile) + "' to '" + Path.GetFileName(txtFile) + "'.");
            }
        }
    }
}