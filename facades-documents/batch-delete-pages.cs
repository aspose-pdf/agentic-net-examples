using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string inputDirectory = "input_pdfs";
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        int[] pagesToDelete = new int[] { 2, 3 };
        PdfFileEditor editor = new PdfFileEditor();

        foreach (string filePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);
            string outputFileName = Path.GetFileNameWithoutExtension(fileName) + "_out.pdf";

            bool result = editor.Delete(filePath, pagesToDelete, outputFileName);
            if (result)
            {
                Console.WriteLine($"{fileName} -> {outputFileName} : pages deleted");
            }
            else
            {
                Console.Error.WriteLine($"Failed to delete pages from {fileName}");
            }
        }
    }
}