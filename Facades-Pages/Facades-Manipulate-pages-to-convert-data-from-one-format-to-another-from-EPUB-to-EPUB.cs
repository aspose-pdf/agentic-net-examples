using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source EPUB file.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input and output EPUB file paths.
        string inputEpub  = Path.Combine(dataDir, "input.epub");
        string outputEpub = Path.Combine(dataDir, "output.epub");

        // Verify that the source file exists.
        if (!File.Exists(inputEpub))
        {
            Console.Error.WriteLine($"Input file not found: {inputEpub}");
            return;
        }

        // Load the EPUB file into a PDF Document using the provided EpubLoadOptions rule.
        EpubLoadOptions loadOptions = new EpubLoadOptions();
        using (Document pdfDoc = new Document(inputEpub, loadOptions))
        {
            // Manipulate pages via the PdfPageEditor facade (also a provided rule).
            using (PdfPageEditor editor = new PdfPageEditor(pdfDoc))
            {
                // Example manipulation: rotate every page 90 degrees.
                // Valid rotation values are 0, 90, 180, 270.
                editor.Rotation = 90;
                editor.ApplyChanges(); // Apply the changes to the underlying document.
            }

            // Save the modified document back to EPUB using the provided EpubSaveOptions rule.
            EpubSaveOptions saveOptions = new EpubSaveOptions();
            pdfDoc.Save(outputEpub, saveOptions);
        }

        Console.WriteLine($"EPUB conversion completed: {outputEpub}");
    }
}