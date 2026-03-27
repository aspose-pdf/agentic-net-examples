using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationInfo
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Aspose.Pdf.Rectangle Rectangle { get; set; }
    public int PageNumber { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        List<AnnotationInfo> annotations = new List<AnnotationInfo>();

        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];
                AnnotationCollection annCollection = page.Annotations;
                for (int j = 1; j <= annCollection.Count; j++)
                {
                    Annotation annotation = annCollection[j];
                    AnnotationInfo info = new AnnotationInfo();
                    info.Name = annotation.Name;
                    info.Type = annotation.AnnotationType.ToString();
                    info.Rectangle = annotation.Rect;
                    info.PageNumber = i;
                    annotations.Add(info);
                }
            }
        }

        Console.WriteLine("Extracted annotation details:");
        foreach (AnnotationInfo info in annotations)
        {
            Console.WriteLine(string.Format(
                "Page {0}: Name='{1}', Type={2}, Rect=[{3}, {4}, {5}, {6}]",
                info.PageNumber,
                info.Name ?? "(none)",
                info.Type,
                info.Rectangle.LLX,
                info.Rectangle.LLY,
                info.Rectangle.URX,
                info.Rectangle.URY));
        }
    }
}