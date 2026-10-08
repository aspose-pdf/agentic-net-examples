using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";
        const string ocrOutput = "OcrResults.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the folder for extracted images exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Prepare a text file to collect OCR results
            using (StreamWriter ocrWriter = new StreamWriter(ocrOutput, false))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    int imageIndex = 0;

                    // Iterate over all images on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        imageIndex++;

                        // Export the image to a memory stream in its original format
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            img.Save(imgStream);
                            imgStream.Position = 0;

                            // ---------- OCR (reflection based) ----------
                            // Try to locate Aspose.Pdf.Facades.OcrEngine at runtime.
                            Type ocrEngineType = Type.GetType("Aspose.Pdf.Facades.OcrEngine, Aspose.Pdf");
                            if (ocrEngineType != null)
                            {
                                // Create an instance of OcrEngine
                                object ocrEngine = Activator.CreateInstance(ocrEngineType);

                                // Locate ImageStream type and create an instance wrapping the image stream
                                Type imageStreamType = Type.GetType("Aspose.Pdf.Facades.ImageStream, Aspose.Pdf");
                                if (imageStreamType != null)
                                {
                                    // ImageStream has a constructor that accepts a Stream
                                    object imageStream = Activator.CreateInstance(imageStreamType, imgStream);
                                    // Set the Image property
                                    PropertyInfo imageProp = ocrEngineType.GetProperty("Image");
                                    imageProp?.SetValue(ocrEngine, imageStream);
                                }

                                // Run OCR
                                MethodInfo processMethod = ocrEngineType.GetMethod("Process");
                                bool success = processMethod != null && (bool)processMethod.Invoke(ocrEngine, null);

                                if (success)
                                {
                                    PropertyInfo textProp = ocrEngineType.GetProperty("Text");
                                    string text = textProp?.GetValue(ocrEngine) as string ?? string.Empty;
                                    ocrWriter.WriteLine($"Page {pageNum}, Image {imageIndex}:");
                                    ocrWriter.WriteLine(text);
                                    ocrWriter.WriteLine(new string('-', 40));
                                }
                                else
                                {
                                    ocrWriter.WriteLine($"Page {pageNum}, Image {imageIndex}: OCR failed.");
                                }
                            }
                            else
                            {
                                // OcrEngine type not available – write a placeholder message.
                                ocrWriter.WriteLine($"Page {pageNum}, Image {imageIndex}: OCR engine not found in the referenced Aspose.Pdf version.");
                            }

                            // ---------- Save extracted image ----------
                            // Determine a suitable file extension by inspecting the first bytes of the image.
                            string extension = GetImageExtensionFromStream(imgStream);
                            string imgPath = Path.Combine(outputFolder, $"Page{pageNum}_Img{imageIndex}{extension}");

                            // Reset stream position before copying to file.
                            imgStream.Position = 0;
                            using (FileStream file = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                            {
                                imgStream.CopyTo(file);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine("Image extraction and OCR completed.");
    }

    // Helper method to infer a suitable file extension from the image's header bytes.
    private static string GetImageExtensionFromStream(Stream stream)
    {
        if (stream == null)
            return ".bin";

        long originalPos = stream.Position;
        byte[] header = new byte[8];
        int read = stream.Read(header, 0, header.Length);
        stream.Position = originalPos; // restore original position

        if (read >= 2 && header[0] == 0xFF && header[1] == 0xD8)
            return ".jpg"; // JPEG
        if (read >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47)
            return ".png"; // PNG
        if (read >= 4 && header[0] == 0x49 && header[1] == 0x49 && header[2] == 0x2A && header[3] == 0x00)
            return ".tif"; // TIFF (little‑endian)
        if (read >= 4 && header[0] == 0x4D && header[1] == 0x4D && header[2] == 0x00 && header[3] == 0x2A)
            return ".tif"; // TIFF (big‑endian)
        if (read >= 2 && header[0] == 0x42 && header[1] == 0x4D)
            return ".bmp"; // BMP
        if (read >= 6 && header[0] == 0x47 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x38)
            return ".gif"; // GIF
        return ".bin"; // fallback
    }
}
