using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for FdfReader

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string fdfPath   = "annotations.fdf"; // FDF file with annotations
        const string pdfOut    = "annotated_output.pdf"; // final PDF
        const string epubOut   = "annotated_output.epub"; // optional EPUB output

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Open the FDF stream and import its annotations into the PDF
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    // FdfReader reads annotations from the FDF stream and adds them to the document
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // OPTIONAL: Save the annotated document as EPUB (demonstrates use of EpubSaveOptions)
                // Note: EPUB is a different output format; the PDF content (including annotations) is preserved.
                var epubOptions = new EpubSaveOptions
                {
                    // Use the default flow recognition mode; other modes are also available.
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };
                doc.Save(epubOut, epubOptions);

                // Finally, save the annotated document back as PDF
                doc.Save(pdfOut);
            }

            Console.WriteLine($"Annotated PDF saved to '{pdfOut}'.");
            Console.WriteLine($"Annotated EPUB saved to '{epubOut}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}