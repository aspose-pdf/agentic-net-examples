using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "booklet_output.pdf"; // final booklet

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Step 1: Load the PDF and rotate all pages to landscape (90°)
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the page editor (facade)
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(doc);

            // Rotate every page 90 degrees clockwise
            pageEditor.Rotation = 90; // valid values: 0, 90, 180, 270
            pageEditor.ApplyChanges(); // apply rotation to the bound document

            // Step 2: Save the rotated PDF into a memory stream
            using (MemoryStream rotatedStream = new MemoryStream())
            {
                doc.Save(rotatedStream);
                rotatedStream.Position = 0; // reset for reading

                // Step 3: Create a booklet from the rotated PDF
                // PdfFileEditor does NOT implement IDisposable, so do NOT use a using block
                PdfFileEditor fileEditor = new PdfFileEditor();

                using (MemoryStream bookletStream = new MemoryStream())
                {
                    // MakeBooklet reads from the input stream and writes to the output stream
                    bool success = fileEditor.MakeBooklet(rotatedStream, bookletStream);
                    if (!success)
                    {
                        Console.Error.WriteLine("Failed to create booklet.");
                        return;
                    }

                    // Write the resulting booklet to the final file
                    File.WriteAllBytes(outputPath, bookletStream.ToArray());
                }
            }
        }

        Console.WriteLine($"Booklet created successfully at '{outputPath}'.");
    }
}