using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationDefinition
{
    public string Type { get; set; }
    public int Page { get; set; }
    public double[] Rect { get; set; }
    public string Contents { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }
    public string Uri { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string jsonPath = "annotations.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON definition file not found: {jsonPath}");
            return;
        }

        string jsonContent = File.ReadAllText(jsonPath);
        List<AnnotationDefinition> definitions = JsonSerializer.Deserialize<List<AnnotationDefinition>>(jsonContent);

        using (Document doc = new Document(inputPdfPath))
        {
            foreach (AnnotationDefinition def in definitions)
            {
                if (def.Page < 1 || def.Page > doc.Pages.Count)
                {
                    continue; // skip invalid page numbers
                }

                Page page = doc.Pages[def.Page];

                if (def.Rect == null || def.Rect.Length != 4)
                {
                    continue; // skip malformed rectangle definitions
                }

                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(def.Rect[0], def.Rect[1], def.Rect[2], def.Rect[3]);

                switch (def.Type)
                {
                    case "Text":
                        TextAnnotation textAnn = new TextAnnotation(page, rect);
                        if (!string.IsNullOrEmpty(def.Contents))
                        {
                            textAnn.Contents = def.Contents;
                        }
                        if (!string.IsNullOrEmpty(def.Title))
                        {
                            textAnn.Title = def.Title;
                        }
                        if (!string.IsNullOrEmpty(def.Color))
                        {
                            textAnn.Color = GetColor(def.Color);
                        }
                        page.Annotations.Add(textAnn);
                        break;

                    case "Link":
                        LinkAnnotation linkAnn = new LinkAnnotation(page, rect);
                        if (!string.IsNullOrEmpty(def.Uri))
                        {
                            linkAnn.Action = new GoToURIAction(def.Uri);
                        }
                        if (!string.IsNullOrEmpty(def.Color))
                        {
                            linkAnn.Color = GetColor(def.Color);
                        }
                        page.Annotations.Add(linkAnn);
                        break;

                    case "Highlight":
                        HighlightAnnotation hlAnn = new HighlightAnnotation(page, rect);
                        if (!string.IsNullOrEmpty(def.Color))
                        {
                            hlAnn.Color = GetColor(def.Color);
                        }
                        page.Annotations.Add(hlAnn);
                        break;

                    case "Square":
                        SquareAnnotation sqAnn = new SquareAnnotation(page, rect);
                        if (!string.IsNullOrEmpty(def.Color))
                        {
                            sqAnn.Color = GetColor(def.Color);
                        }
                        page.Annotations.Add(sqAnn);
                        break;

                    case "Circle":
                        CircleAnnotation ciAnn = new CircleAnnotation(page, rect);
                        if (!string.IsNullOrEmpty(def.Color))
                        {
                            ciAnn.Color = GetColor(def.Color);
                        }
                        page.Annotations.Add(ciAnn);
                        break;

                    default:
                        // Unsupported annotation type – ignore
                        break;
                }
            }

            doc.Save(outputPdfPath);
        }
    }

    private static Aspose.Pdf.Color GetColor(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Aspose.Pdf.Color.Black;
        }

        string lower = name.ToLowerInvariant();
        switch (lower)
        {
            case "black":
                return Aspose.Pdf.Color.Black;
            case "blue":
                return Aspose.Pdf.Color.Blue;
            case "red":
                return Aspose.Pdf.Color.Red;
            case "green":
                return Aspose.Pdf.Color.Green;
            case "yellow":
                return Aspose.Pdf.Color.Yellow;
            case "gray":
                return Aspose.Pdf.Color.Gray;
            case "lightgray":
                return Aspose.Pdf.Color.LightGray;
            default:
                return Aspose.Pdf.Color.Black;
        }
    }
}