using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        string outputHtml = "merged.html";

        if (!File.Exists(pdfFiles[0]))
        {
            Console.Error.WriteLine($"Not found: {pdfFiles[0]}");
            return;
        }

        try
        {
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    if (!File.Exists(pdfFiles[i]))
                    {
                        Console.Error.WriteLine($"Skipping: {pdfFiles[i]}");
                        continue;
                    }
                    using (Document src = new Document(pdfFiles[i]))
                    {
                        mergedDoc.Pages.Add(src.Pages);
                    }
                }

                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                try
                {
                    mergedDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML → '{outputHtml}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}