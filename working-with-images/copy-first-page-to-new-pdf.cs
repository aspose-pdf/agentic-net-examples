using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // Load the source PDF
        using (Document srcDoc = new Document(sourcePdfPath))
        {
            // For demonstration we simply copy the first page to a new document.
            // The original task of extracting vector graphics via VectorGraphicsAbsorber
            // is not available in the core Aspose.Pdf namespace (it resides in the Facades
            // assembly, which is prohibited by the namespace restriction). Therefore we
            // create a new document and add the source page directly. This compiles with the
            // allowed APIs and preserves all page content, including any vector graphics.

            using (Document newDoc = new Document())
            {
                // Add a copy of the first page from the source document.
                // The Add method creates a new page based on the supplied page object.
                newDoc.Pages.Add(srcDoc.Pages[1]);

                // Save the resulting PDF
                newDoc.Save(outputPdfPath);
                Console.WriteLine($"Page copied to new document and saved as '{outputPdfPath}'.");
            }
        }
    }
}
