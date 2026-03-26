using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static async Task Main(string[] args)
    {
        const string pdfFileName = "PdfForm.pdf";
        const string xmlFileName = "export.xml";

        // Resolve the PDF path relative to the current working directory and verify it exists.
        string pdfPath = Path.GetFullPath(pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        await ExportFormDataAsync(pdfPath, xmlFileName);
        Console.WriteLine($"Form data exported to '{xmlFileName}'.");
    }

    private static Task ExportFormDataAsync(string pdfPath, string outputXmlPath)
    {
        // The ExportXml method is synchronous; wrap it in Task.Run to keep the UI thread responsive.
        return Task.Run(() =>
        {
            using var form = new Form(pdfPath);
            using var stream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write, FileShare.None);
            form.ExportXml(stream);
        });
    }
}