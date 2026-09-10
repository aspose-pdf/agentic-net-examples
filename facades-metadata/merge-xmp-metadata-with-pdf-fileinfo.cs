using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "merged_output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the source PDF exists. In the sandbox there is no pre‑existing file,
        // so we create a minimal document on‑the‑fly. This follows the
        // "hardcoded-input-file-generate-inline-first" pattern.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var seed = new Document())
            {
                seed.Pages.Add(); // add a blank page
                seed.Save(inputPath);
            }
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // ---------------------------------------------------------------------
        // Update standard PDF metadata (FileInfo equivalent).
        // ---------------------------------------------------------------------
        pdf.Info.Title    = "Merged Metadata PDF";
        pdf.Info.Author   = "John Doe";
        pdf.Info.Subject  = "Demo of XMP + FileInfo merge";
        pdf.Info.Keywords = "Aspose.Pdf, XMP, Metadata";

        // ---------------------------------------------------------------------
        // Register the Dublin Core (dc) namespace and add XMP properties.
        // The RegisterNamespaceUri call prevents the "Unknown prefix" exception.
        // ---------------------------------------------------------------------
        pdf.Metadata.RegisterNamespaceUri("dc", "http://purl.org/dc/elements/1.1/");
        pdf.Metadata["dc:creator"]      = "John Doe";
        pdf.Metadata["dc:title"]        = "Merged Metadata PDF";
        pdf.Metadata["dc:description"]  = "Document with combined XMP and FileInfo metadata";

        // Save the merged PDF. All standard metadata and XMP packet are persisted.
        pdf.Save(outputPath);
        Console.WriteLine($"Successfully saved to '{outputPath}'.");
    }
}
