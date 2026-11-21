using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp (cross‑platform, no GDI+ required)
            TextStamp stamp = new TextStamp(stampText)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity             = 0.5,
                Background          = false
            };

            // Apply the stamp to every page – Page.AddStamp is the correct API
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Instantiate PdfContentEditor and ensure it is disposed in a finally block
            PdfContentEditor editor = null;
            try
            {
                editor = new PdfContentEditor();
                // (If additional editing were required, it would be performed here.
                //  For this example we only demonstrate proper disposal.)
            }
            finally
            {
                // PdfContentEditor implements IDisposable; dispose it to free native resources
                if (editor != null)
                    editor.Dispose();
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}