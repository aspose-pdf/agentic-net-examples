using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text; // for SimpleFontSubstitution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page11.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Register font substitution so that any missing font is rendered with Comic Sans MS.
        // Add entries for the most common base‑14 fonts. Additional fonts can be added as needed.
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Times-Roman", "Comic Sans MS"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Comic Sans MS"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Courier", "Comic Sans MS"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Comic Sans MS"));
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("ZapfDingbats", "Comic Sans MS"));

        // Load the PDF document with deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Verify the document has at least 11 pages (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count < 11)
            {
                Console.Error.WriteLine("The PDF does not contain 11 pages.");
                return;
            }

            // Create a PNG device with a resolution of 300 DPI.
            var pngDevice = new PngDevice(new Resolution(300));

            // Convert the 11th page to PNG and write to a file.
            using (FileStream imageStream = new FileStream(outputPng, FileMode.Create))
            {
                pngDevice.Process(doc.Pages[11], imageStream);
            }
        }

        Console.WriteLine($"Page 11 has been saved as PNG to '{outputPng}'.");
    }
}
