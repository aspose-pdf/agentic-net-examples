using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string extractedImagePath = "extractedImage.png";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // -------------------------------------------------
            // 1. Extract the first embedded image (signature image) using PdfFileSignature
            // -------------------------------------------------
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdfPath);

                // Retrieve the list of signature names; if none, skip extraction
                var signatureNames = pdfSignature.GetSignatureNames(false);
                if (signatureNames != null && signatureNames.Count > 0)
                {
                    // Use the first signature name
                    var firstSignature = signatureNames[0];

                    // Extract the image associated with the signature (may be null)
                    Stream? imageStream = pdfSignature.ExtractImage(firstSignature);
                    if (imageStream != null)
                    {
                        // Save the extracted image to a file
                        using (FileStream fileStream = new FileStream(extractedImagePath, FileMode.Create, FileAccess.Write))
                        {
                            imageStream.CopyTo(fileStream);
                        }
                        Console.WriteLine($"Image extracted to: {extractedImagePath}");
                    }
                    else
                    {
                        Console.WriteLine("No image found for the first signature.");
                    }
                }
                else
                {
                    Console.WriteLine("No signatures found in the document; image extraction skipped.");
                }
            }

            // -------------------------------------------------
            // 2. Add a page stamp and change its position
            // -------------------------------------------------
            // Ensure the document has at least two pages for demonstration
            if (pdfDocument.Pages.Count >= 2)
            {
                // Create a stamp using page 1 as the source
                PdfPageStamp pageStamp = new PdfPageStamp(pdfDocument.Pages[1]);

                // Set desired position (coordinates are measured from the bottom‑left corner)
                pageStamp.XIndent = 100; // horizontal offset from the left edge
                pageStamp.YIndent = 150; // vertical offset from the bottom edge

                // Optionally adjust size (example: half the original width/height)
                pageStamp.Width = pdfDocument.Pages[1].PageInfo.Width / 2;
                pageStamp.Height = pdfDocument.Pages[1].PageInfo.Height / 2;

                // Apply the stamp to page 2
                pdfDocument.Pages[2].AddStamp(pageStamp);
                Console.WriteLine("Stamp added to page 2 with new position.");
            }
            else
            {
                Console.WriteLine("Document has fewer than 2 pages; stamp operation skipped.");
            }

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}