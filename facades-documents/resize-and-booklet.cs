using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.Error.WriteLine("Usage: program.exe <pdf1> <pdf2> ...");
            return;
        }

        string[] inputFiles = args;
        string[] resizedFiles = new string[inputFiles.Length];

        // Resize each PDF to 1024x768
        for (int i = 0; i < inputFiles.Length; i++)
        {
            string inputPath = inputFiles[i];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            string resizedFileName = Path.GetFileNameWithoutExtension(inputPath) + "_resized.pdf";
            using (Document doc = new Document(inputPath))
            {
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    page.PageInfo.Width = 1024.0;
                    page.PageInfo.Height = 768.0;
                }
                doc.Save(resizedFileName);
            }
            resizedFiles[i] = resizedFileName;
        }

        // Concatenate resized PDFs
        string concatenatedFile = "merged.pdf";
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.Concatenate(resizedFiles, concatenatedFile);

        // Create booklet from concatenated PDF
        string bookletFile = "booklet.pdf";
        fileEditor.MakeBooklet(concatenatedFile, bookletFile);

        Console.WriteLine($"Booklet created: {bookletFile}");
    }
}