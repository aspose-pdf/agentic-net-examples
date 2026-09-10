using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the source PDF to the PdfPageEditor facade
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve the original rotation of the first page (pages are 1‑based)
            int originalRotation = editor.GetPageRotation(1);
            Console.WriteLine($"Original rotation of page 1: {originalRotation} degrees");

            // Change the page size (example: A4 landscape – 842×595 points)
            editor.PageSize = new PageSize(842, 595);

            // Apply changes and save the modified PDF
            editor.Save(outputPath);
            editor.Close();

            // Verify that the rotation value is unchanged after resizing
            using (PdfPageEditor verifier = new PdfPageEditor())
            {
                verifier.BindPdf(outputPath);
                int newRotation = verifier.GetPageRotation(1);
                Console.WriteLine($"Rotation after resizing: {newRotation} degrees");

                if (originalRotation == newRotation)
                    Console.WriteLine("Rotation unchanged after page size modification.");
                else
                    Console.WriteLine("Rotation changed unexpectedly.");

                verifier.Close();
            }
        }
    }
}