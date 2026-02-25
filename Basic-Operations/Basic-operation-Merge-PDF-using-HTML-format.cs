using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class HtmlToPdfMerge
{
    static void Main()
    {
        // Directory that contains the HTML files
        string dataDir = @"C:\Data";

        // HTML source files to be merged
        string[] htmlFiles = {
            Path.Combine(dataDir, "input1.html"),
            Path.Combine(dataDir, "input2.html")
        };

        // Output merged PDF file
        string outputPdf = Path.Combine(dataDir, "merged.pdf");

        // Convert each HTML file to an in‑memory PDF document
        var pdfDocs = new List<Aspose.Pdf.Document>();
        foreach (string htmlPath in htmlFiles)
        {
            if (!File.Exists(htmlPath))
            {
                Console.WriteLine($"HTML file not found: {htmlPath}");
                return;
            }

            // Load HTML and convert to PDF
            var loadOptions = new Aspose.Pdf.HtmlLoadOptions();
            var pdfDoc = new Aspose.Pdf.Document(htmlPath, loadOptions);
            pdfDocs.Add(pdfDoc);
        }

        // Merge the PDF documents using PdfFileEditor
        var destination = new Aspose.Pdf.Document();
        var editor = new Aspose.Pdf.Facades.PdfFileEditor();
        editor.Concatenate(pdfDocs.ToArray(), destination);

        // Save the merged PDF to disk
        destination.Save(outputPdf);
        Console.WriteLine($"Merged PDF saved to: {outputPdf}");
    }
}