using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;

public class ImageInfo
{
    public int PageNumber { get; set; }
    public int Index { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string Base64 { get; set; }
}

public class ExtractImagesToJson
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputJsonPath = "images.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        List<ImageInfo> imageList = new List<ImageInfo>();
        using (Document doc = new Document(inputPath))
        {
            int pageNumber = 1;
            foreach (Page page in doc.Pages)
            {
                int imageIndex = 0;
                foreach (XImage img in page.Resources.Images)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);

                        ImageInfo info = new ImageInfo();
                        info.PageNumber = pageNumber;
                        info.Index = imageIndex;
                        info.Width = img.Width;
                        info.Height = img.Height;
                        info.Base64 = base64String;
                        imageList.Add(info);
                    }
                    imageIndex++;
                }
                pageNumber++;
            }
        }

        string json = JsonSerializer.Serialize(imageList, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJsonPath, json);
        Console.WriteLine($"Extracted {imageList.Count} images to '{outputJsonPath}'.");
    }
}