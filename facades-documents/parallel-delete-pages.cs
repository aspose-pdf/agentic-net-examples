using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        List<string> inputFiles = new List<string>();
        inputFiles.Add("doc1.pdf");
        inputFiles.Add("doc2.pdf");
        inputFiles.Add("doc3.pdf");

        // Define which pages to delete for each input file (1‑based indexing)
        Dictionary<string, int[]> pagesMap = new Dictionary<string, int[]>();
        pagesMap.Add("doc1.pdf", new int[] { 2, 3 });
        pagesMap.Add("doc2.pdf", new int[] { 1 });
        pagesMap.Add("doc3.pdf", new int[] { 5 });

        Parallel.ForEach(inputFiles, inputPath =>
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine("File not found: " + inputPath);
                return;
            }

            int[] pagesToDelete;
            if (!pagesMap.TryGetValue(inputPath, out pagesToDelete))
            {
                Console.Error.WriteLine("No page list for: " + inputPath);
                return;
            }

            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_out.pdf";

            PdfFileEditor fileEditor = new PdfFileEditor();
            bool result = fileEditor.Delete(inputPath, pagesToDelete, outputFileName);
            if (result)
            {
                Console.WriteLine("Deleted pages from " + inputPath + " -> " + outputFileName);
            }
            else
            {
                Console.Error.WriteLine("Failed to delete pages from " + inputPath);
            }
        });
    }
}
