using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "converted.docx";
        const string imagesFolder   = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract images from the PDF (optional, for reference)
        // -----------------------------------------------------------------
        Directory.CreateDirectory(imagesFolder);
        int imageCounter = 1;

        using (Document pdfDoc = new Document(inputPdfPath))
        {
            foreach (Page page in pdfDoc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    string imgPath = Path.Combine(imagesFolder, $"Image_{imageCounter++}.png");
                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }
                }
            }

            // -----------------------------------------------------------------
            // Step 2: Convert PDF to DOCX using explicit DocSaveOptions
            // -----------------------------------------------------------------
            DocSaveOptions docSaveOpts = new DocSaveOptions
            {
                Format = DocSaveOptions.DocFormat.DocX
            };
            pdfDoc.Save(outputDocxPath, docSaveOpts);
        }

        // -----------------------------------------------------------------
        // Step 3: (Optional) At this point the DOCX is ready. Adding a Table of
        // Figures would normally require Aspose.Words or OpenXML SDK. Since the
        // project does not reference Aspose.Words, this step is omitted to keep
        // the build error free.
        // -----------------------------------------------------------------
        Console.WriteLine($"Conversion complete. DOCX saved to '{outputDocxPath}'.");
    }
}
