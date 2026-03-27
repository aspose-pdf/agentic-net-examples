using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main(string[] args)
    {
        // Expected arguments: <input.pdf> <output.pdf> <pageNumbersCommaSeparated>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: delete-pdf-pages <input.pdf> <output.pdf> <page1,page2,...>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pagesArgument = args[2];

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse comma‑separated page numbers into an int array
        string[] pageTokens = pagesArgument.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int[] pages = new int[pageTokens.Length];
        for (int i = 0; i < pageTokens.Length; i++)
        {
            pages[i] = int.Parse(pageTokens[i]);
        }

        // Delete the specified pages using PdfFileEditor
        Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();
        bool success = editor.Delete(inputPath, pages, outputPath);

        if (success)
        {
            Console.WriteLine($"Pages deleted successfully. Output saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Failed to delete pages from the PDF.");
        }
    }
}