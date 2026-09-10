using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (wrapped in a using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1) Extract images that are part of signature fields (if any)
            // -----------------------------------------------------------------
            foreach (var field in pdfDoc.Form)
            {
                if (field is SignatureField sigField)
                {
                    // Extract the signature image as a stream
                    using (Stream imgStream = sigField.ExtractImage())
                    {
                        if (imgStream != null)
                        {
                            string sigImagePath = Path.Combine(
                                outputFolder,
                                $"{sigField.PartialName ?? "signature"}_{Guid.NewGuid()}.png");

                            // Save the extracted stream to a PNG file
                            using (FileStream fileOut = new FileStream(sigImagePath, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(fileOut);
                            }

                            Console.WriteLine($"Signature image saved: {sigImagePath}");
                        }
                    }
                }
            }

            // -----------------------------------------------------------------
            // 2) Extract all other embedded images from each page
            // -----------------------------------------------------------------
            int pageNumber = 1;
            foreach (Page page in pdfDoc.Pages)
            {
                int imageIndex = 1;
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each image
                    string imagePath = Path.Combine(
                        outputFolder,
                        $"page{pageNumber}_img{imageIndex}.png");

                    // XImage.Save expects a Stream, so write to a FileStream
                    using (FileStream imgOut = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        xImg.Save(imgOut);
                    }

                    Console.WriteLine($"Image extracted: {imagePath}");
                    imageIndex++;
                }

                pageNumber++;
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
