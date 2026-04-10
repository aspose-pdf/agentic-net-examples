using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "signed.pdf";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Detect whether the PDF already contains a digital signature
            PdfFileSignature sigFacade = new PdfFileSignature();
            sigFacade.BindPdf(inputPath);
            bool hasSignature = sigFacade.ContainsSignature();

            if (hasSignature)
                Console.WriteLine("Digital signature detected – modifications will be applied preserving the signature.");
            else
                Console.WriteLine("No digital signature found – proceeding with modifications.");

            // Modify pages using PdfPageEditor while preserving signature integrity
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example: rotate page 2 by 90 degrees (pages are 1‑based)
                editor.ProcessPages = new int[] { 2 };
                editor.Rotation = 90; // valid values: 0, 90, 180, 270
                editor.ApplyChanges(); // apply the changes to the document
            }

            // Save the modified PDF. The facade performs an incremental update,
            // which keeps existing signatures valid as long as the signed content is unchanged.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}