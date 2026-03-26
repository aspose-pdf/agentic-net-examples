using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDocx = "output.docx";
        const string reportPath = "conversion_report.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        using (Document pdfDoc = new Document(inputPdf))
        {
            int pageCount = pdfDoc.Pages.Count;
            long inputSize = new FileInfo(inputPdf).Length;

            // Convert PDF to DOCX
            var docxOptions = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocx, docxOptions);

            long outputSize = new FileInfo(outputDocx).Length;

            stopwatch.Stop();

            var report = new
            {
                InputFile = inputPdf,
                OutputFile = outputDocx,
                InputSizeBytes = inputSize,
                OutputSizeBytes = outputSize,
                PageCount = pageCount,
                ConversionTimeMs = stopwatch.ElapsedMilliseconds
            };

            string json = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(reportPath, json);
            Console.WriteLine($"Conversion completed. Report saved to {reportPath}");
        }
    }
}