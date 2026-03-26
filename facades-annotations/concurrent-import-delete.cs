using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string importPath = "source.pdf";
        const string deleteOutputPath = "deleted_output.pdf";
        const string importOutputPath = "imported_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }
        if (!File.Exists(importPath))
        {
            Console.Error.WriteLine("Import PDF not found: " + importPath);
            return;
        }

        Task deleteTask = Task.Run(() =>
        {
            PdfFileEditor editor = new PdfFileEditor();
            int[] pagesToDelete = new int[] { 2 };
            bool result = editor.TryDelete(inputPath, pagesToDelete, deleteOutputPath);
            Console.WriteLine("Delete task completed: " + result);
        });

        Task importTask = Task.Run(() =>
        {
            using (Document targetDoc = new Document(inputPath))
            {
                using (Document sourceDoc = new Document(importPath))
                {
                    targetDoc.Pages.Add(sourceDoc.Pages);
                    targetDoc.Save(importOutputPath);
                }
            }
            Console.WriteLine("Import task completed.");
        });

        Task.WaitAll(deleteTask, importTask);
        Console.WriteLine("Concurrent operations finished.");
    }
}