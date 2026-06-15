using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithICC
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output.pdf";
        const string replacementImgPath = "replacement_with_icc.jpg"; // JPEG that already contains an ICC profile
        const string iccProfilePath    = "sRGB.icc"; // ICC profile to embed in the PDF (optional)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(replacementImgPath))
        {
            Console.Error.WriteLine($"Replacement image not found: {replacementImgPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            for (int pageIdx = 1; pageIdx <= pdfDoc.Pages.Count; pageIdx++)
            {
                Page page = pdfDoc.Pages[pageIdx];

                // XImageCollection does not support dictionary enumeration – use foreach
                int imgIndex = 1; // collection indices are also 1‑based
                foreach (XImage img in page.Resources.Images)
                {
                    // Replace the current image with the replacement that already carries an ICC profile
                    using (FileStream replStream = File.OpenRead(replacementImgPath))
                    {
                        // XImageCollection.Replace expects the index (1‑based) and a stream containing the new image data
                        page.Resources.Images.Replace(imgIndex, replStream);
                    }
                    imgIndex++;
                }
            }

            // Optional: embed an ICC profile for the whole PDF (color management)
            // This uses PdfFormatConversionOptions.IccProfileFileName property.
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                IccProfileFileName = iccProfilePath
            };
            // Convert the document with the specified ICC profile (no other conversion changes)
            pdfDoc.Convert(convOptions);

            // Save the modified PDF (lifecycle rule: save inside using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with replaced images and ICC profile: {outputPdfPath}");
    }
}