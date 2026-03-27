using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFilePath = "input.pdf";
        const string outputFilePath = "output.pdf";
        // Pages to delete (1‑based indexing)
        int[] pagesToDelete = new int[] { 2, 3 };

        if (!File.Exists(inputFilePath))
        {
            Console.Error.WriteLine("Input file not found: " + inputFilePath);
            return;
        }

        using (FileStream inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
        using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor editor = new PdfFileEditor();
            bool result = editor.Delete(inputStream, pagesToDelete, outputStream);
            if (result)
            {
                Console.WriteLine("Pages deleted successfully. Output saved to " + outputFilePath);
            }
            else
            {
                Console.Error.WriteLine("Failed to delete pages.");
            }
        }
    }
}