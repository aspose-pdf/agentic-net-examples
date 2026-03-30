using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string uncFolder = @"\\server\share\images";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the UNC directory exists
        if (!Directory.Exists(uncFolder))
        {
            try
            {
                Directory.CreateDirectory(uncFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create UNC folder: {ex.Message}");
                return;
            }
        }

        using (Document doc = new Document(inputPath))
        {
            int imageIndex = 1;
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    string destFileName = $"image_{imageIndex}.png";
                    string destPath = Path.Combine(uncFolder, destFileName);
                    try
                    {
                        // XImage.Save expects a Stream; use FileStream to write to the UNC path
                        using (FileStream fs = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }
                        Console.WriteLine($"Saved image {imageIndex} to {destPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to save image {imageIndex}: {ex.Message}");
                    }
                    imageIndex++;
                }
            }
        }
    }
}
