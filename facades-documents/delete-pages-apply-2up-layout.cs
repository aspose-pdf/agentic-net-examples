using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point
    public static void Main(string[] args)
    {
        // Example file paths – replace with actual paths or streams as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_2up.pdf";

        // Pages to delete (1‑based indexing). Adjust as required.
        int[] pagesToDelete = new int[] { 2, 3 }; // delete page 2 and 3

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Perform the operations using streams and PdfFileEditor
        using (FileStream inputStream = new FileStream(inputPdfPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream afterDeleteStream = new MemoryStream())
        using (MemoryStream finalOutputStream = new MemoryStream())
        {
            // 1. Delete specified pages
            PdfFileEditor editor = new PdfFileEditor();
            bool deleteSuccess = editor.Delete(inputStream, pagesToDelete, afterDeleteStream);
            if (!deleteSuccess)
            {
                Console.Error.WriteLine("Failed to delete pages.");
                return;
            }

            // Reset stream position for the next operation
            afterDeleteStream.Position = 0;

            // 2. Apply 2‑up layout (2 columns, 1 row)
            // This will place two original pages side‑by‑side on each output page.
            bool nupSuccess = editor.MakeNUp(afterDeleteStream, finalOutputStream, 2, 1);
            if (!nupSuccess)
            {
                Console.Error.WriteLine("Failed to create 2‑up layout.");
                return;
            }

            // Reset final stream position before saving to file
            finalOutputStream.Position = 0;

            // Write the result to the output file
            using (FileStream outFile = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                finalOutputStream.CopyTo(outFile);
            }

            Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
        }
    }
}