using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationInfo
{
    public int PageNumber { get; set; }
    public string AnnotationType { get; set; }
    public RectangleBounds Bounds { get; set; }
    public string Color { get; set; }
    public string Contents { get; set; }
    public string Title { get; set; }
}

public class RectangleBounds
{
    public double LLX { get; set; }
    public double LLY { get; set; }
    public double URX { get; set; }
    public double URY { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotations_log.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            List<AnnotationInfo> annotationList = new List<AnnotationInfo>();

            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    AnnotationInfo info = new AnnotationInfo();
                    info.PageNumber = pageIndex;
                    info.AnnotationType = annotation.AnnotationType.ToString();

                    Aspose.Pdf.Rectangle rect = annotation.Rect;
                    RectangleBounds bounds = new RectangleBounds();
                    bounds.LLX = rect.LLX;
                    bounds.LLY = rect.LLY;
                    bounds.URX = rect.URX;
                    bounds.URY = rect.URY;
                    info.Bounds = bounds;

                    info.Color = annotation.Color != null ? annotation.Color.ToString() : null;
                    info.Contents = annotation.Contents;

                    MarkupAnnotation markup = annotation as MarkupAnnotation;
                    if (markup != null)
                    {
                        info.Title = markup.Title;
                    }

                    annotationList.Add(info);
                }
            }

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;

            string json = JsonSerializer.Serialize(annotationList, jsonOptions);

            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
        }

        Console.WriteLine("Annotations exported to " + outputPath);
    }
}