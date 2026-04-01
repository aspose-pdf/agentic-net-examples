using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Runtime.CompilerServices;

public static class PdfDocumentExtensions
{
    public static void AddCreatorTool(this Document pdfDocument, string creator)
    {
        // Use PdfFileInfo facade to set the Creator metadata
        PdfFileInfo fileInfo = new PdfFileInfo();
        fileInfo.BindPdf(pdfDocument);
        fileInfo.Creator = creator;
        // No explicit Save required; the change is applied to the bound document
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string creatorValue = "MyApp Creator";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document document = new Document(inputPath))
        {
            document.AddCreatorTool(creatorValue);
            document.Save(outputPath);
        }

        Console.WriteLine($"Creator set to '{creatorValue}' and saved to '{outputPath}'.");
    }
}