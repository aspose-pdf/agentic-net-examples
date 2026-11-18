using System;
using System.IO;
using System.Globalization;
using Aspose.Pdf;
using System.Drawing; // Bitmap, Graphics etc. (Image will be fully qualified)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "resized.pdf";
        const string configPath = "config.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        double scaleFactor;
        try
        {
            string factorText = File.ReadAllText(configPath).Trim();
            scaleFactor = double.Parse(factorText, CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read scaling factor: {ex.Message}");
            return;
        }

        if (scaleFactor <= 0)
        {
            Console.Error.WriteLine("Scaling factor must be positive.");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    int imgCount = page.Resources.Images.Count;

                    // XImageCollection is 1‑based as well
                    for (int j = 1; j <= imgCount; j++)
                    {
                        XImage xImg = page.Resources.Images[j];

                        // Export the original image to a memory stream
                        using (MemoryStream srcStream = new MemoryStream())
                        {
                            xImg.Save(srcStream);
                            srcStream.Position = 0;

                            // Load it with System.Drawing to resize (fully qualified to avoid ambiguity)
                            using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(srcStream))
                            {
                                int newWidth = (int)(sysImg.Width * scaleFactor);
                                int newHeight = (int)(sysImg.Height * scaleFactor);

                                using (Bitmap resizedBmp = new Bitmap(sysImg, newWidth, newHeight))
                                {
                                    using (MemoryStream outStream = new MemoryStream())
                                    {
                                        // Preserve original format (PNG, JPEG, etc.)
                                        resizedBmp.Save(outStream, sysImg.RawFormat);
                                        outStream.Position = 0;

                                        // Replace the image in the PDF resources
                                        page.Resources.Images.Replace(j, outStream);
                                    }
                                }
                            }
                        }
                    }
                }

                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Images resized by factor {scaleFactor} and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Processing error: {ex.Message}");
        }
    }
}
