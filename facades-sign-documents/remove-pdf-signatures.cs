using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        string inputDirectory = args.Length > 0 ? args[0] : "pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Directory not found: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.AllDirectories);
        foreach (string pdfPath in pdfFiles)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_nosig.pdf";

            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(pdfPath);
                pdfSignature.RemoveSignatures();
                pdfSignature.Save(outputFileName);
            }

            Console.WriteLine($"Processed: {pdfPath} -> {outputFileName}");
        }

        Console.WriteLine("Signature removal completed.");
    }
}