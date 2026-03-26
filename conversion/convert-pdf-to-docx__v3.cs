using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Set custom metadata
            doc.Info.Title = "Custom Title";
            doc.Info.Author = "Custom Author";

            // Save as DOCX using explicit save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            doc.Save(outputPath, saveOptions);
            Console.WriteLine($"PDF converted to DOCX with metadata: {outputPath}");
        }
    }
}