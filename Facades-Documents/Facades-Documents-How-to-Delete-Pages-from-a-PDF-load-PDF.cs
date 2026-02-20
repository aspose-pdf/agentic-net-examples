using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // args: inputPdfPath outputPdfPath pagesToDelete (comma‑separated, 1‑based)
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: DeletePages <inputPdf> <outputPdf> <pagesToDelete>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArg = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        int[] pagesToDelete;
        try
        {
            pagesToDelete = Array.ConvertAll(pagesArg.Split(','), s => int.Parse(s.Trim()));
        }
        catch
        {
            Console.Error.WriteLine("Error: Invalid page numbers format.");
            return;
        }

        try
        {
            // Create the facade and bind the PDF (load rule)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                // Delete pages in descending order to keep indices valid
                Array.Sort(pagesToDelete);
                for (int i = pagesToDelete.Length - 1; i >= 0; i--)
                {
                    int pageNum = pagesToDelete[i];
                    if (pageNum < 1 || pageNum > editor.Document.Pages.Count)
                    {
                        Console.WriteLine($"Skipping invalid page number: {pageNum}");
                        continue;
                    }
                    editor.Document.Pages.Delete(pageNum);
                }

                // Save the modified PDF (save rule)
                editor.Save(outputPath);
            }

            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}