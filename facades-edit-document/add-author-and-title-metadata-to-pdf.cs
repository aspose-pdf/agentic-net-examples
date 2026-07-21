using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPath))
        {
            var emptyDoc = new Document();
            emptyDoc.Pages.Add(); // add a blank page so the file is not completely empty
            emptyDoc.Save(inputPath);
        }

        // Load the PDF document.
        var doc = new Document(inputPath);

        // ---------------------------------------------------------------------
        // NOTE: The XMP metadata classes (Aspose.Pdf.Xmp) are not available in the
        // version of Aspose.Pdf referenced by this project. To keep the code
        // functional we set the standard PDF Info dictionary, which is the
        // most widely supported way to store author and title information.
        // If a newer Aspose.Pdf version (with the Xmp assembly) is added, the
        // XMP block can be re‑enabled by restoring the using directive and the
        // three lines below that manipulate doc.XmpMetadata.
        // ---------------------------------------------------------------------

        // Add Author and Title to the standard PDF Info dictionary.
        doc.Info.Author = "John Doe";
        doc.Info.Title = "Project Plan";

        // Save the PDF with the updated metadata.
        doc.Save(outputPath, SaveFormat.Pdf);

        Console.WriteLine($"Metadata added and saved to '{outputPath}'.");
    }
}
