using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace ExtractImagesToFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample image file (required for a self‑contained example)
            string sampleImagePath = "sample.png";
            using (Bitmap bitmap = new Bitmap(100, 100))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(System.Drawing.Color.Red);
                }
                bitmap.Save(sampleImagePath, ImageFormat.Png);
            }

            // Step 2: Create a sample PDF that contains the image
            string samplePdfPath = "sample.pdf";
            using (Document document = new Document())
            {
                document.Pages.Add();
                Page page = document.Pages[1];
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                pdfImage.File = sampleImagePath;
                page.Paragraphs.Add(pdfImage);
                document.Save(samplePdfPath);
            }

            // Step 3: Prepare a simple binary file to store image BLOBs
            string blobFilePath = "Images.bin";
            if (File.Exists(blobFilePath))
            {
                File.Delete(blobFilePath);
            }
            using (FileStream blobStream = new FileStream(blobFilePath, FileMode.Create, FileAccess.Write))
            {
                // Step 4: Extract images from the PDF, save to file and store as BLOBs in the binary file
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(samplePdfPath);
                    extractor.ExtractImage();
                    int imageCounter = 1;
                    while (extractor.HasNextImage())
                    {
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            bool extracted = extractor.GetNextImage(imageStream);
                            if (extracted)
                            {
                                byte[] imageBytes = imageStream.ToArray();

                                // Write the length of the image followed by the image bytes to the binary file
                                byte[] lengthPrefix = BitConverter.GetBytes(imageBytes.Length);
                                blobStream.Write(lengthPrefix, 0, lengthPrefix.Length);
                                blobStream.Write(imageBytes, 0, imageBytes.Length);

                                // Also write the image to a file for visual verification
                                string outputFolder = "ExtractedImages";
                                if (!Directory.Exists(outputFolder))
                                {
                                    Directory.CreateDirectory(outputFolder);
                                }
                                string outputFilePath = Path.Combine(outputFolder, $"image_{imageCounter}.png");
                                File.WriteAllBytes(outputFilePath, imageBytes);
                                Console.WriteLine($"Image {imageCounter} extracted, saved to '{outputFilePath}' and stored in binary BLOB file.");
                            }
                        }
                        imageCounter++;
                    }
                }
            }
        }
    }
}
