using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        List<string> imagesMissingAlt = new List<string>();

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    List<string> altTexts = img.GetAlternativeText(page);
                    if (altTexts == null || altTexts.Count == 0)
                    {
                        // Use the image name (or collection name) as its identifier
                        string id = !string.IsNullOrEmpty(img.Name) ? img.Name : img.GetNameInCollection();
                        imagesMissingAlt.Add(id);
                    }
                }
            }
        }

        Console.WriteLine("Images missing alternative text:");
        foreach (string id in imagesMissingAlt)
        {
            Console.WriteLine(id);
        }
    }
}