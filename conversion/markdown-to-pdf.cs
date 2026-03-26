using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string dataDir = "YOUR_DATA_DIRECTORY";
        string mdFile = Path.Combine(dataDir, "sample.md");
        string pdfFile = Path.Combine(dataDir, "sample.pdf");

        if (!File.Exists(mdFile))
        {
            Console.Error.WriteLine($"Markdown file not found: {mdFile}");
            return;
        }

        MdLoadOptions loadOptions = new MdLoadOptions();
        using (Document pdfDocument = new Document(mdFile, loadOptions))
        {
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"Converted '{mdFile}' to PDF '{pdfFile}'.");
    }
}