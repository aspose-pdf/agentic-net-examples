using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        string inputDirectory;
        if (args.Length > 0)
        {
            inputDirectory = args[0];
        }
        else
        {
            inputDirectory = "pdfs";
        }

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine("Directory not found: " + inputDirectory);
            return;
        }

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in directory: " + inputDirectory);
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputFileName = fileNameWithoutExt + "_clean.pdf";

            using (PdfFileSignature signature = new PdfFileSignature())
            {
                signature.BindPdf(pdfPath);
                signature.RemoveSignatures();
                signature.Save(outputFileName);
            }

            Console.WriteLine("Processed: " + pdfPath + " -> " + outputFileName);
        }

        Console.WriteLine("Signature removal completed.");
    }
}