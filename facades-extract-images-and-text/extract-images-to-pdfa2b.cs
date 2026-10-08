using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa2b.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document srcDoc = new Document(inputPath))
            {
                // Create a new PDF document that will become PDF/A‑2b
                using (Document outDoc = new Document())
                {
                    // Add a page to host the extracted images
                    outDoc.Pages.Add();
                    Aspose.Pdf.Page targetPage = outDoc.Pages[1];

                    // Extract each image from the source PDF and place it on the new page
                    foreach (Aspose.Pdf.Page srcPage in srcDoc.Pages)
                    {
                        foreach (Aspose.Pdf.XImage img in srcPage.Resources.Images)
                        {
                            // Write the image to a memory stream
                            using (MemoryStream ms = new MemoryStream())
                            {
                                img.Save(ms);
                                ms.Position = 0;

                                // Create an ImageStamp from the stream
                                ImageStamp stamp = new ImageStamp(ms)
                                {
                                    // Center the image on the page
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    VerticalAlignment   = VerticalAlignment.Center
                                    // No XPosition/YPosition needed; alignment handles placement
                                };

                                // Add the stamp (image) to the target page
                                targetPage.AddStamp(stamp);
                            }
                        }
                    }

                    // Convert the document to PDF/A‑2b compliance
                    outDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                    // Save the PDF/A‑2b document
                    outDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"PDF/A‑2b document created: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}