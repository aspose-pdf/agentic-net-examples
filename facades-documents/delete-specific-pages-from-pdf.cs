using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PDF
        const string sourcePath = "input.pdf";
        const string destinationPath = "output.pdf";

        // Pages to delete (1‑based indexing, inclusive)
        // Example: delete pages 2 and 4
        int[] pagesToDelete = new int[] { 2, 4 };

        // Validate source file existence
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Open input and output streams inside using blocks for deterministic disposal
        using (FileStream inputStream = File.OpenRead(sourcePath))
        using (FileStream outputStream = File.Create(destinationPath))
        {
            // Load the PDF from the input stream using Document (PdfFileEditor has no DeletePages method)
            Document pdfDoc = new Document(inputStream);

            // Delete pages in descending order so that subsequent indices remain valid
            Array.Sort(pagesToDelete);
            for (int i = pagesToDelete.Length - 1; i >= 0; i--)
            {
                int pageNumber = pagesToDelete[i];
                if (pageNumber >= 1 && pageNumber <= pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Delete(pageNumber);
                }
            }

            // Save the modified PDF to the output stream
            pdfDoc.Save(outputStream);
        }

        Console.WriteLine($"Pages deleted and saved to '{destinationPath}'.");
    }
}