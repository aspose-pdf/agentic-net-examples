using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdfA = "output_pdfa2b.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Document that will contain the extracted images
        using (Document imgDoc = new Document())
        {
            // Open the source PDF
            using (Document srcDoc = new Document(inputPdf))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= srcDoc.Pages.Count; i++)
                {
                    Page srcPage = srcDoc.Pages[i];

                    // Iterate over each image resource on the page
                    foreach (XImage xImg in srcPage.Resources.Images)
                    {
                        // Add a new page for the image
                        Page newPage = imgDoc.Pages.Add();

                        // Convert the XImage to a stream (PNG is a safe default)
                        MemoryStream imgStream = new MemoryStream();
                        xImg.Save(imgStream, ImageFormat.Png);
                        imgStream.Position = 0;

                        // Create an Aspose.Pdf.Image paragraph that uses the stream as its source
                        Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                        {
                            ImageStream = imgStream,
                            // Fit the image to the whole page
                            FixWidth = newPage.PageInfo.Width,
                            FixHeight = newPage.PageInfo.Height
                        };

                        // Add the image to the page's paragraph collection
                        newPage.Paragraphs.Add(pdfImage);
                    }
                }
            }

            // Ensure at least one page exists if no images were found
            if (imgDoc.Pages.Count == 0)
                imgDoc.Pages.Add();

            // Convert the document to PDF/A‑2b compliance
            imgDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

            // Save the PDF/A‑2b compliant document
            imgDoc.Save(outputPdfA);
        }

        Console.WriteLine($"PDF/A‑2b document created: {outputPdfA}");
    }
}
